using System;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace DersKayitAkademikTakip.Hoca
{
    public partial class NotGirisi : HocaBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DersleriYukle();
            }
        }

        private void DersleriYukle()
        {
            string cs = ConfigurationManager.ConnectionStrings["UniversiteDB"].ConnectionString;
            int hocaId = Convert.ToInt32(Session["KullaniciID"]);

            using (var conn = new MySqlConnection(cs))
            {
                conn.Open();
                string sql = "SELECT ders_kodu, ders_adi FROM Dersler WHERE hoca_id = @hid ORDER BY ders_kodu";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@hid", hocaId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        ddlDersler.Items.Clear();
                        ddlDersler.Items.Add(new ListItem("-- Seçiniz --", ""));

                        while (reader.Read())
                        {
                            string dersKodu = reader["ders_kodu"].ToString();
                            string dersAdi = reader["ders_adi"].ToString();
                            ddlDersler.Items.Add(new ListItem(dersAdi + " (" + dersKodu + ")", dersKodu));
                        }
                    }
                }
            }
        }

        protected void ddlDersler_SelectedIndexChanged(object sender, EventArgs e)
        {
            OgrencileriYukle();
        }

        private void OgrencileriYukle()
        {
            if (string.IsNullOrEmpty(ddlDersler.SelectedValue))
            {
                gvOgrenciler.DataSource = null;
                gvOgrenciler.DataBind();
                return;
            }

            string cs = ConfigurationManager.ConnectionStrings["UniversiteDB"].ConnectionString;
            int hocaId = Convert.ToInt32(Session["KullaniciID"]);

            using (var conn = new MySqlConnection(cs))
            {
                conn.Open();
                string sql = @"SELECT 
                                    k.ogrenci_id,
                                    CONCAT(o.ad, ' ', o.soyad, ' (', IFNULL(o.kullanici_no,''), ')') AS ogrenci_adi,
                                    n.vize_notu,
                                    n.final_notu,
                                    n.butunleme_notu,
                                    n.ortalama,
                                    n.harf_notu,
                                    n.durum
                               FROM Kayitlar k
                               INNER JOIN Dersler d ON k.ders_kodu = d.ders_kodu
                               INNER JOIN Kullanicilar o ON k.ogrenci_id = o.kullanici_id
                               LEFT JOIN notlar n ON n.ders_kodu = k.ders_kodu AND n.ogrenci_id = k.ogrenci_id
                               WHERE k.ders_kodu = @dersKodu
                                 AND k.durum = 'onaylandi'
                                 AND d.hoca_id = @hid
                               ORDER BY o.ad, o.soyad";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@dersKodu", ddlDersler.SelectedValue);
                    cmd.Parameters.AddWithValue("@hid", hocaId);

                    var da = new MySqlDataAdapter(cmd);
                    var dt = new DataTable();
                    da.Fill(dt);
                    gvOgrenciler.DataSource = dt;
                    gvOgrenciler.DataBind();
                }
            }
        }

        protected void btnKaydet_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlDersler.SelectedValue))
                return;

            string cs = ConfigurationManager.ConnectionStrings["UniversiteDB"].ConnectionString;
            string dersKodu = ddlDersler.SelectedValue;

            try
            {
                using (var conn = new MySqlConnection(cs))
                {
                    conn.Open();

                    foreach (GridViewRow row in gvOgrenciler.Rows)
                    {
                        if (row.RowType != DataControlRowType.DataRow)
                            continue;

                        var hfOgrenciId = (HiddenField)row.FindControl("hfOgrenciId");
                        var txtVize = (TextBox)row.FindControl("txtVize");
                        var txtFinal = (TextBox)row.FindControl("txtFinal");
                        var txtBut = (TextBox)row.FindControl("txtBut");

                        int ogrenciId;
                        if (!int.TryParse(hfOgrenciId.Value, out ogrenciId))
                            continue;

                        decimal? vize = ParseNullableDecimal(txtVize.Text);
                        decimal? final = ParseNullableDecimal(txtFinal.Text);
                        decimal? butunleme = ParseNullableDecimal(txtBut.Text);

                        decimal? ortalama;
                        string harfNotu;
                        string durum;
                        HesaplaNot(vize, final, butunleme, out ortalama, out harfNotu, out durum);

                        // Önce mevcut kayıt var mı kontrol et
                        int? notId = null;
                        using (var checkCmd = new MySqlCommand("SELECT not_id FROM notlar WHERE ogrenci_id = @ogrenciId AND ders_kodu = @dersKodu LIMIT 1", conn))
                        {
                            checkCmd.Parameters.AddWithValue("@ogrenciId", ogrenciId);
                            checkCmd.Parameters.AddWithValue("@dersKodu", dersKodu);
                            object obj = checkCmd.ExecuteScalar();
                            if (obj != null && obj != DBNull.Value)
                            {
                                notId = Convert.ToInt32(obj);
                            }
                        }

                        if (notId.HasValue)
                        {
                            using (var updateCmd = new MySqlCommand(@"UPDATE notlar
                                                                          SET vize_notu = @vize,
                                                                              final_notu = @final,
                                                                              butunleme_notu = @but,
                                                                              ortalama = @ortalama,
                                                                              harf_notu = @harf,
                                                                              durum = @durum,
                                                                              guncelleme_tarihi = NOW()
                                                                        WHERE not_id = @notId", conn))
                            {
                                updateCmd.Parameters.AddWithValue("@vize", (object)vize ?? DBNull.Value);
                                updateCmd.Parameters.AddWithValue("@final", (object)final ?? DBNull.Value);
                                updateCmd.Parameters.AddWithValue("@but", (object)butunleme ?? DBNull.Value);
                                updateCmd.Parameters.AddWithValue("@ortalama", (object)ortalama ?? DBNull.Value);
                                updateCmd.Parameters.AddWithValue("@harf", (object)harfNotu ?? DBNull.Value);
                                updateCmd.Parameters.AddWithValue("@durum", (object)durum ?? DBNull.Value);
                                updateCmd.Parameters.AddWithValue("@notId", notId.Value);
                                updateCmd.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            using (var insertCmd = new MySqlCommand(@"INSERT INTO notlar
                                                                            (ogrenci_id, ders_kodu, vize_notu, final_notu, butunleme_notu, ortalama, harf_notu, durum, olusturma_tarihi, guncelleme_tarihi)
                                                                     VALUES (@ogrenciId, @dersKodu, @vize, @final, @but, @ortalama, @harf, @durum, NOW(), NOW())", conn))
                            {
                                insertCmd.Parameters.AddWithValue("@ogrenciId", ogrenciId);
                                insertCmd.Parameters.AddWithValue("@dersKodu", dersKodu);
                                insertCmd.Parameters.AddWithValue("@vize", (object)vize ?? DBNull.Value);
                                insertCmd.Parameters.AddWithValue("@final", (object)final ?? DBNull.Value);
                                insertCmd.Parameters.AddWithValue("@but", (object)butunleme ?? DBNull.Value);
                                insertCmd.Parameters.AddWithValue("@ortalama", (object)ortalama ?? DBNull.Value);
                                insertCmd.Parameters.AddWithValue("@harf", (object)harfNotu ?? DBNull.Value);
                                insertCmd.Parameters.AddWithValue("@durum", (object)durum ?? DBNull.Value);
                                insertCmd.ExecuteNonQuery();
                            }
                        }
                    }
                }

                SuccessPanel.Visible = true;
                ErrorPanel.Visible = false;
                SuccessText.Text = "Notlar kaydedildi.";
                OgrencileriYukle();
            }
            catch (Exception ex)
            {
                SuccessPanel.Visible = false;
                ErrorPanel.Visible = true;
                ErrorText.Text = "Hata: " + ex.Message;
            }
        }

        private decimal? ParseNullableDecimal(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return null;

            decimal value;
            // Hem "," hem "." destekle
            text = text.Replace(',', '.');
            if (decimal.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out value))
            {
                return value;
            }
            return null;
        }

        private void HesaplaNot(decimal? vize, decimal? final, decimal? butunleme, out decimal? ortalama, out string harfNotu, out string durum)
        {
            ortalama = null;
            harfNotu = null;
            durum = "devam_ediyor";

            decimal? kullanilacakFinal = null;
            if (butunleme.HasValue)
                kullanilacakFinal = butunleme.Value;
            else if (final.HasValue)
                kullanilacakFinal = final.Value;

            if (vize.HasValue && kullanilacakFinal.HasValue)
            {
                ortalama = Math.Round(vize.Value * 0.4m + kullanilacakFinal.Value * 0.6m, 2);
                harfNotu = HarfNotuHesapla(ortalama.Value);
                durum = ortalama.Value >= 60m ? "gecti" : "kaldi";
            }
        }

        private string HarfNotuHesapla(decimal ortalama)
        {
            if (ortalama >= 90) return "AA";
            if (ortalama >= 85) return "BA";
            if (ortalama >= 80) return "BB";
            if (ortalama >= 75) return "CB";
            if (ortalama >= 70) return "CC";
            if (ortalama >= 65) return "DC";
            if (ortalama >= 60) return "DD";
            if (ortalama >= 50) return "FD";
            return "FF";
        }
    }
}
