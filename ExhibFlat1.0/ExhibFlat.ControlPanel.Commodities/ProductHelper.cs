namespace ExhibFlat.ControlPanel.Commodities
{
    
    using ExhibFlat.Entities;
    
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Web;
    using System.Text.RegularExpressions;
    using System.Collections;
    using System.Text;
    using System.Configuration;
    using System.Net;
    using System.Xml;
    using ExhibFlat.Core;
    using System.Web.Caching;
    using ExhibFlat.Entities.Commodities;
    using ExhibFlat.SiteSet;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System.Data;
    using System.Data.Common;
    using ExhibFlat.Core.Entities;
    
    public static class ProductHelper
    {
        public static XmlDocument GetProductSubjectDocument()
        {
            string key = "ProductSubjectFileCache-Admin";
            if (HiContext.Current.SiteSettings.UserId.HasValue)
            {
                key = string.Format("ProductSubjectFileCache-{0}", HiContext.Current.SiteSettings.UserId.Value);
            }
            XmlDocument document = HiCache.Get(key) as XmlDocument;
            if (document == null)
            {
                string filename = HiContext.Current.Context.Request.MapPath(HiContext.Current.GetSkinPath() + "/ProductSubjects.xml");
                document = new XmlDocument();
                document.Load(filename);
                HiCache.Max(key, document, new CacheDependency(filename));
            }
            return document;
        }
        public static DataTable GetSubjectList(SubjectListQuery query)
        {
            return ProductProvider.Instance().GetSubjectList(query);
        }

        public static DataTable UserLogin(string name, string pwd)
        {
            return ProductProvider.Instance().UserLogin(name, pwd);
        }
        public static DataTable GetCategories()
        {
            return ProductProvider.Instance().GetCategories();
        }
        public static IList<CategoryInfo> GetMainCategories()
        {
            IList<CategoryInfo> list = new List<CategoryInfo>();
            DataRow[] rowArray = GetCategories().Select("Depth = 1");
            for (int i = 0; i < rowArray.Length; i++)
            {
                list.Add(DataMapper.ConvertDataRowToProductCategory(rowArray[i]));
            }
            return list;
        }
        public static IList<CategoryInfo> SearchCategories(int parentCategoryId, string keyword)
        {
            IList<CategoryInfo> list = new List<CategoryInfo>();
            string filterExpression = "ParentCategoryId = " + parentCategoryId;
            if (!string.IsNullOrEmpty(keyword))
            {
                filterExpression = filterExpression + string.Format(" AND Name like '%{0}%'", DataHelper.CleanSearchString(keyword));
            }
            DataRow[] rowArray = GetCategories().Select(filterExpression);
            for (int i = 0; i < rowArray.Length; i++)
            {
                list.Add(DataMapper.ConvertDataRowToProductCategory(rowArray[i]));
            }
            return list;
        }
        public static CategoryInfo GetCategory(int categoryId)
        {
            DataRow[] rowArray = GetCategories().Select("CategoryId=" + categoryId.ToString());
            if (rowArray.Length > 0)
            {
                return DataMapper.ConvertDataRowToProductCategory(rowArray[0]);
            }
            return null;
        }
        public static string GenerateProductAutoCode()
        {
            return ProductProvider.Instance().GenerateProductAutoCode();
        }
        public static string GetFullCategory(int categoryId)
        {
            CategoryInfo category = GetCategory(categoryId);
            if (category == null)
            {
                return null;
            }
            string name = category.Name;
            while ((category != null) && category.ParentCategoryId.HasValue)
            {
                category = GetCategory(category.ParentCategoryId.Value);
                if (category != null)
                {
                    name = category.Name + " &raquo; " + name;
                }
            }
            return name;
        }
        public static string GetCodeByProductCode(string precode, int endcode)
        {
            return ProductProvider.Instance().GetCodeByProductCode(precode, endcode);
        }
        public static ProductActionStatus AddProduct(ProductInfo product)
        {
            if (null == product)
            {
                return ProductActionStatus.UnknowError;
            }
            Globals.EntityCoding(product, true);
            int decimalLength = HiContext.Current.SiteSettings.DecimalLength;
            if (product.MarketPrice.HasValue)
            {
                product.MarketPrice = new decimal?(Math.Round(product.MarketPrice.Value, decimalLength));
            }
            product.LowestSalePrice = Math.Round(product.LowestSalePrice, decimalLength);
            ProductActionStatus unknowError = ProductActionStatus.UnknowError;
            
            using (DbConnection connection = DatabaseFactory.CreateDatabase().CreateConnection())
            {
                connection.Open();
                DbTransaction dbTran = connection.BeginTransaction();
                try
                {
                    ProductProvider provider = ProductProvider.Instance();
                    int productId = provider.AddProduct(product, dbTran);
                    if (productId == 0)
                    {
                        dbTran.Rollback();
                        return ProductActionStatus.DuplicateName;
                    }
                    dbTran.Commit();
                    unknowError = ProductActionStatus.Success;
                }
                catch (Exception ex)
                {

                    dbTran.Rollback();
                }
                finally
                {
                    connection.Close();
                }

            }

            if (unknowError == ProductActionStatus.Success)
            {

            }
            return unknowError;
            
        }
        public static DataTable GetNewtable()
        {
            DataTable categories = new DataTable();

            //if (null == categories)
            //{
            //    HiCache.Insert("DataCache-Categories", categories, 360, CacheItemPriority.Normal);
            //}
            GetCategoriesDate(ref categories, 0);
            return categories;
        }
        public static DataTable GetNewCategories(int ParentCategoryId)
        {
            DataTable categories = ProductProvider.Instance().GetNewtable(ParentCategoryId);
            return categories;
        }
        private static void GetCategoriesDate(ref DataTable dt, int Id)
        {

            DataTable tdt = GetNewCategories(Id);

            if (dt.Rows.Count == 0)
                dt = tdt.Clone();
            if (tdt.Rows.Count == 0)
                return;
            for (int i = 0; i < tdt.Rows.Count; i++)
            {
                dt.ImportRow(tdt.Rows[i]);
                GetCategoriesDate(ref dt, int.Parse(tdt.Rows[i][0].ToString()));
            }
        }
        public static DbQueryResult GetProducts(ProductQuery query)
        {
            return ProductProvider.Instance().GetProducts(query);
        }
        public static int CanclePenetrationProducts(IList<int> productIds)
        {
            int num;
            
            using (DbConnection connection = DatabaseFactory.CreateDatabase().CreateConnection())
            {
                connection.Open();
                DbTransaction dbTran = connection.BeginTransaction();
                try
                {
                    num = ProductProvider.Instance().CanclePenetrationProducts(productIds, dbTran);
                    if (num <= 0)
                    {
                        dbTran.Rollback();
                        return 0;
                    }
                    if (!DeleteCanclePenetrationProducts(productIds, dbTran))
                    {
                        dbTran.Rollback();
                        return 0;
                    }
                    dbTran.Commit();
                }
                catch
                {
                    dbTran.Rollback();
                    return 0;
                }
                finally
                {
                    connection.Close();
                }
                if (num > 0)
                {
                    
                }
            }
            return num;
        }
        public static bool DeleteCanclePenetrationProducts(IList<int> productIds, DbTransaction dbTran)
        {
            return ProductProvider.Instance().DeleteCanclePenetrationProducts(productIds, dbTran);
        }
        public static int RemoveProduct(string productIds)
        {
            
            if (string.IsNullOrEmpty(productIds))
            {
                return 0;
            }
            int num = ProductProvider.Instance().UpdateProductSaleStatus(productIds, ProductSaleStatus.Delete);
            if (num > 0)
            {
               
            }
            return num;
        }

        public static int OffShelf(int productId)
        {
            if (productId <= 0)
            {
                return 0;
            }
            int num = ProductProvider.Instance().UpdateProductSaleStatus(productId.ToString(), ProductSaleStatus.UnSale);
            if (num > 0)
            {
                
            }
            return num;
        }

        public static int OffShelf(string productIds)
        {
            
            if (string.IsNullOrEmpty(productIds))
            {
                return 0;
            }
            int num = ProductProvider.Instance().UpdateProductSaleStatus(productIds, ProductSaleStatus.UnSale);
            if (num > 0)
            {
                
            }
            return num;
        }
        public static int InStock(string productIds)
        {
            
            if (string.IsNullOrEmpty(productIds))
            {
                return 0;
            }
            int num = ProductProvider.Instance().UpdateProductSaleStatus(productIds, ProductSaleStatus.OnStock);
            if (num > 0)
            {
               
            }
            return num;
        }
        public static void SwapCategorySequence(int categoryId, CategoryZIndex zIndex)
        {
            if (categoryId > 0)
            {
                ProductProvider.Instance().SwapCategorySequence(categoryId, zIndex);
                HiCache.Remove("DataCache-Categories");
            }
        }

        public static bool SwapCategorySequence(int categoryId, int displaysequence)
        {
            bool flag = false;
            if (categoryId <= 0)
            {
                return false;
            }
            flag = ProductProvider.Instance().SwapCategorySequence(categoryId, displaysequence);
            HiCache.Remove("DataCache-Categories");
            return flag;
        }
        public static bool DeleteCategory(int categoryId)
        {
            bool flag = false;
            bool tem = false;
            CategoryInfo category = GetCategory(categoryId);
            flag = ProductProvider.Instance().DeleteCategory(categoryId);
            int temHas = (int)category.ParentCategoryId;
            if (category.ParentCategoryId != 0)
            {
                
                {
                    tem = true;
                }
            }
            else
                tem = true;
            if (flag && tem)
            {
                //EventLogs.WriteOperationLog(Privilege.DeleteProductCategory, string.Format(CultureInfo.InvariantCulture, "删除了编号为 “{0}” 的商品类目", new object[] { categoryId }));
                HiCache.Remove("DataCache-Categories");
            }
            return flag;
        }
        public static CategoryActionStatus AddCategory(CategoryInfo category)
        {
            if (null == category)
            {
                return CategoryActionStatus.UnknowError;
            }
            Globals.EntityCoding(category, true);
            bool tem;
            int temHas = category.ParentCategoryId != null ? Convert.ToInt32(category.ParentCategoryId) : 0;
            if (category.ParentCategoryId != null && category.ParentCategoryId != 0)
            {
                tem = ProductProvider.Instance().UpdateCateHas(temHas, false);
            }
            else
                tem = true;
            if (ProductProvider.Instance().CreateCategory(category) > 0 && tem)
            {
                //EventLogs.WriteOperationLog(Privilege.AddProductCategory, string.Format(CultureInfo.InvariantCulture, "创建了一个新的商品类目:”{0}”", new object[] { category.Name }));
                //HiCache.Remove("DataCache-Categories");
            }
            return CategoryActionStatus.Success;
        }
    }

}