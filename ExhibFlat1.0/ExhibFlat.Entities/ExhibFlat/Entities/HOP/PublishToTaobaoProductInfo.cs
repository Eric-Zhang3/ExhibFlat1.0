namespace ExhibFlat.Entities.HOP
{
    using System;
    using System.Runtime.CompilerServices;

    public class PublishToTaobaoProductInfo : TaobaoProductInfo
    {
        public string Description { get; set; }

        public string ImageUrl1 { get; set; }

        public string ImageUrl2 { get; set; }

        public string ImageUrl3 { get; set; }

        public string ImageUrl4 { get; set; }

        public string ImageUrl5 { get; set; }

        public string ProductCode { get; set; }

        public decimal SalePrice { get; set; }

        public long TaobaoProductId { get; set; }

        public int Weight { get; set; }
    }
}

