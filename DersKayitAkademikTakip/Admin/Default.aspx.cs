using System;
using System.Web.UI;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace DersKayitAkademikTakip.Admin
{
    public partial class Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KullaniciID"] == null || Session["Rol"] == null || Session["Rol"].ToString() != "admin")
            {
                Response.Redirect("~/Account/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                IstatistikleriYukle();
            }
        }

        private void IstatistikleriYukle()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["UniversiteDB"].ConnectionString;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                // Toplam Kullanıcı Sayısı
                string kullaniciQuery = "SELECT COUNT(*) FROM Kullanicilar";
                MySqlCommand kullaniciCmd = new MySqlCommand(kullaniciQuery, conn);
                toplamKullanici.InnerText = kullaniciCmd.ExecuteScalar().ToString();

                // Toplam Ders Sayısı
                string dersQuery = "SELECT COUNT(*) FROM Dersler";
                MySqlCommand dersCmd = new MySqlCommand(dersQuery, conn);
                toplamDers.InnerText = dersCmd.ExecuteScalar().ToString();

                // Toplam Kayıt Sayısı
                string kayitQuery = "SELECT COUNT(*) FROM Kayitlar";
                MySqlCommand kayitCmd = new MySqlCommand(kayitQuery, conn);
                toplamKayit.InnerText = kayitCmd.ExecuteScalar().ToString();
            }
        }
    }
}