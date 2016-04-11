namespace ExhibFlat.UI.Common.Controls
{
    using ExhibFlat.ControlPanel.Commodities;
    using ExhibFlat.Core;
    using ExhibFlat.Entities.Commodities;
    using System;
    using System.Data;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Web.UI.WebControls;

    public class ProductCategoriesDropDownList : DropDownList
    {
        private bool isTopCategory;
        private bool m_AutoDataBind;
        private string m_NullToDisplay = "";
        private string strDepth = "　　";

        public override void DataBind()
        {
            this.Items.Clear();
            this.Items.Add(new ListItem(this.NullToDisplay, string.Empty));
            if (this.IsUnclassified)
            {
                this.Items.Add(new ListItem("全部分类商品", "0"));
            }
            if (this.IsTopCategory)
            {
                foreach (CategoryInfo info in ProductHelper.GetMainCategories())
                {
                    this.Items.Add(new ListItem(Globals.HtmlDecode(info.Name), info.CategoryId.ToString()));
                }
            }
            else
            {
                DataTable categories = ProductHelper.GetNewtable();
                if (categories != null) { 
                    for (int i = 0; i < categories.Rows.Count; i++)
                    {
                        int num3 = (int) categories.Rows[i]["CategoryId"];
                        this.Items.Add(new ListItem(this.FormatDepth((int) categories.Rows[i]["Depth"], Globals.HtmlDecode((string) categories.Rows[i]["Name"])), num3.ToString(CultureInfo.InvariantCulture)));
                    }
                }
            }
        }

        private string FormatDepth(int depth, string categoryName)
        {
            for (int i = 1; i < depth; i++)
            {
                categoryName = this.strDepth + categoryName;
            }
            return categoryName;
        }

        protected override void OnLoad(EventArgs e)
        {
            if (this.AutoDataBind && !this.Page.IsPostBack)
            {
                this.DataBind();
            }
        }

        public bool AutoDataBind
        {
            get
            {
                return this.m_AutoDataBind;
            }
            set
            {
                this.m_AutoDataBind = value;
            }
        }

        public bool IsTopCategory
        {
            get
            {
                return this.isTopCategory;
            }
            set
            {
                this.isTopCategory = value;
            }
        }

        public bool IsUnclassified { get; set; }

        public string NullToDisplay
        {
            get
            {
                return this.m_NullToDisplay;
            }
            set
            {
                this.m_NullToDisplay = value;
            }
        }

        public int? SelectedValue
        {
            get
            {
                if (!string.IsNullOrEmpty(base.SelectedValue))
                {
                    return new int?(int.Parse(base.SelectedValue, CultureInfo.InvariantCulture));
                }
                return null;
            }
            set
            {
                if (value.HasValue)
                {
                    base.SelectedIndex = base.Items.IndexOf(base.Items.FindByValue(value.Value.ToString(CultureInfo.InvariantCulture)));
                }
                else
                {
                    base.SelectedIndex = -1;
                }
            }
        }
    }
}

