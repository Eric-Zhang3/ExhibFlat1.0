namespace ExhibFlat.Entities.Distribution
{
    using ExhibFlat.Components.Validation.Validators;
    using System;
    using System.Runtime.CompilerServices;

    [HasSelfValidation]
    public class SiteRequestInfo
    {
        [StringLengthValidator(1, 30, Ruleset="ValSiteRequest", MessageTemplate="域名不能为空,长度限制在30个字符以内,必须为有效格式")]
        public string FirstSiteUrl { get; set; }

        public string RefuseReason { get; set; }

        public int RequestId { get; set; }

        public SiteRequestStatus RequestStatus { get; set; }

        public DateTime RequestTime { get; set; }

        public int UserId { get; set; }
    }
}

