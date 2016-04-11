namespace ExhibFlat.UI.Flat.Tags
{
    using ExhibFlat.Core.Enums;
    using ExhibFlat.Entities.Commodities;
    using ExhibFlat.ControlPanel.Commodities;
    using ExhibFlat.UI.Common.Controls;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using System.Xml;

    public class Common_GoodsList_Default : ThemedTemplatedRepeater
    {
        private int desc;
        private int imageNum = 1;
        private string orderBy = "DisplaySequence";

        private void BindList()
        {
            SubjectListQuery query = new SubjectListQuery {
                SortBy = this.OrderBy
            };
            //if (this.Desc == 0)
            //{
                query.SortOrder = SortAction.Desc;
            //}
            //else
            //{
            //    query.SortOrder = SortAction.Asc;
            //}
                XmlNode node = ProductHelper.GetProductSubjectDocument().SelectSingleNode("root/Subject[SubjectId='" + this.SubjectId + "']");
            if (node != null)
            {
                if (!string.IsNullOrEmpty(node.SelectSingleNode("Type").InnerText))
                {
                    query.TagId = int.Parse(node.SelectSingleNode("Type").InnerText);
                }
                query.CategoryIds = node.SelectSingleNode("Categories").InnerText;
                string innerText = node.SelectSingleNode("MaxPrice").InnerText;
                if (!string.IsNullOrEmpty(innerText))
                {
                    int result = 0;
                    if (int.TryParse(innerText, out result))
                    {
                        query.MaxPrice = new decimal?(result);
                    }
                }
                string str2 = node.SelectSingleNode("MinPrice").InnerText;
                if (!string.IsNullOrEmpty(str2))
                {
                    int num2 = 0;
                    if (int.TryParse(str2, out num2))
                    {
                        query.MinPrice = new decimal?(num2);
                    }
                }
                query.Keywords = node.SelectSingleNode("Keywords").InnerText;
                query.MaxNum = int.Parse(node.SelectSingleNode("MaxNum").InnerText);
                if (this.IsSaleCountOrderBy)
                {
                    int num3 = 0;
                    if (int.TryParse(node.SelectSingleNode("SaleContMaxNum").InnerText, out num3))
                    {
                        query.MaxNum = num3;
                    }
                }
                if (!string.IsNullOrEmpty(node.SelectSingleNode("BrandCategoryId").InnerText))
                {
                    query.BrandCategoryId = new int?(int.Parse(node.SelectSingleNode("BrandCategoryId").InnerText));
                }
                if (!string.IsNullOrEmpty(node.SelectSingleNode("ProductTypeId").InnerText))
                {
                    query.ProductTypeId = new int?(int.Parse(node.SelectSingleNode("ProductTypeId").InnerText));
                }
                string str3 = node.SelectSingleNode("AttributeString").InnerText;
                if (!string.IsNullOrEmpty(str3))
                {
                    IList<AttributeValueInfo> list = new List<AttributeValueInfo>();
                    string[] strArray = str3.Split(new char[] { ',' });
                    for (int i = 0; i < strArray.Length; i++)
                    {
                        string[] strArray2 = strArray[i].Split(new char[] { '_' });
                        AttributeValueInfo item = new AttributeValueInfo {
                            AttributeId = Convert.ToInt32(strArray2[0]),
                            ValueId = Convert.ToInt32(strArray2[1])
                        };
                        list.Add(item);
                    }
                    query.AttributeValues = list;
                }
            }
            base.DataSource = ProductHelper.GetSubjectList(query);
            base.DataBind();
        }

        private void Common_GoodsList_Default_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (this.ImageNum > 0)
            {
                Control control = e.Item.Controls[0];
                if (e.Item.ItemIndex >= this.ImageNum)
                {
                    HtmlGenericControl control2 = (HtmlGenericControl) control.FindControl("divImage");
                    control2.Visible = false;
                }
                else
                {
                    HtmlGenericControl control3 = (HtmlGenericControl) control.FindControl("divName");
                    control3.Visible = false;
                }
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            if (this.IsSaleCountOrderBy)
            {
                this.OrderBy = "SaleCounts";
                base.ItemDataBound += new RepeaterItemEventHandler(this.Common_GoodsList_Default_ItemDataBound);
            }
            if (!this.Page.IsPostBack)
            {
                this.BindList();
            }
        }

        public int Desc
        {
            get
            {
                return this.desc;
            }
            set
            {
                this.desc = value;
            }
        }

        public int ImageNum
        {
            get
            {
                return this.imageNum;
            }
            set
            {
                this.imageNum = value;
            }
        }

        public bool IsSaleCountOrderBy { get; set; }

        public string OrderBy
        {
            get
            {
                return this.orderBy;
            }
            set
            {
                this.orderBy = value;
            }
        }

        public int SubjectId { get; set; }
    }
}

