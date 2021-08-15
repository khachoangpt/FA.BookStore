using Owin;
using Microsoft.Owin;

[assembly: OwinStartup(typeof(FA.BookStore.WebMVC.Startup))]
namespace FA.BookStore.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
