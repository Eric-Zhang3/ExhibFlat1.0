using ExhibFlat.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExhibFlat.Entities.Sales
{

    public class AssignQuery : Pagination
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int AssignVaule { get; set; }
        public string ToId { get; set; }
        public int RecordId { get; set; }
    }
}
