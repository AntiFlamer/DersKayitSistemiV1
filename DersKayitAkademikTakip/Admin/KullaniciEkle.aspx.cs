using System;
using System.Web.UI;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace DersKayitAkademikTakip.Admin
{
    public partial class KullaniciEkle : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Sadece session kontrolü - query string YOK
            if (Session["KullaniciID"] == null || Session["Rol"] == null || Session["Rol"].ToString() != "admin")
            {
                System.Diagnostics.Debug.WriteLine($"=== REDIRECT - Session: KullaniciID={Session["KullaniciID"]}, Rol={Session["Rol"]} ===");
                Response.Redirect("~/Account/Login.aspx");
                return;
            }

            System.Diagnostics.Debug.WriteLine($"=== SESSION ÇALIŞIYOR: {Session["Ad"]} {Session["Soyad"]} ===");
        }

        protected void btnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["UniversiteDB"].ConnectionString;

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string rol = ddlRol.SelectedValue;
                    string kullaniciNo = UretKullaniciNo(rol);

                    string query = @"INSERT INTO Kullanicilar 
                                    (tc_kimlik, ad, soyad, email, sifre, rol, kullanici_no) 
                                    VALUES (@tc, @ad, @soyad, @email, @sifre, @rol, @kno)";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@tc", txtTC.Text.Trim());
                    cmd.Parameters.AddWithValue("@ad", txtAd.Text.Trim());
                    cmd.Parameters.AddWithValue("@soyad", txtSoyad.Text.Trim());
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@sifre", txtSifre.Text.Trim());
                    cmd.Parameters.AddWithValue("@rol", rol);
                    cmd.Parameters.AddWithValue("@kno", kullaniciNo);

                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                    {
                        // Başarılı mesajı göster
                        SuccessPanel.Visible = true;
                        ErrorPanel.Visible = false;

                        // Formu temizle
                        FormuTemizle();
                    }
                }
            }
            catch (Exception ex)
            {
                // Hata mesajı göster
                SuccessPanel.Visible = false;
                ErrorPanel.Visible = true;
                ErrorText.Text = "Hata: " + ex.Message;
            }
        }

        private string UretKullaniciNo(string rol)
        {
            // İlk iki hane: yılın son iki hanesi
            int yil = DateTime.Now.Year;
            string yy = (yil % 100).ToString("D2");

            // Kalan 7 haneyi admin girebilsin: basitçe 0000001, 0000002... otomatik verelim
            // (İstersen burayı fakülte/bölüm kodu ile genişletebilirsin)

            string connectionString = ConfigurationManager.ConnectionStrings["UniversiteDB"].ConnectionString;
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"SELECT MAX(SUBSTRING(kullanici_no, 3, 7))
                                 FROM Kullanicilar
                                 WHERE kullanici_no LIKE @prefix";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    string prefix = yy + "%";
                    cmd.Parameters.AddWithValue("@prefix", prefix);

                    object result = cmd.ExecuteScalar();
                    int lastSeq = 0;
                    if (result != DBNull.Value && result != null)
                    {
                        int.TryParse(result.ToString(), out lastSeq);
                    }

                    int nextSeq = lastSeq + 1;
                    string seqPart = nextSeq.ToString("D7");
                    return yy + seqPart;
                }
            }
        }

        private void FormuTemizle()
        {
            txtTC.Text = "";
            txtAd.Text = "";
            txtSoyad.Text = "";
            txtEmail.Text = "";
            txtSifre.Text = "";
            ddlRol.SelectedIndex = 0;
        }
    }
}