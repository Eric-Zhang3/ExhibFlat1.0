using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExhibFlat.Entities
{
    public class StockInfos
    {
        public string result { get; set; }

        public List<Stocks> data { get; set; }
    }

    public class Stocks
    {
        public string skuCode { get; set; }

        public List<Stock> stock { get; set; }
    }

    public class Stock
    {
        public string whseId { get; set; }

        public string stocks { get; set; }
    }

    public class Attributemodel
    {
        public int attrID { get; set; }

        public int attrValue { get; set; }
    }
}
