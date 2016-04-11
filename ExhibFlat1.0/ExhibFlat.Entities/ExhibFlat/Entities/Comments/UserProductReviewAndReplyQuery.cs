namespace ExhibFlat.Entities.Comments
{
    using ExhibFlat.Core.Entities;
    using System;
    using System.Runtime.CompilerServices;

    public class UserProductReviewAndReplyQuery : Pagination
    {
        public int ProductId { get; set; }
    }
}

