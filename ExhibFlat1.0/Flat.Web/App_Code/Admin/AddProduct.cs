namespace Flat.Web.Admin
{
    using ExhibFlat.ControlPanel.Commodities;
    using ExhibFlat.Core;
    using ExhibFlat.Entities.Commodities;
    using ExhibFlat.Entities.Store;
    using ExhibFlat.SiteSet;
    using ExhibFlat.UI.Common.Controls;
    using ExhibFlat.Components.Validation;
    
    using kindeditor.Net;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Globalization;
    using System.Runtime.InteropServices;
    using System.Web;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    
    public class AddProduct : ProductBasePage
    {
        protected Button btnAdd;
        private int categoryId;
        protected KindeditorControl editDescription;
        protected Literal litCategoryName;
        protected HyperLink lnkEditCategory;
        protected RadioButton radInStock;
        protected RadioButton radOnSales;
        protected RadioButton radUnSales;
        protected Script Script1;
        protected Script Script2;
        protected TrimTextBox txtProductName;
        protected TrimTextBox txtMarketPrice;
        protected TrimTextBox txtSubhead;
        protected TrimTextBox txtProBarCode;
        protected TrimTextBox txtProductShortName;
        protected TrimTextBox txtProductCode;
        protected ImageUploader uploader1;
        protected ImageUploader uploader2;
        protected ImageUploader uploader3;
        protected ImageUploader uploader4;
        protected ImageUploader uploader5;
        protected TrimTextBox txtShortDescription;

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int num;
            int num2;
            int num3;
            decimal num4;
            decimal num5;
            decimal num6;
            decimal? nullable;
            decimal? nullable2;
            int? nullable3;
            decimal nullable4;
            if (this.ValidateConverts(out num4, out num5, out num6, out nullable,
                out nullable2, out num, out num2, out nullable3, out num3, out nullable4))
            {
                
                string text = this.editDescription.Text;
                text=base.DownRemotePic(text);

                
                ProductInfo target = new ProductInfo
                {
                    CategoryId = this.categoryId,
                    ProductName = this.txtProductName.Text,
                    ProductCode = this.txtProductCode.Text,
                    //15.09.20
                    ProductSubName=this.txtSubhead.Text,
                    ProductShortName=this.txtProductShortName.Text,
                    ProductBarCode = this.txtProBarCode.Text,
                    
                    LineId = num3,
                    LowestSalePrice = num5,
                    MarketPrice = nullable2,
                    OriginalPrice = nullable4,
                    ImageUrl1 = this.uploader1.UploadedImageUrl,
                    ImageUrl2 = this.uploader2.UploadedImageUrl,
                    ImageUrl3 = this.uploader3.UploadedImageUrl,
                    ImageUrl4 = this.uploader4.UploadedImageUrl,
                    ImageUrl5 = this.uploader5.UploadedImageUrl,
                    ThumbnailUrl40 = this.uploader1.ThumbnailUrl40,
                    ThumbnailUrl60 = this.uploader1.ThumbnailUrl60,
                    ThumbnailUrl100 = this.uploader1.ThumbnailUrl100,
                    ThumbnailUrl160 = this.uploader1.ThumbnailUrl160,
                    ThumbnailUrl180 = this.uploader1.ThumbnailUrl180,
                    ThumbnailUrl220 = this.uploader1.ThumbnailUrl220,
                    ThumbnailUrl310 = this.uploader1.ThumbnailUrl310,
                    ThumbnailUrl410 = this.uploader1.ThumbnailUrl410,
                    ShortDescription = this.txtShortDescription.Text,
                    Description = (!string.IsNullOrEmpty(text) && (text.Length > 0)) ? text : null,
                    
                    AddedDate = DateTime.Now,
                    MainCategoryPath = ProductHelper.GetCategory(this.categoryId).Path + "|",
                    
                    
                };

                ProductSaleStatus onSale = ProductSaleStatus.OnStock;
                if (this.radInStock.Checked)
                {
                    onSale = ProductSaleStatus.OnStock;
                }
                if (this.radUnSales.Checked)
                {
                    onSale = ProductSaleStatus.UnSale;
                }

                target.SaleStatus = onSale;
                
                ValidationResults validateResults = ExhibFlat.Components.Validation.Validation.Validate<ProductInfo>(
                    target, new string[] {"AddProduct"});
                if (!validateResults.IsValid)
                {
                    this.ShowMsg(validateResults);
                }
                else
                {
                    
                    switch (ProductHelper.AddProduct(target))
                    {
                        case ProductActionStatus.Success:
                            this.ShowMsg("添加商品成功", true);
                            base.Response.Redirect(
                                Globals.GetAdminAbsolutePath(
                                    string.Format("/product/AddProductComplete.aspx?categoryId={0}&productId={1}",
                                        this.categoryId, target.ProductId)), true);
                            return;

                        case ProductActionStatus.AttributeError:
                            this.ShowMsg("添加商品失败，保存商品属性时出错", false);
                            return;

                        case ProductActionStatus.DuplicateName:
                            this.ShowMsg("添加商品失败，商品名称或商家编码不能重复", false);
                            return;

                        case ProductActionStatus.DuplicateSKU:
                            this.ShowMsg("添加商品失败，SKU编码不能重复", false);
                            return;

                        case ProductActionStatus.SKUError:
                            this.ShowMsg("添加商品失败，SKU编码不能重复", false);
                            return;

                        case ProductActionStatus.ProductTagEroor:
                            this.ShowMsg("添加商品失败，保存商品标签时出错", false);
                            return;
                    }
                    this.ShowMsg("添加商品失败，未知错误", false);
                }
            }
        }

        protected override void OnInitComplete(EventArgs e)
        {
            base.OnInitComplete(e);
            this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
            bool a = this.Page.IsPostBack;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!string.IsNullOrEmpty(base.Request.QueryString["isCallback"]) &&
                (base.Request.QueryString["isCallback"] == "true"))
            {
                base.DoCallback();
            }
            else if (!int.TryParse(base.Request.QueryString["categoryId"], out this.categoryId))
            {
                base.GotoResourceNotFound();
            }
            else if (!this.Page.IsPostBack)
            {
                //this.txtGeneralCode.Text = ProductHelper.GenerateProductAutoCode();

                this.litCategoryName.Text = ProductHelper.GetFullCategory(this.categoryId);
                CategoryInfo category = ProductHelper.GetCategory(this.categoryId);
                if (category == null)
                {
                    base.GotoResourceNotFound();
                }
                else
                {
                    this.lnkEditCategory.NavigateUrl = "SelectCategory.aspx?categoryId=" +
                                                       this.categoryId.ToString(CultureInfo.InvariantCulture);
                    this.txtProductCode.Text =ProductHelper.GetCodeByProductCode(category.SKUPrefix,
                                new Random(DateTime.Now.Millisecond).Next(1, 0x1869f));
                    
                }
            }
        }

        private bool ValidateConverts(out decimal purchasePrice, out decimal lowestSalePrice,
            out decimal salePrice, out decimal? costPrice, out decimal? marketPrice, out int stock, out int alertStock,
            out int? weight, out int lineId, out decimal originalPrice)
        {
            int num4;
            decimal num6;
            string str = string.Empty;
            costPrice = 0;
            marketPrice = 0;
            originalPrice = 0;
            weight = 0;
            lineId = num4 = 0;
            stock = alertStock = num4;
            salePrice = num6 = 0M;
            purchasePrice = lowestSalePrice = num6;
            
            if (this.txtProductName.Text.Trim() == "")
            {
                str = str + Formatter.FormatErrorMessage("商品名称不能为空");
            }
            if (this.txtProductShortName.Text.Trim() == "")
            {
                str = str + Formatter.FormatErrorMessage("商品简称不能为空");
            }
            if (this.txtProductCode.Text.Length > 20)
            {
                str = str + Formatter.FormatErrorMessage("商家编码的长度不能超过20个字符");
            }
            if (!string.IsNullOrEmpty(this.txtMarketPrice.Text))
            {
                decimal num;
                if (decimal.TryParse(this.txtMarketPrice.Text, out num))
                {
                    marketPrice = new decimal?(num);
                }
                else
                {
                    str = str + Formatter.FormatErrorMessage("请正确填写商品的市场价");
                }
            }
            else
            {
                str = str + Formatter.FormatErrorMessage("请填写商品的市场价");
            }
            if (!string.IsNullOrEmpty(str))
            {
                this.ShowMsg(str, false);
                return false;
            }
            return true;
        }
    }
}