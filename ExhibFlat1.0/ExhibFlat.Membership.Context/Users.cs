namespace ExhibFlat.Membership.Context
{
    using ExhibFlat.Core;
    using ExhibFlat.Core.Enums;
    using ExhibFlat.Membership.Core;
    using ExhibFlat.Membership.Core.Enums;
    using System;
    using System.Collections;
    using System.Globalization;
    using System.Web;

    public static class Users
    {
        public static void ClearUserCache(IUser user)
        {
            Hashtable hashtable = UserCache();
            hashtable[UserKey(user.UserId.ToString(CultureInfo.InvariantCulture))] = null;
            hashtable[UserKey(user.Username)] = null;
        }

       
        
        public static CreateUserStatus CreateOnlyDistributorUser(IUser user, string roles)
        {
            CreateUserStatus unknownFailure = UserHelper.CreateOnlyDistributor(user.MembershipUser,new string[] { roles});
            if ((unknownFailure == CreateUserStatus.Created) && !DistributorFactory.Instance().Create(user))
            {
                HiMembership.Delete(user.Username);
                unknownFailure = CreateUserStatus.UnknownFailure;
            }
            return unknownFailure;
        }
        public static IUser FindUserByUsername(string username)
        {
            return GetUser(0, username, true, false);
        }

        public static AnonymousUser GetAnonymousUser()
        {
            AnonymousUser anonymousUser = HiCache.Get("DataCache-AnonymousUser") as AnonymousUser;
            if (anonymousUser == null)
            {
                anonymousUser = MemberUserProvider.Instance().GetAnonymousUser();
                if (((anonymousUser != null) && (anonymousUser.Username != null)) && (anonymousUser.UserId > 0))
                {
                    HiCache.Insert("DataCache-AnonymousUser", anonymousUser, 120);
                }
            }
            return anonymousUser;
        }

        public static string GetLoggedOnUsername()
        {
            HttpContext current = HttpContext.Current;
            if (!(current.User.Identity.IsAuthenticated && !string.IsNullOrEmpty(current.User.Identity.Name)))
            {
                return "Anonymous";
            }
            return current.User.Identity.Name;
        }

        public static IUser GetUser()
        {
            IUser user = GetUser(0, GetLoggedOnUsername(), true, true);
            if (!user.IsAnonymous)
            {
                ApplicationType applicationType = HiContext.Current.ApplicationType;
                if (applicationType == ApplicationType.Unknown)
                {
                    return GetAnonymousUser();
                }
                if ((applicationType == ApplicationType.Admin) && (user.UserRole != UserRole.SiteManager))
                {
                    return GetAnonymousUser();
                }
                if ((applicationType == ApplicationType.Distributor) && (user.UserRole != UserRole.Distributor))
                {
                    return GetAnonymousUser();
                }
                if ((applicationType == ApplicationType.Supplier) && (user.UserRole != UserRole.Supplier))
                {
                    return GetAnonymousUser();
                }
                if ((((applicationType == ApplicationType.Member) || (applicationType == ApplicationType.Common)) && (user.UserRole != UserRole.Member)) && (user.UserRole != UserRole.Underling) && (user.UserRole != UserRole.Supplier) && user.UserRole != UserRole.Channel&&user.UserRole!=UserRole.Service)
                {
                    return GetAnonymousUser();
                }

            }
            return user;
        }

        public static IUser GetUser(int userId)
        {
            return GetUser(userId, null, true, false);
        }
      
        public static IUser GetUser(int userId, bool isCacheable)
        {
            return GetUser(userId, null, isCacheable, false);
        }

        public static IUser GetUser(int userId, string username, bool isCacheable, bool userIsOnline)
        {
            IUser user = null;
            try
            { 
                if (((userId == 0) && !string.IsNullOrEmpty(username)) && username.Equals("Anonymous", StringComparison.CurrentCultureIgnoreCase))
                {
                    return GetAnonymousUser();
                }
                Hashtable hashtable = UserCache();
                 
                string str = (userId > 0) ? UserKey(userId.ToString(CultureInfo.InvariantCulture)) : UserKey(username);
              
                if (isCacheable)
                {
                    user = hashtable[str] as IUser;
                    if (user != null)
                    {
                        return user;
                    }
                }

                HiMembershipUser membershipUser = UserHelper.GetMembershipUser(userId, username, userIsOnline);
             
                if (membershipUser == null)
                {
                    return GetAnonymousUser();
                }
                user = UserFactory.Create(membershipUser.UserRole).GetUser(membershipUser); 
                if (isCacheable)
                {
                    hashtable[UserKey(user.Username)] = user;
                    hashtable[UserKey(user.UserId.ToString(CultureInfo.InvariantCulture))] = user;
                }
            }
            catch (Exception e)
            {
                log.GetInstance().writeLog(e.Message.ToString());
                throw;
            }
            return user;
        }
        public static IUser GetDistributor(int userId, string username, bool isCacheable, bool userIsOnline)
        {
            if (((userId == 0) && !string.IsNullOrEmpty(username)) && username.Equals("Anonymous", StringComparison.CurrentCultureIgnoreCase))
            {
                return GetAnonymousUser();
            }
            Hashtable hashtable = UserCache();
            string str = (userId > 0) ? UserKey(userId.ToString(CultureInfo.InvariantCulture)) : UserKey(username);
            IUser user = null;
            if (isCacheable)
            {
                user = hashtable[str] as IUser;
                if (user != null)
                {
                    return user;
                }
            }
            HiMembershipUser membershipUser = UserHelper.GetMembershipUser(userId, username, userIsOnline);
            if (membershipUser == null)
            {
                return GetAnonymousUser();
            }
            user = DistributorFactory.Instance().GetUser(membershipUser);
            if (isCacheable)
            {
                hashtable[UserKey(user.Username)] = user;
                hashtable[UserKey(user.UserId.ToString(CultureInfo.InvariantCulture))] = user;
            }
            return user;
        }


        public static IUser GetSupplierUser(int userId, string username, bool isCacheable, bool userIsOnline)
        {
            if (((userId == 0) && !string.IsNullOrEmpty(username)) && username.Equals("Anonymous", StringComparison.CurrentCultureIgnoreCase))
            {
                return GetAnonymousUser();
            }
            Hashtable hashtable = UserCache();
            string str = (userId > 0) ? UserKey(userId.ToString(CultureInfo.InvariantCulture)) : UserKey(username);
            IUser user = null;
            if (isCacheable)
            {
                user = hashtable[str] as IUser;
                if (user != null)
                {
                    return user;
                }
            }
            HiMembershipUser membershipUser = UserHelper.GetMembershipUser(userId, username, userIsOnline);
            if (membershipUser == null)
            {
                return GetAnonymousUser();
            }
            user = ManagerFactory.Instance().GetUser(membershipUser);
            if (isCacheable)
            {
                hashtable[UserKey(user.Username)] = user;
                hashtable[UserKey(user.Password)] = user;
                hashtable[UserKey(user.UserId.ToString(CultureInfo.InvariantCulture))] = user;
            }
            return user;
        }

        public static bool UpdateUser(IUser user)
        {
            if (null == user)
            {
                return false;
            }
            bool flag = UserHelper.UpdateUser(user.MembershipUser);
            if (flag)
            {
                flag = UserFactory.Create(user.UserRole).UpdateUser(user);
                HiContext current = HiContext.Current;
                if (current.User.UserId == user.UserId)
                {
                    current.User = user;
                }
            }
            ClearUserCache(user);
            return flag;
        }
        public static bool UpdateDistributor(IUser user)
        {
            if (null == user)
            {
                return false;
            }
            bool flag = UserHelper.UpdateUser(user.MembershipUser);
            if (flag)
            {
                flag = DistributorFactory.Instance().UpdateUser(user);
                HiContext current = HiContext.Current;
                if (current.User.UserId == user.UserId)
                {
                    current.User = user;
                }
            }
            ClearUserCache(user);
            return flag;
        }
        private static Hashtable UserCache()
        {
            Hashtable hashtable = HiCache.Get("DataCache-UserLookuptable") as Hashtable;
            if (hashtable == null)
            {
                hashtable = new Hashtable();
                HiCache.Insert("DataCache-UserLookuptable", hashtable, 120);
            }
            return hashtable;
        }

        private static string UserKey(object key)
        {
            return string.Format(CultureInfo.InvariantCulture, "User-{0}", new object[] { key }).ToLower(CultureInfo.InvariantCulture);
        }

        public static LoginUserStatus ValidateUser(IUser user)
        {
            return UserHelper.ValidateUser(user.MembershipUser);
        }

        public static bool ValidTradePassword(IUser user)
        {
            return UserFactory.Create(user.UserRole).ValidTradePassword(user.Username, user.TradePassword);
        }
    }
}

