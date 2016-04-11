namespace ExhibFlat.Membership.Core
{
    using ExhibFlat.Membership.Core.Enums;
    using ExhibFlat.Core;
    using System;
    using System.Data;

    public abstract class MemberUserProvider
    {
        private static readonly MemberUserProvider _defaultInstance = (DataProviders.CreateInstance("ExhibFlat.Membership.Data.UserData,ExhibFlat.Membership.Data") as MemberUserProvider);

        protected MemberUserProvider()
        {
        }

        public abstract bool BindOpenId(string username, string openId, string openIdType);
        public abstract bool ChangePasswordQuestionAndAnswer(string username, string newQuestion, string newAnswer);
        
        public abstract AnonymousUser GetAnonymousUser();
        public abstract HiMembershipUser GetMembershipUser(int userId, string username, bool isOnline);
        public abstract string GetUsernameWithOpenId(string openId, string openIdType);
        public static MemberUserProvider Instance()
        {
            return _defaultInstance;
        }
        public abstract string ValiCompanyName(string cname);
        public abstract bool CheckBnessCode(string code);
    
        public abstract bool UpdateMembershipUser(HiMembershipUser user);
        public abstract bool ValidatePasswordAnswer(string username, string answer);
        public abstract bool UpdateSupplierIdByUser(int UserId, string SupplierId, string RelName, string Phone);
    }
}

