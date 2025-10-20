using System;
using System.Web;

namespace DersKayitAkademikTakip.Admin
{
    public class AdminBasePage : System.Web.UI.Page
    {
        protected override void OnLoad(EventArgs e)
        {
            // DEBUG
            System.Diagnostics.Debug.WriteLine($"ADMINBASEPAGE - Session Rol: {HttpContext.Current.Session["Rol"]}");

            if (HttpContext.Current.Session["KullaniciID"] == null ||
                HttpContext.Current.Session["Rol"] == null ||
                HttpContext.Current.Session["Rol"].ToString() != "admin")
            {
                System.Diagnostics.Debug.WriteLine("ADMINBASEPAGE - REDIRECT TO LOGIN");
                Response.Redirect("~/Account/Login.aspx");
                return;
            }

            base.OnLoad(e);
        }
    }
}