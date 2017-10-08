using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JobSystem.Web.Startup))]
namespace JobSystem.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
