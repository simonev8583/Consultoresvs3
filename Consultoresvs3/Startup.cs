using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Consultoresvs3.Startup))]
namespace Consultoresvs3
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
