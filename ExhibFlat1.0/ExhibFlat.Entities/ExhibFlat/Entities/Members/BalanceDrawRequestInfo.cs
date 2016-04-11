﻿namespace ExhibFlat.Entities.Members
{
    using ExhibFlat.Core;
    using ExhibFlat.Components.Validation.Validators;
    using System;
    using System.Runtime.CompilerServices;

    [HasSelfValidation]
    public class BalanceDrawRequestInfo
    {
        [StringLengthValidator(1, 30, Ruleset="ValBalanceDrawRequestInfo", MessageTemplate="开户人真实姓名不能为空,长度限制在30字符以内"), HtmlCoding]
        public string AccountName { get; set; }

        [RangeValidator(typeof(decimal), "0.01", RangeBoundaryType.Inclusive, "10000000", RangeBoundaryType.Inclusive, Ruleset="ValBalanceDrawRequestInfo", MessageTemplate="提现金额不能为空，金额大小0.01-1000万之间")]
        public decimal Amount { get; set; }

        [HtmlCoding, StringLengthValidator(1, 60, Ruleset="ValBalanceDrawRequestInfo", MessageTemplate="开银行名称不能为空,长度限制在60字符以内")]
        public string BankName { get; set; }

        [StringLengthValidator(1, 100, Ruleset="ValBalanceDrawRequestInfo", MessageTemplate="个人银行帐号不能为空,限制在100个字符以内"), RegexValidator("^[0-9]*$", Ruleset="ValBalanceDrawRequestInfo", MessageTemplate="个人银行帐号只允许输入数字")]
        public string MerchantCode { get; set; }

        [HtmlCoding, StringLengthValidator(0, 300, Ruleset="ValBalanceDrawRequestInfo", MessageTemplate="备注长度限制在300字符以内")]
        public string Remark { get; set; }

        public DateTime RequestTime { get; set; }

        public int UserId { get; set; }

        [HtmlCoding]
        public string UserName { get; set; }
    }
}

