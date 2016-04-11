using System;

namespace ExhibFlat.Entities.JSC
{
    /// <summary>
    ///     聚生产 报名、审核、上下线商品管理
    /// </summary>
    [Serializable]
    public class JSC_IntervalPrice
    {
        #region Model

        private string _jsc_code;
        private decimal? _price;
        private int? _smallquantity;

        /// <summary>
        /// </summary>
        public string JSC_CODE
        {
            set { _jsc_code = value; }
            get { return _jsc_code; }
        }

        /// <summary>
        ///     取价格方法： 跟据用户订单商品数量，按商品数量搜索本表 “订单商品数据>起批量”字段，取价格低的最小的为报价，如果未查到，则取主表的原聚单价为价格
        /// </summary>
        public int? SmallQuantity
        {
            set { _smallquantity = value; }
            get { return _smallquantity; }
        }

        /// <summary>
        /// </summary>
        public decimal? Price
        {
            set { _price = value; }
            get { return _price; }
        }

        #endregion Model
    }
}