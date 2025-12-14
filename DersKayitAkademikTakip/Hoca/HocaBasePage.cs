using System;
using System.Web.UI;

namespace DersKayitAkademikTakip.Hoca
{
    /// <summary>
    /// Tüm hoca sayfalarýnýn miras alacaðý temel sayfa.
    /// Hoca olmayan kullanýcýlarý Login sayfasýna yönlendirir.
    /// </summary>
    public class HocaBasePage : Page
    {
        protected override void OnLoad(EventArgs e)
        {
            if (Session["KullaniciID"] == null || Session["Rol"] == null || Session["Rol"].ToString() != "hoca")
            {
                Response.Redirect("~/Account/Login.aspx");
                return;
            }

            base.OnLoad(e);
        }
    }
}
