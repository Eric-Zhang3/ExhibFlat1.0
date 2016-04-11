namespace ExhibFlat.Membership.Context
{
    using ExhibFlat.Membership.Core;
    using ExhibFlat.Core;
    using System;

    public abstract class BizActorProvider
    {
        private static readonly BizActorProvider _defaultInstance = (DataProviders.CreateInstance("ExhibFlat.Membership.Data.BizActorData,ExhibFlat.Membership.Data") as BizActorProvider);

        protected BizActorProvider()
        {
        }

        public abstract bool ChangeDistributorTradePassword(string username, string oldPassword, string newPassword);
        public abstract bool ChangeMemberTradePassword(string username, string oldPassword, string newPassword);
        public abstract bool ChangeUnderlingTradePassword(string username, string oldPassword, string newPassword);
        public abstract bool CreateDistributor(Distributor distributor);
        public abstract bool CreateSupplier(Supplier supplier);//供应商
        public abstract bool CreateManager(SiteManager manager);
        public abstract bool CreateMember(Member member);
        public abstract bool CreateUnderling(Member underling);
        public abstract Distributor GetDistributor(HiMembershipUser membershipUser);
        public abstract Supplier GetSupplier(HiMembershipUser membershipUser);
        public abstract SiteManager GetManager(HiMembershipUser membershipUser);
        public abstract Member GetMember(HiMembershipUser membershipUser);
        public abstract Member GetUnderling(HiMembershipUser membershipUser);
        public static BizActorProvider Instance()
        {
            return _defaultInstance;
        }

        public abstract bool UpdateDistributor(Distributor distributor);
        public abstract bool UpdateSupplier(Supplier supplier);
        public abstract bool UpdateMember(Member member);
        public abstract bool UpdateUnderling(Member underling);
        public abstract bool ValidDistributorTradePassword(string username, string password);
        public abstract bool ValidMemberTradePassword(string username, string password);
        public abstract bool ValidUnderlingTradePassword(string username, string password);
    }
}

