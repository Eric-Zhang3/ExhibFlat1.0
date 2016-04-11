using ExhibFlat.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExhibFlat.Entities.Members
{
    public class ChanelQueryParm : Pagination
    {
        public string UserName { get; set; }

        public string RealName { get; set; }

        public int? UserID { get; set; }

        public string Province { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string Region { get; set; }

        public string RequertCode { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int? ChannelID { get; set; }    //如果ChannelId小于0， 就是管理员

        public string Dstatus { get; set; }

    }
}
