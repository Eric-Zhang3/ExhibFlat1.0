using System;

namespace ExhibFlat.Entities.Refund
{
    /// <summary>
    /// JS_Refund:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class JS_Refund
    {
        public JS_Refund()
        {
        }

        #region Model

        private string _refundno;
        private string _dealno;
        private string _ordercode;
        private int? _refundsubjectid;
        private string _refundsubjectname;
        private DateTime? _createtime;
        private string _amountsum;
        private int? _refundpartysubjectid;
        private string _refundpartysubjectname;
        private decimal? _refundamount;
        private DateTime? _refundtime;
        private int? _refundstatus;
        private int? _refundtype;
        private string _refundmemo;
        private string _refundpartymsg;

        /// <summary>
        /// 退款编号
        /// </summary>
        public string RefundNo
        {
            set { _refundno = value; }
            get { return _refundno; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string DealNo
        {
            set { _dealno = value; }
            get { return _dealno; }
        }

        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderCode
        {
            set { _ordercode = value; }
            get { return _ordercode; }
        }

        /// <summary>
        /// 退款申请方
        /// </summary>
        public int? RefundSubjectID
        {
            set { _refundsubjectid = value; }
            get { return _refundsubjectid; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string RefundSubjectName
        {
            set { _refundsubjectname = value; }
            get { return _refundsubjectname; }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }

        /// <summary>
        /// 交易总额
        /// </summary>
        public string AmountSum
        {
            set { _amountsum = value; }
            get { return _amountsum; }
        }

        /// <summary>
        /// 退款方
        /// </summary>
        public int? RefundPartySubjectID
        {
            set { _refundpartysubjectid = value; }
            get { return _refundpartysubjectid; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string RefundPartySubjectName
        {
            set { _refundpartysubjectname = value; }
            get { return _refundpartysubjectname; }
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal? RefundAmount
        {
            set { _refundamount = value; }
            get { return _refundamount; }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? RefundTime
        {
            set { _refundtime = value; }
            get { return _refundtime; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int? RefundStatus
        {
            set { _refundstatus = value; }
            get { return _refundstatus; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int? RefundType
        {
            set { _refundtype = value; }
            get { return _refundtype; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string RefundMemo
        {
            set { _refundmemo = value; }
            get { return _refundmemo; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string RefundPartyMsg
        {
            set { _refundpartymsg = value; }
            get { return _refundpartymsg; }
        }

        #endregion Model
    }
}