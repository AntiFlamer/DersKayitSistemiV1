using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DersKayitAkademikTakip.Startup))]
namespace DersKayitAkademikTakip
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
