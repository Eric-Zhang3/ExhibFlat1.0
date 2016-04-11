namespace Hidistro.UI.SaleSystem.CodeBehind
{
    using Hidistro.AccountCenter.Comments;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class AjaxPage :Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["productId"]))
            {
                int result = 0;
                int.TryParse(this.Page.Request.QueryString["productId"], out result);
                if (CommentsHelper.ExistsProduct(result))
                {
                    Response.Clear();
                    Response.Write("same");
                    Response.End();
                }

                else if (!CommentsHelper.ExistsProduct(result) && !CommentsHelper.AddProductToFavorite(result))
                {
                    Response.Clear();
                    Response.Write("false");
                    Response.End();
                }
                else
                {
                    Response.Clear();
                    Response.Write("true");
                    Response.End();
                }
            }
        }
    }
}