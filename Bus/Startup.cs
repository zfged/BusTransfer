using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Bus.Startup))]
namespace Bus
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
