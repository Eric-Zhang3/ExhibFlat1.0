namespace ExhibFlat.Entities.Sales
{
    using ExhibFlat.Entities.Promotions;
   
    using ExhibFlat.Components.Validation.Validators;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class OrderInfo
    {
        private decimal adjustedFreigh;
        private IList<OrderGiftInfo> gifts;
        private Dictionary<string, LineItemInfo> lineItems;

        public static  event EventHandler<EventArgs> Closed;

        public static  event EventHandler<EventArgs> Created;

        public static  event EventHandler<EventArgs> Deliver;

        public static  event EventHandler<EventArgs> Payment;

        public static  event EventHandler<EventArgs> Refund;

        public OrderInfo()
        {
            this.OrderStatus = ExhibFlat.Entities.Sales.OrderStatus.WaitBuyerPay;
            this.RefundStatus = ExhibFlat.Entities.Sales.RefundStatus.None;
        }

        public bool CheckAction(OrderActions action)
        {
            if ((this.OrderStatus != ExhibFlat.Entities.Sales.OrderStatus.Finished) && (this.OrderStatus != ExhibFlat.Entities.Sales.OrderStatus.Closed))
            {
                switch (action)
                {
                    case OrderActions.BUYER_PAY:
                    case OrderActions.SUBSITE_SELLER_MODIFY_DELIVER_ADDRESS:
                    case OrderActions.SUBSITE_SELLER_MODIFY_PAYMENT_MODE:
                    case OrderActions.SUBSITE_SELLER_MODIFY_SHIPPING_MODE:
                    case OrderActions.SELLER_CONFIRM_PAY:
                    case OrderActions.SELLER_MODIFY_TRADE:
                    case OrderActions.SELLER_CLOSE:
                    case OrderActions.SUBSITE_SELLER_MODIFY_GIFTS:
                        return (this.OrderStatus == ExhibFlat.Entities.Sales.OrderStatus.WaitBuyerPay);

                    case OrderActions.BUYER_CONFIRM_GOODS:
                    case OrderActions.SELLER_FINISH_TRADE:
                        return (this.OrderStatus == ExhibFlat.Entities.Sales.OrderStatus.SellerAlreadySent);

                    case OrderActions.SELLER_SEND_GOODS:
                        return (this.OrderStatus == ExhibFlat.Entities.Sales.OrderStatus.WaitToBeSent);

                    case OrderActions.SELLER_REJECT_REFUND:
                        return ((this.OrderStatus == ExhibFlat.Entities.Sales.OrderStatus.BuyerAlreadyPaid) || (this.OrderStatus == ExhibFlat.Entities.Sales.OrderStatus.SellerAlreadySent));

                    case OrderActions.MASTER_SELLER_MODIFY_DELIVER_ADDRESS:
                    case OrderActions.MASTER_SELLER_MODIFY_PAYMENT_MODE:
                    case OrderActions.MASTER_SELLER_MODIFY_SHIPPING_MODE:
                    case OrderActions.MASTER_SELLER_MODIFY_GIFTS:
                        return ((this.OrderStatus == ExhibFlat.Entities.Sales.OrderStatus.WaitBuyerPay) || (this.OrderStatus == ExhibFlat.Entities.Sales.OrderStatus.BuyerAlreadyPaid));

                    case OrderActions.SUBSITE_CREATE_PURCHASEORDER:
                        return (((this.GroupBuyId > 0) && (this.GroupBuyStatus == ExhibFlat.Entities.Promotions.GroupBuyStatus.Success)) && (this.OrderStatus == ExhibFlat.Entities.Sales.OrderStatus.BuyerAlreadyPaid));
                }
            }
            return false;
        }

        public decimal GetAmount()
        {
            decimal num = 0M;
            foreach (LineItemInfo info in this.LineItems.Values)
            {
                num += info.GetSubTotal();
            }
            return num;
        }

        public virtual decimal GetCostPrice()
        {
            decimal num = 0M;
            foreach (LineItemInfo info in this.LineItems.Values)
            {
                num += info.ItemCostPrice * info.ShipmentQuantity;
            }
            foreach (OrderGiftInfo info2 in this.Gifts)
            {
                num += info2.CostPrice * info2.Quantity;
            }
           
            return num;
        }

        public int GetGroupBuyOerderNumber()
        {
            if (this.GroupBuyId > 0)
            {
                foreach (LineItemInfo info in this.LineItems.Values)
                {
                    return info.Quantity;
                }
            }
            return 0;
        }

        public virtual decimal GetProfit()
        {
            return ((this.GetTotal() - this.RefundAmount) - this.GetCostPrice());
        }

        public decimal GetTotal()
        {
            if (this.OrderTotal!=0)
                return this.OrderTotal;
            else
            { 
                decimal amount = this.GetAmount();
                if (this.BundlingID > 0)
                {
                    amount = this.BundlingPrice;
                }
                if (this.IsReduced)
                {
                    amount -= this.ReducedPromotionAmount;
                }
                amount += this.AdjustedFreight;
                amount += this.PayCharge;
                amount += this.Tax;
                if (!string.IsNullOrEmpty(this.CouponCode))
                {
                    amount -= this.CouponValue;
                }
                return (amount + this.AdjustedDiscount);
            }
        }

        public void OnClosed()
        {
            if (Closed != null)
            {
                Closed(this, new EventArgs());
            }
        }

        public static void OnClosed(OrderInfo order)
        {
            if (Closed != null)
            {
                Closed(order, new EventArgs());
            }
        }

        public void OnCreated()
        {
            if (Created != null)
            {
                Created(this, new EventArgs());
            }
        }

        public static void OnCreated(OrderInfo order)
        {
            if (Created != null)
            {
                Created(order, new EventArgs());
            }
        }

        public void OnDeliver()
        {
            if (Deliver != null)
            {
                Deliver(this, new EventArgs());
            }
        }

        public static void OnDeliver(OrderInfo order)
        {
            if (Deliver != null)
            {
                Deliver(order, new EventArgs());
            }
        }

        public void OnPayment()
        {
            if (Payment != null)
            {
                Payment(this, new EventArgs());
            }
        }

        public static void OnPayment(OrderInfo order)
        {
            if (Payment != null)
            {
                Payment(order, new EventArgs());
            }
        }

        public void OnRefund()
        {
            if (Refund != null)
            {
                Refund(this, new EventArgs());
            }
        }

        public static void OnRefund(OrderInfo order)
        {
            if (Refund != null)
            {
                Refund(order, new EventArgs());
            }
        }

        public string Address { get; set; }

        [RangeValidator(typeof(decimal), "-10000000", RangeBoundaryType.Inclusive, "10000000", RangeBoundaryType.Inclusive, Ruleset="ValOrder", MessageTemplate="订单折扣不能为空，金额大小负1000万-1000万之间")]
        public decimal AdjustedDiscount { get; set; }

        [RangeValidator(typeof(decimal), "0.00", RangeBoundaryType.Inclusive, "10000000", RangeBoundaryType.Inclusive, Ruleset="ValOrder", MessageTemplate="运费不能为空，金额大小0-1000万之间")]
        public decimal AdjustedFreight
        {
            get
            {
                return this.adjustedFreigh;
            }
            set
            {
                this.adjustedFreigh = value;
            }
        }

        public int BundlingID { get; set; }

        public int? BundlingNum { get; set; }

        public decimal BundlingPrice { get; set; }

        public string CellPhone { get; set; }

        public string CloseReason { get; set; }

        public int CountDownBuyId { get; set; }

        public decimal CouponAmount { get; set; }

        public string CouponCode { get; set; }

        public string CouponName { get; set; }

        public decimal CouponValue { get; set; }

        public string EmailAddress { get; set; }

        public string ExpressCompanyAbb { get; set; }

        public string ExpressCompanyName { get; set; }

        public DateTime FinishDate { get; set; }

        public decimal Freight { get; set; }

        public int FreightFreePromotionId { get; set; }

        public string FreightFreePromotionName { get; set; }

        public string Gateway { get; set; }
        public decimal RealPayAmount { get; set; }
        public string GatewayOrderId { get; set; }

        public IList<OrderGiftInfo> Gifts
        {
            get
            {
                if (this.gifts == null)
                {
                    this.gifts = new List<OrderGiftInfo>();
                }
                return this.gifts;
            }
        }

        public int GroupBuyId { get; set; }

        public ExhibFlat.Entities.Promotions.GroupBuyStatus GroupBuyStatus { get; set; }

        public string InvoiceTitle { get; set; }

        public bool IsFreightFree { get; set; }

        public bool IsReduced { get; set; }

        public bool IsSendTimesPoint { get; set; }

        public Dictionary<string, LineItemInfo> LineItems
        {
            get
            {
                if (this.lineItems == null)
                {
                    this.lineItems = new Dictionary<string, LineItemInfo>();
                }
                return this.lineItems;
            }
        }

        public OrderMark? ManagerMark { get; set; }

        public string ManagerRemark { get; set; }

        public string ModeName { get; set; }

        public string MSN { get; set; }

        public decimal NeedPrice { get; set; }

        public DateTime OrderDate { get; set; }

        public string OrderId { get; set; }

        public ExhibFlat.Entities.Sales.OrderStatus OrderStatus { get; set; }

        public int OrderStatusInt { get; set; }

        public decimal PayCharge { get; set; }

        public DateTime PayDate { get; set; }

        public string PaymentType { get; set; }

        public int PaymentTypeId { get; set; }

        public int Points { get; set; }

        public string QQ { get; set; }

        public string RealModeName { get; set; }

        public string RealName { get; set; }

        public int RealShippingModeId { get; set; }

        public decimal ReducedPromotionAmount { get; set; }

        public int ReducedPromotionId { get; set; }

        public string ReducedPromotionName { get; set; }

        public decimal RefundAmount { get; set; }

        public string RefundRemark { get; set; }

        public ExhibFlat.Entities.Sales.RefundStatus RefundStatus { get; set; }

        public int RegionId { get; set; }

        public string Remark { get; set; }

        public int SentTimesPointPromotionId { get; set; }

        public string SentTimesPointPromotionName { get; set; }

        public string ShipOrderNumber { get; set; }

        public DateTime ShippingDate { get; set; }

        public int ShippingModeId { get; set; }

        public string ShippingRegion { get; set; }

        public string ShipTo { get; set; }

        public string ShipToDate { get; set; }

        public decimal Tax { get; set; }

        public string TelPhone { get; set; }

        public decimal TimesPoint { get; set; }

        public int UserId { get; set; }

        public string Username { get; set; }

        public string Wangwang { get; set; }

        public int Weight
        {
            get
            {
                int num = 0;
                foreach (LineItemInfo info in this.LineItems.Values)
                {
                    num += info.ItemWeight * info.ShipmentQuantity;
                }
                return num;
            }
        }

        public string ZipCode { get; set; }

        //活动Code
        public string JSC_CODE { get; set; }
        /// <summary>
        /// 1.自行发货 2.仓配发货
        /// </summary>
        public int JSC_Delivery { get; set; }

        /// <summary>
        /// 聚生产订单默认为1，其它订单为0
        /// </summary>
        public int OrderType { get; set; }

        public string OrderTypeStr { get; set; }
        /// <summary>
        /// 违约状态
        /// </summary>
        public int BreachStatus { get; set; }

        public decimal OrderTotal { get; set; }

        public decimal Amount { get; set; }
       
    }
}

