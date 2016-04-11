using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExhibFlat.Entities
{
    public class SKUDistributorPrice
    {
        public string SKU { get; set; }

        public decimal Price { get; set; }
        public decimal? CostPrice { get; set; }//成本价
    }

    public class SKUDistributorPriceQuery
    {
        public int UserId { get; set; }

        public string SKU { get; set; }
    }

    public class ProductToWarehouseModel
    {
        public string nbr { get; set; }

        public string name { get; set; }

        public string code { get; set; }

        public string abbr { get; set; }

        public string vdr_id { get; set; }

        public string vdr_name { get; set; }

        public string ProductAutoCode { get; set; }

    }

    public class SkusToWarehouseModel
    {
        public string code { get; set; }

        public string name { get; set; }

        public string mfg_code { get; set; }

        public decimal? vdr_price { get; set; }

        public int AlertStock { get; set; }

        public string barcode1 { get; set; } //条形码

        public string barcode2 { get; set; }//国标码
        public decimal costprice { get; set; }//成本价
    }
}
