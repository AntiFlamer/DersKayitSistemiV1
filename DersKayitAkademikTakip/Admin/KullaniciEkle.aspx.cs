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

                    string query = @"INSERT INTO Kullanicilar 
                                    (tc_kimlik, ad, soyad, email, sifre, rol) 
                                    VALUES (@tc, @ad, @soyad, @email, @sifre, @rol)";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@tc", txtTC.Text);
                    cmd.Parameters.AddWithValue("@ad", txtAd.Text);
                    cmd.Parameters.AddWithValue("@soyad", txtSoyad.Text);
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@sifre", txtSifre.Text);
                    cmd.Parameters.AddWithValue("@rol", ddlRol.SelectedValue);

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