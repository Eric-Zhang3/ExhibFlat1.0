namespace ExhibFlat.Entities.Promotions
{
    using System;
    using System.Runtime.CompilerServices;

    public class BundlingItemInfo
    {
        public int BundlingID { get; set; }

        public int BundlingItemId { get; set; }

        public int ProductID { get; set; }

        public string ProductName { get; set; }

        public int ProductNum { get; set; }

        public decimal ProductPrice { get; set; }

        public string SkuId { get; set; }
    }
}

