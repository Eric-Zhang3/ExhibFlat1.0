namespace ExhibFlat.Entities.Comments
{
    using ExhibFlat.Core.Entities;
    using System;
    using System.Runtime.CompilerServices;

    public class MessageBoxQuery : Pagination
    {
        public string Accepter { get; set; }

        public ExhibFlat.Entities.Comments.MessageStatus MessageStatus { get; set; }

        public string Sernder { get; set; }
    }
}

