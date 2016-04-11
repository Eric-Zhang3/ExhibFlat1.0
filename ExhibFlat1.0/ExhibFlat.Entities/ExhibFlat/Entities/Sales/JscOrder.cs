namespace ExhibFlat.Entities.Sales
{
    using ExhibFlat.Core.Entities;
    using System;
    using System.Runtime.CompilerServices;

    public class JscOrder : Pagination
    {
        public string OrderId { get; set; }

        public string Remark { get; set; }

        public string SKU { get; set; }

        public string ProductName { get; set; }

        public int? ProductID { get; set; }

        public int? BundlingNum { get; set; }

        public string CompanyName { get; set; }

        public string JSC_CODE { get; set; }
        public int? OrderStatus{ get; set; }

        public decimal? Amount { get; set; }

        public string ShipOrderNumber { get; set; }

        public int? Weight{ get; set; }

        public string ModeName{ get; set; }

        public string ExpressCompanyName { get; set; }

        public int? MinProduction { get; set; }

        public int? MaxProduction { get; set; }

        public decimal? ItemListPrice { get; set; }
        public DateTime? PayDate { get; set; }

        public int? ShipTo { get; set; }

        public string TelPhone { get; set; }

        public string  Address { get; set; }

        public DateTime? ShipToDate { get; set; }

        public decimal? OrderTotal { get; set; }

        public string ImageSmall { get; set; }

        public decimal? CostPrice { get; set; }

        public int? Quantity { get; set; }

        public decimal? Freight { get; set; }



        public DateTime? OrderDate { get; set; }

        public DateTime? ShippingDate { get; set; }

        public DateTime? FinishDate { get; set; }

        public int? BreachStatus { get; set; }


        public int? JSC_Delivery { get; set; }

        public DateTime? ActivityEndTime { get; set; }

        public int? ActivityStatus { get; set; }

        public string ThumbnailUrl160 { get; set; }

        public string ThumbnailUrl100 { get; set; }


        public string ExpressCompanyAbb { get; set; }
    }
}

