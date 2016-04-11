using System;

namespace ExhibFlat.Entities.JSC
{
    /// <summary>
    /// 聚生产 报名、审核、上下线商品管理
    /// </summary>
    [Serializable]
    public class JSC_Activity
    {
        public JSC_Activity()
        {
        }

        #region Model

        private string _jsc_code;
        private string _supplierid;
        private string _skuid;
        private string _jsc_catid;
        private int? _activitystatus;
        private int? _productid;
        private string _productname;
        private int? _minproduction;
        private int? _maxproduction;
        private int? _productionphase;
        private int? _startorderquantity;
        private decimal? _jscprice;
        private int? _breachrate;
        private string _contact;
        private string _mobliephone;
        private string _telphone;
        private int? _activityphase;
        private string _addremarks;
        private int? _createdby;
        private string _createdbyname;
        private DateTime? _applicationtime;
        private DateTime? _approvedtime;
        private int? _approverid;
        private string _approvername;
        private DateTime? _activitystarttime;
        private DateTime? _activityendtime;
        private decimal? _cashdeposit;
        private decimal? _gotbreachsum;
        private decimal? _paybreachsum;
        private decimal? _orderpricesum;
        private int? _ordercountsum;
        private int? _countsum;
        private int? _closesum;
        private int? _cancelsum;
        private int? _productprogress;
        private string _productprogressdesc;
        private int? _productsum;
        private string _rejectreason;
        private decimal? _servicecharge;
        private decimal? _supplierbreach;
        private int? _supplierbreachrate;
        private string _paysn;
        private string _expsn;
        private DateTime? _paytime;
        private DateTime? _successtime;
        private DateTime? _abendtime;
        private string _abendreason;
        private decimal? _returndesposit;
        private string _failurereason;
        private int? _positionsort;
        private string _imagebig;
        private string _imagesmall;
        private string _memo;

        /// <summary>
        /// 
        /// </summary>
        public string JSC_CODE
        {
            set { _jsc_code = value; }
            get { return _jsc_code; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string SupplierID
        {
            set { _supplierid = value; }
            get { return _supplierid; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string SkuId
        {
            set { _skuid = value; }
            get { return _skuid; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string JSC_CATID
        {
            set { _jsc_catid = value; }
            get { return _jsc_catid; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int? ActivityStatus
        {
            set { _activitystatus = value; }
            get { return _activitystatus; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int? ProductID
        {
            set { _productid = value; }
            get { return _productid; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string ProductName
        {
            set { _productname = value; }
            get { return _productname; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int? MinProduction
        {
            set { _minproduction = value; }
            get { return _minproduction; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int? MaxProduction
        {
            set { _maxproduction = value; }
            get { return _maxproduction; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int? ProductionPhase
        {
            set { _productionphase = value; }
            get { return _productionphase; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int? StartOrderQuantity
        {
            set { _startorderquantity = value; }
            get { return _startorderquantity; }
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal? JSCPrice
        {
            set { _jscprice = value; }
            get { return _jscprice; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int? BreachRate
        {
            set { _breachrate = value; }
            get { return _breachrate; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Contact
        {
            set { _contact = value; }
            get { return _contact; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string MobliePhone
        {
            set { _mobliephone = value; }
            get { return _mobliephone; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Telphone
        {
            set { _telphone = value; }
            get { return _telphone; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int? ActivityPhase
        {
            set { _activityphase = value; }
            get { return _activityphase; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string AddRemarks
        {
            set { _addremarks = value; }
            get { return _addremarks; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int? CreatedBy
        {
            set { _createdby = value; }
            get { return _createdby; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string CreatedByName
        {
            set { _createdbyname = value; }
            get { return _createdbyname; }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? ApplicationTime
        {
            set { _applicationtime = value; }
            get { return _applicationtime; }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? ApprovedTime
        {
            set { _approvedtime = value; }
            get { return _approvedtime; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int? ApproverID
        {
            set { _approverid = value; }
            get { return _approverid; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string ApproverName
        {
            set { _approvername = value; }
            get { return _approvername; }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? ActivityStartTime
        {
            set { _activitystarttime = value; }
            get { return _activitystarttime; }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? ActivityEndTime
        {
            set { _activityendtime = value; }
            get { return _activityendtime; }
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal? CashDeposit
        {
            set { _cashdeposit = value; }
            get { return _cashdeposit; }
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal? GotBreachSum
        {
            set { _gotbreachsum = value; }
            get { return _gotbreachsum; }
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal? PayBreachSum
        {
            set { _paybreachsum = value; }
            get { return _paybreachsum; }
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal? OrderPriceSum
        {
            set { _orderpricesum = value; }
            get { return _orderpricesum; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int? OrderCountSum
        {
            set { _ordercountsum = value; }
            get { return _ordercountsum; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int? CountSum
        {
            set { _countsum = value; }
            get { return _countsum; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int? CloseSum
        {
            set { _closesum = value; }
            get { return _closesum; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int? CancelSum
        {
            set { _cancelsum = value; }
            get { return _cancelsum; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int? ProductProgress
        {
            set { _productprogress = value; }
            get { return _productprogress; }
        }

        /// <summary>
        /// 生产状态描述
        /// </summary>
        public string ProductProgressDesc
        {
            set { _productprogressdesc = value; }
            get { return _productprogressdesc; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int? ProductSum
        {
            set { _productsum = value; }
            get { return _productsum; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string RejectReason
        {
            set { _rejectreason = value; }
            get { return _rejectreason; }
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal? ServiceCharge
        {
            set { _servicecharge = value; }
            get { return _servicecharge; }
        }

        /// <summary>
        /// 活动审批时，运营方与供应商之间协商的违约金
        /// </summary>
        public decimal? SupplierBreach
        {
            set { _supplierbreach = value; }
            get { return _supplierbreach; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int? SupplierBreachRate
        {
            set { _supplierbreachrate = value; }
            get { return _supplierbreachrate; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string PaySN
        {
            set { _paysn = value; }
            get { return _paysn; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string ExpSN
        {
            set { _expsn = value; }
            get { return _expsn; }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? PayTime
        {
            set { _paytime = value; }
            get { return _paytime; }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? SuccessTime
        {
            set { _successtime = value; }
            get { return _successtime; }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? AbendTime
        {
            set { _abendtime = value; }
            get { return _abendtime; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string AbendReason
        {
            set { _abendreason = value; }
            get { return _abendreason; }
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal? ReturnDesposit
        {
            set { _returndesposit = value; }
            get { return _returndesposit; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string FailureReason
        {
            set { _failurereason = value; }
            get { return _failurereason; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int? PositionSort
        {
            set { _positionsort = value; }
            get { return _positionsort; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string ImageBig
        {
            set { _imagebig = value; }
            get { return _imagebig; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string ImageSmall
        {
            set { _imagesmall = value; }
            get { return _imagesmall; }
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