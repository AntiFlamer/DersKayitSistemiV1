using System;
using System.Web.UI;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace DersKayitAkademikTakip.Admin
{
    public partial class KullanicilarDuzenle : Page
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
                string kullaniciId = Request.QueryString["kullanici_id"];
                if (string.IsNullOrEmpty(kullaniciId))
                {
                    Response.Redirect("Kullanicilar.aspx");
                    return;
                }

                KullaniciBilgileriniYukle(Convert.ToInt32(kullaniciId));
            }
        }

        private void KullaniciBilgileriniYukle(int kullaniciId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["UniversiteDB"].ConnectionString;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = @"SELECT kullanici_id, tc_kimlik, ad, soyad, email, rol, aktif, kullanici_no 
                               FROM Kullanicilar 
                               WHERE kullanici_id = @id";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", kullaniciId);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        ViewState["kullanici_id"] = reader["kullanici_id"].ToString();
                        txtTC.Text = reader["tc_kimlik"].ToString();
                        txtAd.Text = reader["ad"].ToString();
                        txtSoyad.Text = reader["soyad"].ToString();
                        txtEmail.Text = reader["email"].ToString();
                        ddlRol.SelectedValue = reader["rol"].ToString();
                        chkAktif.Checked = Convert.ToBoolean(reader["aktif"]);
                        txtKullaniciNo.Text = reader["kullanici_no"].ToString();
                    }
                    else
                    {
                        Response.Redirect("Kullanicilar.aspx");
                    }
                }
            }
        }

        protected void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAd.Text) ||
                string.IsNullOrWhiteSpace(txtSoyad.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text))
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

                    string sifreKismi = string.Empty;
                    if (!string.IsNullOrWhiteSpace(txtSifre.Text))
                    {
                        sifreKismi = ", sifre = @sifre";
                    }

                    string query = @"UPDATE Kullanicilar 
                                     SET ad = @ad, soyad = @soyad, email = @email, rol = @rol, aktif = @aktif, kullanici_no = @kno" + sifreKismi +
                                   " WHERE kullanici_id = @id";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ad", txtAd.Text.Trim());
                    cmd.Parameters.AddWithValue("@soyad", txtSoyad.Text.Trim());
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@rol", ddlRol.SelectedValue);
                    cmd.Parameters.AddWithValue("@aktif", chkAktif.Checked);
                    cmd.Parameters.AddWithValue("@kno", string.IsNullOrWhiteSpace(txtKullaniciNo.Text) ? (object)DBNull.Value : txtKullaniciNo.Text.Trim());
                    cmd.Parameters.AddWithValue("@id", Convert.ToInt32(ViewState["kullanici_id"]));

                    if (!string.IsNullOrWhiteSpace(txtSifre.Text))
                    {
                        cmd.Parameters.AddWithValue("@sifre", txtSifre.Text.Trim());
                    }

                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                    {
                        SuccessPanel.Visible = true;
                        ErrorPanel.Visible = false;
                        SuccessText.Text = "Kullanýcý baþarýyla güncellendi!";
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorPanel.Visible = true;
                SuccessPanel.Visible = false;
                ErrorText.Text = "Hata: " + ex.Message;
            }
        }

        protected void btnIptal_Click(object sender, EventArgs e)
        {
            Response.Redirect("Kullanicilar.aspx");
        }
    }
}
