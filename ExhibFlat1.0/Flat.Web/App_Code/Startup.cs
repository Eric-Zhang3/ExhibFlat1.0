using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Flat.Web.Startup))]
namespace Flat.Web
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
