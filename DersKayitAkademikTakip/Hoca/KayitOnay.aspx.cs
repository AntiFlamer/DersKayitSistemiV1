using System;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace DersKayitAkademikTakip.Hoca
{
    public partial class KayitOnay : HocaBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                KayitlariYukle();
            }
        }

        private void KayitlariYukle()
        {
            string cs = ConfigurationManager.ConnectionStrings["UniversiteDB"].ConnectionString;
            int hocaId = Convert.ToInt32(Session["KullaniciID"]);

            using (var conn = new MySqlConnection(cs))
            {
                conn.Open();
                string sql = @"SELECT k.kayit_id, k.ders_kodu, d.ders_adi, k.kayit_tarihi,
                                       CONCAT(o.ad, ' ', o.soyad, ' (', IFNULL(o.kullanici_no,''), ')') AS ogrenci_adi
                                FROM Kayitlar k
                                INNER JOIN Dersler d ON k.ders_kodu = d.ders_kodu
                                INNER JOIN Kullanicilar o ON k.ogrenci_id = o.kullanici_id
                                WHERE d.hoca_id = @hid AND k.durum = 'onay_bekliyor'
                                ORDER BY k.kayit_tarihi DESC";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@hid", hocaId);
                    var da = new MySqlDataAdapter(cmd);
                    var dt = new DataTable();
                    da.Fill(dt);
                    gvKayitlar.DataSource = dt;
                    gvKayitlar.DataBind();
                }
            }
        }

        protected void gvKayitlar_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName != "Onayla" && e.CommandName != "Reddet")
                return;

            int kayitId;
            if (!int.TryParse(e.CommandArgument.ToString(), out kayitId))
                return;

            string yeniDurum = e.CommandName == "Onayla" ? "onaylandi" : "reddedildi";

            string cs = ConfigurationManager.ConnectionStrings["UniversiteDB"].ConnectionString;
            int hocaId = Convert.ToInt32(Session["KullaniciID"]);

            try
            {
                using (var conn = new MySqlConnection(cs))
                {
                    conn.Open();
                    string sql = @"UPDATE Kayitlar k
                                   INNER JOIN Dersler d ON k.ders_kodu = d.ders_kodu
                                   SET k.durum = @durum
                                   WHERE k.kayit_id = @id AND d.hoca_id = @hid";

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@durum", yeniDurum);
                        cmd.Parameters.AddWithValue("@id", kayitId);
                        cmd.Parameters.AddWithValue("@hid", hocaId);

                        int affected = cmd.ExecuteNonQuery();
                        if (affected > 0)
                        {
                            SuccessPanel.Visible = true;
                            ErrorPanel.Visible = false;
                            SuccessText.Text = yeniDurum == "onaylandi" ? "Kayýt onaylandý." : "Kayýt reddedildi.";
                            KayitlariYukle();
                        }
                        else
                        {
                            SuccessPanel.Visible = false;
                            ErrorPanel.Visible = true;
                            ErrorText.Text = "Ýþlem yapýlamadý. (Kayýt bulunamadý veya yetkiniz yok)";
                        }
                    }
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
