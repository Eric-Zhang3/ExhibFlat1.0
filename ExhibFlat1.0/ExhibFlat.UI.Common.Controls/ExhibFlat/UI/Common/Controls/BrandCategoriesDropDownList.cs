namespace ExhibFlat.UI.Common.Controls
{
    using ExhibFlat.ControlPanel.Commodities;
    using ExhibFlat.SiteSet;
    using System;
    using System.Data;
    using System.Globalization;
    using System.Web.UI.WebControls;

    public class BrandCategoriesDropDownList : DropDownList
    {
        private bool allowNull = true;
        private string nullToDisplay = ""; 
        private string supplierid="";

        public override void DataBind()
        {
            this.Items.Clear();
            if (this.AllowNull)
            {
                base.Items.Add(new ListItem(this.NullToDisplay, string.Empty));
            }
            DataTable table = ProductProvider.Instance().GetBrandCategories();
            bool res = false;
            foreach (DataRow row in table.Rows)
            {
                if ((SupplierId != "" && SupplierId == row["SupplierID"].ToString()))
                {
                    res = true;
                }
            }
            if (res)
            {
                foreach (DataRow row in table.Rows)
                {
                    if ((SupplierId != "" && SupplierId != "--请选择--" && SupplierId == row["SupplierID"].ToString()))
                    {
                        int num = (int)row["BrandId"];
                        this.Items.Add(new ListItem((string)row["BrandName"], num.ToString(CultureInfo.InvariantCulture)));
                    }
                }
            }
            else
            {
                foreach (DataRow row in table.Rows)
                { 
                    if(row["SupplierID"]==null||row["SupplierID"].ToString()=="")
                    { 
                        int num = (int)row["BrandId"];
                        this.Items.Add(new ListItem((string)row["BrandName"], num.ToString(CultureInfo.InvariantCulture)));
                    }
                }
            }
        }
        public string SupplierId
        {
            get 
            {
                return this.supplierid;
            }
            set 
            {
                this.supplierid = value;
            }
        }

        public bool AllowNull
        {
            get
            {
                return this.allowNull;
            }
            set
            {
                this.allowNull = value;
            }
        }

        public string NullToDisplay
        {
            get
            {
                return this.nullToDisplay;
            }
            set
            {
                this.nullToDisplay = value;
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

