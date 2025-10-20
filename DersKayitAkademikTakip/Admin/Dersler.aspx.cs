using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace DersKayitAkademikTakip.Admin
{
    public partial class Dersler : Page
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
                DersleriYukle();
                HocaFiltresiYukle();
            }
        }

        private void DersleriYukle()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["UniversiteDB"].ConnectionString;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = @"SELECT d.ders_id, d.ders_kodu, d.ders_adi, d.kredi, d.akts_kredi, 
                               d.kontenjan, d.ders_donemi, d.ders_tipi, d.hoca_id,
                               CONCAT(k.ad, ' ', k.soyad) AS hoca_adi
                               FROM Dersler d
                               LEFT JOIN Kullanicilar k ON d.hoca_id = k.kullanici_id
                               ORDER BY d.ders_kodu";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvDersler.DataSource = dt;
                gvDersler.DataBind();
            }
        }

        private void HocaFiltresiYukle()
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

                ddlHocaFiltre.Items.Clear();
                ddlHocaFiltre.Items.Add(new ListItem("Tüm Hocalar", ""));

                while (reader.Read())
                {
                    ddlHocaFiltre.Items.Add(new ListItem(reader["ad_soyad"].ToString(), reader["kullanici_id"].ToString()));
                }

                reader.Close();
            }
        }

        private void HocalariYukleEdit()
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

                ddlEditHoca.Items.Clear();
                ddlEditHoca.Items.Add(new ListItem("-- Seçiniz --", ""));

                while (reader.Read())
                {
                    ddlEditHoca.Items.Add(new ListItem(reader["ad_soyad"].ToString(), reader["kullanici_id"].ToString()));
                }

                reader.Close();
            }
        }

        // DERS TÝPÝ BADGE METODLARI
        public string GetDersTipiBadgeClass(string dersTipi)
        {
            switch (dersTipi)
            {
                case "zorunlu": return "bg-danger";
                case "secmeli": return "bg-info";
                default: return "bg-secondary";
            }
        }

        public string GetDersTipiDisplayName(string dersTipi)
        {
            switch (dersTipi)
            {
                case "zorunlu": return "Zorunlu";
                case "secmeli": return "Seçmeli";
                default: return dersTipi;
            }
        }

        // ARAMA BUTONU
        protected void btnAra_Click(object sender, EventArgs e)
        {
            string aramaKelimesi = txtArama.Text.Trim();
            string dersTipiFiltre = ddlDersTipiFiltre.SelectedValue;
            string hocaFiltre = ddlHocaFiltre.SelectedValue;

            DersleriFiltrele(aramaKelimesi, dersTipiFiltre, hocaFiltre);
        }

        // FÝLTRE DEÐÝÞÝKLÝKLERÝ
        protected void ddlDersTipiFiltre_SelectedIndexChanged(object sender, EventArgs e)
        {
            string aramaKelimesi = txtArama.Text.Trim();
            string dersTipiFiltre = ddlDersTipiFiltre.SelectedValue;
            string hocaFiltre = ddlHocaFiltre.SelectedValue;

            DersleriFiltrele(aramaKelimesi, dersTipiFiltre, hocaFiltre);
        }

        protected void ddlHocaFiltre_SelectedIndexChanged(object sender, EventArgs e)
        {
            string aramaKelimesi = txtArama.Text.Trim();
            string dersTipiFiltre = ddlDersTipiFiltre.SelectedValue;
            string hocaFiltre = ddlHocaFiltre.SelectedValue;

            DersleriFiltrele(aramaKelimesi, dersTipiFiltre, hocaFiltre);
        }

        protected void btnYenile_Click(object sender, EventArgs e)
        {
            txtArama.Text = "";
            ddlDersTipiFiltre.SelectedIndex = 0;
            ddlHocaFiltre.SelectedIndex = 0;
            DersleriYukle();
        }

        private void DersleriFiltrele(string aramaKelimesi, string dersTipiFiltre, string hocaFiltre)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["UniversiteDB"].ConnectionString;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = @"SELECT d.ders_id, d.ders_kodu, d.ders_adi, d.kredi, d.akts_kredi, 
                               d.kontenjan, d.ders_donemi, d.ders_tipi, d.hoca_id,
                               CONCAT(k.ad, ' ', k.soyad) AS hoca_adi
                               FROM Dersler d
                               LEFT JOIN Kullanicilar k ON d.hoca_id = k.kullanici_id
                               WHERE 1=1";

                MySqlCommand cmd = new MySqlCommand(query, conn);

                if (!string.IsNullOrEmpty(aramaKelimesi))
                {
                    query += " AND (d.ders_kodu LIKE @arama OR d.ders_adi LIKE @arama)";
                    cmd.Parameters.AddWithValue("@arama", "%" + aramaKelimesi + "%");
                }

                if (!string.IsNullOrEmpty(dersTipiFiltre))
                {
                    query += " AND d.ders_tipi = @dersTipi";
                    cmd.Parameters.AddWithValue("@dersTipi", dersTipiFiltre);
                }

                if (!string.IsNullOrEmpty(hocaFiltre))
                {
                    query += " AND d.hoca_id = @hocaId";
                    cmd.Parameters.AddWithValue("@hocaId", Convert.ToInt32(hocaFiltre));
                }

                query += " ORDER BY d.ders_kodu";
                cmd.CommandText = query;

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvDersler.DataSource = dt;
                gvDersler.DataBind();
            }
        }

        // GRIDVIEW ÝÞLEMLERÝ
        protected void gvDersler_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Duzenle")
            {
                int dersId = Convert.ToInt32(e.CommandArgument);
                DersBilgileriniYukle(dersId);
            }
            else if (e.CommandName == "Sil")
            {
                int dersId = Convert.ToInt32(e.CommandArgument);
                DersSil(dersId);
            }
        }

        protected void gvDersler_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // Gerekirse ekstra styling eklenebilir
        }

        private void DersBilgileriniYukle(int dersId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["UniversiteDB"].ConnectionString;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = @"SELECT ders_id, ders_kodu, ders_adi, kredi, akts_kredi, 
                               kontenjan, ders_donemi, hoca_id, ders_tipi 
                               FROM Dersler 
                               WHERE ders_id = @id";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", dersId);

                int? hocaId = null;

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        hfDersId.Value = reader["ders_id"].ToString();
                        txtEditDersKodu.Text = reader["ders_kodu"].ToString();
                        txtEditDersAdi.Text = reader["ders_adi"].ToString();
                        txtEditKredi.Text = reader["kredi"].ToString();
                        txtEditAktsKredi.Text = reader["akts_kredi"].ToString();
                        txtEditKontenjan.Text = reader["kontenjan"].ToString();
                        txtEditDersDonemi.Text = reader["ders_donemi"].ToString();
                        ddlEditDersTipi.SelectedValue = reader["ders_tipi"].ToString();

                        // Hoca ID'sini kaydet
                        if (reader["hoca_id"] != DBNull.Value)
                        {
                            hocaId = Convert.ToInt32(reader["hoca_id"]);
                        }
                    }
                }

                // Hoca dropdown'unu yükle
                HocalariYukleEdit();

                // Seçili hocayý ayarla
                if (hocaId.HasValue)
                {
                    ddlEditHoca.SelectedValue = hocaId.Value.ToString();
                }
            }

            // Modal'ý göster
            ScriptManager.RegisterStartupScript(this, GetType(), "showModal", "showEditModal();", true);
        }

        protected void btnGuncelle_Click(object sender, EventArgs e)
        {
            // Validasyon
            if (string.IsNullOrWhiteSpace(txtEditDersKodu.Text) ||
                string.IsNullOrWhiteSpace(txtEditDersAdi.Text) ||
                string.IsNullOrWhiteSpace(txtEditKredi.Text) ||
                string.IsNullOrWhiteSpace(txtEditAktsKredi.Text) ||
                string.IsNullOrWhiteSpace(txtEditKontenjan.Text) ||
                string.IsNullOrWhiteSpace(txtEditDersDonemi.Text) ||
                string.IsNullOrWhiteSpace(ddlEditDersTipi.SelectedValue))
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

                    string query = @"UPDATE Dersler 
                                   SET ders_kodu = @ders_kodu, 
                                       ders_adi = @ders_adi, 
                                       kredi = @kredi, 
                                       akts_kredi = @akts_kredi, 
                                       kontenjan = @kontenjan, 
                                       ders_donemi = @ders_donemi, 
                                       hoca_id = @hoca_id, 
                                       ders_tipi = @ders_tipi 
                                   WHERE ders_id = @id";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", Convert.ToInt32(hfDersId.Value));
                    cmd.Parameters.AddWithValue("@ders_kodu", txtEditDersKodu.Text.Trim());
                    cmd.Parameters.AddWithValue("@ders_adi", txtEditDersAdi.Text.Trim());
                    cmd.Parameters.AddWithValue("@kredi", Convert.ToInt32(txtEditKredi.Text));
                    cmd.Parameters.AddWithValue("@akts_kredi", Convert.ToInt32(txtEditAktsKredi.Text));
                    cmd.Parameters.AddWithValue("@kontenjan", Convert.ToInt32(txtEditKontenjan.Text));
                    cmd.Parameters.AddWithValue("@ders_donemi", txtEditDersDonemi.Text.Trim());
                    cmd.Parameters.AddWithValue("@ders_tipi", ddlEditDersTipi.SelectedValue);

                    // Hoca seçilmemiþse NULL gönder
                    if (string.IsNullOrEmpty(ddlEditHoca.SelectedValue))
                    {
                        cmd.Parameters.AddWithValue("@hoca_id", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@hoca_id", Convert.ToInt32(ddlEditHoca.SelectedValue));
                    }

                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                    {
                        SuccessPanel.Visible = true;
                        ErrorPanel.Visible = false;
                        SuccessText.Text = "Ders baþarýyla güncellendi!";
                        DersleriYukle();
                    }
                }
            }
            catch (MySqlException mysqlEx)
            {
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
                SuccessPanel.Visible = false;
                ErrorPanel.Visible = true;
                ErrorText.Text = "Hata: " + ex.Message;
            }
        }

        private void DersSil(int dersId)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["UniversiteDB"].ConnectionString;

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Önce bu derse kayýtlý öðrenci var mý kontrol et
                    string checkQuery = "SELECT COUNT(*) FROM Kayitlar WHERE ders_id = @id";
                    MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("@id", dersId);
                    int kayitSayisi = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (kayitSayisi > 0)
                    {
                        SuccessPanel.Visible = false;
                        ErrorPanel.Visible = true;
                        ErrorText.Text = "Bu derse kayýtlý öðrenciler bulunmaktadýr. Önce kayýtlarý silmelisiniz!";
                        return;
                    }

                    // Kayýt yoksa dersi sil
                    string query = "DELETE FROM Dersler WHERE ders_id = @id";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", dersId);

                    cmd.ExecuteNonQuery();

                    SuccessPanel.Visible = true;
                    ErrorPanel.Visible = false;
                    SuccessText.Text = "Ders baþarýyla silindi!";
                    DersleriYukle();
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
