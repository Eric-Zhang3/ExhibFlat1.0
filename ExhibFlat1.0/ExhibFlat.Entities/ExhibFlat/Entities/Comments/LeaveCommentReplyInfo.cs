namespace ExhibFlat.Entities.Comments
{
    using ExhibFlat.Components.Validation.Validators;
    using System;
    using System.Runtime.CompilerServices;

    public class LeaveCommentReplyInfo
    {
        public long LeaveId { get; set; }

        [NotNullValidator(Ruleset="ValLeaveCommentReply", MessageTemplate="回复内容不能为空")]
        public string ReplyContent { get; set; }

        public DateTime ReplyDate { get; set; }

        public long ReplyId { get; set; }

        public int UserId { get; set; }
    }
}

