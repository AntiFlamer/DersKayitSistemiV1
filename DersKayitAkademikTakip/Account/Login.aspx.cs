using System;
using System.Web;
using System.Web.UI;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace DersKayitAkademikTakip.Account
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Register link kodları zaten silmiştin
        }

        protected void LogIn(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("=== LOGIN BUTON CLICK ===");
            System.Diagnostics.Debug.WriteLine($"Giris: {Email.Text}");
            System.Diagnostics.Debug.WriteLine($"Current URL: {Request.Url}");

            if (IsValid)
            {
                if (MySQLGirisKontrol(Email.Text, Password.Text))
                {
                    System.Diagnostics.Debug.WriteLine("=== LOGIN SUCCESS ===");
                    KullaniciBilgileriniSessionaKaydet(Email.Text);

                    // Rol bazlı yönlendirme
                    string rol = Session["Rol"]?.ToString();
                    string target = "";

                    switch (rol)
                    {
                        case "admin":
                            target = ResolveUrl("~/Admin/Default.aspx");
                            break;
                        case "hoca":
                            target = ResolveUrl("~/Hoca/Default.aspx");
                            break;
                        case "ogrenci":
                            target = ResolveUrl("~/Ogrenci/Default.aspx");
                            break;
                        default:
                            target = ResolveUrl("~/Default.aspx");
                            break;
                    }

                    System.Diagnostics.Debug.WriteLine($"Redirect to (resolved): {target} (Rol: {rol})");
                    Response.Redirect(target, false);
                    Context.ApplicationInstance.CompleteRequest();
                }
                else
                {
                    FailureText.Text = "Geçersiz e-posta/numara veya şifre!";
                    ErrorMessage.Visible = true;
                }
            }
        }

        private bool MySQLGirisKontrol(string girisDegeri, string sifre)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["UniversiteDB"].ConnectionString;

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT COUNT(*) FROM Kullanicilar 
                                     WHERE aktif = 1 
                                       AND sifre = @sifre
                                       AND (email = @giris OR kullanici_no = @giris)";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@giris", girisDegeri);
                    cmd.Parameters.AddWithValue("@sifre", sifre);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("MySQL giriş hatası: " + ex.Message);
                return false;
            }
        }

        private void KullaniciBilgileriniSessionaKaydet(string girisDegeri)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["UniversiteDB"].ConnectionString;

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT kullanici_id, ad, soyad, rol, email, kullanici_no
                                     FROM Kullanicilar
                                     WHERE email = @giris OR kullanici_no = @giris
                                     LIMIT 1";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@giris", girisDegeri);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Session["KullaniciID"] = reader["kullanici_id"];
                            Session["Ad"] = reader["ad"];
                            Session["Soyad"] = reader["soyad"];
                            Session["Rol"] = reader["rol"];
                            Session["Email"] = reader["email"];
                            Session["KullaniciNo"] = reader["kullanici_no"];

                            System.Diagnostics.Debug.WriteLine($"SESSION KAYDEDİLDİ: {Session["Rol"]} - {Session["Ad"]}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Session kaydetme hatası: " + ex.Message);
            }
        }
    }
}