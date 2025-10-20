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
            // Sadece session kontrolü - query string YOK
            if (Session["KullaniciID"] == null || Session["Rol"] == null || Session["Rol"].ToString() != "admin")
            {
                System.Diagnostics.Debug.WriteLine($"=== REDIRECT - Session: KullaniciID={Session["KullaniciID"]}, Rol={Session["Rol"]} ===");
                Response.Redirect("~/Account/Login.aspx");
                return;
            }

            System.Diagnostics.Debug.WriteLine($"=== SESSION ÇALIŞIYOR: {Session["Ad"]} {Session["Soyad"]} ===");

            if (!IsPostBack)
            {
                // Normal sayfa yükleme işlemleri
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
                KullaniciBilgileriniYukle(kullaniciId);
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

        protected void gvKullanicilar_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // GridView stil ayarları
        }

        // KULLANICI DÜZENLEME
        private void KullaniciBilgileriniYukle(int kullaniciId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["UniversiteDB"].ConnectionString;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = @"SELECT kullanici_id, tc_kimlik, ad, soyad, email, rol, aktif 
                               FROM Kullanicilar 
                               WHERE kullanici_id = @id";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", kullaniciId);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        hfKullaniciId.Value = reader["kullanici_id"].ToString();
                        txtEditTC.Text = reader["tc_kimlik"].ToString();
                        txtEditAd.Text = reader["ad"].ToString();
                        txtEditSoyad.Text = reader["soyad"].ToString();
                        txtEditEmail.Text = reader["email"].ToString();
                        ddlEditRol.SelectedValue = reader["rol"].ToString();
                        chkEditAktif.Checked = Convert.ToBoolean(reader["aktif"]);
                        txtEditSifre.Text = ""; // Şifre alanı boş
                    }
                }
            }

            // Modal'ı göster
            ScriptManager.RegisterStartupScript(this, GetType(), "showModal", "window.shouldShowUserEditModal = true; showEditUserModal();", true);
        }

        protected void btnKullaniciGuncelle_Click(object sender, EventArgs e)
        {
            // Validasyon
            if (string.IsNullOrWhiteSpace(txtEditAd.Text) ||
                string.IsNullOrWhiteSpace(txtEditSoyad.Text) ||
                string.IsNullOrWhiteSpace(txtEditEmail.Text))
            {
                SuccessPanel.Visible = false;
                ErrorPanel.Visible = true;
                ErrorText.Text = "Lütfen zorunlu alanları doldurunuz! (*)";
                return;
            }

            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["UniversiteDB"].ConnectionString;

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"UPDATE Kullanicilar 
                                   SET ad = @ad, 
                                       soyad = @soyad, 
                                       email = @email, 
                                       rol = @rol, 
                                       aktif = @aktif";

                    // Şifre değiştirilecek mi?
                    if (!string.IsNullOrWhiteSpace(txtEditSifre.Text))
                    {
                        query += ", sifre = @sifre";
                    }

                    query += " WHERE kullanici_id = @id";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", Convert.ToInt32(hfKullaniciId.Value));
                    cmd.Parameters.AddWithValue("@ad", txtEditAd.Text.Trim());
                    cmd.Parameters.AddWithValue("@soyad", txtEditSoyad.Text.Trim());
                    cmd.Parameters.AddWithValue("@email", txtEditEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@rol", ddlEditRol.SelectedValue);
                    cmd.Parameters.AddWithValue("@aktif", chkEditAktif.Checked);

                    if (!string.IsNullOrWhiteSpace(txtEditSifre.Text))
                    {
                        cmd.Parameters.AddWithValue("@sifre", txtEditSifre.Text.Trim());
                    }

                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                    {
                        SuccessPanel.Visible = true;
                        ErrorPanel.Visible = false;
                        SuccessText.Text = "Kullanıcı başarıyla güncellendi!";
                        KullanicilariYukle();
                    }
                }
            }
            catch (MySqlException mysqlEx)
            {
                SuccessPanel.Visible = false;
                ErrorPanel.Visible = true;

                if (mysqlEx.Number == 1062) // Duplicate entry
                {
                    ErrorText.Text = "Bu e-posta adresi zaten kullanılıyor!";
                }
                else
                {
                    ErrorText.Text = "Veritabanı hatası: " + mysqlEx.Message;
                }
            }
            catch (Exception ex)
            {
                SuccessPanel.Visible = false;
                ErrorPanel.Visible = true;
                ErrorText.Text = "Hata: " + ex.Message;
            }
        }
    }
}