using System;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using DersKayitAkademikTakip; // AkademikTakvimHelper için

namespace DersKayitAkademikTakip.Hoca
{
    /// <summary>
    /// Not Giriþi Sayfasý
    /// 
    /// AKADEMÝK TAKVÝM ENTEGRASYONU:
    /// - Vize notu giriþi sadece vize döneminde yapýlabilir
    /// - Final notu giriþi sadece final döneminde yapýlabilir
    /// - Bütünleme notu giriþi sadece bütünleme döneminde yapýlabilir
    /// </summary>
    public partial class NotGirisi : HocaBasePage
    {
        // Tarih kontrol sonuçlarý - sayfa genelinde kullanýlacak
        private TarihKontrolSonucu _vizeKontrol;
        private TarihKontrolSonucu _finalKontrol;
        private TarihKontrolSonucu _butunlemeKontrol;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Akademik takvim durumlarýný kontrol et
                TakvimDurumlariniKontrolEt();
                DersleriYukle();
            }
        }

        /// <summary>
        /// Not giriþ dönemlerinin açýk olup olmadýðýný kontrol eder ve kullanýcýyý bilgilendirir
        /// </summary>
        private void TakvimDurumlariniKontrolEt()
        {
            _vizeKontrol = AkademikTakvimHelper.VizeNotuGirisiKontrol();
            _finalKontrol = AkademikTakvimHelper.FinalNotuGirisiKontrol();
            _butunlemeKontrol = AkademikTakvimHelper.ButunlemeNotuGirisiKontrol();

            // Dönem bilgisi
            string donemBilgisi = AkademikTakvimHelper.AktifDonemBilgisi();

            // Durum mesajý oluþtur
            string durumMesaji = $"<strong>{donemBilgisi}</strong><br/><ul class='mb-0'>";
            
            // Vize durumu
            durumMesaji += $"<li>Vize Notu Giriþi: {(_vizeKontrol.Acik ? "<span class='badge bg-success'>AÇIK</span>" : "<span class='badge bg-secondary'>KAPALI</span>")} - {_vizeKontrol.Mesaj}</li>";
            
            // Final durumu
            durumMesaji += $"<li>Final Notu Giriþi: {(_finalKontrol.Acik ? "<span class='badge bg-success'>AÇIK</span>" : "<span class='badge bg-secondary'>KAPALI</span>")} - {_finalKontrol.Mesaj}</li>";
            
            // Bütünleme durumu
            durumMesaji += $"<li>Bütünleme Notu Giriþi: {(_butunlemeKontrol.Acik ? "<span class='badge bg-success'>AÇIK</span>" : "<span class='badge bg-secondary'>KAPALI</span>")} - {_butunlemeKontrol.Mesaj}</li>";
            
            durumMesaji += "</ul>";

            // Herhangi biri açýk deðilse uyarý göster
            if (!_vizeKontrol.Acik && !_finalKontrol.Acik && !_butunlemeKontrol.Acik)
            {
                ErrorPanel.Visible = true;
                ErrorText.Text = "<i class='fas fa-calendar-times'></i> Þu anda hiçbir not giriþ dönemi açýk deðil.<br/>" + durumMesaji;
            }
            else
            {
                // En az biri açýk - bilgi mesajý göster
                SuccessPanel.Visible = true;
                SuccessText.Text = "<i class='fas fa-calendar-check'></i> " + durumMesaji;
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

            // Tarih kontrollerini güncelle
            _vizeKontrol = AkademikTakvimHelper.VizeNotuGirisiKontrol();
            _finalKontrol = AkademikTakvimHelper.FinalNotuGirisiKontrol();
            _butunlemeKontrol = AkademikTakvimHelper.ButunlemeNotuGirisiKontrol();

            // Hiçbir dönem açýk deðilse iþlem yapma
            if (!_vizeKontrol.Acik && !_finalKontrol.Acik && !_butunlemeKontrol.Acik)
            {
                SuccessPanel.Visible = false;
                ErrorPanel.Visible = true;
                ErrorText.Text = "<i class='fas fa-calendar-times'></i> Þu anda hiçbir not giriþ dönemi açýk deðil. Not giriþi yapýlamaz.";
                return;
            }

            string cs = ConfigurationManager.ConnectionStrings["UniversiteDB"].ConnectionString;
            string dersKodu = ddlDersler.SelectedValue;
            int kaydedilenSayisi = 0;
            string uyarilar = "";

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

                        // Mevcut notlarý al
                        decimal? mevcutVize = null;
                        decimal? mevcutFinal = null;
                        decimal? mevcutButunleme = null;

                        using (var getCmd = new MySqlCommand("SELECT vize_notu, final_notu, butunleme_notu FROM notlar WHERE ogrenci_id = @ogrenciId AND ders_kodu = @dersKodu", conn))
                        {
                            getCmd.Parameters.AddWithValue("@ogrenciId", ogrenciId);
                            getCmd.Parameters.AddWithValue("@dersKodu", dersKodu);
                            using (var reader = getCmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    mevcutVize = reader.IsDBNull(0) ? (decimal?)null : reader.GetDecimal(0);
                                    mevcutFinal = reader.IsDBNull(1) ? (decimal?)null : reader.GetDecimal(1);
                                    mevcutButunleme = reader.IsDBNull(2) ? (decimal?)null : reader.GetDecimal(2);
                                }
                            }
                        }

                        // Formdan gelen deðerler
                        decimal? yeniVize = ParseNullableDecimal(txtVize.Text);
                        decimal? yeniFinal = ParseNullableDecimal(txtFinal.Text);
                        decimal? yeniButunleme = ParseNullableDecimal(txtBut.Text);

                        // Dönem kontrolü ile hangi notlarýn deðiþtirilebileceðini belirle
                        decimal? kaydedilecekVize = mevcutVize;
                        decimal? kaydedilecekFinal = mevcutFinal;
                        decimal? kaydedilecekButunleme = mevcutButunleme;

                        // Vize notu - sadece vize dönemi açýksa deðiþtirilebilir
                        if (_vizeKontrol.Acik)
                        {
                            kaydedilecekVize = yeniVize;
                        }
                        else if (yeniVize != mevcutVize && yeniVize.HasValue)
                        {
                            // Vize deðiþtirilmeye çalýþýlýyor ama dönem kapalý
                            if (!uyarilar.Contains("Vize"))
                                uyarilar += "Vize notu giriþ dönemi kapalý olduðu için vize notlarý güncellenmedi. ";
                        }

                        // Final notu - sadece final dönemi açýksa deðiþtirilebilir
                        if (_finalKontrol.Acik)
                        {
                            kaydedilecekFinal = yeniFinal;
                        }
                        else if (yeniFinal != mevcutFinal && yeniFinal.HasValue)
                        {
                            if (!uyarilar.Contains("Final"))
                                uyarilar += "Final notu giriþ dönemi kapalý olduðu için final notlarý güncellenmedi. ";
                        }

                        // Bütünleme notu - sadece bütünleme dönemi açýksa deðiþtirilebilir
                        if (_butunlemeKontrol.Acik)
                        {
                            kaydedilecekButunleme = yeniButunleme;
                        }
                        else if (yeniButunleme != mevcutButunleme && yeniButunleme.HasValue)
                        {
                            if (!uyarilar.Contains("Bütünleme"))
                                uyarilar += "Bütünleme notu giriþ dönemi kapalý olduðu için bütünleme notlarý güncellenmedi. ";
                        }

                        // Ortalama ve harf notu hesapla
                        decimal? ortalama;
                        string harfNotu;
                        string durum;
                        HesaplaNot(kaydedilecekVize, kaydedilecekFinal, kaydedilecekButunleme, out ortalama, out harfNotu, out durum);

                        // Önce mevcut kayýt var mý kontrol et
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
                                updateCmd.Parameters.AddWithValue("@vize", (object)kaydedilecekVize ?? DBNull.Value);
                                updateCmd.Parameters.AddWithValue("@final", (object)kaydedilecekFinal ?? DBNull.Value);
                                updateCmd.Parameters.AddWithValue("@but", (object)kaydedilecekButunleme ?? DBNull.Value);
                                updateCmd.Parameters.AddWithValue("@ortalama", (object)ortalama ?? DBNull.Value);
                                updateCmd.Parameters.AddWithValue("@harf", (object)harfNotu ?? DBNull.Value);
                                updateCmd.Parameters.AddWithValue("@durum", (object)durum ?? DBNull.Value);
                                updateCmd.Parameters.AddWithValue("@notId", notId.Value);
                                updateCmd.ExecuteNonQuery();
                                kaydedilenSayisi++;
                            }
                        }
                        else
                        {
                            // Sadece en az bir not varsa yeni kayýt oluþtur
                            if (kaydedilecekVize.HasValue || kaydedilecekFinal.HasValue || kaydedilecekButunleme.HasValue)
                            {
                                using (var insertCmd = new MySqlCommand(@"INSERT INTO notlar
                                                                                (ogrenci_id, ders_kodu, vize_notu, final_notu, butunleme_notu, ortalama, harf_notu, durum, olusturma_tarihi, guncelleme_tarihi)
                                                                         VALUES (@ogrenciId, @dersKodu, @vize, @final, @but, @ortalama, @harf, @durum, NOW(), NOW())", conn))
                                {
                                    insertCmd.Parameters.AddWithValue("@ogrenciId", ogrenciId);
                                    insertCmd.Parameters.AddWithValue("@dersKodu", dersKodu);
                                    insertCmd.Parameters.AddWithValue("@vize", (object)kaydedilecekVize ?? DBNull.Value);
                                    insertCmd.Parameters.AddWithValue("@final", (object)kaydedilecekFinal ?? DBNull.Value);
                                    insertCmd.Parameters.AddWithValue("@but", (object)kaydedilecekButunleme ?? DBNull.Value);
                                    insertCmd.Parameters.AddWithValue("@ortalama", (object)ortalama ?? DBNull.Value);
                                    insertCmd.Parameters.AddWithValue("@harf", (object)harfNotu ?? DBNull.Value);
                                    insertCmd.Parameters.AddWithValue("@durum", (object)durum ?? DBNull.Value);
                                    insertCmd.ExecuteNonQuery();
                                    kaydedilenSayisi++;
                                }
                            }
                        }
                    }
                }

                // Sonuç mesajý
                if (kaydedilenSayisi > 0)
                {
                    SuccessPanel.Visible = true;
                    ErrorPanel.Visible = false;
                    SuccessText.Text = $"<i class='fas fa-check-circle'></i> {kaydedilenSayisi} öðrencinin notlarý kaydedildi.";
                    
                    if (!string.IsNullOrEmpty(uyarilar))
                    {
                        SuccessText.Text += $"<br/><small class='text-warning'><i class='fas fa-exclamation-triangle'></i> {uyarilar}</small>";
                    }
                }
                else
                {
                    ErrorPanel.Visible = true;
                    SuccessPanel.Visible = false;
                    ErrorText.Text = "Kaydedilecek not bulunamadý. " + uyarilar;
                }

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
