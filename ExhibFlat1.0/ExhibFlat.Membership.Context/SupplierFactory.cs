namespace ExhibFlat.Membership.Context
{
    using ExhibFlat.Membership.Core;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System;
    using System.Data;
    using System.Data.Common;
    using System.Runtime.InteropServices;
    using System.Web.Security;

    internal class SupplierFactory : UserFactory
    {
        private static readonly SupplierFactory _defaultInstance = new SupplierFactory();
        private BizActorProvider provider;

        static SupplierFactory()
        {
            _defaultInstance.provider = BizActorProvider.Instance();
        }

        private SupplierFactory()
        {
        }

        public override bool ChangeTradePassword(string username, string newPassword)
        {
            SiteManager user = HiContext.Current.User as SiteManager;
            if (user == null)
            {
                return false;
            }
            string oldPassword = this.ResetTradePassword(username);
            return this.ChangeTradePassword(username, oldPassword, newPassword);
        }

        public override bool ChangeTradePassword(string username, string oldPassword, string newPassword)
        {
            return this.provider.ChangeDistributorTradePassword(username, oldPassword, newPassword);
        }

        public override bool Create(IUser userToCreate)
        {
            try
            {
                return this.provider.CreateSupplier(userToCreate as Supplier);
            }
            catch
            {
                return false;
            }
        }
        public override IUser GetUser(HiMembershipUser membershipUser)
        {
            return this.provider.GetSupplier(membershipUser);
        }

        public static SupplierFactory Instance()
        {
            return _defaultInstance;
        }

        public override bool OpenBalance(int userId, string tradePassword)
        {
            return true;
        }

        public override string ResetTradePassword(string username)
        {
            return "000000";
        }

        public override bool UpdateUser(IUser user)
        {
            return this.provider.UpdateSupplier(user as Supplier);
        }

        public override bool ValidTradePassword(string username, string password)
        {
            return this.provider.ValidDistributorTradePassword(username, password);
        }
    }
}

