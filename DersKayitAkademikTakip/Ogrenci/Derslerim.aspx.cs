using System;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace DersKayitAkademikTakip.Ogrenci
{
    public partial class Derslerim : Page
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
                KayitlariYukle();
            }
        }

        /// <summary>
        /// Ders kaydý/iptal döneminin açýk olup olmadýðýný kontrol eder
        /// </summary>
        private void TakvimDurumunuKontrolEt()
        {
            var sonuc = AkademikTakvimHelper.DersKaydiKontrol();
            
            if (!sonuc.Acik)
            {
                // Ders kayýt/iptal dönemi kapalý - uyarý göster
                pnlUyari.Visible = true;
                lblUyari.Text = "<i class='fas fa-calendar-times'></i> " + sonuc.Mesaj + "<br/><small>Bu dönemde ders kaydý yapamaz veya iptal edemezsiniz.</small>";
            }
        }

        private void KayitlariYukle()
        {
            string cs = ConfigurationManager.ConnectionStrings["UniversiteDB"].ConnectionString;
            int ogrenciId = Convert.ToInt32(Session["KullaniciID"]);
            using (var conn = new MySqlConnection(cs))
            {
                conn.Open();
                string sql = @"SELECT k.kayit_id, k.ders_kodu, d.ders_adi, d.kredi, k.durum,
                                      CASE k.durum 
                                          WHEN 'onaylandi' THEN '<span class=''badge bg-success''>Onaylandý</span>'
                                          WHEN 'onay_bekliyor' THEN '<span class=''badge bg-warning''>Onay Bekliyor</span>'
                                          WHEN 'reddedildi' THEN '<span class=''badge bg-danger''>Reddedildi</span>'
                                          ELSE CONCAT('<span class=''badge bg-secondary''>', k.durum, '</span>')
                                      END AS durum_label
                               FROM Kayitlar k
                               INNER JOIN Dersler d ON k.ders_kodu = d.ders_kodu
                               WHERE k.ogrenci_id = @o
                               ORDER BY k.kayit_tarihi DESC";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@o", ogrenciId);
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
            if (e.CommandName == "Iptal")
            {
                // ÖNCELÝKLE AKADEMÝK TAKVÝM KONTROLÜ
                var takvimSonuc = AkademikTakvimHelper.DersKaydiKontrol();
                if (!takvimSonuc.Acik)
                {
                    pnlHata.Visible = true;
                    lblHata.Text = "<i class='fas fa-calendar-times'></i> " + takvimSonuc.Mesaj + "<br/><small>Ders kayýt dönemi dýþýnda kayýt iptal iþlemi yapýlamaz.</small>";
                    return;
                }

                int kayitId = Convert.ToInt32(e.CommandArgument);
                KaydiIptalEt(kayitId);
                KayitlariYukle();
            }
        }

        private void KaydiIptalEt(int kayitId)
        {
            string cs = ConfigurationManager.ConnectionStrings["UniversiteDB"].ConnectionString;
            int ogrenciId = Convert.ToInt32(Session["KullaniciID"]);
            using (var conn = new MySqlConnection(cs))
            {
                conn.Open();
                // Sadece bu öðrenciye ait ise iptal et
                string sql = "DELETE FROM Kayitlar WHERE kayit_id=@id AND ogrenci_id=@o";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", kayitId);
                    cmd.Parameters.AddWithValue("@o", ogrenciId);
                    int affected = cmd.ExecuteNonQuery();
                    
                    if (affected > 0)
                    {
                        pnlBasari.Visible = true;
                        lblBasari.Text = "<i class='fas fa-check-circle'></i> Ders kaydý baþarýyla iptal edildi.";
                        pnlHata.Visible = false;
                    }
                }
            }
        }
    }
}
