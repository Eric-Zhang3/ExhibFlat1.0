using ExhibFlat.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExhibFlat.Entities.Sales
{

    public class YiQuanQuery : Pagination
    {
        public DateTime? EndDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? OrderDate { get; set; }
        public string OrderId { get; set; }
        public int Status { get; set; } 
    }
}
