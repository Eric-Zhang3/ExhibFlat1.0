using System;

namespace ExhibFlat.Entities.JSC
{
    /// <summary>
    /// 违约表
    /// </summary>
    [Serializable]
    public class JSC_Breach
    {
        public JSC_Breach()
        {
        }

        #region Model

        private string _breachcode;
        private string _orderid;
        private string _jsc_code;
        private int? _breachuser;
        private string _breachusername;
        private int? _victimsuser;
        private string _victimsusername;
        private decimal? _breachamount;
        private decimal? _actualamount;
        private int? _breachstatus;
        private string _breachreason;
        private DateTime? _createddate;
        private int? _createduser;
        private string _createdusername;
        private string _memo;

        /// <summary>
        /// 违约编号
        /// </summary>
        public string BreachCode
        {
            set { _breachcode = value; }
            get { return _breachcode; }
        }

        /// <summary>
        /// OrderId
        /// </summary>
        public string OrderId
        {
            set { _orderid = value; }
            get { return _orderid; }
        }

        /// <summary>
        /// 活动编码
        /// </summary>
        public string JSC_CODE
        {
            set { _jsc_code = value; }
            get { return _jsc_code; }
        }

        /// <summary>
        /// 违约人
        /// </summary>
        public int? BreachUser
        {
            set { _breachuser = value; }
            get { return _breachuser; }
        }

        /// <summary>
        /// 违约人名称
        /// </summary>
        public string BreachUserName
        {
            set { _breachusername = value; }
            get { return _breachusername; }
        }

        /// <summary>
        /// 被违约人
        /// </summary>
        public int? VictimsUser
        {
            set { _victimsuser = value; }
            get { return _victimsuser; }
        }

        /// <summary>
        /// 被违约人名称
        /// </summary>
        public string VictimsUserName
        {
            set { _victimsusername = value; }
            get { return _victimsusername; }
        }

        /// <summary>
        /// 违约金额
        /// </summary>
        public decimal? BreachAmount
        {
            set { _breachamount = value; }
            get { return _breachamount; }
        }

        /// <summary>
        /// 实际违约金额
        /// </summary>
        public decimal? ActualAmount
        {
            set { _actualamount = value; }
            get { return _actualamount; }
        }

        /// <summary>
        /// 违约状态
        /// </summary>
        public int? BreachStatus
        {
            set { _breachstatus = value; }
            get { return _breachstatus; }
        }

        /// <summary>
        /// 违约原因
        /// </summary>
        public string BreachReason
        {
            set { _breachreason = value; }
            get { return _breachreason; }
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreatedDate
        {
            set { _createddate = value; }
            get { return _createddate; }
        }

        /// <summary>
        /// 创建人
        /// </summary>
        public int? CreatedUser
        {
            set { _createduser = value; }
            get { return _createduser; }
        }

        /// <summary>
        /// 创建人名称
        /// </summary>
        public string CreatedUserName
        {
            set { _createdusername = value; }
            get { return _createdusername; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Memo
        {
            set { _memo = value; }
            get { return _memo; }
        }

        #endregion Model
    }
}
