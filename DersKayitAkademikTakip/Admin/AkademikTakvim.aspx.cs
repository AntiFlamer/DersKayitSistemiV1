using System;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace DersKayitAkademikTakip.Admin
{
    public partial class AkademikTakvim : AdminBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TakvimleriYukle();
                AktifDonemiGoster();
                FormuTemizle();
            }
        }

        private void TakvimleriYukle()
        {
            string cs = ConfigurationManager.ConnectionStrings["UniversiteDB"].ConnectionString;
            using (var conn = new MySqlConnection(cs))
            {
                conn.Open();
                string sql = @"SELECT takvim_id, donem_adi, akademik_yil, donem_tipi, aktif 
                               FROM akademiktakvim 
                               ORDER BY olusturma_tarihi DESC";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    var da = new MySqlDataAdapter(cmd);
                    var dt = new DataTable();
                    da.Fill(dt);
                    gvTakvimler.DataSource = dt;
                    gvTakvimler.DataBind();
                }
            }
        }

        private void AktifDonemiGoster()
        {
            string cs = ConfigurationManager.ConnectionStrings["UniversiteDB"].ConnectionString;
            using (var conn = new MySqlConnection(cs))
            {
                conn.Open();
                string sql = @"SELECT donem_adi, akademik_yil, 
                                      ders_kayit_baslangic, ders_kayit_bitis,
                                      vize_baslangic, vize_not_giris_bitis,
                                      final_baslangic, final_not_giris_bitis,
                                      butunleme_baslangic, butunleme_not_giris_bitis
                               FROM akademiktakvim WHERE aktif = 1 LIMIT 1";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string donemAdi = reader["donem_adi"].ToString();
                            string akademikYil = reader["akademik_yil"].ToString();
                            
                            string bilgi = $"<strong>{donemAdi}</strong> ({akademikYil})<br/>";
                            
                            // Ders Kayýt durumu
                            DateTime bugun = DateTime.Today;
                            if (!reader.IsDBNull(reader.GetOrdinal("ders_kayit_baslangic")) && !reader.IsDBNull(reader.GetOrdinal("ders_kayit_bitis")))
                            {
                                var dkBaslangic = reader.GetDateTime("ders_kayit_baslangic");
                                var dkBitis = reader.GetDateTime("ders_kayit_bitis");
                                bool dersKaydiAcik = bugun >= dkBaslangic && bugun <= dkBitis;
                                bilgi += $"<small>Ders Kayýt: {dkBaslangic:dd.MM} - {dkBitis:dd.MM} " + 
                                        (dersKaydiAcik ? "<span class='badge bg-success'>AÇIK</span>" : "<span class='badge bg-secondary'>KAPALI</span>") + "</small><br/>";
                            }
                            
                            // Vize durumu
                            if (!reader.IsDBNull(reader.GetOrdinal("vize_baslangic")) && !reader.IsDBNull(reader.GetOrdinal("vize_not_giris_bitis")))
                            {
                                var vBaslangic = reader.GetDateTime("vize_baslangic");
                                var vNotBitis = reader.GetDateTime("vize_not_giris_bitis");
                                bool vizeAcik = bugun >= vBaslangic && bugun <= vNotBitis;
                                bilgi += $"<small>Vize Not Giriþi: {vBaslangic:dd.MM} - {vNotBitis:dd.MM} " + 
                                        (vizeAcik ? "<span class='badge bg-success'>AÇIK</span>" : "<span class='badge bg-secondary'>KAPALI</span>") + "</small><br/>";
                            }
                            
                            // Final durumu
                            if (!reader.IsDBNull(reader.GetOrdinal("final_baslangic")) && !reader.IsDBNull(reader.GetOrdinal("final_not_giris_bitis")))
                            {
                                var fBaslangic = reader.GetDateTime("final_baslangic");
                                var fNotBitis = reader.GetDateTime("final_not_giris_bitis");
                                bool finalAcik = bugun >= fBaslangic && bugun <= fNotBitis;
                                bilgi += $"<small>Final Not Giriþi: {fBaslangic:dd.MM} - {fNotBitis:dd.MM} " + 
                                        (finalAcik ? "<span class='badge bg-success'>AÇIK</span>" : "<span class='badge bg-secondary'>KAPALI</span>") + "</small><br/>";
                            }

                            // Bütünleme durumu
                            if (!reader.IsDBNull(reader.GetOrdinal("butunleme_baslangic")) && !reader.IsDBNull(reader.GetOrdinal("butunleme_not_giris_bitis")))
                            {
                                var bBaslangic = reader.GetDateTime("butunleme_baslangic");
                                var bNotBitis = reader.GetDateTime("butunleme_not_giris_bitis");
                                bool butAcik = bugun >= bBaslangic && bugun <= bNotBitis;
                                bilgi += $"<small>Bütünleme Not Giriþi: {bBaslangic:dd.MM} - {bNotBitis:dd.MM} " + 
                                        (butAcik ? "<span class='badge bg-success'>AÇIK</span>" : "<span class='badge bg-secondary'>KAPALI</span>") + "</small>";
                            }

                            lblAktifDonem.Text = bilgi;
                        }
                        else
                        {
                            lblAktifDonem.Text = "<span class='text-warning'>Aktif dönem tanýmlanmamýþ. Lütfen bir takvim oluþturun ve aktif yapýn.</span>";
                        }
                    }
                }
            }
        }

        protected void gvTakvimler_SelectedIndexChanged(object sender, EventArgs e)
        {
            int takvimId = Convert.ToInt32(gvTakvimler.SelectedDataKey.Value);
            TakvimiYukle(takvimId);
        }

        protected void gvTakvimler_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Select komutu için iþlem yok, SelectedIndexChanged tetikleniyor
        }

        private void TakvimiYukle(int takvimId)
        {
            string cs = ConfigurationManager.ConnectionStrings["UniversiteDB"].ConnectionString;
            using (var conn = new MySqlConnection(cs))
            {
                conn.Open();
                string sql = @"SELECT * FROM akademiktakvim WHERE takvim_id = @id";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", takvimId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            hfTakvimId.Value = takvimId.ToString();
                            lblFormBaslik.Text = "Takvim Düzenle";

                            txtDonemAdi.Text = reader["donem_adi"].ToString();
                            txtAkademikYil.Text = reader["akademik_yil"].ToString();
                            
                            string donemTipi = reader["donem_tipi"].ToString();
                            if (ddlDonemTipi.Items.FindByValue(donemTipi) != null)
                                ddlDonemTipi.SelectedValue = donemTipi;

                            // Tarih alanlarý
                            txtDersKayitBaslangic.Text = TarihFormatla(reader["ders_kayit_baslangic"]);
                            txtDersKayitBitis.Text = TarihFormatla(reader["ders_kayit_bitis"]);
                            
                            txtVizeBaslangic.Text = TarihFormatla(reader["vize_baslangic"]);
                            txtVizeBitis.Text = TarihFormatla(reader["vize_bitis"]);
                            txtVizeNotGirisBitis.Text = TarihFormatla(reader["vize_not_giris_bitis"]);
                            
                            txtFinalBaslangic.Text = TarihFormatla(reader["final_baslangic"]);
                            txtFinalBitis.Text = TarihFormatla(reader["final_bitis"]);
                            txtFinalNotGirisBitis.Text = TarihFormatla(reader["final_not_giris_bitis"]);
                            
                            txtButunlemeBaslangic.Text = TarihFormatla(reader["butunleme_baslangic"]);
                            txtButunlemeBitis.Text = TarihFormatla(reader["butunleme_bitis"]);
                            txtButunlemeNotGirisBitis.Text = TarihFormatla(reader["butunleme_not_giris_bitis"]);
                            
                            txtDonemBaslangic.Text = TarihFormatla(reader["donem_baslangic"]);
                            txtDonemBitis.Text = TarihFormatla(reader["donem_bitis"]);

                            chkAktifInput.Checked = Convert.ToBoolean(reader["aktif"]);

                            btnSil.Visible = true;
                        }
                    }
                }
            }
        }

        private string TarihFormatla(object value)
        {
            if (value == null || value == DBNull.Value)
                return "";
            return Convert.ToDateTime(value).ToString("yyyy-MM-dd");
        }

        protected void btnYeniTakvim_Click(object sender, EventArgs e)
        {
            FormuTemizle();
        }

        private void FormuTemizle()
        {
            hfTakvimId.Value = "0";
            lblFormBaslik.Text = "Yeni Takvim Ekle";

            txtDonemAdi.Text = "";
            txtAkademikYil.Text = "";
            ddlDonemTipi.SelectedIndex = 0;

            txtDersKayitBaslangic.Text = "";
            txtDersKayitBitis.Text = "";
            txtVizeBaslangic.Text = "";
            txtVizeBitis.Text = "";
            txtVizeNotGirisBitis.Text = "";
            txtFinalBaslangic.Text = "";
            txtFinalBitis.Text = "";
            txtFinalNotGirisBitis.Text = "";
            txtButunlemeBaslangic.Text = "";
            txtButunlemeBitis.Text = "";
            txtButunlemeNotGirisBitis.Text = "";
            txtDonemBaslangic.Text = "";
            txtDonemBitis.Text = "";

            chkAktifInput.Checked = false;
            btnSil.Visible = false;
            gvTakvimler.SelectedIndex = -1;
        }

        protected void btnKaydet_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDonemAdi.Text) || string.IsNullOrWhiteSpace(txtAkademikYil.Text))
            {
                ErrorPanel.Visible = true;
                ErrorText.Text = "Dönem adý ve akademik yýl zorunludur.";
                return;
            }

            string cs = ConfigurationManager.ConnectionStrings["UniversiteDB"].ConnectionString;
            int takvimId = Convert.ToInt32(hfTakvimId.Value);

            try
            {
                using (var conn = new MySqlConnection(cs))
                {
                    conn.Open();

                    // Eðer aktif yapýlacaksa, diðerlerini pasif yap
                    if (chkAktifInput.Checked)
                    {
                        using (var deaktif = new MySqlCommand("UPDATE akademiktakvim SET aktif = 0 WHERE aktif = 1", conn))
                        {
                            deaktif.ExecuteNonQuery();
                        }
                    }

                    string sql;
                    if (takvimId == 0)
                    {
                        // Yeni kayýt
                        sql = @"INSERT INTO akademiktakvim 
                                (donem_adi, akademik_yil, donem_tipi, 
                                 ders_kayit_baslangic, ders_kayit_bitis,
                                 vize_baslangic, vize_bitis, vize_not_giris_bitis,
                                 final_baslangic, final_bitis, final_not_giris_bitis,
                                 butunleme_baslangic, butunleme_bitis, butunleme_not_giris_bitis,
                                 donem_baslangic, donem_bitis, aktif, olusturma_tarihi)
                                VALUES 
                                (@donemAdi, @akademikYil, @donemTipi,
                                 @dkBaslangic, @dkBitis,
                                 @vBaslangic, @vBitis, @vNotBitis,
                                 @fBaslangic, @fBitis, @fNotBitis,
                                 @bBaslangic, @bBitis, @bNotBitis,
                                 @donemBaslangic, @donemBitis, @aktif, NOW())";
                    }
                    else
                    {
                        // Güncelleme
                        sql = @"UPDATE akademiktakvim SET
                                donem_adi = @donemAdi, akademik_yil = @akademikYil, donem_tipi = @donemTipi,
                                ders_kayit_baslangic = @dkBaslangic, ders_kayit_bitis = @dkBitis,
                                vize_baslangic = @vBaslangic, vize_bitis = @vBitis, vize_not_giris_bitis = @vNotBitis,
                                final_baslangic = @fBaslangic, final_bitis = @fBitis, final_not_giris_bitis = @fNotBitis,
                                butunleme_baslangic = @bBaslangic, butunleme_bitis = @bBitis, butunleme_not_giris_bitis = @bNotBitis,
                                donem_baslangic = @donemBaslangic, donem_bitis = @donemBitis, aktif = @aktif
                                WHERE takvim_id = @id";
                    }

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@donemAdi", txtDonemAdi.Text.Trim());
                        cmd.Parameters.AddWithValue("@akademikYil", txtAkademikYil.Text.Trim());
                        cmd.Parameters.AddWithValue("@donemTipi", ddlDonemTipi.SelectedValue);

                        cmd.Parameters.AddWithValue("@dkBaslangic", ParseDate(txtDersKayitBaslangic.Text));
                        cmd.Parameters.AddWithValue("@dkBitis", ParseDate(txtDersKayitBitis.Text));
                        
                        cmd.Parameters.AddWithValue("@vBaslangic", ParseDate(txtVizeBaslangic.Text));
                        cmd.Parameters.AddWithValue("@vBitis", ParseDate(txtVizeBitis.Text));
                        cmd.Parameters.AddWithValue("@vNotBitis", ParseDate(txtVizeNotGirisBitis.Text));
                        
                        cmd.Parameters.AddWithValue("@fBaslangic", ParseDate(txtFinalBaslangic.Text));
                        cmd.Parameters.AddWithValue("@fBitis", ParseDate(txtFinalBitis.Text));
                        cmd.Parameters.AddWithValue("@fNotBitis", ParseDate(txtFinalNotGirisBitis.Text));
                        
                        cmd.Parameters.AddWithValue("@bBaslangic", ParseDate(txtButunlemeBaslangic.Text));
                        cmd.Parameters.AddWithValue("@bBitis", ParseDate(txtButunlemeBitis.Text));
                        cmd.Parameters.AddWithValue("@bNotBitis", ParseDate(txtButunlemeNotGirisBitis.Text));
                        
                        cmd.Parameters.AddWithValue("@donemBaslangic", ParseDate(txtDonemBaslangic.Text));
                        cmd.Parameters.AddWithValue("@donemBitis", ParseDate(txtDonemBitis.Text));
                        
                        cmd.Parameters.AddWithValue("@aktif", chkAktifInput.Checked ? 1 : 0);
                        
                        if (takvimId > 0)
                            cmd.Parameters.AddWithValue("@id", takvimId);

                        cmd.ExecuteNonQuery();
                    }
                }

                SuccessPanel.Visible = true;
                ErrorPanel.Visible = false;
                SuccessText.Text = takvimId == 0 ? "Takvim baþarýyla eklendi." : "Takvim baþarýyla güncellendi.";

                TakvimleriYukle();
                AktifDonemiGoster();
                FormuTemizle();
            }
            catch (Exception ex)
            {
                ErrorPanel.Visible = true;
                SuccessPanel.Visible = false;
                ErrorText.Text = "Hata: " + ex.Message;
            }
        }

        private object ParseDate(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return DBNull.Value;
            
            DateTime dt;
            if (DateTime.TryParse(text, out dt))
                return dt;
            
            return DBNull.Value;
        }

        protected void btnSil_Click(object sender, EventArgs e)
        {
            int takvimId = Convert.ToInt32(hfTakvimId.Value);
            if (takvimId == 0)
                return;

            string cs = ConfigurationManager.ConnectionStrings["UniversiteDB"].ConnectionString;
            try
            {
                using (var conn = new MySqlConnection(cs))
                {
                    conn.Open();
                    using (var cmd = new MySqlCommand("DELETE FROM akademiktakvim WHERE takvim_id = @id", conn))
                    {
                        cmd.Parameters.AddWithValue("@id", takvimId);
                        cmd.ExecuteNonQuery();
                    }
                }

                SuccessPanel.Visible = true;
                ErrorPanel.Visible = false;
                SuccessText.Text = "Takvim silindi.";

                TakvimleriYukle();
                AktifDonemiGoster();
                FormuTemizle();
            }
            catch (Exception ex)
            {
                ErrorPanel.Visible = true;
                SuccessPanel.Visible = false;
                ErrorText.Text = "Hata: " + ex.Message;
            }
        }

        protected void btnIptal_Click(object sender, EventArgs e)
        {
            FormuTemizle();
            SuccessPanel.Visible = false;
            ErrorPanel.Visible = false;
        }
    }
}
