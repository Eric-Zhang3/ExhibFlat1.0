using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExhibFlat.Entities
{
    public class OrderWarehouseInfo
    {
        
        public string OrderId{get;set;}
        public int                  OrderStatusInt{get;set;}
        public DateTime                  OrderDate{get;set;}
        public DateTime? ShippingDate { get; set; }
        public string OrderDateStr { get; set; }
        public int                  UserId{get;set;}
        public string                  Username{get;set;}
        public string                  RealName{get;set;}
        public string                  ShipTo{get;set;}
        public string                  Address{get;set;}
        public string                  ShippingRegion{get;set;}
        public string                  ZipCode{get;set;}
        public string                  TelPhone{get;set;}
        public string                  CellPhone{get;set;}
        public decimal                  Amount{get;set;}

        public string Province { get; set; }

        public string City { get; set; }

        public string Region { get; set; }
        public decimal? Freight { get; set; }
        public DateTime LastUpdateTime { get; set; }

        public string LastUpdateTimeStr { get; set; }

        public DateTime? FinishDate { get; set; }

        public string FinishDateStr { get; set; }
        public string OrderTypeStr { get; set; }

        public string ExpressCompanyName { get; set; }

        public string ExpressCompanyAbb { get; set; }

        public string InvoiceTitle { get; set; }

        public string Remark { get; set; }
    }

    public class OrderWarehouseDetailInfo
    {

        public string OrderId { get; set; }
        public string SKU { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string SKUContent { get; set; }
        public string ItemDescription { get; set; }
        public decimal ItemListPrice { get; set; }
        public decimal CostPrice { get; set; }
        public decimal ItemAdjustedPrice { get; set; }

    }

    public class UpdateOrderModel
    {
        public string OrderId { get; set; }

        public int OrderStatusInt { get; set; }

        public string SatusComent { get; set; }

        public string LastUpdateTime { get; set; }
    }

    public class UpdateExpressModel
    {
        public string OrderId { get; set; }

        public string ExpressCompanyAbb { set; get; }

        public string ExpressCompanyName { get; set; }

        public string ShipOrderNumber { set; get; }

        public DateTime LastUpdateTime { get; set; }
    }
}
