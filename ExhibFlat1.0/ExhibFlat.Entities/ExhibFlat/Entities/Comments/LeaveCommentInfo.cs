namespace ExhibFlat.Entities.Comments
{
    using ExhibFlat.Core;
    using ExhibFlat.Components.Validation.Validators;
    using System;
    using System.Runtime.CompilerServices;

    public class LeaveCommentInfo
    {
        public DateTime LastDate { get; set; }

        public long LeaveId { get; set; }

        [StringLengthValidator(1, 300, Ruleset="Refer", MessageTemplate="留言内容为必填项，长度限制在300字符以内"), HtmlCoding]
        public string PublishContent { get; set; }

        public DateTime PublishDate { get; set; }

        [StringLengthValidator(1, 60, Ruleset="Refer", MessageTemplate="标题为必填项，长度限制在60字符以内"), HtmlCoding]
        public string Title { get; set; }

        public int? UserId { get; set; }

        [StringLengthValidator(1, 60, Ruleset="Refer", MessageTemplate="用户名为必填项，长度限制在60字符以内"), HtmlCoding]
        public string UserName { get; set; }
    }
}

