using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OnlineTrener.Startup))]
namespace OnlineTrener
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
