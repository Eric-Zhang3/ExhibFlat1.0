namespace ExhibFlat.Entities.Sales
{
    using System;
    using System.Runtime.CompilerServices;

    public class OrderGiftInfo
    {
        public decimal CostPrice { get; set; }

        public int GiftId { get; set; }

        public string GiftName { get; set; }

        public string OrderId { get; set; }

        public int PromoteType { get; set; }

        public int Quantity { get; set; }

        public string ThumbnailsUrl { get; set; }
    }
}

