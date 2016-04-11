namespace ExhibFlat.Entities.Sales
{
    using System;

    public enum BreachStatus
    {
        All = 0,
        BuyerAlreadyPaid = 2,
        Closed = 4,
        Finished = 5,
        History = 0x63,
        SellerAlreadySent = 3,
        WaitBuyerPay = 1
    }
}

