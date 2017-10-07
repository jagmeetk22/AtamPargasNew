using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AtamPargas.Web.Startup))]
namespace AtamPargas.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
