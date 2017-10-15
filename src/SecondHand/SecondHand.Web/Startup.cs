using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SecondHand.Web.Startup))]
namespace SecondHand.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            app.MapSignalR(new HubConfiguration
            {
                Resolver = GlobalHost.DependencyResolver
            });
        }
    }
}
