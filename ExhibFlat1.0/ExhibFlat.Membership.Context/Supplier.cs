namespace ExhibFlat.Membership.Context
{
    using ExhibFlat.Core;
    using ExhibFlat.Core.Configuration;
    using ExhibFlat.Membership.Core;
    using ExhibFlat.Membership.Core.Enums;
    using ExhibFlat.Components.Validation;
    using ExhibFlat.Components.Validation.Validators;
    using System;
    using System.Runtime.CompilerServices;
    using System.Text.RegularExpressions;
    using System.Web.Security;


    [HasSelfValidation]
    public class Supplier : IUser
    {
        public static event EventHandler<UserEventArgs> DealPasswordChanged;

        public static event EventHandler<UserEventArgs> FindPassword;

        public static event EventHandler<EventArgs> Login;

        public static event EventHandler<UserEventArgs> Logout;

        public static event EventHandler<UserEventArgs> PasswordChanged;

        public static event EventHandler<UserEventArgs> Register;


        public Supplier()
        {
            this.MembershipUser = new HiMembershipUser(false, ExhibFlat.Membership.Core.Enums.UserRole.Supplier);
        }

        public Supplier(HiMembershipUser membershipUser)
        {
            this.MembershipUser = membershipUser;
        }

        public bool ChangePassword(string newPassword)
        {
            if (HiContext.Current.User.UserRole == ExhibFlat.Membership.Core.Enums.UserRole.SiteManager)
            {
                string password = this.MembershipUser.Membership.ResetPassword();
                if (this.MembershipUser.ChangePassword(password, newPassword))
                {
                    return true;
                }
            }
            return false;
        }

        public bool ChangePassword(string oldPassword, string newPassword)
        {
            return this.MembershipUser.ChangePassword(oldPassword, newPassword);
        }

        public bool ChangePasswordQuestionAndAnswer(string newQuestion, string newAnswer)
        {
            return this.MembershipUser.ChangePasswordQuestionAndAnswer(newQuestion, newAnswer);
        }

        public bool ChangePasswordQuestionAndAnswer(string oldAnswer, string newQuestion, string newAnswer)
        {
            return this.MembershipUser.ChangePasswordQuestionAndAnswer(oldAnswer, newQuestion, newAnswer);
        }

        public bool ChangePasswordWithAnswer(string answer, string newPassword)
        {
            return this.MembershipUser.ChangePasswordWithAnswer(answer, newPassword);
        }

        public bool ChangePasswordWithoutAnswer(string newPassword)
        {
            string password = this.MembershipUser.Membership.ResetPassword();
            return this.MembershipUser.ChangePassword(password, newPassword);
        }

        public bool ChangeTradePassword(string newPassword)
        {
            return DistributorFactory.Instance().ChangeTradePassword(this.Username, newPassword);
        }

        public bool ChangeTradePassword(string oldPassword, string newPassword)
        {
            return DistributorFactory.Instance().ChangeTradePassword(this.Username, oldPassword, newPassword);
        }

        [SelfValidation(Ruleset = "ValSupplier")]
        public void CheckDistributor(ValidationResults results)
        {
            HiConfiguration config = HiConfiguration.GetConfig();
            if ((string.IsNullOrEmpty(this.Username) || (this.Username.Length > config.UsernameMaxLength)) ||
                (this.Username.Length < config.UsernameMinLength))
            {
                results.AddResult(
                    new ValidationResult(
                        string.Format("用户名不能为空，长度必须在{0}-{1}个字符之间", config.UsernameMinLength, config.UsernameMaxLength),
                        this, "", "", null));
            }
            else if (!Regex.IsMatch(this.Username, config.UsernameRegex))
            {
                results.AddResult(new ValidationResult("用户名的格式错误", this, "", "", null));
            }
            if (string.IsNullOrEmpty(this.Email) || (this.Email.Length > 0x100))
            {
                results.AddResult(new ValidationResult("电子邮件不能为空，长度必须小于256个字符", this, "", "", null));
            }
            else if (!Regex.IsMatch(this.Email, config.EmailRegex))
            {
                results.AddResult(new ValidationResult("电子邮件的格式错误", this, "", "", null));
            }
            if (this.IsCreate)
            {
                if ((string.IsNullOrEmpty(this.Password) || (this.Password.Length > config.PasswordMaxLength)) ||
                    (this.Password.Length < 6))
                {
                    results.AddResult(
                        new ValidationResult(string.Format("密码不能为空，长度必须在{0}-{1}个字符之间", 6, config.PasswordMaxLength),
                            this, "", "", null));
                }
                //if ((string.IsNullOrEmpty(this.TradePassword) || (this.TradePassword.Length > config.PasswordMaxLength)) || (this.TradePassword.Length < 6))
                //{
                //    results.AddResult(new ValidationResult(string.Format("交易密码不能为空，长度必须在{0}-{1}个字符之间", 6, config.PasswordMaxLength), this, "", "", null));
                //}
            }
            if (
                !(string.IsNullOrEmpty(this.QQ) ||
                  (((this.QQ.Length <= 20) && (this.QQ.Length >= 3)) && Regex.IsMatch(this.QQ, "^[0-9]*$"))))
            {
                results.AddResult(new ValidationResult("QQ号长度限制在3-20个字符之间，只能输入数字", this, "", "", null));
            }
            if (
                !(string.IsNullOrEmpty(this.Zipcode) ||
                  (((this.Zipcode.Length <= 10) && (this.Zipcode.Length >= 3)) &&
                   Regex.IsMatch(this.Zipcode, "^[0-9]*$"))))
            {
                results.AddResult(new ValidationResult("邮编长度限制在3-10个字符之间，只能输入数字", this, "", "", null));
            }
            if (!(string.IsNullOrEmpty(this.Wangwang) || ((this.Wangwang.Length <= 20) && (this.Wangwang.Length >= 3))))
            {
                results.AddResult(new ValidationResult("旺旺长度限制在3-20个字符之间", this, "", "", null));
            }
            if (
                !(string.IsNullOrEmpty(this.MSN) ||
                  (((this.MSN.Length <= 0x100) && (this.MSN.Length >= 1)) && Regex.IsMatch(this.MSN, config.EmailRegex))))
            {
                results.AddResult(new ValidationResult("请输入正确MSN帐号，长度在1-256个字符以内", this, "", "", null));
            }
            if (
                !(string.IsNullOrEmpty(this.CellPhone) ||
                  (((this.CellPhone.Length <= 20) && (this.CellPhone.Length >= 3)) &&
                   Regex.IsMatch(this.CellPhone, "^[0-9]*$"))))
            {
                results.AddResult(new ValidationResult("手机号码长度限制在3-20个字符之间,只能输入数字", this, "", "", null));
            }
            if (
                !(string.IsNullOrEmpty(this.TelPhone) ||
                  (((this.TelPhone.Length <= 20) && (this.TelPhone.Length >= 3)) &&
                   Regex.IsMatch(this.TelPhone, "^[0-9-]*$"))))
            {
                results.AddResult(new ValidationResult("电话号码长度限制在3-20个字符之间，只能输入数字和字符“-”", this, "", "", null));
            }
        }

        public IUserCookie GetUserCookie()
        {
            return new UserCookie(this);
        }

        public bool IsInRole(string roleName)
        {
            return roleName.Equals(HiContext.Current.Config.RolesConfiguration.Supplier);
        }

        public void OnDealPasswordChanged(UserEventArgs args)
        {
            if (DealPasswordChanged != null)
            {
                DealPasswordChanged(this, args);
            }
        }

        public static void OnDealPasswordChanged(Member member, UserEventArgs args)
        {
            if (DealPasswordChanged != null)
            {
                DealPasswordChanged(member, args);
            }
        }

        public void OnFindPassword(UserEventArgs args)
        {
            if (FindPassword != null)
            {
                FindPassword(this, args);
            }
        }

        public static void OnFindPassword(Member member, UserEventArgs args)
        {
            if (FindPassword != null)
            {
                FindPassword(member, args);
            }
        }

        public void OnLogin()
        {
            if (Login != null)
            {
                Login(this, new EventArgs());
            }
        }

        public static void OnLogin(Member member)
        {
            if (Login != null)
            {
                Login(member, new EventArgs());
            }
        }

        public static void OnLogout(UserEventArgs args)
        {
            if (Logout != null)
            {
                Logout(null, args);
            }
        }

        public void OnPasswordChanged(UserEventArgs args)
        {
            if (PasswordChanged != null)
            {
                PasswordChanged(this, args);
            }
        }

        public static void OnPasswordChanged(Member member, UserEventArgs args)
        {
            if (PasswordChanged != null)
            {
                PasswordChanged(member, args);
            }
        }

        public void OnRegister(UserEventArgs args)
        {
            if (Register != null)
            {
                Register(this, args);
            }
        }

        public static void OnRegister(Member member, UserEventArgs args)
        {
            if (Register != null)
            {
                Register(member, args);
            }
        }

        public string ResetPassword(string answer)
        {
            return this.MembershipUser.ResetPassword(answer);
        }

        public bool ValidatePasswordAnswer(string answer)
        {
            return this.MembershipUser.ValidatePasswordAnswer(answer);
        }

        [StringLengthValidator(0, 100, Ruleset = "ValDistributor", MessageTemplate = "详细地址必须控制在100个字符以内"), HtmlCoding]
        public string Address { get; set; }

        public decimal Balance { get; set; }

        public DateTime? BirthDate
        {
            get { return this.MembershipUser.BirthDate; }
            set { this.MembershipUser.BirthDate = value; }
        }

        public string CellPhone { get; set; }

        public string Comment
        {
            get { return this.MembershipUser.Comment; }
            set { this.MembershipUser.Comment = value; }
        }

        [StringLengthValidator(0, 60, Ruleset = "ValDistributor", MessageTemplate = "公司名称必须控制在60个字符以内"), HtmlCoding]
        public string CompanyName { get; set; }

        public DateTime CreateDate
        {
            get { return this.MembershipUser.CreateDate; }
        }

        //登陆时间
        public DateTime RegTime { get; set; }

        public string Email
        {
            get { return this.MembershipUser.Email; }
            set { this.MembershipUser.Email = value; }
        }


        public decimal Expenditure { get; set; }

        public ExhibFlat.Membership.Core.Enums.Gender Gender
        {
            get { return this.MembershipUser.Gender; }
            set { this.MembershipUser.Gender = value; }
        }

        public int GradeId { get; set; }

        public bool IsAnonymous
        {
            get { return this.MembershipUser.IsAnonymous; }
        }

        public bool IsApproved
        {
            get { return this.MembershipUser.IsApproved; }
            set { this.MembershipUser.IsApproved = value; }
        }

        public bool IsCreate { get; set; }

        public bool IsLockedOut
        {
            get { return this.MembershipUser.IsLockedOut; }
        }

        public DateTime LastActivityDate
        {
            get { return this.MembershipUser.LastActivityDate; }
            set { this.MembershipUser.LastActivityDate = value; }
        }

        public DateTime LastLockoutDate
        {
            get { return this.MembershipUser.LastLockoutDate; }
        }

        public DateTime LastLoginDate
        {
            get { return this.MembershipUser.LastLoginDate; }
        }

        public DateTime LastPasswordChangedDate
        {
            get { return this.MembershipUser.LastPasswordChangedDate; }
        }

        public int MemberCount { get; set; }

        public HiMembershipUser MembershipUser { get; private set; }

        public string MobilePIN
        {
            get { return this.MembershipUser.MobilePIN; }
            set { this.MembershipUser.MobilePIN = value; }
        }

        public string MSN { get; set; }

        public string Password
        {
            get { return this.MembershipUser.Password; }
            set { this.MembershipUser.Password = value; }
        }

        public MembershipPasswordFormat PasswordFormat
        {
            get { return this.MembershipUser.PasswordFormat; }
            set { this.MembershipUser.PasswordFormat = value; }
        }

        public string PasswordQuestion
        {
            get { return this.MembershipUser.PasswordQuestion; }
        }

        public int PurchaseOrder { get; set; }
        public DateTime LastLoginTime { get; set; }

        public string QQ { get; set; }

        [StringLengthValidator(0, 20, Ruleset = "ValDistributor", MessageTemplate = "真实姓名必须控制在20个字符以内")]
        public string RealName { get; set; }

        public int RegionId { get; set; }

        [StringLengthValidator(0, 300, Ruleset = "ValDistributor", MessageTemplate = "合作备忘录必须控制在300个字符以内")]
        public string Remark { get; set; }

        public decimal RequestBalance { get; set; }

        public string TelPhone { get; set; }

        /// <summary>
        /// 供应商ID
        /// </summary>
        public string SupplierId { get; set; }

        public int TopRegionId { get; set; }
        public string Supplierpwd { get; set; }

        public int RequestRole { get; set; }

        public string RequestCode { get; set; }

        public string TradePassword
        {
            get { return this.MembershipUser.TradePassword; }
            set { this.MembershipUser.TradePassword = value; }
        }

        public MembershipPasswordFormat TradePasswordFormat
        {
            get { return this.MembershipUser.TradePasswordFormat; }
            set { this.MembershipUser.TradePasswordFormat = value; }
        }

        public int UserId
        {
            get { return this.MembershipUser.UserId; }
            set { this.MembershipUser.UserId = value; }
        }

        public string Username
        {
            get { return this.MembershipUser.Username; }
            set { this.MembershipUser.Username = value; }
        }

        public ExhibFlat.Membership.Core.Enums.UserRole UserRole
        {
            get { return this.MembershipUser.UserRole; }
        }

        public string Wangwang { get; set; }

        /// <summary>
        /// 邮编
        /// </summary>
        public string Zipcode { get; set; }

        /// <summary>
        /// 营业执照
        /// </summary>
        public string Businesslicense { get; set; }

        /// <summary>
        /// 税务登记证
        /// </summary>
        public string Shuiwu { get; set; }

        /// <summary>
        /// 组织机构代码证
        /// </summary>
        public string Zuzhijg { get; set; }

        /// <summary>
        /// 选择功能权限
        /// </summary>
        public string GongnengQx { get; set; }

        /// <summary>
        /// 营业执照编号
        /// </summary>
        public string BnessCode { get; set; }

        /// <summary>
        /// 组织机构代码证编号
        /// </summary>
        public string ShuiwuCode { get; set; }

        /// <summary>
        /// 组织机构代码证编号
        /// </summary>
        public string ZuzhijgCode { get; set; }

        /// <summary>
        /// 开户银行
        /// </summary>
        public string BankName { get; set; }

        /// <summary>
        /// 银行账号
        /// </summary>
        public string BankCode { get; set; }

        /// <summary>
        /// 三证合一
        /// </summary>
        public string ThreeCertificateImg { get; set; }

        public DateTime? ApplyDate { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdentityCardNum { get; set; }
        /// <summary>
        /// 身份证照片
        /// </summary>
        public string IdentityCardImg { get; set; }
        /// <summary>
        /// 店铺门头照片
        /// </summary>
        public string ShopTitleImg { get; set; }
        /// <summary>
        /// 店铺内景照片
        /// </summary>
        public string ShopInnerImg { get; set; }
    }
}