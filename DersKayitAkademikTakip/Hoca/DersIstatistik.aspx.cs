using System;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;

namespace DersKayitAkademikTakip.Hoca
{
    public partial class DersIstatistik : HocaBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DersIstatistikleriniYukle();
            }
        }

        private void DersIstatistikleriniYukle()
        {
            string cs = ConfigurationManager.ConnectionStrings["UniversiteDB"].ConnectionString;
            int hocaId = Convert.ToInt32(Session["KullaniciID"]);

            using (var conn = new MySqlConnection(cs))
            {
                conn.Open();

                string sql = @"SELECT 
                                    d.ders_kodu,
                                    d.ders_adi,
                                    d.kredi,
                                    d.akts_kredi,
                                    COUNT(DISTINCT k.ogrenci_id) AS ogrenci_sayisi,
                                    AVG(n.ortalama) AS ortalama,
                                    SUM(CASE WHEN n.durum = 'gecti' THEN 1 ELSE 0 END) AS gecen_sayisi,
                                    SUM(CASE WHEN n.durum = 'kaldi' THEN 1 ELSE 0 END) AS kalan_sayisi
                               FROM Dersler d
                               LEFT JOIN Kayitlar k ON d.ders_kodu = k.ders_kodu AND k.durum = 'onaylandi'
                               LEFT JOIN notlar n ON n.ders_kodu = k.ders_kodu AND n.ogrenci_id = k.ogrenci_id
                               WHERE d.hoca_id = @hid
                               GROUP BY d.ders_kodu, d.ders_adi, d.kredi, d.akts_kredi
                               ORDER BY d.ders_kodu";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@hid", hocaId);
                    var da = new MySqlDataAdapter(cmd);
                    var dt = new DataTable();
                    da.Fill(dt);

                    // Geçme/Kalma yüzdelerini hesapla
                    dt.Columns.Add("gecen_yuzde", typeof(decimal));
                    dt.Columns.Add("kalan_yuzde", typeof(decimal));

                    foreach (DataRow row in dt.Rows)
                    {
                        int gecen = row["gecen_sayisi"] != DBNull.Value ? Convert.ToInt32(row["gecen_sayisi"]) : 0;
                        int kalan = row["kalan_sayisi"] != DBNull.Value ? Convert.ToInt32(row["kalan_sayisi"]) : 0;
                        int toplam = gecen + kalan;

                        decimal gecenYuzde = 0;
                        decimal kalanYuzde = 0;

                        if (toplam > 0)
                        {
                            gecenYuzde = Math.Round((decimal)gecen * 100 / toplam, 1);
                            kalanYuzde = Math.Round((decimal)kalan * 100 / toplam, 1);
                        }

                        row["gecen_yuzde"] = gecenYuzde;
                        row["kalan_yuzde"] = kalanYuzde;
                    }

                    gvDersIstatistik.DataSource = dt;
                    gvDersIstatistik.DataBind();
                }
            }
        }
    }
}
