namespace ExhibFlat.Entities.Promotions
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class BindInfo
    {
        public BindInfo()
        {
            if (this.BindItemInfos == null)
            {
                this.BindItemInfos = new List<BindItemInfo>();
            }
        }

        public DateTime AddTime { get; set; }

        public int BindID { get; set; }

        public List<BindItemInfo> BindItemInfos { get; set; }

        public int DisplaySequence { get; set; }

        public string Name { get; set; }

        public int Num { get; set; }

        public decimal Price { get; set; }

        public int SaleStatus { get; set; }

        public string ShortDescription { get; set; }
    }
}

