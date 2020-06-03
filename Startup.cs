using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Childrens_Toy_Store.Startup))]
namespace Childrens_Toy_Store
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
