namespace ExhibFlat.Entities.Commodities
{
    using ExhibFlat.Core.Entities;
    using System;
    using System.Runtime.CompilerServices;

    public class ProductTypeQuery : Pagination
    {
        public string TypeName { get; set; }
    }
}

