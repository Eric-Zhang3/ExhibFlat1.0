namespace ExhibFlat.Flat.Catalog
{
    using ExhibFlat.ControlPanel.Commodities;
    using ExhibFlat.Core;
    using ExhibFlat.Entities.Commodities;
    
    using System;
    using System.Text;
    using System.Text.RegularExpressions;

    public abstract class ProductMasterProvider : ProductProvider
    {
        private static readonly ProductMasterProvider _defaultInstance = (DataProviders.CreateInstance("ExhibFlat.ControlPanel.Data.ProductData,ExhibFlat.ControlPanel.Data") as ProductMasterProvider);

        protected ProductMasterProvider()
        {
        }

        
        

        protected static string BuildUnSaleProductBrowseQuerySearch(ProductBrowseQuery query)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("SaleStatus = {0}", 2);
            if (!string.IsNullOrEmpty(query.ProductCode))
            {
                builder.AppendFormat(" AND LOWER(ProductCode) Like  '%{0}%'", DataHelper.CleanSearchString(query.ProductCode).ToLower());
            }
            if (query.AttributeValues.Count > 0)
            {
                foreach (AttributeValueInfo info in query.AttributeValues)
                {
                    builder.AppendFormat(" AND ProductId IN ( SELECT ProductId FROM Hishop_ProductAttributes WHERE AttributeId={0} And ValueId={1}) ", info.AttributeId, info.ValueId);
                }
            }
            if (query.BrandId.HasValue)
            {
                if (query.BrandId.Value == 0)
                {
                    builder.Append(" AND BrandId IS NOT NULL");
                }
                else
                {
                    builder.AppendFormat(" AND BrandId = {0}", query.BrandId.Value);
                }
            }
            if (query.MinSalePrice.HasValue)
            {
                builder.AppendFormat(" AND SalePrice >= {0}", query.MinSalePrice.Value);
            }
            if (query.MaxSalePrice.HasValue)
            {
                builder.AppendFormat(" AND SalePrice <= {0}", query.MaxSalePrice.Value);
            }
            if (!string.IsNullOrEmpty(query.Keywords) && (query.Keywords.Trim().Length > 0))
            {
                if (!query.IsPrecise)
                {
                    query.Keywords = DataHelper.CleanSearchString(query.Keywords);
                    string[] strArray = Regex.Split(query.Keywords.Trim(), @"\s+");
                    builder.AppendFormat(" AND ProductName LIKE '%{0}%'", DataHelper.CleanSearchString(strArray[0]));
                    for (int i = 1; (i < strArray.Length) && (i <= 4); i++)
                    {
                        builder.AppendFormat("AND ProductName LIKE '%{0}%'", DataHelper.CleanSearchString(strArray[i]));
                    }
                }
                else
                {
                    builder.AppendFormat(" AND ProductName = '{0}'", DataHelper.CleanSearchString(query.Keywords));
                }
            }
            if (query.CategoryId.HasValue)
            {
                //CategoryInfo category = CategoryBrowser.GetCategory(query.CategoryId.Value);
                CategoryInfo category = new CategoryInfo();
                if (category != null)
                {
                    builder.AppendFormat(" AND ( MainCategoryPath LIKE '{0}|%' OR ExtendCategoryPath LIKE '{0}|%') ", category.Path);
                }
            }
            if (!string.IsNullOrEmpty(query.SubjectType))
            {
                foreach (string str in query.SubjectType.Split(new char[] { ',' }))
                {
                    builder.AppendFormat(" and ProductId IN (SELECT ProductId FROM Hishop_ProductTag WHERE TagId={0})", str);
                }
            }
            return builder.ToString();
        }

        public static ProductMasterProvider CreateInstance()
        {
            return _defaultInstance;
        }
    }
}

