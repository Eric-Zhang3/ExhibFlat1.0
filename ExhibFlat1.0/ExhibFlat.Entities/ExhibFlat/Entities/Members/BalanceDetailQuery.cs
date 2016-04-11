namespace ExhibFlat.Entities.Members
{
    using ExhibFlat.Core.Entities;
    using System;
    using System.Runtime.CompilerServices;

    public class BalanceDetailQuery : Pagination
    {
        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public TradeTypes TradeType { get; set; }

        public int? UserId { get; set; }

        public string UserName { get; set; }
    }
}

