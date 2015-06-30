using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Test.Products.Startup))]
namespace Test.Products
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
