namespace ExhibFlat.Entities.Sales
{
    using System;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class StatisticsInfo
    {
        public decimal AlreadyPaidOrdersNum { get; set; }

        public int ApplyRequestWaitDispose { get; set; }

        public decimal AreadyPaidOrdersAmount { get; set; }

        public int AuthorizeProductCount { get; set; }

        public decimal Balance { get; set; }

        public decimal BalanceDrawRequested { get; set; }

        public int DistroButorsNewAddToday { get; set; }

        public int DistroButorsNumb { get; set; }

        public int LeaveComments { get; set; }

        public int Messages { get; set; }

        public int OrderNumbToday { get; set; }

        public int OrderNumbWaitConsignment { get; set; }

        public int OrderNumbYesterday { get; set; }

        public decimal OrderPriceToday { get; set; }

        public decimal OrderPriceYesterday { get; set; }

        public decimal OrderProfitToday { get; set; }

        public decimal OrderProfitYesterday { get; set; }

        public int ProductAlert { get; set; }

        public int ProductConsultations { get; set; }

        public int ProductNumbInStock { get; set; }

        public int ProductNumbOnSale { get; set; }

        public int ProductNumStokWarning { get; set; }

        public int PurchaseOrderNumbWaitConsignment { get; set; }

        public int UserNewAddToday { get; set; }

        public int UserNumb { get; set; }

        public int UserNumbBirthdayToday { get; set; }
    }
}

