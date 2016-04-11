namespace ExhibFlat.Entities.Commodities
{
    using ExhibFlat.Core;
    using ExhibFlat.Core.Entities;
    using System;
    using System.Runtime.CompilerServices;

    public class JSC_SOrderQuery : Pagination
    {
        public int? BrandId { get; set; }

        public int? CategoryId { get; set; }

        public DateTime? EndDate { get; set; }
        public DateTime? StartDate { get; set; }

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

        public DateTime? OrderDate { get; set; }

        public int? TagId { get; set; }

        public int? TypeId { get; set; }

        public int? UserId { get; set; }

        public string SupplierId { get; set; }
        public string JSC_CODE { get; set; }//聚生产ID
        public string JSC_Delivery { get; set; }//配送方
        public string OrderId { get; set; }
        public string Username { get; set; }
        public string ModeName { get; set; }
        public int? BreachStatus { get; set; }//违约状态
        public string OrderStatus { get; set; }
        /// <summary>
        /// 快递公司名字
        /// </summary>
        public string ExpressCompanyName { get; set; }
        /// <summary>
        /// 快递单号
        /// </summary>
        public string ShipOrderNumber { get; set; }
        public string Adress { get; set; }
    }
}

