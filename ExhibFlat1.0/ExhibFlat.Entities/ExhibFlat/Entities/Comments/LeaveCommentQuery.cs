namespace ExhibFlat.Entities.Comments
{
    using ExhibFlat.Core.Entities;
    using System;
    using System.Runtime.CompilerServices;

    public class LeaveCommentQuery : Pagination
    {
        public int? AgentId { get; set; }

        public ExhibFlat.Entities.Comments.MessageStatus MessageStatus { get; set; }
    }
}

