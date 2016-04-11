namespace ExhibFlat.Entities.Store
{
    using ExhibFlat.Core.Entities;
    using System;
    using System.Runtime.CompilerServices;

    public class ManagerQuery : Pagination
    {
        public Guid RoleId { get; set; }

        public string Username { get; set; }
    }
}

