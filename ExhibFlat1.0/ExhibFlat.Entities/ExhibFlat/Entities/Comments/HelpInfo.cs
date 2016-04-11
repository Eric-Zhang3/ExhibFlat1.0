namespace ExhibFlat.Entities.Comments
{
    using ExhibFlat.Core;
    using ExhibFlat.Components.Validation.Validators;
    using System;
    using System.Runtime.CompilerServices;

    public class HelpInfo
    {
        public DateTime AddedDate { get; set; }

        public int CategoryId { get; set; }

        [StringLengthValidator(1, 0x3b9ac9ff, Ruleset="ValHelpInfo", MessageTemplate="帮助内容不能为空")]
        public string Content { get; set; }

        [StringLengthValidator(0, 300, Ruleset="ValHelpInfo", MessageTemplate="摘要的长度限制在300个字符以内"), HtmlCoding]
        public string Description { get; set; }

        public int HelpId { get; set; }

        public bool IsShowFooter { get; set; }

        [StringLengthValidator(0, 100, Ruleset="ValHelpInfo", MessageTemplate="告诉搜索引擎此帮助页面的主要内容，长度限制在100个字符以内"), HtmlCoding]
        public string MetaDescription { get; set; }

        [StringLengthValidator(0, 100, Ruleset="ValHelpInfo", MessageTemplate="让用户可以通过搜索引擎搜索到此帮助的浏览页面，长度限制在100个字符以内"), HtmlCoding]
        public string MetaKeywords { get; set; }

        [HtmlCoding, StringLengthValidator(1, 60, Ruleset="ValHelpInfo", MessageTemplate="帮助主题不能为空，长度限制在60个字符以内")]
        public string Title { get; set; }
    }
}

