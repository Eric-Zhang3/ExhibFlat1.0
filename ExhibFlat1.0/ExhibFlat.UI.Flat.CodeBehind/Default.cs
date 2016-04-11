namespace ExhibFlat.UI.Flat.CodeBehind
{
    using ExhibFlat.Entities.Sales;
    using ExhibFlat.SiteSet;
    
    using ExhibFlat.UI.Common.Controls;
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [ParseChildren(true)]
    public class Default : HtmlTemplatedWebControl
    {
        public Literal litLogout;
        protected override void AttachChildControls()
        {
            
            HiContext current = HiContext.Current;
            PageTitle.AddTitle(current.SiteSettings.SiteName + current.SiteSettings.SiteDescription, HiContext.Current.Context);
        }

        protected override void OnInit(EventArgs e)
        {
            if (this.SkinName == null)
            {
                this.SkinName = "Skin-Default.html";
            }
            base.OnInit(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            
            base.OnLoad(e);
        }

        
    }
}

