namespace ExhibFlat.Entities.Sales
{
    using ExhibFlat.Core;
    using ExhibFlat.Components.Validation.Validators;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class PurchaseOrderInfo
    {
        private IList<PurchaseOrderGiftInfo> purchaseOrderGifts;
        private IList<PurchaseOrderItemInfo> purchaseOrderItems;

        public bool CheckAction(PurchaseOrderActions action)
        {
            if ((this.PurchaseStatus != OrderStatus.Finished) && (this.PurchaseStatus != OrderStatus.Closed))
            {
                switch (action)
                {
                    case PurchaseOrderActions.DISTRIBUTOR_CLOSE:
                    case PurchaseOrderActions.DISTRIBUTOR_MODIFY_GIFTS:
                    case PurchaseOrderActions.DISTRIBUTOR_CONFIRM_PAY:
                    case PurchaseOrderActions.MASTER__CLOSE:
                    case PurchaseOrderActions.MASTER__MODIFY_AMOUNT:
                    case PurchaseOrderActions.MASTER_CONFIRM_PAY:
                        return (this.PurchaseStatus == OrderStatus.WaitBuyerPay);

                    case PurchaseOrderActions.DISTRIBUTOR_CONFIRM_GOODS:
                    case PurchaseOrderActions.MASTER_FINISH_TRADE:
                        return (this.PurchaseStatus == OrderStatus.SellerAlreadySent);

                    case PurchaseOrderActions.MASTER__MODIFY_SHIPPING_MODE:
                        return ((this.PurchaseStatus == OrderStatus.WaitBuyerPay) || (this.PurchaseStatus == OrderStatus.BuyerAlreadyPaid));

                    case PurchaseOrderActions.MASTER_MODIFY_DELIVER_ADDRESS:
                        return ((this.PurchaseStatus == OrderStatus.WaitBuyerPay) || (this.PurchaseStatus == OrderStatus.BuyerAlreadyPaid));

                    case PurchaseOrderActions.MASTER_SEND_GOODS:
                        return (this.PurchaseStatus == OrderStatus.BuyerAlreadyPaid);

                    case PurchaseOrderActions.MASTER_REJECT_REFUND:
                        return ((this.PurchaseStatus == OrderStatus.BuyerAlreadyPaid) || (this.PurchaseStatus == OrderStatus.SellerAlreadySent));
                }
            }
            return false;
        }

        public decimal GetGiftAmount()
        {
            decimal num = 0M;
            foreach (PurchaseOrderGiftInfo info in this.PurchaseOrderGifts)
            {
                num += info.GetSubTotal();
            }
            return num;
        }

        public decimal GetProductAmount()
        {
            decimal num = 0M;
            foreach (PurchaseOrderItemInfo info in this.PurchaseOrderItems)
            {
                num += info.GetSubTotal();
            }
            return num;
        }

        public decimal GetPurchaseCostPrice()
        {
            decimal num = 0M;
            foreach (PurchaseOrderItemInfo info in this.PurchaseOrderItems)
            {
                num += info.ItemCostPrice * info.Quantity;
            }
            foreach (PurchaseOrderGiftInfo info2 in this.PurchaseOrderGifts)
            {
                num += info2.CostPrice * info2.Quantity;
            }
            return num;
        }

        public decimal GetPurchaseProfit()
        {
            return ((this.GetPurchaseTotal() - this.RefundAmount) - this.GetPurchaseCostPrice());
        }

        public decimal GetPurchaseTotal()
        {
            return ((((this.GetProductAmount() + this.GetGiftAmount()) + this.AdjustedFreight) + this.AdjustedDiscount) + this.Tax);
        }

        public string Address { get; set; }

        [RangeValidator(typeof(decimal), "-10000000", RangeBoundaryType.Inclusive, "10000000", RangeBoundaryType.Inclusive, Ruleset="ValPurchaseOrder", MessageTemplate="采购单折扣不能为空，金额大小负1000万-1000万之间")]
        public decimal AdjustedDiscount { get; set; }

        public decimal AdjustedFreight { get; set; }

        public string CellPhone { get; set; }

        public string CloseReason { get; set; }

        public string DistributorEmail { get; set; }

        public int DistributorId { get; set; }

        public string DistributorMSN { get; set; }

        public string Distributorname { get; set; }

        public string DistributorQQ { get; set; }

        public string DistributorRealName { get; set; }

        public string DistributorWangwang { get; set; }

        public string ExpressCompanyAbb { get; set; }

        public string ExpressCompanyName { get; set; }

        public DateTime FinishDate { get; set; }

        public decimal Freight { get; set; }

        public string InvoiceTitle { get; set; }

        public bool IsManualPurchaseOrder
        {
            get
            {
                return string.IsNullOrEmpty(this.OrderId);
            }
        }

        public OrderMark? ManagerMark { get; set; }

        public string ManagerRemark { get; set; }

        public string ModeName { get; set; }

        public string OrderId { get; set; }

        public decimal OrderTotal { get; set; }

        public DateTime PayDate { get; set; }

        public DateTime PurchaseDate { get; set; }

        public IList<PurchaseOrderGiftInfo> PurchaseOrderGifts
        {
            get
            {
                if (this.purchaseOrderGifts == null)
                {
                    this.purchaseOrderGifts = new List<PurchaseOrderGiftInfo>();
                }
                return this.purchaseOrderGifts;
            }
        }

        public string PurchaseOrderId { get; set; }

        public IList<PurchaseOrderItemInfo> PurchaseOrderItems
        {
            get
            {
                if (this.purchaseOrderItems == null)
                {
                    this.purchaseOrderItems = new List<PurchaseOrderItemInfo>();
                }
                return this.purchaseOrderItems;
            }
        }

        public OrderStatus PurchaseStatus { get; set; }

        public string RealModeName { get; set; }

        public int RealShippingModeId { get; set; }

        public decimal RefundAmount { get; set; }

        public string RefundRemark { get; set; }

        public ExhibFlat.Entities.Sales.RefundStatus RefundStatus { get; set; }

        public int RegionId { get; set; }

        public string Remark { get; set; }

        [HtmlCoding]
        public string ShipOrderNumber { get; set; }

        public DateTime ShippingDate { get; set; }

        public int ShippingModeId { get; set; }

        public string ShippingRegion { get; set; }

        public string ShipTo { get; set; }

        public string ShipToDate { get; set; }

        public string TaobaoOrderId { get; set; }

        public decimal Tax { get; set; }

        public string TelPhone { get; set; }

        public int Weight { get; set; }

        public string ZipCode { get; set; }
    }
}

