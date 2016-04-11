namespace ExhibFlat.UI.Common.Controls
{
    
    using System;
    using System.Globalization;
    using System.Web;
    using System.Web.UI;
    using ExhibFlat.SiteSet;
    [PersistChildren(true), ParseChildren(false)]
    public class PageTitle : Control
    {
        private const string titleKey = "Flat.Title.Value";

        public static void AddSiteNameTitle(string title, HttpContext context)
        {
            AddTitle(string.Format(CultureInfo.InvariantCulture, "{0} {1}", new object[] { title, HiContext.Current.SiteSettings.SiteName }), context);
        }

        public static void AddTitle(string title, HttpContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            context.Items["Flat.Title.Value"] = title;
        }

        protected override void Render(HtmlTextWriter writer)
        {
            string siteName = this.Context.Items["Flat.Title.Value"] as string;
            if (string.IsNullOrEmpty(siteName))
            {
                siteName = HiContext.Current.SiteSettings.SiteName;
            }
            writer.WriteLine("<title>{0}</title>", siteName);
        }
    }
}

