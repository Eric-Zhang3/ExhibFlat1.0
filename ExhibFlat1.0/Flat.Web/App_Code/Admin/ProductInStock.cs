
namespace Flat.Web.Admin
{
    using ASPNET.WebControls;
    using ExhibFlat.ControlPanel.Commodities;
    using ExhibFlat.Core;
    using ExhibFlat.Core.Entities;
    using ExhibFlat.Core.Enums;
    using ExhibFlat.Entities.Commodities;
    using ExhibFlat.Entities.Store;
    using ExhibFlat.UI.Common.Controls;
    using ExhibFlat.UI.ControlPanel.Utility;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using ExhibFlat.SiteSet;

    public class ProductInStock : AdminPage
    {
        protected TextBox txtSearchText;
        protected ProductCategoriesDropDownList dropCategories;
        protected ProductTypeDownList dropType;
        protected TextBox txtSKU;

        protected HiddenField hidStartDate;
        protected HiddenField hidEndDate;
        protected Button btnSearch;

        protected ImageLinkButton btnDelete;
        protected Button btnOK;
        protected Pager pager;
        protected Pager pager1;
        protected Grid grdProducts;

        protected Button btnStockPentrationStauts;

        private int? categoryId;
        protected CheckBox chkDeleteImage;
        protected CheckBox chkInstock;

        private DateTime? endDate;
        protected HtmlInputHidden hdPenetrationStatus;
        protected PageSize hrefPageSize;
        private int? lineId;

        private string productCode;
        private string productName;
        private DateTime? startDate;


        private int? typeId;
        private int? count; //一共多少数据

        private void BindProducts()
        {
            this.LoadParameters();
            ProductQuery entity = new ProductQuery
            {
                Keywords = this.productName,
                ProductCode = this.productCode,
                CategoryId = this.categoryId,
                ProductLineId = this.lineId,
                PageSize = this.pager.PageSize,
                PageIndex = this.pager.PageIndex,
                SaleStatus = ProductSaleStatus.OnStock,
                SortOrder = SortAction.Desc,
                SortBy = "DisplaySequence",
                StartDate = this.startDate,
                TypeId = this.typeId,
                EndDate = this.endDate
            };
            if (this.categoryId.HasValue)
            {
                entity.MaiCategoryPath = ProductHelper.GetCategory(this.categoryId.Value).Path;
            }
            Globals.EntityCoding(entity, true);
            DbQueryResult products = ProductHelper.GetProducts(entity);
            this.grdProducts.DataSource = products.Data;
            this.grdProducts.DataBind();
            this.txtSearchText.Text = entity.Keywords;
            this.txtSKU.Text = entity.ProductCode;
            this.dropCategories.SelectedValue = entity.CategoryId;
            this.dropType.SelectedValue = entity.TypeId;
            this.pager1.TotalRecords = this.pager.TotalRecords = products.TotalRecords;
            this.count = products.TotalRecords;
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            string str = base.Request.Form["CheckBoxGroup"];
            if (string.IsNullOrEmpty(str))
            {
                this.ShowMsg("请先选择要删除的商品", false);
            }
            else
            {
                List<int> productIds = new List<int>();

                int id;
                foreach (string str2 in str.Split(new char[] { ',' }))
                {
                    id = Convert.ToInt32(str2);
                    productIds.Add(id);
                }


                if (ProductHelper.CanclePenetrationProducts(productIds) >= 1)
                {
                    if (ProductHelper.RemoveProduct(str) > 0)
                    {
                        this.ShowMsg("成功删除了选择的商品", true);
                        this.BindProducts();
                        this.ReloadProductOnSales(false);
                    }
                    else
                    {
                        this.ShowMsg("删除商品失败，未知错误", false);
                    }
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string str = base.Request.Form["CheckBoxGroup"];
            if (string.IsNullOrEmpty(str))
            {
                this.ShowMsg("请先选择要下架的商品", false);
            }
            else
            {
                if (this.hdPenetrationStatus.Value.Equals("1"))
                {
                    List<int> productIds = new List<int>();
                    foreach (string str2 in str.Split(new char[] { ',' }))
                    {
                        productIds.Add(Convert.ToInt32(str2));
                    }
                    if (ProductHelper.CanclePenetrationProducts(productIds) == 0)
                    {
                        this.ShowMsg("取消铺货失败！", false);
                        return;
                    }
                }
                if (ProductHelper.OffShelf(str) > 0)
                {
                    this.ShowMsg("成功下架了选择的商品，您可以在下架区的商品里面找到下架以后的商品", true);
                    this.BindProducts();
                }
                else
                {
                    this.ShowMsg("下架商品失败，未知错误", false);
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.ReloadProductOnSales(true);
        }

        private void btnStockPentrationStauts_Click(object sender, EventArgs e)
        {
            string str = base.Request.Form["CheckBoxGroup"];
            if (string.IsNullOrEmpty(str))
            {
                this.ShowMsg("请先选择要入库的商品", false);
            }
            else
            {
                if (this.hdPenetrationStatus.Value.Equals("1"))
                {
                    List<int> productIds = new List<int>();
                    foreach (string str2 in str.Split(new char[] { ',' }))
                    {
                        productIds.Add(Convert.ToInt32(str2));
                    }
                    if (ProductHelper.CanclePenetrationProducts(productIds) == 0)
                    {
                        this.ShowMsg("取消铺货失败！", false);
                        return;
                    }
                }
                if (ProductHelper.InStock(str) > 0)
                {
                    this.ShowMsg("成功入库选择的商品，您可以在仓库区的商品里面找到入库以后的商品", true);
                    this.BindProducts();
                }
                else
                {
                    this.ShowMsg("入库商品失败，未知错误", false);
                }
            }
        }

        private void grdProducts_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            List<int> productIds = new List<int>();
            string str = this.grdProducts.DataKeys[e.RowIndex].Value.ToString();
            if (str != "")
            {
                int id = Convert.ToInt32(str);

                productIds.Add(id);
            }
            if ((ProductHelper.CanclePenetrationProducts(productIds) == 1) && (ProductHelper.RemoveProduct(str) > 0))
            {
                this.ShowMsg("删除商品成功", true);
                this.BindProducts();
                this.ReloadProductOnSales(false);
            }
        }

        private void LoadParameters()
        {
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["productName"]))
            {
                this.productName = Globals.UrlDecode(this.Page.Request.QueryString["productName"]);
            }
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["productCode"]))
            {
                this.productCode = Globals.UrlDecode(this.Page.Request.QueryString["productCode"]);
            }

            int result = 0;
            if (int.TryParse(this.Page.Request.QueryString["categoryId"], out result))
            {
                this.categoryId = new int?(result);
            }
            int num2 = 0;

            int num3 = 0;
            if (int.TryParse(this.Page.Request.QueryString["lineId"], out num3))
            {
                this.lineId = new int?(num3);
            }
            int num4 = 0;

            int num5 = 0;
            if (int.TryParse(this.Page.Request.QueryString["typeId"], out num5))
            {
                this.typeId = new int?(num5);
            }
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["startDate"]))
            {
                this.startDate = new DateTime?(DateTime.Parse(this.Page.Request.QueryString["startDate"]));
            }
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["endDate"]))
            {
                this.endDate = new DateTime?(DateTime.Parse(this.Page.Request.QueryString["endDate"]));
            }
            this.txtSearchText.Text = this.productName;
            this.txtSKU.Text = this.productCode;
            this.dropCategories.DataBind();
            this.dropCategories.SelectedValue = this.categoryId;
            this.hidStartDate.Value = this.startDate.HasValue ? this.startDate.Value.ToString("yyyy-MM-dd") : string.Empty;
            this.hidEndDate.Value = this.endDate.HasValue ? this.endDate.Value.ToString("yyyy-MM-dd") : string.Empty;
            //this.calendarStartDate.SelectedDate = this.startDate;
            //this.calendarEndDate.SelectedDate = this.endDate;
            this.dropType.SelectedValue = this.typeId;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.btnSearch.Click += new EventHandler(this.btnSearch_Click);
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnStockPentrationStauts.Click += new EventHandler(this.btnStockPentrationStauts_Click);
            this.grdProducts.RowDeleting += new GridViewDeleteEventHandler(this.grdProducts_RowDeleting);
            if (!this.Page.IsPostBack)
            {
                this.dropCategories.DataBind();
                this.dropType.DataBind();
                this.BindProducts();
            }
            CheckBoxColumn.RegisterClientCheckEvents(this.Page, this.Page.Form.ClientID);
        }

        private void ReloadProductOnSales(bool isSearch)
        {
            NameValueCollection queryStrings = new NameValueCollection();
            queryStrings.Add("productName", Globals.UrlEncode(this.txtSearchText.Text.Trim()));
            if (this.dropCategories.SelectedValue.HasValue)
            {
                queryStrings.Add("categoryId", this.dropCategories.SelectedValue.ToString());
            }
            queryStrings.Add("productCode", Globals.UrlEncode(Globals.HtmlEncode(this.txtSKU.Text.Trim())));
            queryStrings.Add("pageSize", this.pager.PageSize.ToString());
            if (!isSearch)
            {
                if (Convert.ToInt32(Convert.ToInt32(this.pager.PageSize) * Convert.ToInt32(this.pager.PageIndex) - 1) >
                    this.count)
                {
                    queryStrings.Add("pageIndex", (Convert.ToInt32(this.pager.PageIndex) - 1).ToString());
                }
                else
                {
                    queryStrings.Add("pageIndex", this.pager.PageIndex.ToString());
                }
            }
            if (!string.IsNullOrEmpty(this.hidStartDate.Value))
            {
                queryStrings.Add("startDate", this.hidStartDate.Value);
            }
            if (!string.IsNullOrEmpty(this.hidEndDate.Value))
            {
                queryStrings.Add("endDate", this.hidEndDate.Value);
            }
            if (this.dropType.SelectedValue.HasValue)
            {
                queryStrings.Add("typeId", this.dropType.SelectedValue.ToString());
            }
            base.ReloadPage(queryStrings);
        }
    }
}