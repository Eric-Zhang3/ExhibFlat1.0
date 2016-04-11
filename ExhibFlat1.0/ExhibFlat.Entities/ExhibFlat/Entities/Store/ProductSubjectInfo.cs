namespace ExhibFlat.Entities.Store
{
    using System;
    using System.Runtime.CompilerServices;

    public class ProductSubjectInfo
    {
        public string Categories { get; set; }

        public string CategoryName { get; set; }

        public string Keywords { get; set; }

        public int MaxNum { get; set; }

        public decimal? MaxPrice { get; set; }

        public decimal? MinPrice { get; set; }

        public int SubjectId { get; set; }

        public string SubjectName { get; set; }

        public int TagId { get; set; }
    }
}

