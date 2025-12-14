using System;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;

namespace DersKayitAkademikTakip.Hoca
{
    public partial class Default : HocaBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                litHocaAd.Text = $"{Session["Ad"]} {Session["Soyad"]}";
                IstatistikleriYukle();
                SonKayitlariYukle();
                DerslerimiYukle();
            }
        }

        private void IstatistikleriYukle()
        {
            string cs = ConfigurationManager.ConnectionStrings["UniversiteDB"].ConnectionString;
            int hocaId = Convert.ToInt32(Session["KullaniciID"]);

            using (var conn = new MySqlConnection(cs))
            {
                conn.Open();

                // Verdiði ders sayýsý
                string dersQuery = "SELECT COUNT(*) FROM Dersler WHERE hoca_id = @hid";
                using (var dersCmd = new MySqlCommand(dersQuery, conn))
                {
                    dersCmd.Parameters.AddWithValue("@hid", hocaId);
                    toplamDers.InnerText = dersCmd.ExecuteScalar().ToString();
                }

                // Onaylý kayýt sayýsý
                string onayliQuery = @"SELECT COUNT(*)
                                        FROM Kayitlar k
                                        INNER JOIN Dersler d ON k.ders_kodu = d.ders_kodu
                                        WHERE d.hoca_id = @hid AND k.durum = 'onaylandi'";
                using (var onayliCmd = new MySqlCommand(onayliQuery, conn))
                {
                    onayliCmd.Parameters.AddWithValue("@hid", hocaId);
                    onayliKayitlar.InnerText = onayliCmd.ExecuteScalar().ToString();
                }

                // Bekleyen kayýt sayýsý
                string bekleyenQuery = @"SELECT COUNT(*)
                                          FROM Kayitlar k
                                          INNER JOIN Dersler d ON k.ders_kodu = d.ders_kodu
                                          WHERE d.hoca_id = @hid AND k.durum = 'onay_bekliyor'";
                using (var bekleyenCmd = new MySqlCommand(bekleyenQuery, conn))
                {
                    bekleyenCmd.Parameters.AddWithValue("@hid", hocaId);
                    bekleyenKayitlar.InnerText = bekleyenCmd.ExecuteScalar().ToString();
                }

                // Toplam öðrenci (onaylý kayýtlardaki benzersiz öðrenci sayýsý)
                string ogrenciQuery = @"SELECT COUNT(DISTINCT k.ogrenci_id)
                                          FROM Kayitlar k
                                          INNER JOIN Dersler d ON k.ders_kodu = d.ders_kodu
                                          WHERE d.hoca_id = @hid AND k.durum = 'onaylandi'";
                using (var ogrenciCmd = new MySqlCommand(ogrenciQuery, conn))
                {
                    ogrenciCmd.Parameters.AddWithValue("@hid", hocaId);
                    toplamOgrenci.InnerText = ogrenciCmd.ExecuteScalar().ToString();
                }
            }
        }

        private void SonKayitlariYukle()
        {
            string cs = ConfigurationManager.ConnectionStrings["UniversiteDB"].ConnectionString;
            int hocaId = Convert.ToInt32(Session["KullaniciID"]);

            using (var conn = new MySqlConnection(cs))
            {
                conn.Open();

                string query = @"SELECT 
                                    k.kayit_id,
                                    k.ders_kodu,
                                    d.ders_adi,
                                    CONCAT(o.ad, ' ', o.soyad) AS ogrenci_adi,
                                    k.kayit_tarihi,
                                    k.durum
                                  FROM Kayitlar k
                                  INNER JOIN Dersler d ON k.ders_kodu = d.ders_kodu
                                  INNER JOIN Kullanicilar o ON k.ogrenci_id = o.kullanici_id
                                  WHERE d.hoca_id = @hid
                                  ORDER BY k.kayit_tarihi DESC
                                  LIMIT 5";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@hid", hocaId);

                    var da = new MySqlDataAdapter(cmd);
                    var dt = new DataTable();
                    da.Fill(dt);
                    gvSonKayitlar.DataSource = dt;
                    gvSonKayitlar.DataBind();
                }
            }
        }

        private void DerslerimiYukle()
        {
            string cs = ConfigurationManager.ConnectionStrings["UniversiteDB"].ConnectionString;
            int hocaId = Convert.ToInt32(Session["KullaniciID"]);

            using (var conn = new MySqlConnection(cs))
            {
                conn.Open();
                string sql = @"SELECT ders_kodu, ders_adi, kredi, akts_kredi, kontenjan, ders_donemi, ders_tipi
                                FROM Dersler
                                WHERE hoca_id = @hid
                                ORDER BY ders_kodu";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@hid", hocaId);
                    var da = new MySqlDataAdapter(cmd);
                    var dt = new DataTable();
                    da.Fill(dt);
                    gvDersler.DataSource = dt;
                    gvDersler.DataBind();
                }
            }
        }

        public string GetDurumBadge(string durum)
        {
            switch (durum)
            {
                case "onaylandi":
                    return "<span class='badge bg-success'>Onaylandý</span>";
                case "onay_bekliyor":
                    return "<span class='badge bg-warning text-dark'>Onay Bekliyor</span>";
                case "reddedildi":
                    return "<span class='badge bg-danger'>Reddedildi</span>";
                default:
                    return "<span class='badge bg-secondary'>" + durum + "</span>";
            }
        }
    }
}
