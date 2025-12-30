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
        }

        protected void btnGonder_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            try
            {
                string adSoyad = txtAdSoyad.Text.Trim();
                string eposta = txtEposta.Text.Trim();
                string konu = ddlKonu.SelectedValue;
                string mesaj = txtMesaj.Text.Trim();

                EpostaGonder(adSoyad, eposta, konu, mesaj);

                pnlSuccess.Visible = true;
                pnlError.Visible = false;
                pnlForm.Visible = false;
            }
            catch (Exception ex)
            {
                pnlSuccess.Visible = false;
                pnlError.Visible = true;
                litError.Text = "E-posta gönderilirken bir hata oluştu: " + ex.Message;
            }
        }

        /// <summary>
        /// Web.config'deki SMTP ayarlarını kullanarak e-posta gönderme kısmı
        /// </summary>
        private void EpostaGonder(string adSoyad, string gonderenEposta, string konu, string mesaj)
        {
            string smtpHost = ConfigurationManager.AppSettings["SmtpHost"];
            int smtpPort = int.Parse(ConfigurationManager.AppSettings["SmtpPort"]);
            string smtpUser = ConfigurationManager.AppSettings["SmtpUser"];
            string smtpPass = ConfigurationManager.AppSettings["SmtpPass"];
            bool enableSsl = bool.Parse(ConfigurationManager.AppSettings["SmtpEnableSsl"]);
            string adminEmail = ConfigurationManager.AppSettings["AdminEmail"];

            using (SmtpClient smtp = new SmtpClient(smtpHost, smtpPort))
            {
                smtp.Credentials = new NetworkCredential(smtpUser, smtpPass);
                smtp.EnableSsl = enableSsl;
                smtp.Timeout = 30000;

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(smtpUser, "Üniversite İletişim Formu");
                    mail.To.Add(adminEmail);
                    mail.Subject = $"[İletişim Formu] {konu} - {adSoyad}";
                    mail.IsBodyHtml = true;
                    mail.Body = OlusturEpostaIcerigi(adSoyad, gonderenEposta, konu, mesaj);
                    mail.ReplyToList.Add(new MailAddress(gonderenEposta, adSoyad));

                    smtp.Send(mail);
                }
            }
        }

        /// <summary>
        /// HTML formatında e-posta içeriği oluşturma
        /// </summary>
        private string OlusturEpostaIcerigi(string adSoyad, string eposta, string konu, string mesaj)
        {
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
                        <h2>Yeni İletişim Formu Mesajı</h2>
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
                        <p>Bu mesaj Üniversite Ders Kayıt Sistemi iletişim formundan gönderilmiştir.</p>
                        <p>Tarih: {DateTime.Now:dd.MM.yyyy HH:mm}</p>
                    </div>
                </div>
            </body>
            </html>";

            return html;
        }
    }
}