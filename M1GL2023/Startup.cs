using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(M1GL2023.Startup))]
namespace M1GL2023
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
