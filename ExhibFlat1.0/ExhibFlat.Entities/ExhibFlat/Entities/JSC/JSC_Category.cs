using System;

namespace ExhibFlat.Entities.JSC
{
    /// <summary>
    ///     聚生产 报名、审核、上下线商品管理
    /// </summary>
    [Serializable]
    public class JSC_Category
    {
        #region Model

        private string _catname;
        private int _jsc_catid;

        /// <summary>
        /// </summary>
        public int JSC_CATID
        {
            set { _jsc_catid = value; }
            get { return _jsc_catid; }
        }

        /// <summary>
        /// </summary>
        public string CATName
        {
            set { _catname = value; }
            get { return _catname; }
        }

        #endregion Model
    }
}