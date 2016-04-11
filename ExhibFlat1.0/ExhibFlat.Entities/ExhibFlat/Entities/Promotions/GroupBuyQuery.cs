namespace ExhibFlat.Entities.Promotions
{
    using ExhibFlat.Core.Entities;
    using System;
    using System.Runtime.CompilerServices;

    public class GroupBuyQuery : Pagination
    {
        public string ProductName { get; set; }
    }
}

