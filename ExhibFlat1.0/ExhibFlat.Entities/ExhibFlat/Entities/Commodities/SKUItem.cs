namespace ExhibFlat.Entities.Commodities
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class SKUItem : IComparable
    {
        private Dictionary<int, decimal> distributorPrices;
        private Dictionary<int, decimal> memberPrices;
        private Dictionary<int, int> skuItems;
        private Dictionary<string, List<string>> skutypeItems;
        public int CompareTo(object obj)
        {
            SKUItem item = obj as SKUItem;
            if (item == null)
            {
                return -1;
            }
            if (item.SkuItems.Count != this.SkuItems.Count)
            {
                return -1;
            }
            foreach (int num in item.SkuItems.Keys)
            {
                if (item.SkuItems[num] != this.SkuItems[num])
                {
                    return -1;
                }
            }
            return 0;
        }

        public int AlertStock { get; set; }

        public string GBCode { get; set; }

        public string BarCode { get; set; }

        public string SkuSubName { get; set; }

        public decimal CostPrice { get; set; }

        public Dictionary<int, decimal> DistributorPrices
        {
            get
            {
                return (this.distributorPrices ?? (this.distributorPrices = new Dictionary<int, decimal>()));
            }
        }

        public Dictionary<int, decimal> MemberPrices
        {
            get
            {
                return (this.memberPrices ?? (this.memberPrices = new Dictionary<int, decimal>()));
            }
        }

        public int ProductId { get; set; }

        public decimal PurchasePrice { get; set; }

        public decimal SalePrice { get; set; }

        public string SKU { get; set; }

        public string SkuId { get; set; }

        /// <summary>
        /// 定制图片
        /// </summary>
        public string CustomizeImageURL { get; set; }

        public Dictionary<int, int> SkuItems
        {
            get
            {
                if (this.skuItems == null)
                {
                    this.skuItems = new Dictionary<int, int>();
                }
                return this.skuItems;
            }
            set
            {
                this.skuItems = value;
            }
        }
        public Dictionary<string, List<string>> SkuTypeItems
        {
            get
            {
                if (this.skutypeItems == null)
                {
                    this.skutypeItems = new Dictionary<string, List<string>>();
                }
                return this.skutypeItems;
            }
            set
            {
                this.skutypeItems = value;
            }
        }
        public int Stock { get; set; }

        public int Weight { get; set; }
    }


}

