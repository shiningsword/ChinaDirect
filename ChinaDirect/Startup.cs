using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ChinaDirect.Startup))]
namespace ChinaDirect
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
