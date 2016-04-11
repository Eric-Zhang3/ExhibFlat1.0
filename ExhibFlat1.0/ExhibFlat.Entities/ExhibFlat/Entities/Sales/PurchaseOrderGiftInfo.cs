namespace ExhibFlat.Entities.Sales
{
    using System;
    using System.Runtime.CompilerServices;

    public class PurchaseOrderGiftInfo
    {
        public decimal GetSubTotal()
        {
            return (this.PurchasePrice * this.Quantity);
        }

        public decimal CostPrice { get; set; }

        public int GiftId { get; set; }

        public string GiftName { get; set; }

        public string PurchaseOrderId { get; set; }

        public decimal PurchasePrice { get; set; }

        public int Quantity { get; set; }

        public string ThumbnailsUrl { get; set; }
    }
}

