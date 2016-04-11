using System;

namespace ExhibFlat.Entities.JSC
{
    /// <summary>
    /// 违约详情
    /// </summary>
    [Serializable]
    public class JSC_BreachDetail
    {
        public JSC_BreachDetail()
        {
        }

        #region Model

        private int _breachdetailid;
        private string _breachcode;
        private DateTime? _victimstime;
        private decimal? _applyamount;
        private string _victimscertificate;
        private string _victimsmemo;
        private DateTime? _breachtime;
        private string _breachcertificate;
        private string _breachmemo;
        private int? _appstatus;
        private DateTime? _createdate;

        /// <summary>
        /// 
        /// </summary>
        public int BreachDetailId
        {
            set { _breachdetailid = value; }
            get { return _breachdetailid; }
        }

        /// <summary>
        /// 违约编号
        /// </summary>
        public string BreachCode
        {
            set { _breachcode = value; }
            get { return _breachcode; }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? VictimsTime
        {
            set { _victimstime = value; }
            get { return _victimstime; }
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal? ApplyAmount
        {
            set { _applyamount = value; }
            get { return _applyamount; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string VictimsCertificate
        {
            set { _victimscertificate = value; }
            get { return _victimscertificate; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string VictimsMemo
        {
            set { _victimsmemo = value; }
            get { return _victimsmemo; }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? BreachTime
        {
            set { _breachtime = value; }
            get { return _breachtime; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string BreachCertificate
        {
            set { _breachcertificate = value; }
            get { return _breachcertificate; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string BreachMemo
        {
            set { _breachmemo = value; }
            get { return _breachmemo; }
        }

        /// <summary>
        /// 1.同意，0不同意
        /// </summary>
        public int? AppStatus
        {
            set { _appstatus = value; }
            get { return _appstatus; }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }

        #endregion Model
    }
}