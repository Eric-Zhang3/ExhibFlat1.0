

namespace ExhibFlat.Entities.Store
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    public class DistriShopInfo
    {
        public string ShopName { get; set; }
        public string ShopId { get; set; }
        public string DimensionCode { get; set; }
        public string ShopImg { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreatedBy { get; set; }
    }
}
