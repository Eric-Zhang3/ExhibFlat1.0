namespace ExhibFlat.Entities.Sales
{
    using System;
    using System.Runtime.CompilerServices;

    public class AdminStatisticsInfo : StatisticsInfo
    {
        public decimal BalanceTotal
        {
            get
            {
                return (this.MembersBalance + this.DistrosBalance);
            }
        }

        public int DistributorApplyRequestWaitDispose { get; set; }

        public int DistributorBlancedrawRequest { get; set; }

        public int DistributorSiteRequest { get; set; }

        public int DistroNewAddYesterToday { get; set; }

        public decimal DistrosBalance { get; set; }

        public int MemberBlancedrawRequest { get; set; }

        public decimal MembersBalance { get; set; }

        public decimal OrderPriceMonth { get; set; }

        public decimal OrderPriceYesterDay { get; set; }

        public int ProductAlert { get; set; }

        public decimal ProfitTotal
        {
            get
            {
                return (this.PurchaseOrderProfitToday + base.OrderProfitToday);
            }
        }

        public int PurchaseOrderNumbToday { get; set; }

        public int PurchaseOrderNumbWait { get; set; }

        public decimal PurchaseorderPriceMonth { get; set; }

        public decimal PurchaseorderPriceToDay { get; set; }

        public decimal PurchaseOrderPriceToday { get; set; }

        public decimal PurchaseorderPriceYesterDay { get; set; }

        public decimal PurchaseOrderProfitToday { get; set; }

        public int TodayFinishOrder { get; set; }

        public int TodayFinishPurchaseOrder { get; set; }

        public int TotalDistributors { get; set; }

        public int TotalMembers { get; set; }

        public int TotalProducts { get; set; }

        public int UserNewAddYesterToday { get; set; }

        public int YesterdayFinishOrder { get; set; }

        public int YesterdayFinishPurchaseOrder { get; set; }
    }
}

