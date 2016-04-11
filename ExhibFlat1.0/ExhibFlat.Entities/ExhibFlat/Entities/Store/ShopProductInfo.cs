using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExhibFlat.Entities.Store
{
    public class ShopProductInfo
    {
        public int ShopID{get;set;}
        public int ProductID { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreatedBy { get; set; }
    }
}
