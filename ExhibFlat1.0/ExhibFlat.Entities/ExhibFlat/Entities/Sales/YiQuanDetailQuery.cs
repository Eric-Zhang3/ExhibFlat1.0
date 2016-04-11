using ExhibFlat.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExhibFlat.Entities.Sales
{

    public class YiQuanDetailQuery : Pagination
    {
        public DateTime? EndDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? OrderDate { get; set; }
        public string Purpose { get; set; }
        public int Status { get; set; } 
    }
}
