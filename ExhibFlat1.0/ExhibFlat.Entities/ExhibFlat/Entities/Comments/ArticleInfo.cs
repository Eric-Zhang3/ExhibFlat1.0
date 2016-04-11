namespace ExhibFlat.Entities.Comments
{
    using ExhibFlat.Core;
    using ExhibFlat.Components.Validation.Validators;
    using System;
    using System.Runtime.CompilerServices;

    public class ArticleInfo
    {
        public DateTime AddedDate { get; set; }

        public int ArticleId { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        [StringLengthValidator(1, 0x3b9ac9ff, Ruleset="ValArticleInfo", MessageTemplate="文章内容不能为空")]
        public string Content { get; set; }

        [HtmlCoding, StringLengthValidator(0, 300, Ruleset="ValArticleInfo", MessageTemplate="文章摘要的长度限制在300个字符以内")]
        public string Description { get; set; }

        public string IconUrl { get; set; }

        public bool IsRelease { get; set; }

        [StringLengthValidator(0, 100, Ruleset="ValArticleInfo", MessageTemplate="告诉搜索引擎此文章页面的主要内容，长度限制在100个字符以内"), HtmlCoding]
        public string MetaDescription { get; set; }

        [StringLengthValidator(0, 100, Ruleset="ValArticleInfo", MessageTemplate="让用户可以通过搜索引擎搜索到此文章的浏览页面，长度限制在100个字符以内"), HtmlCoding]
        public string MetaKeywords { get; set; }

        [StringLengthValidator(1, 60, Ruleset="ValArticleInfo", MessageTemplate="文章标题不能为空，长度限制在60个字符以内"), HtmlCoding]
        public string Title { get; set; }
    }
}

