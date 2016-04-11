using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Hidistro.Entities.Sales;

namespace Hidistro.UI.Web.Filter
{
    public class OrderComp : System.Collections.Generic.IComparer<OrderInfo>
    {


        public int Compare(OrderInfo x, OrderInfo y)
        {
            int i = (x.OrderDate - y.OrderDate).Milliseconds;
            if (i > 0)
            {
                return 1;
            }
            if (i < 0)
            {
                return -1;
            }

            return 0;
        }
    }
}