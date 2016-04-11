namespace ExhibFlat.Entities.Sales
{
    using System;
    using System.Runtime.CompilerServices;

    public class PurchaseOrderItemInfo
    {
        public decimal GetSubTotal()
        {
            return (this.ItemPurchasePrice * this.Quantity);
        }

        public decimal ItemCostPrice { get; set; }

        public string ItemDescription { get; set; }

        public string ItemHomeSiteDescription { get; set; }

        public decimal ItemListPrice { get; set; }

        public decimal ItemPurchasePrice { get; set; }

        public int ItemWeight { get; set; }

        public int ProductId { get; set; }

        public string PurchaseOrderId { get; set; }

        public int Quantity { get; set; }

        public string SKU { get; set; }

        public string SKUContent { get; set; }

        public string SkuId { get; set; }

        public string ThumbnailsUrl { get; set; }
    }
}

