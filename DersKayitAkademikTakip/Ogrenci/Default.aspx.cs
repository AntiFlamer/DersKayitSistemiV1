using System;
using System.Web.UI;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;

namespace DersKayitAkademikTakip.Ogrenci
{
    public partial class Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Session kontrolü - Öðrenci olmalý
            if (Session["KullaniciID"] == null || Session["Rol"] == null || Session["Rol"].ToString() != "ogrenci")
            {
                System.Diagnostics.Debug.WriteLine($"=== REDIRECT - Session: KullaniciID={Session["KullaniciID"]}, Rol={Session["Rol"]} ===");
                Response.Redirect("~/Account/Login.aspx");
                return;
            }

            System.Diagnostics.Debug.WriteLine($"=== SESSION ÇALIÞIYOR: {Session["Ad"]} {Session["Soyad"]} ===");

            if (!IsPostBack)
            {
                litOgrenciAd.Text = $"{Session["Ad"]} {Session["Soyad"]}";
                IstatistikleriYukle();
                SonKayitlariYukle();
            }
        }

        private void IstatistikleriYukle()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["UniversiteDB"].ConnectionString;
            int ogrenciId = Convert.ToInt32(Session["KullaniciID"]);

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                // Toplam Kayýtlý Ders Sayýsý
                string toplamQuery = "SELECT COUNT(*) FROM Kayitlar WHERE ogrenci_id = @ogrenciId";
                MySqlCommand toplamCmd = new MySqlCommand(toplamQuery, conn);
                toplamCmd.Parameters.AddWithValue("@ogrenciId", ogrenciId);
                toplamKayitliDers.InnerText = toplamCmd.ExecuteScalar().ToString();

                // Onaylý Ders Sayýsý
                string onayliQuery = "SELECT COUNT(*) FROM Kayitlar WHERE ogrenci_id = @ogrenciId AND durum = 'onaylandi'";
                MySqlCommand onayliCmd = new MySqlCommand(onayliQuery, conn);
                onayliCmd.Parameters.AddWithValue("@ogrenciId", ogrenciId);
                onayliDersler.InnerText = onayliCmd.ExecuteScalar().ToString();

                // Bekleyen Kayýt Sayýsý
                string bekleyenQuery = "SELECT COUNT(*) FROM Kayitlar WHERE ogrenci_id = @ogrenciId AND durum = 'onay_bekliyor'";
                MySqlCommand bekleyenCmd = new MySqlCommand(bekleyenQuery, conn);
                bekleyenCmd.Parameters.AddWithValue("@ogrenciId", ogrenciId);
                bekleyenKayitlar.InnerText = bekleyenCmd.ExecuteScalar().ToString();

                // Genel Ortalama (notlar tablosundan)
                string ortalamaQuery = @"SELECT AVG(ortalama) 
                                       FROM notlar 
                                       WHERE ogrenci_id = @ogrenciId 
                                       AND ortalama IS NOT NULL";
                MySqlCommand ortalamaCmd = new MySqlCommand(ortalamaQuery, conn);
                ortalamaCmd.Parameters.AddWithValue("@ogrenciId", ogrenciId);
                object ortalamaObj = ortalamaCmd.ExecuteScalar();
                
                if (ortalamaObj != DBNull.Value && ortalamaObj != null)
                {
                    genelOrtalama.InnerText = Convert.ToDouble(ortalamaObj).ToString("F2");
                }
                else
                {
                    genelOrtalama.InnerText = "0.00";
                }
            }
        }

        private void SonKayitlariYukle()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["UniversiteDB"].ConnectionString;
            int ogrenciId = Convert.ToInt32(Session["KullaniciID"]);

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = @"SELECT 
                                k.kayit_id,
                                k.ders_kodu,
                                d.ders_adi,
                                d.kredi,
                                CONCAT(h.ad, ' ', h.soyad) AS hoca_adi,
                                k.kayit_tarihi,
                                k.durum
                               FROM Kayitlar k
                               INNER JOIN Dersler d ON k.ders_kodu = d.ders_kodu
                               LEFT JOIN Kullanicilar h ON d.hoca_id = h.kullanici_id
                               WHERE k.ogrenci_id = @ogrenciId
                               ORDER BY k.kayit_tarihi DESC
                               LIMIT 5";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ogrenciId", ogrenciId);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvSonKayitlar.DataSource = dt;
                gvSonKayitlar.DataBind();
            }
        }

        // Durum badge'i için helper method
        public string GetDurumBadge(string durum)
        {
            switch (durum)
            {
                case "onaylandi":
                    return "<span class='badge bg-success'>Onaylandý</span>";
                case "onay_bekliyor":
                    return "<span class='badge bg-warning'>Onay Bekliyor</span>";
                case "reddedildi":
                    return "<span class='badge bg-danger'>Reddedildi</span>";
                default:
                    return "<span class='badge bg-secondary'>" + durum + "</span>";
            }
        }
    }
}
