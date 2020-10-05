using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TecTkt.Backend.Startup))]
namespace TecTkt.Backend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
