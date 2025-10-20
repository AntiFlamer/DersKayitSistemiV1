using System;
using System.Web;

namespace DersKayitAkademikTakip
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("=== APPLICATION START ===");
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("=== YENI SESSION BASLADI ===");
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            // Her request'te session'ı kontrol et
            if (Context.Session != null)
            {
                System.Diagnostics.Debug.WriteLine($"=== REQUEST: {Request.Url} ===");
            }
        }
    }
}