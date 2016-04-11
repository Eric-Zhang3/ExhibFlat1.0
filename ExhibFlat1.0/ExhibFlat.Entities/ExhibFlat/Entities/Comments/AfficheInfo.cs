namespace ExhibFlat.Entities.Comments
{
    using ExhibFlat.Core;
    using ExhibFlat.Components.Validation.Validators;
    using System;
    using System.Runtime.CompilerServices;

    public class AfficheInfo
    {
        public DateTime AddedDate { get; set; }

        public int AfficheId { get; set; }

        [StringLengthValidator(1, 0x3b9ac9ff, Ruleset = "ValAfficheInfo", MessageTemplate = "公告内容不能为空")]
        public string Content { get; set; }

        [HtmlCoding, StringLengthValidator(1, 60, Ruleset = "ValAfficheInfo", MessageTemplate = "公告标题不能为空，长度限制在60个字符以内")]
        public string Title { get; set; }

        public string OutLink { get; set; }
    }
}

