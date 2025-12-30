using System;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace DersKayitAkademikTakip.Hoca
{
    /// <summary>
    /// Kayit Onay Sayfasi - AJAX (UpdatePanel) Ornegi
    /// 
    /// BU SAYFA NASIL CALISIR?
    /// =======================
    /// 1. Sayfa yuklendiginde bekleyen kayitlar listelenir
    /// 2. Hoca "Onayla" veya "Reddet" butonuna tiklar
    /// 3. UpdatePanel sayesinde sayfa YENILENMEDEN islem yapilir
    /// 4. Sadece tablo ve mesajlar guncellenir (Partial PostBack)
    /// 5. Kullanici deneyimi cok daha iyi olur
    /// 
    /// UPDATEPANEL AVANTAJLARI:
    /// - Sayfa yenilenmez (flickering yok)
    /// - Daha hizli islem
    /// - Kullanici scroll pozisyonunu kaybetmez
    /// - Profesyonel gorunum
    /// 
    /// AKADEMIK TAKVIM ENTEGRASYONU:
    /// - Kayýt onayý sadece ders kayýt döneminde yapýlabilir
    /// </summary>
    public partial class KayitOnay : HocaBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Akademik takvim kontrolü
                TakvimDurumunuKontrolEt();
                KayitlariYukle();
            }
        }

        /// <summary>
        /// Kayýt onay döneminin açýk olup olmadýðýný kontrol eder
        /// </summary>
        private void TakvimDurumunuKontrolEt()
        {
            var sonuc = AkademikTakvimHelper.KayitOnayKontrol();
            
            if (!sonuc.Acik)
            {
                // Kayýt onay dönemi kapalý
                ErrorPanel.Visible = true;
                ErrorText.Text = "<i class='fas fa-calendar-times'></i> " + sonuc.Mesaj + " <br/><small>Kayýt onay iþlemleri þu an yapýlamaz.</small>";
            }
        }

        /// <summary>
        /// Bekleyen kayitlari veritabanindan yukler ve GridView'e baglar
        /// </summary>
        private void KayitlariYukle()
        {
            string cs = ConfigurationManager.ConnectionStrings["UniversiteDB"].ConnectionString;
            int hocaId = Convert.ToInt32(Session["KullaniciID"]);

            using (var conn = new MySqlConnection(cs))
            {
                conn.Open();
                
                // Hocanin verdigi derslere yapilan bekleyen kayitlari getir
                string sql = @"SELECT k.kayit_id, k.ders_kodu, d.ders_adi, k.kayit_tarihi,
                                      CONCAT(o.ad, ' ', o.soyad, ' (', IFNULL(o.kullanici_no,''), ')') AS ogrenci_adi
                               FROM Kayitlar k
                               INNER JOIN Dersler d ON k.ders_kodu = d.ders_kodu
                               INNER JOIN Kullanicilar o ON k.ogrenci_id = o.kullanici_id
                               WHERE d.hoca_id = @hid AND k.durum = 'onay_bekliyor'
                               ORDER BY k.kayit_tarihi DESC";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@hid", hocaId);
                    var da = new MySqlDataAdapter(cmd);
                    var dt = new DataTable();
                    da.Fill(dt);
                    
                    // GridView'e bagla
                    gvKayitlar.DataSource = dt;
                    gvKayitlar.DataBind();
                    
                    // Bekleyen kayit sayisini goster
                    lblBekleyenSayisi.Text = dt.Rows.Count.ToString();
                }
            }
        }

        /// <summary>
        /// GridView'deki butonlara tiklandiginda calisir (Onayla/Reddet)
        /// 
        /// AJAX ILE NASIL CALISIR?
        /// 1. Kullanici butona tiklar
        /// 2. JavaScript confirm() ile onay alinir
        /// 3. UpdatePanel AJAX istegi gonderir (sayfa yenilenmez)
        /// 4. Bu metot sunucuda calisir
        /// 5. Veritabani guncellenir
        /// 6. Sadece UpdatePanel icindeki alan yenilenir
        /// </summary>
        protected void gvKayitlar_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Sadece Onayla ve Reddet komutlarini isle
            if (e.CommandName != "Onayla" && e.CommandName != "Reddet")
                return;

            // ÖNCELÝKLE AKADEMÝK TAKVÝM KONTROLÜ
            var takvimSonuc = AkademikTakvimHelper.KayitOnayKontrol();
            if (!takvimSonuc.Acik)
            {
                SuccessPanel.Visible = false;
                ErrorPanel.Visible = true;
                ErrorText.Text = "<i class='fas fa-calendar-times'></i> " + takvimSonuc.Mesaj + " <br/><small>Kayýt onay iþlemleri þu an yapýlamaz.</small>";
                return;
            }

            int kayitId;
            if (!int.TryParse(e.CommandArgument.ToString(), out kayitId))
                return;

            // Komuta gore yeni durumu belirle
            string yeniDurum = e.CommandName == "Onayla" ? "onaylandi" : "reddedildi";

            string cs = ConfigurationManager.ConnectionStrings["UniversiteDB"].ConnectionString;
            int hocaId = Convert.ToInt32(Session["KullaniciID"]);

            try
            {
                using (var conn = new MySqlConnection(cs))
                {
                    conn.Open();
                    
                    // Kayit durumunu guncelle (sadece hocanin kendi derslerini guncelleyebilir)
                    string sql = @"UPDATE Kayitlar k
                                   INNER JOIN Dersler d ON k.ders_kodu = d.ders_kodu
                                   SET k.durum = @durum
                                   WHERE k.kayit_id = @id AND d.hoca_id = @hid";

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@durum", yeniDurum);
                        cmd.Parameters.AddWithValue("@id", kayitId);
                        cmd.Parameters.AddWithValue("@hid", hocaId);

                        int affected = cmd.ExecuteNonQuery();
                        
                        if (affected > 0)
                        {
                            // Basari mesaji goster
                            SuccessPanel.Visible = true;
                            ErrorPanel.Visible = false;
                            SuccessText.Text = yeniDurum == "onaylandi" 
                                ? "<strong>Basarili!</strong> Kayit onaylandi." 
                                : "<strong>Basarili!</strong> Kayit reddedildi.";
                            
                            // Listeyi yenile (UpdatePanel sayesinde sadece tablo guncellenir)
                            KayitlariYukle();
                        }
                        else
                        {
                            // Hata mesaji goster
                            SuccessPanel.Visible = false;
                            ErrorPanel.Visible = true;
                            ErrorText.Text = "Islem yapilamadi. (Kayit bulunamadi veya yetkiniz yok)";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Exception durumunda hata mesaji goster
                SuccessPanel.Visible = false;
                ErrorPanel.Visible = true;
                ErrorText.Text = "Hata: " + ex.Message;
                
                // Hatayi logla
                System.Diagnostics.Debug.WriteLine("KayitOnay Hata: " + ex.ToString());
            }
        }
    }
}
