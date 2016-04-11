namespace Flat.Web.Admin
{
    using ExhibFlat.ControlPanel.Commodities;
    using ExhibFlat.Core;
    using ExhibFlat.Entities.Commodities;
    using ExhibFlat.Entities.Store;
    using ExhibFlat.UI.Common.Controls;
    using ExhibFlat.UI.ControlPanel.Utility;
    using Hishop.Components.Validation;
    using kindeditor.Net;
    using System;
    using System.Collections.Generic;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class AddCategory : AdminPage
    {
        protected Button btnSaveAddCategory;
        protected Button btnSaveCategory;
        protected ProductCategoriesDropDownList dropCategories;
        protected ProductTypeDownList dropProductTypes;
        protected KindeditorControl fckNotes1;
        protected KindeditorControl fckNotes2;
        protected KindeditorControl fckNotes3;
        protected HtmlGenericControl liURL;
        protected TextBox txtCategoryName;
        protected TextBox TerCateName;
        protected TextBox txtPageDesc;
        protected TextBox txtPageKeyTitle;
        protected TextBox txtPageKeyWords;
        protected TextBox txtRewriteName;
        protected TextBox txtSKUPrefix;
        protected CheckBox IsSuggest;
        protected ImageUploader TerCateImage;


        private void btnSaveAddCategory_Click(object sender, EventArgs e)
        {
            CategoryInfo category = this.GetCategory();
            if (category != null)
            {
                if (ProductHelper.AddCategory(category) == CategoryActionStatus.Success)
                {
                    
                    this.ShowMsg("成功添加了商品类目", true);
                    this.dropCategories.DataBind();
                    this.dropProductTypes.DataBind();
                    this.TerCateName.Text = string.Empty;
                    this.TerCateImage.UploadedImageUrl = string.Empty;
                    this.IsSuggest.Checked  = false;
                    this.txtCategoryName.Text = string.Empty;
                    this.txtSKUPrefix.Text = string.Empty;
                    this.txtRewriteName.Text = string.Empty;
                    this.txtPageKeyTitle.Text = string.Empty;
                    this.txtPageKeyWords.Text = string.Empty;
                    this.txtPageDesc.Text = string.Empty;
                    this.fckNotes1.Text = string.Empty;
                    this.fckNotes2.Text = string.Empty;
                    this.fckNotes3.Text = string.Empty;
                }
                else
                {
                    this.ShowMsg("添加商品类目失败,未知错误", false);
                }
            }
        }

        private void btnSaveCategory_Click(object sender, EventArgs e)
        {
            CategoryInfo category = this.GetCategory();
            if (category != null)
            {
                if (ProductHelper.AddCategory(category) == CategoryActionStatus.Success)
                {
                    
                    base.Response.Redirect(Globals.GetAdminAbsolutePath("/product/ManageCategories.aspx"), true);
                }
                else
                {
                    this.ShowMsg("添加商品类目失败,未知错误", false);
                }
            }
        }

        private CategoryInfo GetCategory()
        {
            string oldpath;
            CategoryInfo target = new CategoryInfo {
                Name = this.txtCategoryName.Text.Trim(),
                ParentCategoryId = this.dropCategories.SelectedValue,
                SKUPrefix = this.txtSKUPrefix.Text.Trim(),
                AssociatedProductType = this.dropProductTypes.SelectedValue
            };
            if (target.ParentCategoryId == null)
                target.ParentCategoryId = 1;
            if (!string.IsNullOrEmpty(this.txtRewriteName.Text.Trim()))
            {
                target.RewriteName = this.txtRewriteName.Text.Trim();
            }
            else
            {
                target.RewriteName = null;
            }
            string IMAGEURL = System.Configuration.ConfigurationManager.AppSettings["ImageUrl"].ToString().TrimEnd('/');
            target.TerCateName = this.TerCateName.Text.Trim();
            target.issuggest = this.IsSuggest.Checked;
            oldpath = target.Path;
            target.TerCateImage = this.TerCateImage.UploadedImageUrl;
            target.MetaTitle = this.txtPageKeyTitle.Text.Trim();
            target.MetaKeywords = this.txtPageKeyWords.Text.Trim();
            target.MetaDescription = this.txtPageDesc.Text.Trim();
            target.Notes1 = this.fckNotes1.Text;
            target.Notes2 = this.fckNotes2.Text;
            target.Notes3 = this.fckNotes3.Text;
            target.DisplaySequence = 1;
            if (target.ParentCategoryId.HasValue)
            {
                CategoryInfo category = ProductHelper.GetCategory(target.ParentCategoryId.Value);
                if ((category == null) || (category.Depth >= 5))
                {
                    this.ShowMsg(string.Format("您选择的上级分类有误，商品类目最多只支持{0}级分类", 5), false);
                    return null;
                }
                if (string.IsNullOrEmpty(target.Notes1))
                {
                    target.Notes1 = category.Notes1;
                }
                if (string.IsNullOrEmpty(target.Notes2))
                {
                    target.Notes2 = category.Notes2;
                }
                if (string.IsNullOrEmpty(target.Notes3))
                {
                    target.Notes3 = category.Notes3;
                }
                if (string.IsNullOrEmpty(target.RewriteName))
                {
                    target.RewriteName = category.RewriteName;
                }
            }
            ValidationResults results = Hishop.Components.Validation.Validation.Validate<CategoryInfo>(target, new string[] { "ValCategory" });
            string msg = string.Empty;
            if (results.IsValid)
            {
                return target;
            }
            foreach (ValidationResult result in (IEnumerable<ValidationResult>) results)
            {
                msg = msg + Formatter.FormatErrorMessage(result.Message);
            }
            this.ShowMsg(msg, false);
            return null;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.btnSaveCategory.Click += new EventHandler(this.btnSaveCategory_Click);
            this.btnSaveAddCategory.Click += new EventHandler(this.btnSaveAddCategory_Click);
            if (!string.IsNullOrEmpty(base.Request["isCallback"]) && (base.Request["isCallback"] == "true"))
            {
                int result = 0;
                int.TryParse(base.Request["parentCategoryId"], out result);
                CategoryInfo category = ProductHelper.GetCategory(result);
                if (category != null)
                {
                    base.Response.Clear();
                    base.Response.ContentType = "application/json";
                    base.Response.Write("{ ");
                    base.Response.Write(string.Format("\"SKUPrefix\":\"{0}\"", category.SKUPrefix));
                    base.Response.Write("}");
                    base.Response.End();
                }
            }
            if (!this.Page.IsPostBack)
            {
                this.dropCategories.DataBind();
                this.dropProductTypes.DataBind();
            }
        }
    }
}

