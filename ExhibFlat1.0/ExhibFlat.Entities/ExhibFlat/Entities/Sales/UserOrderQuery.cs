namespace ExhibFlat.Entities.Sales
{
    using ExhibFlat.Core.Entities;
    using System;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class UserOrderQuery : Pagination
    {
        public DateTime? EndDate { get; set; }

        public string OrderId { get; set; }

        public string ShipTo { get; set; }

        public DateTime? StartDate { get; set; }

        public string UserName { get; set; }
    }
}

