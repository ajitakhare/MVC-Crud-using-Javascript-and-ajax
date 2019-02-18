using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EditV2.Startup))]
namespace EditV2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
