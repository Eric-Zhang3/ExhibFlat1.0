namespace ExhibFlat.Entities.Comments
{
    using ExhibFlat.Core;
    using ExhibFlat.Components.Validation.Validators;
    using System;
    using System.Runtime.CompilerServices;

    public class ArticleCategoryInfo
    {
        public int CategoryId { get; set; }

        [HtmlCoding, StringLengthValidator(0, 300, Ruleset="ValArticleCategoryInfo", MessageTemplate="分类介绍最多只能输入300个字符")]
        public string Description { get; set; }

        public int DisplaySequence { get; set; }

        public string IconUrl { get; set; }

        [StringLengthValidator(1, 60, Ruleset="ValArticleCategoryInfo", MessageTemplate="分类名称不能为空，长度限制在60个字符以内"), HtmlCoding]
        public string Name { get; set; }
    }
}

