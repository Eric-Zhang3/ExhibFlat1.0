namespace ExhibFlat.ControlPanel.Commodities
{
    using ExhibFlat.Core.Entities;
    using ExhibFlat.Core.Enums;

    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Runtime.InteropServices;
    using ExhibFlat.Core;
    using ExhibFlat.Entities.Commodities;
    using ExhibFlat.Entities.Sales;
    using ExhibFlat.Entities;


    public abstract class ProductProvider
    {
        private static readonly ProductProvider _defaultInstance =
            (DataProviders.CreateInstance("ExhibFlat.ControlPanel.Data.ProductData,ExhibFlat.ControlPanel.Data") as
                ProductProvider);

        protected ProductProvider()
        {
        }

        public static ProductProvider Instance()
        {
            return _defaultInstance;
        }

        public abstract DataTable GetSubjectList(SubjectListQuery query);

        public abstract DataTable UserLogin(string name, string pwd);
        public abstract DataTable GetCategories();
        public abstract string GenerateProductAutoCode();
        public abstract string GetCodeByProductCode(string precode, int endcode);
        public abstract int AddProduct(ProductInfo product, DbTransaction dbTran);

        public abstract DataTable GetBrandCategories();
        public abstract DataTable GetBrandCategoriesByTypeId(int typeId);
        public abstract void GetMemberExpandInfo(int gradeId, string userName, out string gradeName, out int messageNum);
        public abstract IList<ProductLineInfo> GetProductLineList();
        public abstract IList<ProductTypeInfo> GetProductTypes();
        public abstract IList<ShippingModeInfo> GetShippingModes();

        /// <summary>
        ///JSC-下拉列表状态配置
        /// </summary>
        /// <param name="typeID"></param>
        /// <returns></returns>
        public abstract List<DropdownlistModel> GetStatusList(int typeID);
        public abstract DataTable GetSkuContentBySku(string skuId);
        public abstract DataTable GetTags();
        public abstract DataTable GetNewtable(int ProductProvider);
        public abstract DbQueryResult GetProducts(ProductQuery query);

        public abstract int CanclePenetrationProducts(IList<int> productIds, DbTransaction dbTran);
        public abstract bool DeleteCanclePenetrationProducts(IList<int> productIds, DbTransaction dbTran);
        public abstract int UpdateProductSaleStatus(string productIds, ProductSaleStatus saleStatus);
        public abstract void SwapCategorySequence(int categoryId, CategoryZIndex zIndex);
        public abstract bool SwapCategorySequence(int categoryId, int displaysequence);
        public abstract bool DeleteCategory(int categoryId);
        /*增加商品类目时更改父节点HasChildren字段*/
        public abstract bool UpdateCateHas(int CategoryId, bool Del);
        public abstract int CheckCateHas(int ParentCategoryId);
        public abstract int UpdateProductFromCategory();
        public abstract int CreateCategory(CategoryInfo category);
    }
}