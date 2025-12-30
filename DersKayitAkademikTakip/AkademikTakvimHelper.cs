using System;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace DersKayitAkademikTakip
{
    /// <summary>
    /// Akademik Takvim Yardýmcý Sýnýfý
    /// 
    /// Bu sýnýf, ders kaydý, not giriþi gibi iþlemlerin
    /// akademik takvimde belirlenen tarih aralýklarýnda olup olmadýðýný kontrol eder.
    /// 
    /// KULLANIM:
    /// - AkademikTakvimHelper.DersKaydiAcikMi() -> Ders kaydý yapýlabilir mi?
    /// - AkademikTakvimHelper.VizeNotuGirisiAcikMi() -> Vize notu girilebilir mi?
    /// - AkademikTakvimHelper.FinalNotuGirisiAcikMi() -> Final notu girilebilir mi?
    /// - AkademikTakvimHelper.ButunlemeNotuGirisiAcikMi() -> Bütünleme notu girilebilir mi?
    /// </summary>
    public static class AkademikTakvimHelper
    {
        private static string ConnectionString => ConfigurationManager.ConnectionStrings["UniversiteDB"].ConnectionString;

        /// <summary>
        /// Aktif akademik takvimi veritabanýndan getirir
        /// </summary>
        public static AkademikTakvimBilgisi AktifTakvimiGetir()
        {
            try
            {
                using (var conn = new MySqlConnection(ConnectionString))
                {
                    conn.Open();
                    string sql = @"SELECT takvim_id, donem_adi, akademik_yil, donem_tipi,
                                          ders_kayit_baslangic, ders_kayit_bitis,
                                          vize_baslangic, vize_bitis, vize_not_giris_bitis,
                                          final_baslangic, final_bitis, final_not_giris_bitis,
                                          butunleme_baslangic, butunleme_bitis, butunleme_not_giris_bitis,
                                          donem_baslangic, donem_bitis
                                   FROM akademiktakvim 
                                   WHERE aktif = 1 
                                   ORDER BY takvim_id DESC 
                                   LIMIT 1";

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new AkademikTakvimBilgisi
                                {
                                    TakvimId = reader.GetInt32("takvim_id"),
                                    DonemAdi = reader.IsDBNull(reader.GetOrdinal("donem_adi")) ? null : reader.GetString("donem_adi"),
                                    AkademikYil = reader.IsDBNull(reader.GetOrdinal("akademik_yil")) ? null : reader.GetString("akademik_yil"),
                                    DonemTipi = reader.IsDBNull(reader.GetOrdinal("donem_tipi")) ? null : reader.GetString("donem_tipi"),
                                    DersKayitBaslangic = reader.IsDBNull(reader.GetOrdinal("ders_kayit_baslangic")) ? (DateTime?)null : reader.GetDateTime("ders_kayit_baslangic"),
                                    DersKayitBitis = reader.IsDBNull(reader.GetOrdinal("ders_kayit_bitis")) ? (DateTime?)null : reader.GetDateTime("ders_kayit_bitis"),
                                    VizeBaslangic = reader.IsDBNull(reader.GetOrdinal("vize_baslangic")) ? (DateTime?)null : reader.GetDateTime("vize_baslangic"),
                                    VizeBitis = reader.IsDBNull(reader.GetOrdinal("vize_bitis")) ? (DateTime?)null : reader.GetDateTime("vize_bitis"),
                                    VizeNotGirisBitis = reader.IsDBNull(reader.GetOrdinal("vize_not_giris_bitis")) ? (DateTime?)null : reader.GetDateTime("vize_not_giris_bitis"),
                                    FinalBaslangic = reader.IsDBNull(reader.GetOrdinal("final_baslangic")) ? (DateTime?)null : reader.GetDateTime("final_baslangic"),
                                    FinalBitis = reader.IsDBNull(reader.GetOrdinal("final_bitis")) ? (DateTime?)null : reader.GetDateTime("final_bitis"),
                                    FinalNotGirisBitis = reader.IsDBNull(reader.GetOrdinal("final_not_giris_bitis")) ? (DateTime?)null : reader.GetDateTime("final_not_giris_bitis"),
                                    ButunlemeBaslangic = reader.IsDBNull(reader.GetOrdinal("butunleme_baslangic")) ? (DateTime?)null : reader.GetDateTime("butunleme_baslangic"),
                                    ButunlemeBitis = reader.IsDBNull(reader.GetOrdinal("butunleme_bitis")) ? (DateTime?)null : reader.GetDateTime("butunleme_bitis"),
                                    ButunlemeNotGirisBitis = reader.IsDBNull(reader.GetOrdinal("butunleme_not_giris_bitis")) ? (DateTime?)null : reader.GetDateTime("butunleme_not_giris_bitis"),
                                    DonemBaslangic = reader.IsDBNull(reader.GetOrdinal("donem_baslangic")) ? (DateTime?)null : reader.GetDateTime("donem_baslangic"),
                                    DonemBitis = reader.IsDBNull(reader.GetOrdinal("donem_bitis")) ? (DateTime?)null : reader.GetDateTime("donem_bitis")
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("AkademikTakvimHelper Hata: " + ex.Message);
            }

            return null;
        }

        /// <summary>
        /// Ders kaydý döneminde mi kontrol eder
        /// </summary>
        public static TarihKontrolSonucu DersKaydiKontrol()
        {
            var takvim = AktifTakvimiGetir();
            if (takvim == null)
            {
                return new TarihKontrolSonucu
                {
                    Acik = false,
                    Mesaj = "Aktif akademik takvim bulunamadý. Lütfen yöneticiyle iletiþime geçin."
                };
            }

            DateTime bugun = DateTime.Today;

            if (!takvim.DersKayitBaslangic.HasValue || !takvim.DersKayitBitis.HasValue)
            {
                return new TarihKontrolSonucu
                {
                    Acik = false,
                    Mesaj = "Ders kayýt tarihleri henüz belirlenmemiþ."
                };
            }

            if (bugun < takvim.DersKayitBaslangic.Value)
            {
                return new TarihKontrolSonucu
                {
                    Acik = false,
                    Mesaj = $"Ders kayýt dönemi henüz baþlamadý. Baþlangýç: {takvim.DersKayitBaslangic.Value:dd.MM.yyyy}"
                };
            }

            if (bugun > takvim.DersKayitBitis.Value)
            {
                return new TarihKontrolSonucu
                {
                    Acik = false,
                    Mesaj = $"Ders kayýt dönemi sona erdi. Bitiþ: {takvim.DersKayitBitis.Value:dd.MM.yyyy}"
                };
            }

            return new TarihKontrolSonucu
            {
                Acik = true,
                Mesaj = $"Ders kayýt dönemi açýk. Bitiþ: {takvim.DersKayitBitis.Value:dd.MM.yyyy}"
            };
        }

        /// <summary>
        /// Ders kaydý açýk mý? (kýsa versiyon)
        /// </summary>
        public static bool DersKaydiAcikMi()
        {
            return DersKaydiKontrol().Acik;
        }

        /// <summary>
        /// Vize notu giriþi açýk mý kontrol eder
        /// Vize sýnavý baþlangýcýndan not giriþ bitiþine kadar açýk
        /// </summary>
        public static TarihKontrolSonucu VizeNotuGirisiKontrol()
        {
            var takvim = AktifTakvimiGetir();
            if (takvim == null)
            {
                return new TarihKontrolSonucu
                {
                    Acik = false,
                    Mesaj = "Aktif akademik takvim bulunamadý."
                };
            }

            DateTime bugun = DateTime.Today;

            if (!takvim.VizeBaslangic.HasValue || !takvim.VizeNotGirisBitis.HasValue)
            {
                return new TarihKontrolSonucu
                {
                    Acik = false,
                    Mesaj = "Vize tarihleri henüz belirlenmemiþ."
                };
            }

            if (bugun < takvim.VizeBaslangic.Value)
            {
                return new TarihKontrolSonucu
                {
                    Acik = false,
                    Mesaj = $"Vize notu giriþ dönemi henüz baþlamadý. Vize Baþlangýç: {takvim.VizeBaslangic.Value:dd.MM.yyyy}"
                };
            }

            if (bugun > takvim.VizeNotGirisBitis.Value)
            {
                return new TarihKontrolSonucu
                {
                    Acik = false,
                    Mesaj = $"Vize notu giriþ dönemi sona erdi. Not Giriþ Bitiþ: {takvim.VizeNotGirisBitis.Value:dd.MM.yyyy}"
                };
            }

            return new TarihKontrolSonucu
            {
                Acik = true,
                Mesaj = $"Vize notu giriþi açýk. Bitiþ: {takvim.VizeNotGirisBitis.Value:dd.MM.yyyy}"
            };
        }

        /// <summary>
        /// Final notu giriþi açýk mý kontrol eder
        /// </summary>
        public static TarihKontrolSonucu FinalNotuGirisiKontrol()
        {
            var takvim = AktifTakvimiGetir();
            if (takvim == null)
            {
                return new TarihKontrolSonucu
                {
                    Acik = false,
                    Mesaj = "Aktif akademik takvim bulunamadý."
                };
            }

            DateTime bugun = DateTime.Today;

            if (!takvim.FinalBaslangic.HasValue || !takvim.FinalNotGirisBitis.HasValue)
            {
                return new TarihKontrolSonucu
                {
                    Acik = false,
                    Mesaj = "Final tarihleri henüz belirlenmemiþ."
                };
            }

            if (bugun < takvim.FinalBaslangic.Value)
            {
                return new TarihKontrolSonucu
                {
                    Acik = false,
                    Mesaj = $"Final notu giriþ dönemi henüz baþlamadý. Final Baþlangýç: {takvim.FinalBaslangic.Value:dd.MM.yyyy}"
                };
            }

            if (bugun > takvim.FinalNotGirisBitis.Value)
            {
                return new TarihKontrolSonucu
                {
                    Acik = false,
                    Mesaj = $"Final notu giriþ dönemi sona erdi. Not Giriþ Bitiþ: {takvim.FinalNotGirisBitis.Value:dd.MM.yyyy}"
                };
            }

            return new TarihKontrolSonucu
            {
                Acik = true,
                Mesaj = $"Final notu giriþi açýk. Bitiþ: {takvim.FinalNotGirisBitis.Value:dd.MM.yyyy}"
            };
        }

        /// <summary>
        /// Bütünleme notu giriþi açýk mý kontrol eder
        /// </summary>
        public static TarihKontrolSonucu ButunlemeNotuGirisiKontrol()
        {
            var takvim = AktifTakvimiGetir();
            if (takvim == null)
            {
                return new TarihKontrolSonucu
                {
                    Acik = false,
                    Mesaj = "Aktif akademik takvim bulunamadý."
                };
            }

            DateTime bugun = DateTime.Today;

            if (!takvim.ButunlemeBaslangic.HasValue || !takvim.ButunlemeNotGirisBitis.HasValue)
            {
                return new TarihKontrolSonucu
                {
                    Acik = false,
                    Mesaj = "Bütünleme tarihleri henüz belirlenmemiþ."
                };
            }

            if (bugun < takvim.ButunlemeBaslangic.Value)
            {
                return new TarihKontrolSonucu
                {
                    Acik = false,
                    Mesaj = $"Bütünleme notu giriþ dönemi henüz baþlamadý. Bütünleme Baþlangýç: {takvim.ButunlemeBaslangic.Value:dd.MM.yyyy}"
                };
            }

            if (bugun > takvim.ButunlemeNotGirisBitis.Value)
            {
                return new TarihKontrolSonucu
                {
                    Acik = false,
                    Mesaj = $"Bütünleme notu giriþ dönemi sona erdi. Not Giriþ Bitiþ: {takvim.ButunlemeNotGirisBitis.Value:dd.MM.yyyy}"
                };
            }

            return new TarihKontrolSonucu
            {
                Acik = true,
                Mesaj = $"Bütünleme notu giriþi açýk. Bitiþ: {takvim.ButunlemeNotGirisBitis.Value:dd.MM.yyyy}"
            };
        }

        /// <summary>
        /// Kayýt onay iþlemi için kontrol (ders kaydý döneminde açýk)
        /// </summary>
        public static TarihKontrolSonucu KayitOnayKontrol()
        {
            // Kayýt onayý, ders kayýt döneminde yapýlabilir
            return DersKaydiKontrol();
        }

        /// <summary>
        /// Herhangi bir not giriþi açýk mý? (Vize, Final veya Bütünleme)
        /// </summary>
        public static bool HerhangiBirNotGirisiAcikMi()
        {
            return VizeNotuGirisiKontrol().Acik || 
                   FinalNotuGirisiKontrol().Acik || 
                   ButunlemeNotuGirisiKontrol().Acik;
        }

        /// <summary>
        /// Aktif dönem bilgisini string olarak döndürür
        /// </summary>
        public static string AktifDonemBilgisi()
        {
            var takvim = AktifTakvimiGetir();
            if (takvim == null)
                return "Aktif dönem yok";

            return $"{takvim.AkademikYil} - {takvim.DonemAdi}";
        }
    }

    /// <summary>
    /// Akademik Takvim Bilgisi Model Sýnýfý
    /// </summary>
    public class AkademikTakvimBilgisi
    {
        public int TakvimId { get; set; }
        public string DonemAdi { get; set; }
        public string AkademikYil { get; set; }
        public string DonemTipi { get; set; }
        
        public DateTime? DersKayitBaslangic { get; set; }
        public DateTime? DersKayitBitis { get; set; }
        
        public DateTime? VizeBaslangic { get; set; }
        public DateTime? VizeBitis { get; set; }
        public DateTime? VizeNotGirisBitis { get; set; }
        
        public DateTime? FinalBaslangic { get; set; }
        public DateTime? FinalBitis { get; set; }
        public DateTime? FinalNotGirisBitis { get; set; }
        
        public DateTime? ButunlemeBaslangic { get; set; }
        public DateTime? ButunlemeBitis { get; set; }
        public DateTime? ButunlemeNotGirisBitis { get; set; }
        
        public DateTime? DonemBaslangic { get; set; }
        public DateTime? DonemBitis { get; set; }
    }

    /// <summary>
    /// Tarih Kontrol Sonucu
    /// </summary>
    public class TarihKontrolSonucu
    {
        public bool Acik { get; set; }
        public string Mesaj { get; set; }
    }
}
