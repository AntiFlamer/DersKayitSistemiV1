using System;
using System.Web;
using System.Web.UI;

namespace DersKayitAkademikTakip.Account
{
    public partial class Logout : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CikisYap();
            }
        }

        private void CikisYap()
        {
            // Session'ı temizle
            Session.Clear();
            Session.Abandon();

            // Cookie'leri temizle (varsa)
            if (Request.Cookies["KullaniciBilgi"] != null)
            {
                Response.Cookies["KullaniciBilgi"].Expires = DateTime.Now.AddDays(-1);
            }

            // Ana sayfaya yönlendir
            Response.Redirect("~/Default.aspx");
        }
    }
}