namespace ExhibFlat.Entities.Members
{
    using System;
    using System.Runtime.CompilerServices;

    public class InpourRequestInfo
    {
        public decimal InpourBlance { get; set; }

        public string InpourId { get; set; }

        public int PaymentId { get; set; }

        public DateTime TradeDate { get; set; }

        public int UserId { get; set; }
    }
}

