using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExhibFlat.Entities.Refund
{
    public class RefundAmountListInput
    {
        /// <summary>
        /// 流水号
        /// </summary>
        public string SerialNumber { get; set; }
        /// <summary>
        /// 退款单号
        /// </summary>
        public string RefundNO { get; set; }
        /// <summary>
        /// 退款摘要
        /// </summary>
        public string RefundDesc { get; set; }
        /// <summary>
        /// 退款类型 1.取消订单 2.退货退款 3.光退邮费
        /// </summary>
        public int RefundTypeID { get; set; }
        /// <summary>
        /// 收款人
        /// </summary>
        public int ReceiveUser { get; set; }
        /// <summary>
        /// 退款金额
        /// </summary>
        public decimal RefundAmount { get; set; }
        /// <summary>
        /// 退款创建人
        /// </summary>
        public int CreateUser { get; set; }
        /// <summary>
        /// 退款时间
        /// </summary>
        public DateTime RefundDate { get; set; }
        /// <summary>
        /// 相关单号
        /// </summary>
        public string OrderNO { get; set; }
        /// <summary>
        /// 退款方式 1.在线退款 2.线下退款
        /// </summary>
        public int RefundMode { get; set; }
        /// <summary>
        /// 退款状态 1.待处理2.退款成功3.退款失败
        /// </summary>
        public int RefundStatus { get; set; }
        /// <summary>
        /// 退货单状态
        /// </summary>
        public int RefundMainStatus { get; set; }
    }
}
