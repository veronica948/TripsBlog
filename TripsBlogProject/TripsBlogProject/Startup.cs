using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TripsBlogProject.Startup))]
namespace TripsBlogProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
