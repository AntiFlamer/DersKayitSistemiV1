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
            if (Session["KullaniciID"] == null || Session["Rol"] == null || Session["Rol"].ToString() != "admin")
            {
                Response.Redirect("~/Account/Login.aspx");
                return;
            }

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

                string query = @"SELECT d.ders_kodu, d.ders_adi, d.kredi, d.akts_kredi, 
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

                string query = @"SELECT d.ders_kodu, d.ders_adi, d.kredi, d.akts_kredi, 
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
                string dersKodu = e.CommandArgument.ToString();
                Response.Redirect($"DerslerDuzenle.aspx?ders_kodu={dersKodu}");
            }
            else if (e.CommandName == "Sil")
            {
                string dersKodu = e.CommandArgument.ToString();
                DersSil(dersKodu);
            }
        }

        // RowDataBound kullanýlmýyor



        private void DersSil(string dersKodu)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["UniversiteDB"].ConnectionString;

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Önce bu derse kayýtlý öðrenci var mý kontrol et
                    string checkQuery = "SELECT COUNT(*) FROM Kayitlar WHERE ders_kodu = @dersKodu";
                    MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("@dersKodu", dersKodu);
                    int kayitSayisi = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (kayitSayisi > 0)
                    {
                        SuccessPanel.Visible = false;
                        ErrorPanel.Visible = true;
                        ErrorText.Text = "Bu derse kayýtlý öðrenciler bulunmaktadýr. Önce kayýtlarý silmelisiniz!";
                        return;
                    }

                    // Kayýt yoksa dersi sil
                    string query = "DELETE FROM Dersler WHERE ders_kodu = @dersKodu";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@dersKodu", dersKodu);

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
