namespace ExhibFlat.Core.Configuration
{
    using System;
    using System.Globalization;

    public class RolesConfiguration
    {
        private string distributor = "Distributor";
        private string manager = "Manager";
        private string member = "Member";
        private string systemAdmin = "SystemAdministrator";
        private string underling = "Underling";
        private string supplier = "Supplier";

        public string RoleList()
        {
            return string.Format(CultureInfo.InvariantCulture, "^({0}|{1}|{2}|{3}|{4}|{5})$", new object[] { this.Distributor, this.Member, this.Underling, this.SystemAdministrator, this.Manager,this.Supplier });
        }

        public string Distributor
        {
            get
            {
                return this.distributor;
            }
        }

        public string Manager
        {
            get
            {
                return this.manager;
            }
        }

        public string Member
        {
            get
            {
                return this.member;
            }
        }

        public string SystemAdministrator
        {
            get
            {
                return this.systemAdmin;
            }
        }

        public string Underling
        {
            get
            {
                return this.underling;
            }
        }
        public string Supplier
        {
            get
            {
                return this.supplier;
            }
        }
    }
}

