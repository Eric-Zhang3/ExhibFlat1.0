namespace ExhibFlat.ControlPanel.Data
{
    using ExhibFlat.ControlPanel.Commodities;
    using ExhibFlat.Core;
    using ExhibFlat.Core.Entities;
    using ExhibFlat.Core.Enums;
    using ExhibFlat.Entities;
    using ExhibFlat.Entities.Commodities;
    using ExhibFlat.Entities.Sales;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Globalization;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;
    

    public class ProductData : ProductProvider
    {
        private Database database = DatabaseFactory.CreateDatabase();

        public override DataTable GetSubjectList(SubjectListQuery query)
        {
            StringBuilder builder = new StringBuilder();
           
            //Member user = HiContext.Current.User as Member;
            //int memberDiscount = MemberProvider.Instance().GetMemberDiscount(user.GradeId);
            builder.AppendFormat(
                @"SELECT TOP {0} ROW_NUMBER() over (order by  DisplaySequence desc) id ,ProductId,ProductName,ProductCode,ShowSaleCounts AS SaleCounts,ShortDescription,RIGHT(CONVERT(NVARCHAR(10),AddedDate, 102),5) as GroundingDate,",query.MaxNum);
            builder.Append(
                " ThumbnailUrl60,ThumbnailUrl100,ThumbnailUrl160,ThumbnailUrl180,ThumbnailUrl220,ThumbnailUrl310,MarketPrice,SalePrice");

            builder.Append(" FROM vw_HiFlat_BrowseProductList p WHERE ");
            builder.Append("1=1");
            if (!string.IsNullOrEmpty(query.SortBy))
            {
                builder.AppendFormat(" ORDER BY {0} {1}", DataHelper.CleanSearchString(query.SortBy),
                    DataHelper.CleanSearchString(query.SortOrder.ToString()));
            }
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                return DataHelper.ConverDataReaderToDataTable(reader);
            }
        }
        public override DataTable UserLogin(string name, string pwd)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Select User_ID,User_Account,User_Pwd,User_Name,DeleteMark from Base_UserInfo where ");
            strSql.Append("User_Account=@User_Account ");
            strSql.Append("and User_Pwd=@User_Pwd ");
            strSql.Append("and DeleteMark!=0");
           
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(strSql.ToString());
            this.database.AddInParameter(sqlStringCommand, "User_Account", DbType.String, name);
            this.database.AddInParameter(sqlStringCommand, "User_Pwd", DbType.String, Md5Helper.MD5(pwd, 32));
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                return DataHelper.ConverDataReaderToDataTable(reader);
            }
        }
        public override DataTable GetCategories()
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("SELECT * FROM HiFlat_Categories ORDER BY DisplaySequence");
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                return DataHelper.ConverDataReaderToDataTable(reader);
            }
        }
        public override string GenerateProductAutoCode()
        {
            DataSet ds = this.database.ExecuteDataSet(CommandType.StoredProcedure, "P_I_GenerateCode");
            if (ds != null && ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                return ds.Tables[1].Rows[0][0].ToString().PadLeft(6, '0');
            return "000001";
        }
        public override string GetCodeByProductCode(string precode, int endcode)
        {
            DbCommand sqlStringCommand =
                this.database.GetSqlStringCommand("select * from HiFlat_Products where ProductCode=@ProductCode");
            this.database.AddInParameter(sqlStringCommand, "ProductCode", DbType.String,
                (precode.ToUpper() + Convert.ToString(endcode.ToString(CultureInfo.InvariantCulture).PadLeft(6 - precode.Length, '0'))).Substring(0, 6));

            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                DataTable dt = DataHelper.ConverDataReaderToDataTable(reader);
                if (dt != null && dt.Rows.Count > 0)
                {
                    return (precode.ToUpper() + (endcode + 1).ToString(CultureInfo.InvariantCulture).PadLeft(6 - precode.Length, '0')).Substring(0, 6);
                }
                return (precode.ToUpper() + endcode.ToString(CultureInfo.InvariantCulture).PadLeft(6 - precode.Length, '0')).Substring(0, 6);
            }
        }
        public override int AddProduct(ProductInfo product, DbTransaction dbTran)
        {
            DbCommand storedProcCommand = this.database.GetStoredProcCommand("cp_Product_Create");
            this.database.AddInParameter(storedProcCommand, "CategoryId", DbType.Int32, product.CategoryId);
            this.database.AddInParameter(storedProcCommand, "MainCategoryPath", DbType.String, product.MainCategoryPath);
            this.database.AddInParameter(storedProcCommand, "TypeId", DbType.Int32, product.TypeId);
            this.database.AddInParameter(storedProcCommand, "UserId", DbType.Int32, product.UserId);
            this.database.AddInParameter(storedProcCommand, "ProductName", DbType.String, product.ProductName);
            this.database.AddInParameter(storedProcCommand, "ProductCode", DbType.String, product.ProductCode);
            this.database.AddInParameter(storedProcCommand, "ShortDescription", DbType.String, product.ShortDescription);
            this.database.AddInParameter(storedProcCommand, "Unit", DbType.String, product.Unit);
            this.database.AddInParameter(storedProcCommand, "Description", DbType.String, product.Description);
            this.database.AddInParameter(storedProcCommand, "Title", DbType.String, product.Title);
            this.database.AddInParameter(storedProcCommand, "Meta_Description", DbType.String, product.MetaDescription);
            this.database.AddInParameter(storedProcCommand, "Meta_Keywords", DbType.String, product.MetaKeywords);
            this.database.AddInParameter(storedProcCommand, "SaleStatus", DbType.Int32, (int)product.SaleStatus);
            this.database.AddInParameter(storedProcCommand, "AddedDate", DbType.DateTime, product.AddedDate);
            this.database.AddInParameter(storedProcCommand, "Image", DbType.String, product.Image);
            this.database.AddInParameter(storedProcCommand, "ImageUrl1", DbType.String, product.ImageUrl1);
            this.database.AddInParameter(storedProcCommand, "ImageUrl2", DbType.String, product.ImageUrl2);
            this.database.AddInParameter(storedProcCommand, "ImageUrl3", DbType.String, product.ImageUrl3);
            this.database.AddInParameter(storedProcCommand, "ImageUrl4", DbType.String, product.ImageUrl4);
            this.database.AddInParameter(storedProcCommand, "ImageUrl5", DbType.String, product.ImageUrl5);
            this.database.AddInParameter(storedProcCommand, "ThumbnailUrl40", DbType.String, product.ThumbnailUrl40);
            this.database.AddInParameter(storedProcCommand, "ThumbnailUrl60", DbType.String, product.ThumbnailUrl60);
            this.database.AddInParameter(storedProcCommand, "ThumbnailUrl100", DbType.String, product.ThumbnailUrl100);
            this.database.AddInParameter(storedProcCommand, "ThumbnailUrl160", DbType.String, product.ThumbnailUrl160);
            this.database.AddInParameter(storedProcCommand, "ThumbnailUrl180", DbType.String, product.ThumbnailUrl180);
            this.database.AddInParameter(storedProcCommand, "ThumbnailUrl220", DbType.String, product.ThumbnailUrl220);
            this.database.AddInParameter(storedProcCommand, "ThumbnailUrl310", DbType.String, product.ThumbnailUrl310);
            this.database.AddInParameter(storedProcCommand, "ThumbnailUrl410", DbType.String, product.ThumbnailUrl410);
            this.database.AddInParameter(storedProcCommand, "LineId", DbType.Int32, product.LineId);
            this.database.AddInParameter(storedProcCommand, "MarketPrice", DbType.Currency, product.MarketPrice);
            this.database.AddInParameter(storedProcCommand, "LowestSalePrice", DbType.Currency, product.LowestSalePrice);
            this.database.AddInParameter(storedProcCommand, "PenetrationStatus", DbType.Int16,
                (int)product.PenetrationStatus);
            this.database.AddInParameter(storedProcCommand, "BrandId", DbType.Int32, product.BrandId);
            this.database.AddInParameter(storedProcCommand, "HasSKU", DbType.Boolean, product.HasSKU);
            this.database.AddInParameter(storedProcCommand, "HasCommission", DbType.Boolean, product.HasCommission);
            this.database.AddInParameter(storedProcCommand, "CommissionPath", DbType.String, product.CommissionPath);
            this.database.AddInParameter(storedProcCommand, "SupplierId ", DbType.Guid, product.SupplierId);
            this.database.AddOutParameter(storedProcCommand, "ProductId", DbType.Int32, 4);
            this.database.AddInParameter(storedProcCommand, "IsCustomize", DbType.Boolean, product.IsCustomize);
            this.database.AddInParameter(storedProcCommand, "CustomizeURL", DbType.String, product.CustomizeURL);
            this.database.AddInParameter(storedProcCommand, "Sipping", DbType.String, product.Sipping);
            this.database.AddInParameter(storedProcCommand, "DistributeMode", DbType.String, product.DistributeMode);
            this.database.ExecuteNonQuery(storedProcCommand, dbTran);
            return (int)this.database.GetParameterValue(storedProcCommand, "ProductId");
        }
        public override DataTable GetBrandCategories()
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("SELECT * FROM Hishop_BrandCategories ORDER BY DisplaySequence DESC");
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                return DataHelper.ConverDataReaderToDataTable(reader);
            }
        }

        public override DataTable GetBrandCategoriesByTypeId(int typeId)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("SELECT B.BrandId,B.BrandName FROM Hishop_BrandCategories B INNER JOIN Hishop_ProductTypeBrands PB ON B.BrandId=PB.BrandId WHERE ProductTypeId=@ProductTypeId ORDER BY DisplaySequence DESC");
            this.database.AddInParameter(sqlStringCommand, "ProductTypeId", DbType.Int32, typeId);
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                return DataHelper.ConverDataReaderToDataTable(reader);
            }
        }

        public override void GetMemberExpandInfo(int gradeId, string userName, out string gradeName, out int messageNum)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("SELECT Name FROM aspnet_MemberGrades WHERE GradeId = @GradeId;SELECT COUNT(*) AS NoReadMessageNum FROM Hishop_ReceivedMessages WHERE Addressee = @Addressee AND IsRead=0");
            
            this.database.AddInParameter(sqlStringCommand, "GradeId", DbType.Int32, gradeId);
            this.database.AddInParameter(sqlStringCommand, "Addressee", DbType.String, userName);
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                if (reader.Read())
                {
                    gradeName = (string)reader["Name"];
                }
                else
                {
                    gradeName = string.Empty;
                }
                if (reader.NextResult() && reader.Read())
                {
                    messageNum = (int)reader["NoReadMessageNum"];
                }
                else
                {
                    messageNum = 0;
                }
            }
        }

        public override IList<ProductLineInfo> GetProductLineList()
        {
            IList<ProductLineInfo> list = new List<ProductLineInfo>();
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("SELECT * FROM Hishop_ProductLines");
            
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                while (reader.Read())
                {
                    ProductLineInfo item = new ProductLineInfo
                    {
                        LineId = (int)reader["LineId"],
                        Name = (string)reader["Name"]
                    };
                    list.Add(item);
                }
            }
            return list;
        }

        public override IList<ProductTypeInfo> GetProductTypes()
        {
            IList<ProductTypeInfo> list = new List<ProductTypeInfo>();
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("SELECT * FROM Hishop_ProductTypes");
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                while (reader.Read())
                {
                    ProductTypeInfo item = new ProductTypeInfo
                    {
                        TypeId = (int)reader["TypeId"],
                        TypeName = (string)reader["TypeName"],
                        Remark = (string)reader["Remark"]
                    };
                    list.Add(item);
                }
            }
            return list;
        }


        /// <summary>
        /// 状态下拉配置
        /// </summary>
        /// <returns></returns>
        public override List<DropdownlistModel> GetStatusList(int typeID)
        {
            List<DropdownlistModel> list = new List<DropdownlistModel>();
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("SELECT d.DictCode,d.DictName FROM JSC_Dictionary d WHERE d.BizType= @TypeId");
            this.database.AddInParameter(sqlStringCommand, "TypeId", DbType.Int32, typeID);
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                while (reader.Read())
                {
                    DropdownlistModel item = new DropdownlistModel
                    {
                        pzcode = (int)reader["DictCode"],
                        pzname = (string)reader["DictName"]
                    };
                    list.Add(item);
                }
            }
            return list;
        }



        public override IList<ShippingModeInfo> GetShippingModes()
        {
            IList<ShippingModeInfo> list = new List<ShippingModeInfo>();
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("SELECT * FROM Hishop_ShippingTypes st INNER JOIN Hishop_ShippingTemplates temp ON st.TemplateId=temp.TemplateId Order By DisplaySequence");
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                while (reader.Read())
                {
                    list.Add(DataMapper.PopulateShippingMode(reader));
                }
            }
            return list;
        }



        public override DataTable GetSkuContentBySku(string skuId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT AttributeName, ValueStr");
            builder.Append(" FROM Hishop_SKUs s join Hishop_SKUItems si on s.SkuId = si.SkuId");
            builder.Append(" join Hishop_Attributes a on si.AttributeId = a.AttributeId join Hishop_AttributeValues av on si.ValueId = av.ValueId WHERE s.SkuId = @SkuId");
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            this.database.AddInParameter(sqlStringCommand, "SkuId", DbType.String, skuId);
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                return DataHelper.ConverDataReaderToDataTable(reader);
            }
        }

        public override DataTable GetTags()
        {
            DataTable table = new DataTable();
            try
            {
                DbCommand sqlStringCommand = this.database.GetSqlStringCommand("SELECT *  FROM  Hishop_Tags order by DisplaySequence");
                using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
                {
                    table = DataHelper.ConverDataReaderToDataTable(reader);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ParentCategoryId"></param>
        /// <returns></returns>
        public override DataTable GetNewtable(int ParentCategoryId)
        {
            DbCommand sqlStringCommand =
               this.database.GetSqlStringCommand("SELECT * FROM HiFlat_Categories where ParentCategoryId=" + ParentCategoryId.ToString());
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                return DataHelper.ConverDataReaderToDataTable(reader);
            }
        }
        public override DbQueryResult GetProducts(ProductQuery query)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" 1=1 ");
            if (query.SaleStatus != ProductSaleStatus.All)
            {
                builder.AppendFormat(" AND SaleStatus = {0}", (int)query.SaleStatus);
            }
            else
            {
                builder.AppendFormat(" AND SaleStatus not in ({0})", 0);
            }

           
            if (query.BrandId.HasValue)
            {
                builder.AppendFormat(" AND BrandId = {0}", query.BrandId.Value);
            }
            if (query.TypeId.HasValue)
            {
                builder.AppendFormat(" AND TypeId = {0}", query.TypeId.Value);
            }
           
            if (!string.IsNullOrEmpty(query.SupplierId))
            {
                builder.AppendFormat(" AND SupplierId = '{0}'", query.SupplierId);
            }
            else
            {
                if (query.UserId.HasValue)
                {
                    builder.AppendFormat(" AND UserId = {0}", query.UserId);
                }
            }
            
            if (!string.IsNullOrEmpty(query.Keywords))
            {
                query.Keywords = DataHelper.CleanSearchString(query.Keywords);
                string[] strArray = Regex.Split(query.Keywords.Trim(), @"\s+");
                builder.AppendFormat(" AND ProductName LIKE '%{0}%'", DataHelper.CleanSearchString(strArray[0]));
                for (int i = 1; (i < strArray.Length) && (i <= 4); i++)
                {
                    builder.AppendFormat("AND ProductName LIKE '%{0}%'", DataHelper.CleanSearchString(strArray[i]));
                }
            }
            if (query.ProductLineId.HasValue && (query.ProductLineId.Value > 0))
            {
                builder.AppendFormat(" AND LineId={0}", Convert.ToInt32(query.ProductLineId.Value));
            }
            if (query.PenetrationStatus != PenetrationStatus.NotSet)
            {
                builder.AppendFormat(" AND PenetrationStatus={0}", (int)query.PenetrationStatus);
            }
            if (query.IsMakeTaobao.HasValue && (query.IsMakeTaobao.Value >= 0))
            {
                builder.AppendFormat(" AND IsMaketaobao={0}", query.IsMakeTaobao.Value);
            }
            
            if (!string.IsNullOrEmpty(query.ProductCode))
            {
                builder.AppendFormat(" AND ProductCode LIKE '%{0}%'", DataHelper.CleanSearchString(query.ProductCode));
            }
            if (query.CategoryId.HasValue && (query.CategoryId.Value > 1))
            {
                string maincid = query.MaiCategoryPath.TrimEnd('|');
               
                builder.AppendFormat(" AND ( MainCategoryPath LIKE '{0}|%'  OR ExtendCategoryPath LIKE '{0}|%' )",
                 maincid);

            }
            if (query.StartDate.HasValue)
            {
                builder.AppendFormat(" AND Convert(varchar(10),AddedDate,120) >='{0}'", Convert.ToDateTime(DataHelper.GetSafeDateTimeFormat(query.StartDate.Value)).ToString("yyyy-MM-dd"));
            }
            if (query.EndDate.HasValue)
            {
                builder.AppendFormat(" AND Convert(varchar(10),AddedDate,120) <='{0}'", Convert.ToDateTime(DataHelper.GetSafeDateTimeFormat
                    (query.EndDate.Value)).ToString("yyyy-MM-dd"));
            }
            if (!string.IsNullOrEmpty(query.SupplierName))
            {
                builder.AppendFormat(" AND CompanyName like '%{0}%'", query.SupplierName);
            }

            string selectFields =
                "ProductId, ProductCode,IsMakeTaobao,UserId,ProductName, ThumbnailUrl40, MarketPrice, SalePrice, Stock, DisplaySequence,LowestSalePrice,PenetrationStatus,CompanyName,ActivityCount,Convert(varchar(10),AddedDate,120) AddedDate";
            return DataHelper.PagingByRownumber(query.PageIndex, query.PageSize, query.SortBy, query.SortOrder,
                query.IsCount, "vw_HiFlat_BrowseProductList p", "ProductId", builder.ToString(), selectFields);
        }
        public override int CanclePenetrationProducts(IList<int> productIds, DbTransaction dbTran)
        {
            string str = "(";
            foreach (int num in productIds)
            {
                str = str + num + ",";
            }
            if (str.Length == 1)
            {
                return 0;
            }
            str = str.Substring(0, str.Length - 1) + ")";
            DbCommand sqlStringCommand =
                this.database.GetSqlStringCommand(
                    "UPDATE HiFlat_Products SET PenetrationStatus = 2 WHERE ProductId IN " + str);
            if (dbTran != null)
            {
                return this.database.ExecuteNonQuery(sqlStringCommand, dbTran);
            }
            return this.database.ExecuteNonQuery(sqlStringCommand);
        }
        public override bool DeleteCanclePenetrationProducts(IList<int> productIds, DbTransaction dbTran)
        {
            try
            {
                string str = "(";
                foreach (int num in productIds)
                {
                    str = str + num + ",";
                }
                if (str.Length == 1)
                {
                    return false;
                }
                DbCommand sqlStringCommand =
                    this.database.GetSqlStringCommand("DELETE FROM HiFlat_Products WHERE ProductId IN " +
                                                      (str.Substring(0, str.Length - 1) + ")"));
                if (dbTran != null)
                {
                    this.database.ExecuteNonQuery(sqlStringCommand, dbTran);
                }
                else
                {
                    this.database.ExecuteNonQuery(sqlStringCommand);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public override int UpdateProductSaleStatus(string productIds, ProductSaleStatus saleStatus)
        {
            DbCommand sqlStringCommand;
            if (saleStatus == ProductSaleStatus.OnSale)
            {
                sqlStringCommand =
                    this.database.GetSqlStringCommand(
                        string.Format(
                            "UPDATE a SET a.SaleStatus = {0} from HiFlat_Products  a inner join vw_HiFlat_BrowseProductList b on b.ProductId=a.ProductId " +
                            " WHERE a.ProductId IN ({1}) and b.Stock>0 and  b.Weight>0",
                            (int)saleStatus, productIds));
            }
            else
                sqlStringCommand =
                    this.database.GetSqlStringCommand(
                        string.Format("UPDATE HiFlat_Products SET SaleStatus = {0} WHERE ProductId IN ({1})",
                            (int)saleStatus, productIds));
            return this.database.ExecuteNonQuery(sqlStringCommand);
        }
        public override void SwapCategorySequence(int categoryId, CategoryZIndex zIndex)
        {
            DbCommand storedProcCommand = this.database.GetStoredProcCommand("cp_Category_SwapDisplaySequence");
            this.database.AddInParameter(storedProcCommand, "CategoryId", DbType.Int32, categoryId);
            this.database.AddInParameter(storedProcCommand, "ZIndex", DbType.Int32, (int)zIndex);
            this.database.ExecuteNonQuery(storedProcCommand);
        }

        public override bool SwapCategorySequence(int categoryId, int displaysequence)
        {
            DbCommand sqlStringCommand =
                this.database.GetSqlStringCommand(
                    "update Hishop_Categories  set DisplaySequence=@DisplaySequence where CategoryId=@CategoryId");
            this.database.AddInParameter(sqlStringCommand, "@DisplaySequence", DbType.Int32, displaysequence);
            this.database.AddInParameter(sqlStringCommand, "@CategoryId", DbType.Int32, categoryId);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }
        public override bool DeleteCategory(int categoryId)
        {
            DbCommand storedProcCommand = this.database.GetStoredProcCommand("cp_Category_Delete");
            this.database.AddInParameter(storedProcCommand, "CategoryId", DbType.Int32, categoryId);
            return (this.database.ExecuteNonQuery(storedProcCommand) > 0);
        }
        /*增加或删除商品类目时更改父节点HasChildren字段*/
        public override bool UpdateCateHas(int CategoryId, bool Del)
        {
            DbCommand sqlStringCommand;
            if (!Del)
            {
                sqlStringCommand = this.database.GetSqlStringCommand("update Hishop_Categories set HasChildren=1 where CategoryId= @CategoryId");
            }
            else
                sqlStringCommand = this.database.GetSqlStringCommand("update Hishop_Categories set HasChildren=0 where CategoryId= @CategoryId");
            this.database.AddInParameter(sqlStringCommand, "CategoryId", DbType.Int32, CategoryId);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }
        /*通过某节点的父节点判断该节点下是否含有除此之外的节点*/
        public override int CheckCateHas(int ParentCategoryId)
        {
            DbCommand sqlStringCommand;
            sqlStringCommand = this.database.GetSqlStringCommand("select count(*) from Hishop_Categories where ParentCategoryId=@ParentCategoryId");
            this.database.AddInParameter(sqlStringCommand, "ParentCategoryId", DbType.Int32, ParentCategoryId);
            return this.database.ExecuteNonQuery(sqlStringCommand);
        }

        /// <summary>
        /// 更新类目path后，更新相应的商品MainCategoryPath属性
        /// </summary>
        public override int UpdateProductFromCategory()
        {
            DbCommand sqlStringCommand =
                this.database.GetSqlStringCommand(
                    "update t1 set MainCategoryPath=t2.Path+'|' from Hishop_Products t1 inner join Hishop_Categories t2 on t1.CategoryId=t2.CategoryId");
            return this.database.ExecuteNonQuery(sqlStringCommand);
        }
        public override int CreateCategory(CategoryInfo category)
        {
            DbCommand storedProcCommand = this.database.GetStoredProcCommand("cp_Category_Create");
            this.database.AddOutParameter(storedProcCommand, "CategoryId", DbType.Int32, 4);
            this.database.AddInParameter(storedProcCommand, "Name", DbType.String, category.Name);
            this.database.AddInParameter(storedProcCommand, "SKUPrefix", DbType.String, category.SKUPrefix);
            this.database.AddInParameter(storedProcCommand, "DisplaySequence", DbType.Int32, category.DisplaySequence);
            if (!string.IsNullOrEmpty(category.MetaTitle))
            {
                this.database.AddInParameter(storedProcCommand, "Meta_Title", DbType.String, category.MetaTitle);
            }
            if (!string.IsNullOrEmpty(category.MetaDescription))
            {
                this.database.AddInParameter(storedProcCommand, "Meta_Description", DbType.String,
                    category.MetaDescription);
            }
            if (!string.IsNullOrEmpty(category.MetaKeywords))
            {
                this.database.AddInParameter(storedProcCommand, "Meta_Keywords", DbType.String, category.MetaKeywords);
            }
            if (!string.IsNullOrEmpty(category.TerCateName))
            {
                this.database.AddInParameter(storedProcCommand, "TerCateName ", DbType.String, category.TerCateName);
            }
            if (!string.IsNullOrEmpty(category.TerCateImage))
            {
                this.database.AddInParameter(storedProcCommand, "TerCateImage ", DbType.String, category.TerCateImage);
            }

            if (!string.IsNullOrEmpty(category.Description))
            {
                this.database.AddInParameter(storedProcCommand, "Description", DbType.String, category.Description);
            }
            if (!string.IsNullOrEmpty(category.Notes1))
            {
                this.database.AddInParameter(storedProcCommand, "Notes1", DbType.String, category.Notes1);
            }
            if (!string.IsNullOrEmpty(category.Notes2))
            {
                this.database.AddInParameter(storedProcCommand, "Notes2", DbType.String, category.Notes2);
            }
            if (!string.IsNullOrEmpty(category.Notes3))
            {
                this.database.AddInParameter(storedProcCommand, "Notes3", DbType.String, category.Notes3);
            }
            if (!string.IsNullOrEmpty(category.Notes4))
            {
                this.database.AddInParameter(storedProcCommand, "Notes4", DbType.String, category.Notes4);
            }
            if (!string.IsNullOrEmpty(category.Notes5))
            {
                this.database.AddInParameter(storedProcCommand, "Notes5", DbType.String, category.Notes5);
            }
            if (category.ParentCategoryId.HasValue)
            {
                this.database.AddInParameter(storedProcCommand, "ParentCategoryId", DbType.Int32,
                    category.ParentCategoryId.Value);
            }
            else
            {
                this.database.AddInParameter(storedProcCommand, "ParentCategoryId", DbType.Int32, 0);
            }
            if (category.AssociatedProductType.HasValue)
            {
                this.database.AddInParameter(storedProcCommand, "AssociatedProductType", DbType.Int32,
                    category.AssociatedProductType.Value);
            }
            if (!string.IsNullOrEmpty(category.RewriteName))
            {
                this.database.AddInParameter(storedProcCommand, "RewriteName", DbType.String, category.RewriteName);
            }
            this.database.AddInParameter(storedProcCommand, "IsSuggest", DbType.Boolean, category.issuggest);
            this.database.ExecuteNonQuery(storedProcCommand);
            return (int)this.database.GetParameterValue(storedProcCommand, "CategoryId");
        }

    }
}