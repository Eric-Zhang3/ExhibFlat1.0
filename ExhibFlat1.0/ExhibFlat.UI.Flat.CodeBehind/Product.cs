using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hidistro.Core;
using Hidistro.Entities.Commodities;
using Hidistro.Membership.Context;
using Hidistro.SaleSystem.Catalog;
using Hidistro.UI.Common.Controls;
using Hidistro.UI.SaleSystem.Tags;
using System.Web.UI.WebControls;
using Hidistro.ControlPanel.Commodities;
using System.Data;

using System.Reflection;
using System.IO;
namespace Hidistro.UI.SaleSystem.CodeBehind
{
    public class Product : SearchTemplatedWebControl_HN
    {
        private Common_GoodList_ShopProduct dataListPointDetails;
        private Common_GoodList_ShopImg ShopDetails;
        //private Common_GoodsList_HN SearchProduct;
        private int shopId;
        protected override void AttachChildControls()
        {
            this.dataListPointDetails = (Common_GoodList_ShopProduct)this.FindControl("common_goodlist_shopproduct");
            this.ShopDetails = (Common_GoodList_ShopImg)this.FindControl("common_goodlist_shopimg");
            shopId = this.Page.Request.QueryString["ShopID"] == null ? 0 : Convert.ToInt32(this.Page.Request.QueryString["ShopID"]);

            if (!this.Page.IsPostBack)
            {
                //this.dataListPointDetails.DataSource = ProductHelper.GetShopProduct(shopId);
                //this.dataListPointDetails.DataBind();

                //this.ShopDetails.DataSource = ProductHelper.GetShopInfo(shopId);
                //this.ShopDetails.DataBind();
                if (this.Page.Request["gd"] != null)
                {
                    IList<CategoryInfo> data = CatalogHelper.GetMainCategories(); //一级标签
                    IList<TagInfo> data2 = CatalogHelper.GetSuggestTag();//二级标签

                    string Json = ObjectToJson<CategoryInfo>("yiji", data);
                    Json += "," + "\"" + ObjectToJson<TagInfo>("erji", data2);

                    base.Page.Response.Write("{\"" + Json + "}");
                    base.Page.Response.End();
                }
            }
            base.AttachChildControls();
        }

        public static string ObjectToJson<T>(string jsonName, IList<T> IL)
        {
            StringBuilder Json = new StringBuilder();
            Json.Append(jsonName + "\":[");
            if (IL.Count > 0)
            {
                for (int i = 0; i < IL.Count; i++)
                {
                    T obj = Activator.CreateInstance<T>();
                    Type type = obj.GetType();
                    PropertyInfo[] pis = type.GetProperties();
                    Json.Append("{");
                    for (int j = 0; j < pis.Length; j++)
                    {
                        Json.Append("\"" + pis[j].Name.ToString() + "\":\"" + pis[j].GetValue(IL[i], null) + "\"");
                        if (j < pis.Length - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < IL.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
            Json.Append("]");
            return Json.ToString();
        }

        protected override void OnInit(EventArgs e)
        {
            string shopid = this.Page.Request.QueryString["tx"];

            if (this.SkinName == null)
            {
                if (!string.IsNullOrEmpty(shopid) && shopid != "index")
                    this.SkinName = "MPage/Skin-Product.html";
                else
                    this.SkinName = "MPage/Skin-ShopIndex.html";

            }
            base.OnInit(e);
        }
    }
}
