namespace ExhibFlat.UI.Flat.CodeBehind
{
    using ExhibFlat.Entities.Sales;
    using ExhibFlat.SiteSet;
    using ExhibFlat.UI.Common.Controls;
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [ParseChildren(true)]
    public class Login : HtmlTemplatedWebControl
    {

        protected override void AttachChildControls()
        {

        }

        protected override void OnInit(EventArgs e)
        {
            if (this.SkinName == null)
            {
                this.SkinName = "Skin-Login.html";
            }
            base.OnInit(e);
        }

        protected override void OnLoad(EventArgs e)
        {

            base.OnLoad(e);
        }


        

    }
}

