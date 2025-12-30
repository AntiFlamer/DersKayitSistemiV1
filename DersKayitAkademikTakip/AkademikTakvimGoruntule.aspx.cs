using System;
using System.Web.UI;

namespace DersKayitAkademikTakip
{
    public partial class AkademikTakvimGoruntule : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TakvimiYukle();
            }
        }

        private void TakvimiYukle()
        {
            var takvim = AkademikTakvimHelper.AktifTakvimiGetir();

            if (takvim == null)
            {
                pnlTakvimYok.Visible = true;
                pnlTakvim.Visible = false;
                return;
            }

            pnlTakvimYok.Visible = false;
            pnlTakvim.Visible = true;

            // Dönem özeti
            donemBaslik.InnerText = $"{takvim.AkademikYil} - {takvim.DonemAdi}";
            akademikYil.InnerText = takvim.AkademikYil ?? "Akademik Yýl Belirtilmemiþ";
            donemAdi.InnerText = takvim.DonemAdi ?? "Dönem Adý Belirtilmemiþ";
            donemTarihi.InnerText = FormatTarihAraligi(takvim.DonemBaslangic, takvim.DonemBitis);

            // Kontrolleri al
            var dersKayit = AkademikTakvimHelper.DersKaydiKontrol();
            var vize = AkademikTakvimHelper.VizeNotuGirisiKontrol();
            var final = AkademikTakvimHelper.FinalNotuGirisiKontrol();
            var butunleme = AkademikTakvimHelper.ButunlemeNotuGirisiKontrol();

            // Durum kartlarýný ayarla
            AyarlaKart(cardDersKayit, iconDersKayit, badgeDersKayit, dersKayit.Acik);
            AyarlaKart(cardVize, iconVize, badgeVize, TarihAraligindaMi(takvim.VizeBaslangic, takvim.VizeBitis));
            AyarlaKart(cardFinal, iconFinal, badgeFinal, TarihAraligindaMi(takvim.FinalBaslangic, takvim.FinalBitis));
            AyarlaKart(cardButunleme, iconButunleme, badgeButunleme, TarihAraligindaMi(takvim.ButunlemeBaslangic, takvim.ButunlemeBitis));

            // Tablo verilerini doldur
            // Dönem
            tdDonemBaslangic.InnerText = FormatTarih(takvim.DonemBaslangic);
            tdDonemBitis.InnerText = FormatTarih(takvim.DonemBitis);

            // Ders Kaydý
            tdDersKayitBaslangic.InnerText = FormatTarih(takvim.DersKayitBaslangic);
            tdDersKayitBitis.InnerText = FormatTarih(takvim.DersKayitBitis);
            tdDersKayitDurum.InnerHtml = GetDurumBadge(takvim.DersKayitBaslangic, takvim.DersKayitBitis);

            // Vize
            tdVizeBaslangic.InnerText = FormatTarih(takvim.VizeBaslangic);
            tdVizeBitis.InnerText = FormatTarih(takvim.VizeBitis);
            tdVizeDurum.InnerHtml = GetDurumBadge(takvim.VizeBaslangic, takvim.VizeBitis);

            tdVizeNotGiris.InnerText = FormatTarih(takvim.VizeNotGirisBitis);
            tdVizeNotGirisDurum.InnerHtml = GetNotGirisDurumBadge(takvim.VizeBaslangic, takvim.VizeNotGirisBitis);

            // Final
            tdFinalBaslangic.InnerText = FormatTarih(takvim.FinalBaslangic);
            tdFinalBitis.InnerText = FormatTarih(takvim.FinalBitis);
            tdFinalDurum.InnerHtml = GetDurumBadge(takvim.FinalBaslangic, takvim.FinalBitis);

            tdFinalNotGiris.InnerText = FormatTarih(takvim.FinalNotGirisBitis);
            tdFinalNotGirisDurum.InnerHtml = GetNotGirisDurumBadge(takvim.FinalBaslangic, takvim.FinalNotGirisBitis);

            // Bütünleme
            tdButunlemeBaslangic.InnerText = FormatTarih(takvim.ButunlemeBaslangic);
            tdButunlemeBitis.InnerText = FormatTarih(takvim.ButunlemeBitis);
            tdButunlemeDurum.InnerHtml = GetDurumBadge(takvim.ButunlemeBaslangic, takvim.ButunlemeBitis);

            tdButunlemeNotGiris.InnerText = FormatTarih(takvim.ButunlemeNotGirisBitis);
            tdButunlemeNotGirisDurum.InnerHtml = GetNotGirisDurumBadge(takvim.ButunlemeBaslangic, takvim.ButunlemeNotGirisBitis);
        }

        private void AyarlaKart(System.Web.UI.HtmlControls.HtmlGenericControl card, 
                                System.Web.UI.HtmlControls.HtmlGenericControl icon,
                                System.Web.UI.HtmlControls.HtmlGenericControl badge, 
                                bool acik)
        {
            if (acik)
            {
                card.Attributes["class"] = "card h-100 text-center border-success";
                icon.Attributes["class"] = "fas fa-2x mb-2 text-success " + icon.Attributes["class"].Replace("fas fa-2x mb-2", "").Trim();
                badge.Attributes["class"] = "badge bg-success";
                badge.InnerText = "AÇIK";
            }
            else
            {
                card.Attributes["class"] = "card h-100 text-center border-secondary";
                icon.Attributes["class"] = "fas fa-2x mb-2 text-secondary " + icon.Attributes["class"].Replace("fas fa-2x mb-2", "").Trim();
                badge.Attributes["class"] = "badge bg-secondary";
                badge.InnerText = "KAPALI";
            }
        }

        private bool TarihAraligindaMi(DateTime? baslangic, DateTime? bitis)
        {
            if (!baslangic.HasValue || !bitis.HasValue)
                return false;

            DateTime bugun = DateTime.Today;
            return bugun >= baslangic.Value && bugun <= bitis.Value;
        }

        private string FormatTarih(DateTime? tarih)
        {
            if (!tarih.HasValue)
                return "Belirtilmemiþ";

            return tarih.Value.ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("tr-TR"));
        }

        private string FormatTarihAraligi(DateTime? baslangic, DateTime? bitis)
        {
            if (!baslangic.HasValue && !bitis.HasValue)
                return "Tarih belirtilmemiþ";

            if (!baslangic.HasValue)
                return $"... - {bitis.Value:dd.MM.yyyy}";

            if (!bitis.HasValue)
                return $"{baslangic.Value:dd.MM.yyyy} - ...";

            return $"{baslangic.Value:dd.MM.yyyy} - {bitis.Value:dd.MM.yyyy}";
        }

        private string GetDurumBadge(DateTime? baslangic, DateTime? bitis)
        {
            if (!baslangic.HasValue || !bitis.HasValue)
                return "<span class='badge bg-secondary'>Tarih Yok</span>";

            DateTime bugun = DateTime.Today;

            if (bugun < baslangic.Value)
            {
                int gun = (baslangic.Value - bugun).Days;
                return $"<span class='badge bg-info'>{gun} gün sonra</span>";
            }
            else if (bugun >= baslangic.Value && bugun <= bitis.Value)
            {
                int kalanGun = (bitis.Value - bugun).Days;
                return $"<span class='badge bg-success'>Devam Ediyor ({kalanGun} gün kaldý)</span>";
            }
            else
            {
                return "<span class='badge bg-secondary'>Sona Erdi</span>";
            }
        }

        private string GetNotGirisDurumBadge(DateTime? baslangic, DateTime? bitis)
        {
            if (!baslangic.HasValue || !bitis.HasValue)
                return "<span class='badge bg-secondary'>Tarih Yok</span>";

            DateTime bugun = DateTime.Today;

            if (bugun < baslangic.Value)
            {
                return "<span class='badge bg-info'>Baþlamadý</span>";
            }
            else if (bugun <= bitis.Value)
            {
                int kalanGun = (bitis.Value - bugun).Days;
                if (kalanGun <= 3)
                    return $"<span class='badge bg-warning text-dark'>Son {kalanGun} gün!</span>";
                return $"<span class='badge bg-success'>Açýk ({kalanGun} gün)</span>";
            }
            else
            {
                return "<span class='badge bg-secondary'>Kapandý</span>";
            }
        }
    }
}
