using System.Configuration;
using System.IO;
using System.Net;
using System.Text;

namespace ExhibFlat.UI.Web.Admin
{
    using System;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI;

    public class LoginExit : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //SsoSessionKeyUtil.Logout();

            string redirectURL = "/?sign=logout";
           
            base.Response.Redirect(redirectURL, true);
        }
    }
}