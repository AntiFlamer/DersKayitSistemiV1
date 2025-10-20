using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace DersKayitAkademikTakip.Admin
{
    public partial class DersEkle : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Session kontrolü
            if (Session["KullaniciID"] == null || Session["Rol"] == null || Session["Rol"].ToString() != "admin")
            {
                System.Diagnostics.Debug.WriteLine($"=== REDIRECT - Session: KullaniciID={Session["KullaniciID"]}, Rol={Session["Rol"]} ===");
                Response.Redirect("~/Account/Login.aspx");
                return;
            }

            System.Diagnostics.Debug.WriteLine($"=== SESSION ÇALIÞIYOR: {Session["Ad"]} {Session["Soyad"]} ===");

            if (!IsPostBack)
            {
                HocalariYukle();
            }
        }

        private void HocalariYukle()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["UniversiteDB"].ConnectionString;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = @"SELECT kullanici_id, CONCAT(ad, ' ', soyad) AS ad_soyad 
                               FROM Kullanicilar 
                               WHERE rol = 'hoca' AND aktif = 1 
                               ORDER BY ad, soyad";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                ddlHoca.Items.Clear();
                ddlHoca.Items.Add(new ListItem("-- Seçiniz --", ""));

                while (reader.Read())
                {
                    ddlHoca.Items.Add(new ListItem(reader["ad_soyad"].ToString(), reader["kullanici_id"].ToString()));
                }

                reader.Close();
            }
        }

        protected void btnKaydet_Click(object sender, EventArgs e)
        {
            // Basit validasyon
            if (string.IsNullOrWhiteSpace(txtDersKodu.Text) ||
                string.IsNullOrWhiteSpace(txtDersAdi.Text) ||
                string.IsNullOrWhiteSpace(txtKredi.Text) ||
                string.IsNullOrWhiteSpace(txtAktsKredi.Text) ||
                string.IsNullOrWhiteSpace(txtKontenjan.Text) ||
                string.IsNullOrWhiteSpace(txtDersDonemi.Text) ||
                string.IsNullOrWhiteSpace(ddlDersTipi.SelectedValue))
            {
                SuccessPanel.Visible = false;
                ErrorPanel.Visible = true;
                ErrorText.Text = "Lütfen zorunlu alanlarý doldurunuz! (*)";
                return;
            }

            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["UniversiteDB"].ConnectionString;

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"INSERT INTO Dersler 
                                    (ders_kodu, ders_adi, kredi, akts_kredi, kontenjan, ders_donemi, hoca_id, ders_tipi) 
                                    VALUES (@ders_kodu, @ders_adi, @kredi, @akts_kredi, @kontenjan, @ders_donemi, @hoca_id, @ders_tipi)";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ders_kodu", txtDersKodu.Text.Trim());
                    cmd.Parameters.AddWithValue("@ders_adi", txtDersAdi.Text.Trim());
                    cmd.Parameters.AddWithValue("@kredi", Convert.ToInt32(txtKredi.Text));
                    cmd.Parameters.AddWithValue("@akts_kredi", Convert.ToInt32(txtAktsKredi.Text));
                    cmd.Parameters.AddWithValue("@kontenjan", Convert.ToInt32(txtKontenjan.Text));
                    cmd.Parameters.AddWithValue("@ders_donemi", txtDersDonemi.Text.Trim());
                    
                    // Hoca seçilmemiþse NULL gönder
                    if (string.IsNullOrEmpty(ddlHoca.SelectedValue))
                    {
                        cmd.Parameters.AddWithValue("@hoca_id", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@hoca_id", Convert.ToInt32(ddlHoca.SelectedValue));
                    }

                    cmd.Parameters.AddWithValue("@ders_tipi", ddlDersTipi.SelectedValue);

                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                    {
                        // Baþarýlý mesajý göster
                        SuccessPanel.Visible = true;
                        ErrorPanel.Visible = false;

                        // Formu temizle
                        FormuTemizle();
                    }
                }
            }
            catch (MySqlException mysqlEx)
            {
                // MySQL özel hatalar (duplicate key vb.)
                SuccessPanel.Visible = false;
                ErrorPanel.Visible = true;
                
                if (mysqlEx.Number == 1062) // Duplicate entry
                {
                    ErrorText.Text = "Bu ders kodu zaten mevcut! Lütfen farklý bir ders kodu giriniz.";
                }
                else
                {
                    ErrorText.Text = "Veritabaný hatasý: " + mysqlEx.Message;
                }
            }
            catch (Exception ex)
            {
                // Genel hatalar
                SuccessPanel.Visible = false;
                ErrorPanel.Visible = true;
                ErrorText.Text = "Hata: " + ex.Message;
            }
        }

        private void FormuTemizle()
        {
            txtDersKodu.Text = "";
            txtDersAdi.Text = "";
            txtKredi.Text = "";
            txtAktsKredi.Text = "";
            txtKontenjan.Text = "";
            txtDersDonemi.Text = "";
            ddlDersTipi.SelectedIndex = 0;
            ddlHoca.SelectedIndex = 0;
        }
    }
}
