namespace ExhibFlat.Entities.Sales
{
    using ExhibFlat.Core.Entities;
    using System;
    using System.Runtime.CompilerServices;

    public class PurchaseOrderQuery : Pagination
    {
        public string DistributorName { get; set; }

        public DateTime? EndDate { get; set; }

        public bool IsManualPurchaseOrder { get; set; }

        public int? IsPrinted { get; set; }

        public string OrderId { get; set; }

        public string ProductName { get; set; }

        public string PurchaseOrderId { get; set; }

        public OrderStatus PurchaseStatus { get; set; }

        public int? ShippingModeId { get; set; }

        public string ShipTo { get; set; }

        public DateTime? StartDate { get; set; }
    }
}

