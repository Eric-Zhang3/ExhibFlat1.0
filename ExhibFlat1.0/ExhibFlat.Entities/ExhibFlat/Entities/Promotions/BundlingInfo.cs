namespace ExhibFlat.Entities.Promotions
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class BundlingInfo
    {
        public BundlingInfo()
        {
            if (this.BundlingItemInfos == null)
            {
                this.BundlingItemInfos = new List<BundlingItemInfo>();
            }
        }

        public DateTime AddTime { get; set; }

        public int BundlingID { get; set; }

        public List<BundlingItemInfo> BundlingItemInfos { get; set; }

        public int DisplaySequence { get; set; }

        public string Name { get; set; }

        public int Num { get; set; }

        public decimal Price { get; set; }

        public int SaleStatus { get; set; }

        public string ShortDescription { get; set; }
    }
}

