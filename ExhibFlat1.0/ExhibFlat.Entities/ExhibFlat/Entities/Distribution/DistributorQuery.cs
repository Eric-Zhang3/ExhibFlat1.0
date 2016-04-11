namespace ExhibFlat.Entities.Distribution
{
    using ExhibFlat.Core.Entities;
    using System;
    using System.Runtime.CompilerServices;

    public class DistributorQuery : Pagination
    {
        public int? GradeId { get; set; }

        public bool IsApproved { get; set; }

        public int? LineId { get; set; }

        public string RealName { get; set; }

        public string Username { get; set; }
    }
}

