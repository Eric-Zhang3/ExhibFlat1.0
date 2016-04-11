namespace ExhibFlat.Entities.Comments
{
    using System;
    using System.Runtime.CompilerServices;

    public class MessageBoxInfo
    {
        public string Accepter { get; set; }

        public string Content { get; set; }

        public long ContentId { get; set; }

        public DateTime Date { get; set; }

        public bool IsRead { get; set; }

        public long MessageId { get; set; }

        public string Sernder { get; set; }

        public string Title { get; set; }
    }
}

