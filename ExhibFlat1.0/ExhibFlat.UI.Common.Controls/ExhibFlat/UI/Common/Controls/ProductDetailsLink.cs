namespace ExhibFlat.UI.Common.Controls
{
    using ExhibFlat.Core;
    using ExhibFlat.SiteSet;
    using System;
    using System.Runtime.CompilerServices;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class ProductDetailsLink : HyperLink
    {
        private bool imageLink;
        private bool isCountDownProduct;
        public bool isGroupBuyProduct;
        private bool isUn;
        public const string TagID = "ProductDetailsLink";

        public ProductDetailsLink()
        {
            base.ID = "ProductDetailsLink";
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if ((this.ProductId != null) && (this.ProductId != DBNull.Value))
            {
                if (this.isGroupBuyProduct)
                {
                    base.NavigateUrl = Globals.GetSiteUrls().UrlData.FormatUrl("groupBuyProductDetails", new object[] { this.ProductId });
                }
                else if (this.IsCountDownProduct)
                {
                    base.NavigateUrl = Globals.GetSiteUrls().UrlData.FormatUrl("countdownProductsDetails", new object[] { this.ProductId });
                }
                else if (this.isUn)
                {
                    base.NavigateUrl = Globals.GetSiteUrls().UrlData.FormatUrl("unproductdetails", new object[] { this.ProductId });
                }
                else if (HiContext.Current.Context.Request.RawUrl.ToLower().IndexOf("mpage") != -1)
                {
                    base.NavigateUrl = Globals.GetSiteUrls().UrlData.FormatUrl("productDetail_M", new object[] { this.ProductId });
                }
                else
                {
                    base.NavigateUrl = Globals.GetSiteUrls().UrlData.FormatUrl("productDetails", new object[] { this.ProductId });
                }
            }
            if ((!this.imageLink && (this.ProductId != null)) && (this.ProductId != DBNull.Value))
            {
                if (this.StringLenth.HasValue && (this.ProductName.ToString().Length > this.StringLenth.Value))
                {
                    base.Text = this.ProductName.ToString().Substring(0, this.StringLenth.Value) + "...";
                }
                else
                {
                    base.Text = this.ProductName.ToString();
                }
            }
            base.Target = "_blank";
            base.Render(writer);
        }

        public bool ImageLink
        {
            get
            {
                return this.imageLink;
            }
            set
            {
                this.imageLink = value;
            }
        }

        public bool IsCountDownProduct
        {
            get
            {
                return this.isCountDownProduct;
            }
            set
            {
                this.isCountDownProduct = value;
            }
        }

        public bool IsGroupBuyProduct
        {
            get
            {
                return this.isGroupBuyProduct;
            }
            set
            {
                this.isGroupBuyProduct = value;
            }
        }

        public bool IsUn
        {
            get
            {
                return this.isUn;
            }
            set
            {
                this.isUn = value;
            }
        }

        public object ProductId
        {
            get
            {
                return this.ViewState["ProductId"];
            }
            set
            {
                this.ViewState["ProductId"] = value;
            }
        }

        public object ProductName
        {
            get
            {
                return this.ViewState["ProductName"];
            }
            set
            {
                this.ViewState["ProductName"] = value;
            }
        }

        public int? StringLenth { get; set; }
    }
}

