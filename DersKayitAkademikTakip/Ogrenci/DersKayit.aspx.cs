using System;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace DersKayitAkademikTakip.Ogrenci
{
    public partial class DersKayit : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KullaniciID"] == null || Session["Rol"] == null || Session["Rol"].ToString() != "ogrenci")
            {
                Response.Redirect("~/Account/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                // Akademik takvim kontrolü
                TakvimDurumunuKontrolEt();
                DersleriYukle(null);
            }
        }

        /// <summary>
        /// Ders kaydý döneminin açýk olup olmadýðýný kontrol eder ve kullanýcýyý bilgilendirir
        /// </summary>
        private void TakvimDurumunuKontrolEt()
        {
            var sonuc = AkademikTakvimHelper.DersKaydiKontrol();
            
            if (!sonuc.Acik)
            {
                // Ders kaydý kapalý - uyarý göster ve kayýt butonlarýný devre dýþý býrak
                ErrorPanel.Visible = true;
                ErrorText.Text = "<i class='fas fa-calendar-times'></i> " + sonuc.Mesaj;
            }
            else
            {
                // Ders kaydý açýk - bilgi mesajý göster
                SuccessPanel.Visible = true;
                SuccessText.Text = "<i class='fas fa-calendar-check'></i> " + sonuc.Mesaj;
            }
        }

        protected void btnAra_Click(object sender, EventArgs e)
        {
            DersleriYukle(txtArama.Text.Trim());
        }

        private void DersleriYukle(string arama)
        {
            string cs = ConfigurationManager.ConnectionStrings["UniversiteDB"].ConnectionString;
            using (var conn = new MySqlConnection(cs))
            {
                conn.Open();
                string sql = @"SELECT d.ders_kodu, d.ders_adi, d.kredi, d.akts_kredi, d.kontenjan,
                                       CONCAT(k.ad,' ',k.soyad) AS hoca_adi
                                FROM Dersler d
                                LEFT JOIN Kullanicilar k ON d.hoca_id = k.kullanici_id
                                WHERE (@ara IS NULL OR d.ders_kodu LIKE @like OR d.ders_adi LIKE @like)
                                ORDER BY d.ders_kodu";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    if (string.IsNullOrEmpty(arama))
                    {
                        cmd.Parameters.AddWithValue("@ara", DBNull.Value);
                        cmd.Parameters.AddWithValue("@like", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@ara", arama);
                        cmd.Parameters.AddWithValue("@like", "%" + arama + "%");
                    }

                    var da = new MySqlDataAdapter(cmd);
                    var dt = new DataTable();
                    da.Fill(dt);
                    gvDersler.DataSource = dt;
                    gvDersler.DataBind();
                }
            }
        }

        protected void gvDersler_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Kaydol")
            {
                string dersKodu = e.CommandArgument.ToString();
                KayitOl(dersKodu);
            }
        }

        private void KayitOl(string dersKodu)
        {
            // ÖNCELÝKLE AKADEMÝK TAKVÝM KONTROLÜ
            var takvimSonuc = AkademikTakvimHelper.DersKaydiKontrol();
            if (!takvimSonuc.Acik)
            {
                ErrorPanel.Visible = true;
                SuccessPanel.Visible = false;
                ErrorText.Text = "<i class='fas fa-calendar-times'></i> " + takvimSonuc.Mesaj;
                return;
            }

            string cs = ConfigurationManager.ConnectionStrings["UniversiteDB"].ConnectionString;
            int ogrenciId = Convert.ToInt32(Session["KullaniciID"]);

            try
            {
                using (var conn = new MySqlConnection(cs))
                {
                    conn.Open();

                    // Ayný derse mevcut kayýt var mý?
                    using (var check = new MySqlCommand("SELECT COUNT(*) FROM Kayitlar WHERE ogrenci_id=@o AND ders_kodu=@d", conn))
                    {
                        check.Parameters.AddWithValue("@o", ogrenciId);
                        check.Parameters.AddWithValue("@d", dersKodu);
                        int c = Convert.ToInt32(check.ExecuteScalar());
                        if (c > 0)
                        {
                            ErrorPanel.Visible = true;
                            SuccessPanel.Visible = false;
                            ErrorText.Text = "Bu derse zaten kaydýnýz var.";
                            return;
                        }
                    }

                    // Kontenjan kontrolü (opsiyonel)
                    using (var cnt = new MySqlCommand("SELECT kontenjan FROM Dersler WHERE ders_kodu=@d", conn))
                    {
                        cnt.Parameters.AddWithValue("@d", dersKodu);
                        object kontObj = cnt.ExecuteScalar();
                        if (kontObj != null && kontObj != DBNull.Value)
                        {
                            int kontenjan = Convert.ToInt32(kontObj);
                            using (var dolu = new MySqlCommand("SELECT COUNT(*) FROM Kayitlar WHERE ders_kodu=@d", conn))
                            {
                                dolu.Parameters.AddWithValue("@d", dersKodu);
                                int kayitli = Convert.ToInt32(dolu.ExecuteScalar());
                                if (kayitli >= kontenjan)
                                {
                                    ErrorPanel.Visible = true;
                                    SuccessPanel.Visible = false;
                                    ErrorText.Text = "Kontenjan dolu.";
                                    return;
                                }
                            }
                        }
                    }

                    // Kayýt ekle (durum: onay_bekliyor)
                    using (var ins = new MySqlCommand(
                        "INSERT INTO Kayitlar(ogrenci_id, ders_kodu, kayit_tarihi, durum) VALUES(@o,@d,NOW(),'onay_bekliyor')",
                        conn))
                    {
                        ins.Parameters.AddWithValue("@o", ogrenciId);
                        ins.Parameters.AddWithValue("@d", dersKodu);
                        int r = ins.ExecuteNonQuery();
                        if (r > 0)
                        {
                            SuccessPanel.Visible = true;
                            ErrorPanel.Visible = false;
                            SuccessText.Text = "Kayýt talebiniz oluþturuldu.";
                            DersleriYukle(txtArama.Text.Trim());
                        }
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
    }
}
