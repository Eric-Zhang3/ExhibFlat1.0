namespace ExhibFlat.Entities.Members
{
    using System;
    using System.Runtime.CompilerServices;

    public class AccountSummaryInfo
    {
        public decimal AccountAmount { get; set; }

        public decimal DrawRequestBalance { get; set; }

        public decimal FreezeBalance { get; set; }

        public decimal UseableBalance { get; set; }
    }
}

