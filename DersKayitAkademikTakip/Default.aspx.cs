using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace DersKayitAkademikTakip
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TestMySQLConnection();
            }
        }
        private void TestMySQLConnection()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["UniversiteDB"].ConnectionString;

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Tablo sayısını test et
                    string query = "SELECT COUNT(*) FROM information_schema.tables WHERE table_schema = 'UniversiteDersKayitSistemi'";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    int tableCount = Convert.ToInt32(cmd.ExecuteScalar());

                    Response.Write($"✅ MySQL BAĞLANTISI BAŞARILI! {tableCount} tablo bulundu.");
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Response.Write("❌ MYSQL BAĞLANTI HATASI: " + ex.Message);
            }
        }
    }
}