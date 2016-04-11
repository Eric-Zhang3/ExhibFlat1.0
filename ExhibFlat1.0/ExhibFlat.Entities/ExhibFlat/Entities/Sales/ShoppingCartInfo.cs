namespace ExhibFlat.Entities.Sales
{
   
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class ShoppingCartInfo
    {
        private bool isSendGift;
        private IList<ShoppingCartGiftInfo> lineGifts;
        private Dictionary<string, ShoppingCartItemInfo> lineItems;
        private decimal timesPoint = 1M;

        public decimal GetAmount()
        {
            decimal num = 0M;
            foreach (ShoppingCartItemInfo info in this.lineItems.Values)
            {
                num += info.SubTotal;
            }
            return num;
        }
        public decimal GetAmount(string skuid)
        {
            decimal num = 0M;
            foreach (ShoppingCartItemInfo info in this.lineItems.Values)
            {
                if (info.SkuId == skuid)
                    num += info.SubTotal;
            }
            return num;
        }

        public int GetPoint()
        {
            int num = 0;
            
            return num;
        }

        public int GetPoint(decimal money)
        {
            int num = 0;
            
            return num;
        }

        public int GetQuantity()
        {
            int num = 0;
            foreach (ShoppingCartItemInfo info in this.lineItems.Values)
            {
                num += info.Quantity;
            }
            return num;
        }

        public int GetQuantity(string skuId)
        {
            int num = 0;
            foreach (ShoppingCartItemInfo info in this.lineItems.Values)
            {
                if (info.SkuId == skuId)
                    num += info.Quantity;
            }
            return num;
        }

        public decimal GetTotal()
        {
            return (this.GetAmount() - this.ReducedPromotionAmount);
        }

        public int GetTotalNeedPoint()
        {
            int num = 0;
            foreach (ShoppingCartGiftInfo info in this.LineGifts)
            {
                num += info.SubPointTotal;
            }
            return num;
        }

        public int FreightFreePromotionId { get; set; }

        public string FreightFreePromotionName { get; set; }

        public bool IsFreightFree { get; set; }

        public bool IsReduced { get; set; }

        public bool IsSendGift
        {
            get
            {
                foreach (ShoppingCartItemInfo info in this.lineItems.Values)
                {
                    if (info.IsSendGift)
                    {
                        return true;
                    }
                }
                return this.isSendGift;
            }
            set
            {
                this.isSendGift = value;
            }
        }

        public bool IsSendTimesPoint { get; set; }

        public IList<ShoppingCartGiftInfo> LineGifts
        {
            get
            {
                if (this.lineGifts == null)
                {
                    this.lineGifts = new List<ShoppingCartGiftInfo>();
                }
                return this.lineGifts;
            }
        }

        public Dictionary<string, ShoppingCartItemInfo> LineItems
        {
            get
            {
                if (this.lineItems == null)
                {
                    this.lineItems = new Dictionary<string, ShoppingCartItemInfo>();
                }
                return this.lineItems;
            }
        }

        public decimal ReducedPromotionAmount { get; set; }

        public int ReducedPromotionId { get; set; }

        public string ReducedPromotionName { get; set; }

        public int SendGiftPromotionId { get; set; }

        public string SendGiftPromotionName { get; set; }

        public int SentTimesPointPromotionId { get; set; }

        public string SentTimesPointPromotionName { get; set; }

        public string SkuId { get; set; }
        public string SKU { get; set; }
        public decimal TimesPoint
        {
            get
            {
                return this.timesPoint;
            }
            set
            {
                this.timesPoint = value;
            }
        }

        public int Weight
        {
            get
            {
                int num = 0;
                foreach (ShoppingCartItemInfo info in this.lineItems.Values)
                {
                    num += info.GetSubWeight();
                }
                return num;
            }
        }
    }
}

