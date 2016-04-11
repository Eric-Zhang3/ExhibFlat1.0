namespace ExhibFlat.Entities.Sales
{
    using ExhibFlat.Core.Entities;
    using System;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class SaleStatisticsQuery : Pagination
    {
        public DateTime? EndDate { get; set; }

        public string QueryKey { get; set; }

        public DateTime? StartDate { get; set; }
    }
}

