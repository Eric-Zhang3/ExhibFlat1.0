using System;

namespace ExhibFlat.Entities.JSC
{
    /// <summary>
    ///     聚生产 报名、审核、上下线商品管理
    /// </summary>
    [Serializable]
    public class JSC_Dictionary
    {
        #region Model

        private int? _biztype;
        private int? _dictcode;
        private int _dictid;
        private string _dictname;
        private string _memo;

        /// <summary>
        /// </summary>
        public int DictID
        {
            set { _dictid = value; }
            get { return _dictid; }
        }

        /// <summary>
        /// </summary>
        public int? DictCode
        {
            set { _dictcode = value; }
            get { return _dictcode; }
        }

        /// <summary>
        /// </summary>
        public string DictName
        {
            set { _dictname = value; }
            get { return _dictname; }
        }

        /// <summary>
        /// </summary>
        public int? BizType
        {
            set { _biztype = value; }
            get { return _biztype; }
        }

        /// <summary>
        /// </summary>
        public string Memo
        {
            set { _memo = value; }
            get { return _memo; }
        }

        #endregion Model
    }
}