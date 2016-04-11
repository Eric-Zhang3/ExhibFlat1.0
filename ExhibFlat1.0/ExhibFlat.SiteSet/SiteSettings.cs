namespace ExhibFlat.SiteSet
{
    using ExhibFlat.Core;
    using ExhibFlat.Components.Validation.Validators;
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Xml;

    public class SiteSettings
    {
        public SiteSettings(string siteUrl, int? distributorUserId)
        {
            this.SiteUrl = siteUrl;
            this.UserId = distributorUserId;
            this.Disabled = false;
            this.SiteDescription = "最安全，最专业的供应链平台";
            this.Theme = "default_hpq";
            this.SiteName = "Hishop";
            this.LogoUrl = "/utility/pics/logo.jpg";
            this.DistrLogoUrl = "/utility/pics/fLogo.png";
            this.AdminLogoUrl = "/utility/pics/logogo.png";
            this.DefaultProductImage = "/utility/pics/none.gif";
            this.DefaultProductThumbnail1 = "/utility/pics/none.gif";
            this.DefaultProductThumbnail2 = "/utility/pics/none.gif";
            this.DefaultProductThumbnail3 = "/utility/pics/none.gif";
            this.DefaultProductThumbnail4 = "/utility/pics/none.gif";
            this.DefaultProductThumbnail5 = "/utility/pics/none.gif";
            this.DefaultProductThumbnail6 = "/utility/pics/none.gif";
            this.DefaultProductThumbnail7 = "/utility/pics/none.gif";
            this.DefaultProductThumbnail8 = "/utility/pics/none.gif";
            this.DecimalLength = 2;
            this.IsShowGroupBuy = true;
            this.IsShowCountDown = true;
            this.IsShowOnlineGift = true;
            this.IsOpenSiteSale = true;
            this.PointsRate = 1M;
            this.OrderShowDays = 7;
            this.CloseOrderDays = 3;
            this.ClosePurchaseOrderDays = 5;
            this.FinishOrderDays = 7;
            this.FinishPurchaseOrderDays = 10;
        }

        public static SiteSettings FromXml(XmlDocument doc)
        {
            XmlNode node = doc.SelectSingleNode("Settings");
            return new SiteSettings(node.SelectSingleNode("SiteUrl").InnerText, null) { 
                AssistantIv = node.SelectSingleNode("AssistantIv").InnerText, AssistantKey = node.SelectSingleNode("AssistantKey").InnerText, DecimalLength = int.Parse(node.SelectSingleNode("DecimalLength").InnerText), DefaultProductImage = node.SelectSingleNode("DefaultProductImage").InnerText, DefaultProductThumbnail1 = node.SelectSingleNode("DefaultProductThumbnail1").InnerText, DefaultProductThumbnail2 = node.SelectSingleNode("DefaultProductThumbnail2").InnerText, DefaultProductThumbnail3 = node.SelectSingleNode("DefaultProductThumbnail3").InnerText, DefaultProductThumbnail4 = node.SelectSingleNode("DefaultProductThumbnail4").InnerText, DefaultProductThumbnail5 = node.SelectSingleNode("DefaultProductThumbnail5").InnerText, DefaultProductThumbnail6 = node.SelectSingleNode("DefaultProductThumbnail6").InnerText, DefaultProductThumbnail7 = node.SelectSingleNode("DefaultProductThumbnail7").InnerText, DefaultProductThumbnail8 = node.SelectSingleNode("DefaultProductThumbnail8").InnerText, CheckCode = node.SelectSingleNode("CheckCode").InnerText, IsOpenSiteSale = bool.Parse(node.SelectSingleNode("IsOpenSiteSale").InnerText), IsShowGroupBuy = bool.Parse(node.SelectSingleNode("IsShowGroupBuy").InnerText), IsShowCountDown = bool.Parse(node.SelectSingleNode("IsShowCountDown").InnerText),
                IsShowOnlineGift = bool.Parse(node.SelectSingleNode("IsShowOnlineGift").InnerText),
                Disabled = bool.Parse(node.SelectSingleNode("Disabled").InnerText),
                ReferralDeduct = int.Parse(node.SelectSingleNode("ReferralDeduct").InnerText),
                Footer = node.SelectSingleNode("Footer").InnerText,
                RegisterAgreement = node.SelectSingleNode("RegisterAgreement").InnerText,
                HtmlOnlineServiceCode = node.SelectSingleNode("HtmlOnlineServiceCode").InnerText,
                LogoUrl = node.SelectSingleNode("LogoUrl").InnerText,
                DistrLogoUrl = node.SelectSingleNode("DistrLogoUrl").InnerText,
                AdminLogoUrl = node.SelectSingleNode("AdminLogoUrl").InnerText,
                OrderShowDays = int.Parse(node.SelectSingleNode("OrderShowDays").InnerText),
                CloseOrderDays = int.Parse(node.SelectSingleNode("CloseOrderDays").InnerText),
                ClosePurchaseOrderDays = int.Parse(node.SelectSingleNode("ClosePurchaseOrderDays").InnerText),
                FinishOrderDays = int.Parse(node.SelectSingleNode("FinishOrderDays").InnerText),
                FinishPurchaseOrderDays = int.Parse(node.SelectSingleNode("FinishPurchaseOrderDays").InnerText),
                TaxRate = decimal.Parse(node.SelectSingleNode("TaxRate").InnerText),
                PointsRate = decimal.Parse(node.SelectSingleNode("PointsRate").InnerText),
                SearchMetaDescription = node.SelectSingleNode("SearchMetaDescription").InnerText,
                SearchMetaKeywords = node.SelectSingleNode("SearchMetaKeywords").InnerText, 
                SiteDescription = node.SelectSingleNode("SiteDescription").InnerText, SiteName = node.SelectSingleNode("SiteName").InnerText, SiteUrl = node.SelectSingleNode("SiteUrl").InnerText, UserId = null, Theme = node.SelectSingleNode("Theme").InnerText, YourPriceName = node.SelectSingleNode("YourPriceName").InnerText, DistributorRequestInstruction = node.SelectSingleNode("DistributorRequestInstruction").InnerText, DistributorRequestProtocols = node.SelectSingleNode("DistributorRequestProtocols").InnerText, EmailSender = node.SelectSingleNode("EmailSender").InnerText, EmailSettings = node.SelectSingleNode("EmailSettings").InnerText, SMSSender = node.SelectSingleNode("SMSSender").InnerText, SMSSettings = node.SelectSingleNode("SMSSettings").InnerText, SiteMapTime = node.SelectSingleNode("SiteMapTime").InnerText, SiteMapNum = node.SelectSingleNode(" SiteMapNum").InnerText, EnabledCnzz = bool.Parse(node.SelectSingleNode("EnabledCnzz").InnerText), CnzzUsername = node.SelectSingleNode("CnzzUsername").InnerText, 
                CnzzPassword = node.SelectSingleNode("CnzzPassword").InnerText
             };
        }

        private static void SetNodeValue(XmlDocument doc, XmlNode root, string nodeName, string nodeValue)
        {
            XmlNode newChild = root.SelectSingleNode(nodeName);
            if (newChild == null)
            {
                newChild = doc.CreateElement(nodeName);
                root.AppendChild(newChild);
            }
            newChild.InnerText = nodeValue;
        }

        public void WriteToXml(XmlDocument doc)
        {
            XmlNode root = doc.SelectSingleNode("Settings");
            SetNodeValue(doc, root, "SiteUrl", this.SiteUrl);
            SetNodeValue(doc, root, "AssistantIv", this.AssistantIv);
            SetNodeValue(doc, root, "AssistantKey", this.AssistantKey);
            SetNodeValue(doc, root, "DecimalLength", this.DecimalLength.ToString(CultureInfo.InvariantCulture));
            SetNodeValue(doc, root, "DefaultProductImage", this.DefaultProductImage);
            SetNodeValue(doc, root, "DefaultProductThumbnail1", this.DefaultProductThumbnail1);
            SetNodeValue(doc, root, "DefaultProductThumbnail2", this.DefaultProductThumbnail2);
            SetNodeValue(doc, root, "DefaultProductThumbnail3", this.DefaultProductThumbnail3);
            SetNodeValue(doc, root, "DefaultProductThumbnail4", this.DefaultProductThumbnail4);
            SetNodeValue(doc, root, "DefaultProductThumbnail5", this.DefaultProductThumbnail5);
            SetNodeValue(doc, root, "DefaultProductThumbnail6", this.DefaultProductThumbnail6);
            SetNodeValue(doc, root, "DefaultProductThumbnail7", this.DefaultProductThumbnail7);
            SetNodeValue(doc, root, "DefaultProductThumbnail8", this.DefaultProductThumbnail8);
            SetNodeValue(doc, root, "CheckCode", this.CheckCode);
            SetNodeValue(doc, root, "IsOpenSiteSale", this.IsOpenSiteSale ? "true" : "false");
            SetNodeValue(doc, root, "IsShowGroupBuy", this.IsShowGroupBuy ? "true" : "false");
            SetNodeValue(doc, root, "IsShowCountDown", this.IsShowCountDown ? "true" : "false");
            SetNodeValue(doc, root, "IsShowOnlineGift", this.IsShowOnlineGift ? "true" : "false");
            SetNodeValue(doc, root, "Disabled", this.Disabled ? "true" : "false");
            SetNodeValue(doc, root, "ReferralDeduct", this.ReferralDeduct.ToString(CultureInfo.InvariantCulture));
            SetNodeValue(doc, root, "Footer", this.Footer);
            SetNodeValue(doc, root, "RegisterAgreement", this.RegisterAgreement);
            SetNodeValue(doc, root, "HtmlOnlineServiceCode", this.HtmlOnlineServiceCode);
            SetNodeValue(doc, root, "LogoUrl", this.LogoUrl);
            SetNodeValue(doc, root, "DistrLogoUrl", this.DistrLogoUrl);
            SetNodeValue(doc, root, "AdminLogoUrl", this.AdminLogoUrl);
            SetNodeValue(doc, root, "OrderShowDays", this.OrderShowDays.ToString(CultureInfo.InvariantCulture));
            SetNodeValue(doc, root, "CloseOrderDays", this.CloseOrderDays.ToString(CultureInfo.InvariantCulture));
            SetNodeValue(doc, root, "ClosePurchaseOrderDays", this.ClosePurchaseOrderDays.ToString(CultureInfo.InvariantCulture));
            SetNodeValue(doc, root, "FinishOrderDays", this.FinishOrderDays.ToString(CultureInfo.InvariantCulture));
            SetNodeValue(doc, root, "FinishPurchaseOrderDays", this.FinishPurchaseOrderDays.ToString(CultureInfo.InvariantCulture));
            SetNodeValue(doc, root, "TaxRate", this.TaxRate.ToString(CultureInfo.InvariantCulture));
            SetNodeValue(doc, root, "PointsRate", this.PointsRate.ToString("F"));
            SetNodeValue(doc, root, "SearchMetaDescription", this.SearchMetaDescription);
            SetNodeValue(doc, root, "SearchMetaKeywords", this.SearchMetaKeywords);
            SetNodeValue(doc, root, "SiteDescription", this.SiteDescription);
            SetNodeValue(doc, root, "SiteName", this.SiteName);
            SetNodeValue(doc, root, "Theme", this.Theme);
            SetNodeValue(doc, root, "YourPriceName", this.YourPriceName);
            SetNodeValue(doc, root, "DistributorRequestInstruction", this.DistributorRequestInstruction);
            SetNodeValue(doc, root, "DistributorRequestProtocols", this.DistributorRequestProtocols);
            SetNodeValue(doc, root, "EmailSender", this.EmailSender);
            SetNodeValue(doc, root, "EmailSettings", this.EmailSettings);
            SetNodeValue(doc, root, "SMSSender", this.SMSSender);
            SetNodeValue(doc, root, "SMSSettings", this.SMSSettings);
            SetNodeValue(doc, root, "SiteMapNum", this.SiteMapNum);
            SetNodeValue(doc, root, "SiteMapTime", this.SiteMapTime);
            SetNodeValue(doc, root, "EnabledCnzz", this.EnabledCnzz ? "true" : "false");
            SetNodeValue(doc, root, "CnzzUsername", this.CnzzUsername);
            SetNodeValue(doc, root, "CnzzPassword", this.CnzzPassword);
        }

        public string AssistantIv { get; set; }

        public string AssistantKey { get; set; }

        public string CheckCode { get; set; }

        [RangeValidator(1, RangeBoundaryType.Inclusive, 90, RangeBoundaryType.Inclusive, Ruleset="ValMasterSettings", MessageTemplate="过期几天自动关闭订单的天数必须在1-90之间")]
        public int CloseOrderDays { get; set; }

        [RangeValidator(1, RangeBoundaryType.Inclusive, 90, RangeBoundaryType.Inclusive, Ruleset="ValMasterSettings", MessageTemplate="过期几天自动关闭采购单的天数必须在1-90之间")]
        public int ClosePurchaseOrderDays { get; set; }

        public string CnzzPassword { get; set; }

        public string CnzzUsername { get; set; }

        public DateTime? CreateDate { get; set; }

        public int DecimalLength { get; set; }

        public string DefaultProductImage { get; set; }

        public string DefaultProductThumbnail1 { get; set; }

        public string DefaultProductThumbnail2 { get; set; }

        public string DefaultProductThumbnail3 { get; set; }

        public string DefaultProductThumbnail4 { get; set; }

        public string DefaultProductThumbnail5 { get; set; }

        public string DefaultProductThumbnail6 { get; set; }

        public string DefaultProductThumbnail7 { get; set; }

        public string DefaultProductThumbnail8 { get; set; }

        public bool Disabled { get; set; }

        public string DistributorRequestInstruction { get; set; }

        public string DistributorRequestProtocols { get; set; }

        public bool EmailEnabled
        {
            get
            {
                return (((!string.IsNullOrEmpty(this.EmailSender) && !string.IsNullOrEmpty(this.EmailSettings)) && (this.EmailSender.Trim().Length > 0)) && (this.EmailSettings.Trim().Length > 0));
            }
        }

        public string EmailSender { get; set; }

        public string EmailSettings { get; set; }

        public bool EnabledCnzz { get; set; }

        public DateTime? EtaoApplyTime { get; set; }

        public string EtaoID { get; set; }

        public int EtaoStatus { get; set; }

        [RangeValidator(1, RangeBoundaryType.Inclusive, 90, RangeBoundaryType.Inclusive, Ruleset="ValMasterSettings", MessageTemplate="发货几天自动完成订单的天数必须在1-90之间")]
        public int FinishOrderDays { get; set; }

        [RangeValidator(1, RangeBoundaryType.Inclusive, 90, RangeBoundaryType.Inclusive, Ruleset="ValMasterSettings", MessageTemplate="发货几天自动完成采购单的天数必须在1-90之间")]
        public int FinishPurchaseOrderDays { get; set; }

        public string Footer { get; set; }

        [StringLengthValidator(0, 0xfa0, Ruleset="ValMasterSettings", MessageTemplate="网页客服代码长度限制在4000个字符以内")]
        public string HtmlOnlineServiceCode { get; set; }

        public bool IsDistributorSettings
        {
            get
            {
                return this.UserId.HasValue;
            }
        }

        public bool IsOpenEtao { get; set; }

        public bool IsOpenSiteSale { get; set; }

        public bool IsShowCountDown { get; set; }

        public bool IsShowGroupBuy { get; set; }

        public bool IsShowOnlineGift { get; set; }

        public string LogoUrl { get; set; }

        [RangeValidator(1, RangeBoundaryType.Inclusive, 90, RangeBoundaryType.Inclusive, Ruleset="ValMasterSettings", MessageTemplate="最近几天内订单的天数必须在1-90之间")]
        public int OrderShowDays { get; set; }

        [RangeValidator(typeof(decimal), "0.1", RangeBoundaryType.Inclusive, "10000000", RangeBoundaryType.Inclusive, Ruleset="ValMasterSettings", MessageTemplate="几元一积分必须在0.1-10000000之间")]
        public decimal PointsRate { get; set; }

        public int ReferralDeduct { get; set; }

        public string RegisterAgreement { get; set; }

        public DateTime? RequestDate { get; set; }

        [StringLengthValidator(0, 100, Ruleset="ValMasterSettings", MessageTemplate="店铺描述META_DESCRIPTION的长度限制在100字符以内"), HtmlCoding]
        public string SearchMetaDescription { get; set; }

        [StringLengthValidator(0, 100, Ruleset="ValMasterSettings", MessageTemplate="搜索关键字META_KEYWORDS的长度限制在100字符以内")]
        public string SearchMetaKeywords { get; set; }

        [StringLengthValidator(0, 60, Ruleset="ValMasterSettings", MessageTemplate="简单介绍TITLE的长度限制在60字符以内")]
        public string SiteDescription { get; set; }

        public string SiteMapNum { get; set; }

        public string SiteMapTime { get; set; }

        [StringLengthValidator(1, 60, Ruleset="ValMasterSettings", MessageTemplate="店铺名称为必填项，长度限制在60字符以内")]
        public string SiteName { get; set; }

        [StringLengthValidator(1, 0x80, Ruleset="ValMasterSettings", MessageTemplate="店铺域名必须控制在128个字符以内")]
        public string SiteUrl { get; set; }

        public bool SMSEnabled
        {
            get
            {
                return (((!string.IsNullOrEmpty(this.SMSSender) && !string.IsNullOrEmpty(this.SMSSettings)) && (this.SMSSender.Trim().Length > 0)) && (this.SMSSettings.Trim().Length > 0));
            }
        }

        public string SMSSender { get; set; }

        public string SMSSettings { get; set; }

        [RangeValidator(typeof(decimal), "0", RangeBoundaryType.Inclusive, "100", RangeBoundaryType.Inclusive, Ruleset="ValMasterSettings", MessageTemplate="税率必须在0-100之间")]
        public decimal TaxRate { get; set; }

        public string Theme { get; set; }

        public int? UserId { get; private set; }

        [StringLengthValidator(0, 10, Ruleset="ValMasterSettings", MessageTemplate="“您的价”重命名的长度限制在10字符以内")]
        public string YourPriceName { get; set; }
        public string DistrLogoUrl { get; set; }
        public string AdminLogoUrl { get; set; }


    }
}

