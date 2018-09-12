using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Inv.Backend.Startup))]
namespace Inv.Backend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
