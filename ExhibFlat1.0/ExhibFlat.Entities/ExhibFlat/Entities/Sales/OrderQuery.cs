namespace ExhibFlat.Entities.Sales
{
    using ExhibFlat.Core.Entities;
    using System;
    using System.Runtime.CompilerServices;

    public class OrderQuery : Pagination
    {
        public DateTime? EndDate { get; set; }

        public int? GroupBuyId { get; set; }

        public int? IsPrinted { get; set; }

        public string OrderId { get; set; }

        public int? PaymentType { get; set; }

        public string ProductName { get; set; }

        public int? RegionId { get; set; }

        public int? ShippingModeId { get; set; }

        public string ShipTo { get; set; }

        public DateTime? StartDate { get; set; }

        public OrderStatus Status { get; set; }

        public string UserName { get; set; }

        public string Sku{ get; set; }

        public DateTime? OrderDate { get; set; }

        public decimal? Amount { get; set; }

        public string CompanyName { get; set; }

        public string ShipOrderNumber { get; set; }

        public BreachStatus BreachStatus { get; set; } 

    }
}

