namespace ExhibFlat.Entities.Commodities
{
    using ExhibFlat.Core;
    using ExhibFlat.Core.Entities;
    using System;
    using System.Runtime.CompilerServices;

    public class ProductQuery : Pagination
    {
        public int? BrandId { get; set; }

        public int? CategoryId { get; set; }

        public DateTime? EndDate { get; set; }

        public bool? IsIncludeBundlingProduct { get; set; }

        public bool? IsIncludePromotionProduct { get; set; }

        public int? IsMakeTaobao { get; set; }

        [HtmlCoding]
        public string Keywords { get; set; }

        public string MaiCategoryPath { get; set; }

        public decimal? MaxSalePrice { get; set; }

        public decimal? MinSalePrice { get; set; }

        public ExhibFlat.Entities.Commodities.PenetrationStatus PenetrationStatus { get; set; }

        [HtmlCoding]
        public string ProductCode { get; set; }

        public int? ProductLineId { get; set; }

        public ExhibFlat.Entities.Commodities.PublishStatus PublishStatus { get; set; }

        public ProductSaleStatus SaleStatus { get; set; }

        public DateTime? StartDate { get; set; }

        public int? TagId { get; set; }

        public int? TypeId { get; set; }

        public int? UserId { get; set; }

        public string SupplierId { get; set; }

        public string SupplierName{ get; set; }

        public string ProductName { get; set; }
        public int ProductId { get; set; }
    }
}

