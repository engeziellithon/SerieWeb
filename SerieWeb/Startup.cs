using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SerieWeb.Startup))]
namespace SerieWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
