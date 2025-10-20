# Üniversite Ders Kayýt Sistemi (ASP.NET Web Forms)

<p align="center">
  <img alt=".NET Framework" src="https://img.shields.io/badge/.NET%20Framework-4.8-5C2D91?logo=.net&logoColor=white" />
  <img alt="ASP.NET Web Forms" src="https://img.shields.io/badge/ASP.NET-Web%20Forms-512BD4?logo=dotnet&logoColor=white" />
  <img alt="C#" src="https://img.shields.io/badge/C%23-7.3-239120?logo=csharp&logoColor=white" />
  <img alt="MySQL" src="https://img.shields.io/badge/MySQL-8.x-4479A1?logo=mysql&logoColor=white" />
  <img alt="Bootstrap" src="https://img.shields.io/badge/Bootstrap-5-7952B3?logo=bootstrap&logoColor=white" />
  <img alt="jQuery" src="https://img.shields.io/badge/jQuery-3.7-0769AD?logo=jquery&logoColor=white" />
  <img alt="Platform" src="https://img.shields.io/badge/Platform-Windows-blue" />
</p>

Rol tabanlý (admin / hoca / öðrenci) yetkilendirme ile kullanýcý, ders ve kayýt yönetimi yapan bir ASP.NET Web Forms uygulamasý.

## Özellikler

- Oturum (Session) tabanlý kimlik doðrulama
- Roller: `admin`, `hoca`, `ogrenci`
- Admin paneli
  - Kullanýcý yönetimi (listeleme, arama/filtre, düzenleme, silme, aktif/pasif)
  - Ders yönetimi (listeleme, arama/filtre, düzenleme, silme)
- Düzenleme iþlemleri için ayrý sayfalar
  - `Admin/DerslerDuzenle.aspx`
  - `Admin/KullanicilarDuzenle.aspx`
- Bootstrap 5 ve jQuery ile modern arayüz
- MySQL üzerinden veri yönetimi (MySql.Data)
- Anti-XSRF korumasý (`Site.Master`) ve parametreli sorgular

## Teknoloji Yýðýný

- .NET Framework 4.8
- ASP.NET Web Forms
- C# 7.3
- MySQL (MySql.Data / Connector)
- Bootstrap 5, jQuery 3.7

## Proje Yapýsý

```
DersKayitAkademikTakip/
?? Admin/
?  ?? Default.aspx(.cs)
?  ?? Dersler.aspx(.cs)
?  ?? DerslerDuzenle.aspx(.cs)
?  ?? Kullanicilar.aspx(.cs)
?  ?? KullanicilarDuzenle.aspx(.cs)
?  ?? KullaniciEkle.aspx(.cs)
?? Account/
?  ?? Login.aspx(.cs)
?  ?? Logout.aspx(.cs)
?? Ogrenci/
?  ?? Default.aspx(.cs)
?? App_Start/BundleConfig.cs
?? App_Code/CustomRoleProvider.cs
?? Site.Master(.cs)
?? Content/ (bootstrap.css, Site.css)
?? Scripts/ (jquery, bootstrap, WebForms js)
?? Web.config, ConnectionStrings.config
```

## Kurulum

### Önkoþullar
- Windows + Visual Studio 2019/2022
- .NET Framework 4.8 Developer Pack
- MySQL Server 8.x

### Adýmlar
1. Depoyu klonlayýn
   ```bash
   git clone https://github.com/AntiFlamer/DersKayitSistemiV1.git
   ```
2. Visual Studio ile çözümü açýn
3. `ConnectionStrings.config` içindeki `UniversiteDB` baðlantý bilgisini MySQL sunucunuza göre güncelleyin
   ```xml
   <connectionStrings>
     <add name="UniversiteDB"
          connectionString="Server=localhost;Database=universite;Uid=root;Pwd=parola;CharSet=utf8;SslMode=None;" />
   </connectionStrings>
   ```
4. `DersKayitAkademikTakip` projesini baþlangýç projesi yapýn ve çalýþtýrýn

> Not: Varsayýlan tablolar: `Kullanicilar`, `Dersler`, `Kayitlar`. Gerekli þema sizde farklýysa, sorgularý (Admin sayfalarýndaki `.aspx.cs`) uyarlayabilirsiniz.

## Kullaným

- Giriþ yaptýktan sonra üst menüde rolünüze göre panel butonlarý görünür.
- Admin: `Admin/Default.aspx` üzerinden istatistiklere ve modüllere eriþir.
- Kullanýcý/Ders düzenlemeleri ayrý sayfalarda yapýlýr:
  - `Admin/DerslerDuzenle.aspx?ders_kodu=XXX`
  - `Admin/KullanicilarDuzenle.aspx?kullanici_id=ID`

## Ekran Görüntüleri (Ýsteðe Baðlý)

`docs/` klasörü altýna ekran görüntüleri ekleyip burada referans verebilirsiniz:

```
/docs
  ?? login.png
  ?? admin-dashboard.png
  ?? users.png
  ?? courses.png
```

## Geliþtirme Notlarý

- Script ve stil yollarý `ResolveUrl` ve `runat="server"` ile çözümlenir. Sanal dizin altýnda doðru çalýþýr.
- Modallar yerine ayrý düzenleme sayfalarý kullanýlmýþtýr (stabil ve bakýmý kolay).
- Sorgularda `AddWithValue` kullanýlarak parametre geçilir (SQL Injection’a karþý güvenli).

## Katký

- PR’lar kabul edilir. Lütfen düzenlemeleri ayrý branch’te yapýn ve açýklayýcý bir PR mesajý ekleyin.

## Lisans

Bu repoda lisans dosyasý yoksa, kullaným koþullarý için repo sahibine danýþýn.
