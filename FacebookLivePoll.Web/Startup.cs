using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FacebookLivePoll.Web.Startup))]
namespace FacebookLivePoll.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
