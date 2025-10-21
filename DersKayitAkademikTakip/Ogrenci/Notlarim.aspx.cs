using System;
using System.Configuration;
using System.Data;
using System.Web.UI;
using MySql.Data.MySqlClient;

namespace DersKayitAkademikTakip.Ogrenci
{
    public partial class Notlarim : Page
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
                NotlariYukle();
            }
        }

        private void NotlariYukle()
        {
            string cs = ConfigurationManager.ConnectionStrings["UniversiteDB"].ConnectionString;
            int ogrenciId = Convert.ToInt32(Session["KullaniciID"]);
            try
            {
                using (var conn = new MySqlConnection(cs))
                {
                    conn.Open();
                    string sql = @"SELECT n.ders_kodu, d.ders_adi, n.vize, n.final, n.ortalama, n.harf_notu
                                   FROM notlar n
                                   INNER JOIN Dersler d ON n.ders_kodu = d.ders_kodu
                                   WHERE n.ogrenci_id = @o
                                   ORDER BY d.ders_kodu";
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@o", ogrenciId);
                        var da = new MySqlDataAdapter(cmd);
                        var dt = new DataTable();
                        da.Fill(dt);
                        gvNotlar.DataSource = dt;
                        gvNotlar.DataBind();
                    }
                }
            }
            catch (MySqlException)
            {
                // notlar tablosu henüz oluþturulmamýþsa ya da þema eksikse boþ liste göster
                var dt = new DataTable();
                dt.Columns.Add("ders_kodu");
                dt.Columns.Add("ders_adi");
                dt.Columns.Add("vize", typeof(int));
                dt.Columns.Add("final", typeof(int));
                dt.Columns.Add("ortalama", typeof(double));
                dt.Columns.Add("harf_notu");
                gvNotlar.DataSource = dt;
                gvNotlar.DataBind();
            }
            catch (Exception)
            {
                var dt = new DataTable();
                gvNotlar.DataSource = dt;
                gvNotlar.DataBind();
            }
        }
    }
}
