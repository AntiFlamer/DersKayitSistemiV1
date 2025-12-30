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
                AkademikTakvimDurumunuYukle();
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

        private void AkademikTakvimDurumunuYukle()
        {
            var takvim = AkademikTakvimHelper.AktifTakvimiGetir();
            
            if (takvim == null)
            {
                aktifDonem.InnerText = "Tanımsız";
                takvimDurum.InnerHtml = @"<div class='alert alert-warning mb-0'>
                    <i class='fas fa-exclamation-triangle'></i> Aktif akademik takvim tanımlanmamış. 
                    <a href='AkademikTakvim.aspx' class='alert-link'>Takvim oluşturmak için tıklayın.</a>
                </div>";
                return;
            }

            // Aktif dönem başlığı
            aktifDonem.InnerText = takvim.DonemAdi ?? "Tanımsız";

            // Tarih kontrolleri
            DateTime bugun = DateTime.Today;
            var dersKayit = AkademikTakvimHelper.DersKaydiKontrol();
            var vize = AkademikTakvimHelper.VizeNotuGirisiKontrol();
            var final = AkademikTakvimHelper.FinalNotuGirisiKontrol();
            var butunleme = AkademikTakvimHelper.ButunlemeNotuGirisiKontrol();

            string html = "<div class='row'>";
            
            // Ders Kayıt Durumu
            html += $@"<div class='col-md-3'>
                <div class='card border-{(dersKayit.Acik ? "success" : "secondary")} mb-2'>
                    <div class='card-body p-2'>
                        <h6 class='card-title mb-1'><i class='fas fa-book'></i> Ders Kayıt</h6>
                        <span class='badge bg-{(dersKayit.Acik ? "success" : "secondary")}'>{(dersKayit.Acik ? "AÇIK" : "KAPALI")}</span>
                        <br/><small class='text-muted'>{FormatTarihAraligi(takvim.DersKayitBaslangic, takvim.DersKayitBitis)}</small>
                    </div>
                </div>
            </div>";

            // Vize Notu Girişi
            html += $@"<div class='col-md-3'>
                <div class='card border-{(vize.Acik ? "success" : "secondary")} mb-2'>
                    <div class='card-body p-2'>
                        <h6 class='card-title mb-1'><i class='fas fa-file-alt'></i> Vize Notu</h6>
                        <span class='badge bg-{(vize.Acik ? "success" : "secondary")}'>{(vize.Acik ? "AÇIK" : "KAPALI")}</span>
                        <br/><small class='text-muted'>{FormatTarihAraligi(takvim.VizeBaslangic, takvim.VizeNotGirisBitis)}</small>
                    </div>
                </div>
            </div>";

            // Final Notu Girişi
            html += $@"<div class='col-md-3'>
                <div class='card border-{(final.Acik ? "success" : "secondary")} mb-2'>
                    <div class='card-body p-2'>
                        <h6 class='card-title mb-1'><i class='fas fa-graduation-cap'></i> Final Notu</h6>
                        <span class='badge bg-{(final.Acik ? "success" : "secondary")}'>{(final.Acik ? "AÇIK" : "KAPALI")}</span>
                        <br/><small class='text-muted'>{FormatTarihAraligi(takvim.FinalBaslangic, takvim.FinalNotGirisBitis)}</small>
                    </div>
                </div>
            </div>";

            // Bütünleme Notu Girişi
            html += $@"<div class='col-md-3'>
                <div class='card border-{(butunleme.Acik ? "success" : "secondary")} mb-2'>
                    <div class='card-body p-2'>
                        <h6 class='card-title mb-1'><i class='fas fa-redo'></i> Bütünleme</h6>
                        <span class='badge bg-{(butunleme.Acik ? "success" : "secondary")}'>{(butunleme.Acik ? "AÇIK" : "KAPALI")}</span>
                        <br/><small class='text-muted'>{FormatTarihAraligi(takvim.ButunlemeBaslangic, takvim.ButunlemeNotGirisBitis)}</small>
                    </div>
                </div>
            </div>";

            html += "</div>";
            html += $"<small class='text-muted'>Dönem: {FormatTarihAraligi(takvim.DonemBaslangic, takvim.DonemBitis)}</small>";

            takvimDurum.InnerHtml = html;
        }

        private string FormatTarihAraligi(DateTime? baslangic, DateTime? bitis)
        {
            if (!baslangic.HasValue && !bitis.HasValue)
                return "Tarih belirtilmemiş";
            
            if (!baslangic.HasValue)
                return $"... - {bitis.Value:dd.MM.yyyy}";
            
            if (!bitis.HasValue)
                return $"{baslangic.Value:dd.MM.yyyy} - ...";
            
            return $"{baslangic.Value:dd.MM.yyyy} - {bitis.Value:dd.MM.yyyy}";
        }
    }
}