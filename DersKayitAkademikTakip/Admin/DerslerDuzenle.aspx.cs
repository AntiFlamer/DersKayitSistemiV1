using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace DersKayitAkademikTakip.Admin
{
    public partial class DerslerDuzenle : Page
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
                string dersKodu = Request.QueryString["ders_kodu"];
                if (string.IsNullOrEmpty(dersKodu))
                {
                    Response.Redirect("Dersler.aspx");
                    return;
                }

                HocalariYukle();
                DersBilgileriniYukle(dersKodu);
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

        private void DersBilgileriniYukle(string dersKodu)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["UniversiteDB"].ConnectionString;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = @"SELECT ders_kodu, ders_adi, kredi, akts_kredi, 
                               kontenjan, ders_donemi, hoca_id, ders_tipi 
                               FROM Dersler 
                               WHERE ders_kodu = @dersKodu";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@dersKodu", dersKodu);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        txtDersKodu.Text = reader["ders_kodu"].ToString();
                        txtDersAdi.Text = reader["ders_adi"].ToString();
                        txtKredi.Text = reader["kredi"].ToString();
                        txtAktsKredi.Text = reader["akts_kredi"].ToString();
                        txtKontenjan.Text = reader["kontenjan"].ToString();
                        txtDersDonemi.Text = reader["ders_donemi"].ToString();
                        ddlDersTipi.SelectedValue = reader["ders_tipi"].ToString();

                        if (reader["hoca_id"] != DBNull.Value)
                        {
                            ddlHoca.SelectedValue = reader["hoca_id"].ToString();
                        }
                    }
                    else
                    {
                        Response.Redirect("Dersler.aspx");
                    }
                }
            }
        }

        protected void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDersAdi.Text) ||
                string.IsNullOrWhiteSpace(txtKredi.Text) ||
                string.IsNullOrWhiteSpace(txtAktsKredi.Text) ||
                string.IsNullOrWhiteSpace(txtKontenjan.Text) ||
                string.IsNullOrWhiteSpace(txtDersDonemi.Text) ||
                string.IsNullOrWhiteSpace(ddlDersTipi.SelectedValue))
            {
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

                    string query = @"UPDATE Dersler 
                                   SET ders_adi = @ders_adi, 
                                       kredi = @kredi, 
                                       akts_kredi = @akts_kredi, 
                                       kontenjan = @kontenjan, 
                                       ders_donemi = @ders_donemi, 
                                       hoca_id = @hoca_id, 
                                       ders_tipi = @ders_tipi 
                                   WHERE ders_kodu = @ders_kodu";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ders_kodu", txtDersKodu.Text);
                    cmd.Parameters.AddWithValue("@ders_adi", txtDersAdi.Text.Trim());
                    cmd.Parameters.AddWithValue("@kredi", Convert.ToInt32(txtKredi.Text));
                    cmd.Parameters.AddWithValue("@akts_kredi", Convert.ToInt32(txtAktsKredi.Text));
                    cmd.Parameters.AddWithValue("@kontenjan", Convert.ToInt32(txtKontenjan.Text));
                    cmd.Parameters.AddWithValue("@ders_donemi", txtDersDonemi.Text.Trim());
                    cmd.Parameters.AddWithValue("@ders_tipi", ddlDersTipi.SelectedValue);

                    if (string.IsNullOrEmpty(ddlHoca.SelectedValue))
                    {
                        cmd.Parameters.AddWithValue("@hoca_id", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@hoca_id", Convert.ToInt32(ddlHoca.SelectedValue));
                    }

                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                    {
                        SuccessPanel.Visible = true;
                        SuccessText.Text = "Ders baþarýyla güncellendi! Yönlendiriliyorsunuz...";
                        Response.AddHeader("REFRESH", "2;URL=Dersler.aspx");
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorPanel.Visible = true;
                ErrorText.Text = "Hata: " + ex.Message;
            }
        }

        protected void btnIptal_Click(object sender, EventArgs e)
        {
            Response.Redirect("Dersler.aspx");
        }
    }
}
