using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Web.UI;

namespace DersKayitAkademikTakip
{
    public partial class Contact : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Sayfa ilk yuklendiginde yapilacak islem yok
        }

        /// <summary>
        /// Gonder butonuna tiklandiginda calisir.
        /// Form bilgilerini alir ve e-posta gonderir.
        /// </summary>
        protected void btnGonder_Click(object sender, EventArgs e)
        {
            // Validasyon kontrolu - form gecerli mi?
            if (!Page.IsValid)
                return;

            try
            {
                // Form verilerini al
                string adSoyad = txtAdSoyad.Text.Trim();
                string eposta = txtEposta.Text.Trim();
                string konu = ddlKonu.SelectedValue;
                string mesaj = txtMesaj.Text.Trim();

                // E-posta gonder
                EpostaGonder(adSoyad, eposta, konu, mesaj);

                // Basari durumunda
                pnlSuccess.Visible = true;
                pnlError.Visible = false;
                pnlForm.Visible = false; // Formu gizle

            }
            catch (Exception ex)
            {
                // Hata durumunda
                pnlSuccess.Visible = false;
                pnlError.Visible = true;
                litError.Text = "E-posta gonderilirken bir hata olustu: " + ex.Message;

                // Hatayi logla (Debug icin)
                System.Diagnostics.Debug.WriteLine("E-posta Hatasi: " + ex.ToString());
            }
        }

        /// <summary>
        /// SMTP kullanarak e-posta gonderir.
        /// 
        /// NASIL CALISIR?
        /// 1. Web.config'den SMTP ayarlarini okur
        /// 2. SmtpClient nesnesi olusturur (Gmail sunucusuna baglanir)
        /// 3. MailMessage nesnesi olusturur (e-posta icerigi)
        /// 4. E-postayi gonderir
        /// </summary>
        private void EpostaGonder(string adSoyad, string gonderenEposta, string konu, string mesaj)
        {
            // ===== ADIM 1: Web.config'den SMTP ayarlarini oku =====
            // ConfigurationManager, Web.config dosyasindaki <appSettings> bolumunu okur
            string smtpHost = ConfigurationManager.AppSettings["SmtpHost"];       // smtp.gmail.com
            int smtpPort = int.Parse(ConfigurationManager.AppSettings["SmtpPort"]); // 587
            string smtpUser = ConfigurationManager.AppSettings["SmtpUser"];       // senin@gmail.com
            string smtpPass = ConfigurationManager.AppSettings["SmtpPass"];       // uygulama sifresi
            bool enableSsl = bool.Parse(ConfigurationManager.AppSettings["SmtpEnableSsl"]); // true
            string adminEmail = ConfigurationManager.AppSettings["AdminEmail"];   // mesajin gidecegi adres

            // ===== ADIM 2: SmtpClient olustur (E-posta sunucusuna baglanti) =====
            // SmtpClient, e-posta gondermek icin SMTP sunucusuna baglanir
            // Gmail icin: smtp.gmail.com:587 (TLS ile guvenli baglanti)
            using (SmtpClient smtp = new SmtpClient(smtpHost, smtpPort))
            {
                // Kimlik dogrulama bilgileri (Gmail hesap bilgileri)
                smtp.Credentials = new NetworkCredential(smtpUser, smtpPass);
                
                // SSL/TLS guvenli baglanti (Gmail zorunlu kilar)
                smtp.EnableSsl = enableSsl;
                
                // Timeout suresi (30 saniye)
                smtp.Timeout = 30000;

                // ===== ADIM 3: MailMessage olustur (E-posta icerigi) =====
                using (MailMessage mail = new MailMessage())
                {
                    // Kimden (Gmail hesabin)
                    mail.From = new MailAddress(smtpUser, "Universite Iletisim Formu");
                    
                    // Kime (Admin e-posta adresi)
                    mail.To.Add(adminEmail);
                    
                    // E-posta konusu
                    mail.Subject = $"[Iletisim Formu] {konu} - {adSoyad}";
                    
                    // E-posta govdesi (HTML formatinda)
                    mail.IsBodyHtml = true;
                    mail.Body = OlusturEpostaIcerigi(adSoyad, gonderenEposta, konu, mesaj);
                    
                    // Yanit adresi (kullanicinin e-postasi)
                    // Bu sayede "Yanitla" dediginde kullaniciya gider
                    mail.ReplyToList.Add(new MailAddress(gonderenEposta, adSoyad));

                    // ===== ADIM 4: E-postayi gonder =====
                    smtp.Send(mail);
                }
            }
        }

        /// <summary>
        /// E-posta icerigini HTML formatinda olusturur.
        /// Profesyonel gorunumlu bir e-posta sablonu.
        /// </summary>
        private string OlusturEpostaIcerigi(string adSoyad, string eposta, string konu, string mesaj)
        {
            // HTML e-posta sablonu
            string html = $@"
<!DOCTYPE html>
<html>
<head>
    <style>
        body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; }}
        .container {{ max-width: 600px; margin: 0 auto; padding: 20px; }}
        .header {{ background: #0d6efd; color: white; padding: 20px; text-align: center; border-radius: 5px 5px 0 0; }}
        .content {{ background: #f8f9fa; padding: 20px; border: 1px solid #ddd; }}
        .field {{ margin-bottom: 15px; }}
        .label {{ font-weight: bold; color: #555; }}
        .value {{ background: white; padding: 10px; border-radius: 5px; margin-top: 5px; }}
        .footer {{ text-align: center; padding: 15px; color: #666; font-size: 12px; }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h2>Yeni Iletisim Formu Mesaji</h2>
        </div>
        <div class='content'>
            <div class='field'>
                <div class='label'>Ad Soyad:</div>
                <div class='value'>{WebUtility.HtmlEncode(adSoyad)}</div>
            </div>
            <div class='field'>
                <div class='label'>E-posta:</div>
                <div class='value'><a href='mailto:{WebUtility.HtmlEncode(eposta)}'>{WebUtility.HtmlEncode(eposta)}</a></div>
            </div>
            <div class='field'>
                <div class='label'>Konu:</div>
                <div class='value'>{WebUtility.HtmlEncode(konu)}</div>
            </div>
            <div class='field'>
                <div class='label'>Mesaj:</div>
                <div class='value'>{WebUtility.HtmlEncode(mesaj).Replace("\n", "<br/>")}</div>
            </div>
        </div>
        <div class='footer'>
            <p>Bu mesaj Universite Ders Kayit Sistemi iletisim formundan gonderilmistir.</p>
            <p>Tarih: {DateTime.Now:dd.MM.yyyy HH:mm}</p>
        </div>
    </div>
</body>
</html>";

            return html;
        }
    }
}