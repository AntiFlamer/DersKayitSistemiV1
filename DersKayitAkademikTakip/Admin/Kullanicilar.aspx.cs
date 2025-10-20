using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace DersKayitAkademikTakip.Admin
{
    public partial class Kullanicilar : Page
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
                KullanicilariYukle();
            }
        }

        private void KullanicilariYukle()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["UniversiteDB"].ConnectionString;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = @"SELECT kullanici_id, tc_kimlik, ad, soyad, email, rol, aktif, kayit_tarihi 
                               FROM Kullanicilar 
                               ORDER BY kayit_tarihi DESC";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvKullanicilar.DataSource = dt;
                gvKullanicilar.DataBind();
            }
        }

        // ROL BADGE RENK METODLARI
        public string GetRoleBadgeClass(string rol)
        {
            switch (rol)
            {
                case "admin": return "bg-danger";
                case "hoca": return "bg-primary";
                case "ogrenci": return "bg-success";
                default: return "bg-secondary";
            }
        }

        public string GetRoleDisplayName(string rol)
        {
            switch (rol)
            {
                case "admin": return "Admin";
                case "hoca": return "Öğretim Görevlisi";
                case "ogrenci": return "Öğrenci";
                default: return rol;
            }
        }

        // ARAMA BUTONU
        protected void btnAra_Click(object sender, EventArgs e)
        {
            string aramaKelimesi = txtArama.Text.Trim();
            string connectionString = ConfigurationManager.ConnectionStrings["UniversiteDB"].ConnectionString;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = @"SELECT kullanici_id, tc_kimlik, ad, soyad, email, rol, aktif, kayit_tarihi 
                               FROM Kullanicilar 
                               WHERE ad LIKE @arama OR soyad LIKE @arama OR email LIKE @arama
                               ORDER BY kayit_tarihi DESC";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@arama", "%" + aramaKelimesi + "%");

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvKullanicilar.DataSource = dt;
                gvKullanicilar.DataBind();
            }
        }

        // FİLTRELEME
        protected void ddlRolFiltre_SelectedIndexChanged(object sender, EventArgs e)
        {
            FiltreleVeYukle();
        }

        protected void ddlAktiflik_SelectedIndexChanged(object sender, EventArgs e)
        {
            FiltreleVeYukle();
        }

        private void FiltreleVeYukle()
        {
            string rolFiltre = ddlRolFiltre.SelectedValue;
            string aktiflikFiltre = ddlAktiflik.SelectedValue;
            string aramaKelimesi = txtArama.Text.Trim();

            string connectionString = ConfigurationManager.ConnectionStrings["UniversiteDB"].ConnectionString;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = @"SELECT kullanici_id, tc_kimlik, ad, soyad, email, rol, aktif, kayit_tarihi 
                               FROM Kullanicilar 
                               WHERE 1=1";

                MySqlCommand cmd = new MySqlCommand(query, conn);

                if (!string.IsNullOrEmpty(aramaKelimesi))
                {
                    query += " AND (ad LIKE @arama OR soyad LIKE @arama OR email LIKE @arama)";
                    cmd.Parameters.AddWithValue("@arama", "%" + aramaKelimesi + "%");
                }

                if (!string.IsNullOrEmpty(rolFiltre))
                {
                    query += " AND rol = @rol";
                    cmd.Parameters.AddWithValue("@rol", rolFiltre);
                }

                if (!string.IsNullOrEmpty(aktiflikFiltre))
                {
                    query += " AND aktif = @aktif";
                    cmd.Parameters.AddWithValue("@aktif", aktiflikFiltre == "1");
                }

                query += " ORDER BY kayit_tarihi DESC";
                cmd.CommandText = query;

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvKullanicilar.DataSource = dt;
                gvKullanicilar.DataBind();
            }
        }

        // GRIDVIEW İŞLEMLERİ
        protected void gvKullanicilar_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Duzenle")
            {
                int kullaniciId = Convert.ToInt32(e.CommandArgument);
                Response.Redirect($"KullanicilarDuzenle.aspx?kullanici_id={kullaniciId}");
            }
            else if (e.CommandName == "AktifPasif")
            {
                int kullaniciId = Convert.ToInt32(e.CommandArgument);
                AktifPasifDegistir(kullaniciId);
            }
            else if (e.CommandName == "Sil")
            {
                int kullaniciId = Convert.ToInt32(e.CommandArgument);
                KullaniciSil(kullaniciId);
            }
        }

        private void AktifPasifDegistir(int kullaniciId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["UniversiteDB"].ConnectionString;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = "UPDATE Kullanicilar SET aktif = NOT aktif WHERE kullanici_id = @id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", kullaniciId);

                cmd.ExecuteNonQuery();

                SuccessPanel.Visible = true;
                SuccessText.Text = "Kullanıcı durumu güncellendi!";
                KullanicilariYukle();
            }
        }

        private void KullaniciSil(int kullaniciId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["UniversiteDB"].ConnectionString;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = "DELETE FROM Kullanicilar WHERE kullanici_id = @id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", kullaniciId);

                cmd.ExecuteNonQuery();

                SuccessPanel.Visible = true;
                SuccessText.Text = "Kullanıcı başarıyla silindi!";
                KullanicilariYukle();
            }
        }

        protected void btnYenile_Click(object sender, EventArgs e)
        {
            txtArama.Text = "";
            ddlRolFiltre.SelectedIndex = 0;
            ddlAktiflik.SelectedIndex = 0;
            KullanicilariYukle();
        }

        // RowDataBound kullanılmıyor

        // KULLANICI DÜZENLEME

    }
}