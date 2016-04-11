
namespace ExhibFlat.Entities.JSC
{
    using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

    [Serializable]
    public class JSC_OperatorOrder
    {
        /// <summary>
        /// 活动编号
        /// </summary>
            public string JSC_CODE { get; set; }

            public string ProductName { get; set; }

            public string SKU { get; set; }

            public int? ProductID { get; set; }

            public string OrderId { set; get; }

            public string OrderDateStr { get; set; }
            public DateTime OrderDate { get; set; }

            public string Realname { get; set; }

            /// <summary>
            /// 分销商
            /// </summary>
            public string Username { get; set; }

            public decimal CostPrice { get; set; }

            public int Quantity { get; set; }

            public decimal? PayCharge { get; set; }


            public decimal? OrderTotal { get; set; }        //实付款

        /// <summary>
        /// 收货地址
        /// </summary>
            public string Address { get; set; }

            public string TelPhone { get; set; }

        /// <summary>
        /// 快递单号
        /// </summary>
            public string ShipOrderNumber { get; set; }

        /// <summary>
        /// 配送方
        /// </summary>
            public int? JSC_Delivery { get; set; }

        /// <summary>
        /// 配送方文本
        /// </summary>
            public string DeliveryStr { get; set; }

            public int? ProductProgress { get; set; }

            public int? ProductSum { get; set; }

            public int? CreateByID { get; set; }
            /// <summary>
            /// 快递公司编号
            /// </summary>
            public string ExpressCompanyAbb { get; set; }

        /// <summary>
        /// 快递公司
        /// </summary>
            public string ExpressCompanyName { get; set; }

            /// <summary>
            /// 违约状态
            /// </summary>
            public int? BreachStatus { get; set; }

        /// <summary>
        /// 违约状态文本
        /// </summary>
            public string BreachStatusStr { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
            public int? ActivityStatus { get; set; }

        /// <summary>
        /// 订单状态文本
        /// </summary>
            public string ActivityStatusStr { get; set; }

            /// <summary>
            /// 供应商ID
            /// </summary>
            public string SupplierUserID { get; set; }
        /// <summary>
        /// 供应商名称
        /// </summary>
            public string SupplierCompanyName { get; set; }

         public int Userid { get; set; }
        
    }

    /// <summary>
    /// 查询订单列表模型-运营商
    /// </summary>
    [Serializable]
    public class JSCOperatorQuery
    {
        /// <summary>
        /// 收货人
        /// </summary>
        public string Realname { get; set; }

        public string DeliveryStr { get; set; }

        public List<int?> ShippingSatusList { get; set; }

        public string ActivityStr { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 违约状态
        /// </summary>
        public int? BreachStatus { get; set; }

        
        /// <summary>
        /// 分销商
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 收货地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public int? ActivityStatus { get; set; }

        public bool ActStaAll { set; get; }
        public int CreateByID { get; set; }


        /// <summary>
        /// 供应商ID
        /// </summary>
        public string SupplierUserID { get; set; }
        /// <summary>
        /// 供应商/填写人
        /// </summary>
        public string SupplierName { get; set; }

        public string SupplierID { get; set; }

        /// <summary>
        /// 配送方
        /// </summary>
        public int? JSC_Delivery { get; set; }

        /// <summary>
        /// 快递公司
        /// </summary>
        public string ExpressCompanyName { get; set; }

        /// <summary>
        /// 快递公司100Code
        /// </summary>
        public string ExpressCompanyAbb { get; set; }

        /// <summary>
        /// 快递单号
        /// </summary>
        public string ShipOrderNumber { get; set; }

        /// <summary>
        /// 活动编号
        /// </summary>
        public string JSC_CODE { get; set; }

        public List<int?> Statuslist { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        /// <summary>
        /// 供应商名称
        /// </summary>
        public string SupplierCompanyName { get; set; }

        #region 页面属性
        /// <summary>
        /// 页序
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 页显示条数
        /// </summary>
        public int PageSize { get; set; }
        #endregion

    }

    [Serializable]
    public class JSC_OrderFillinExpress
    {
        public string OrderId { set; get; }

        /// <summary>
        /// 快递公司编号
        /// </summary>
        public string ExpressCompanyAbb { get; set; }

        public string ExpressCompanyName { get; set; }

        /// <summary>
        /// 快递单号
        /// </summary>
        public string ShipOrderNumber { get; set; }


    }
}
