# �niversite Ders Kay�t Sistemi (ASP.NET Web Forms)

<p align="center">
  <img alt=".NET Framework" src="https://img.shields.io/badge/.NET%20Framework-4.8-5C2D91?logo=.net&logoColor=white" />
  <img alt="ASP.NET Web Forms" src="https://img.shields.io/badge/ASP.NET-Web%20Forms-512BD4?logo=dotnet&logoColor=white" />
  <img alt="C#" src="https://img.shields.io/badge/C%23-7.3-239120?logo=csharp&logoColor=white" />
  <img alt="MySQL" src="https://img.shields.io/badge/MySQL-8.x-4479A1?logo=mysql&logoColor=white" />
  <img alt="Bootstrap" src="https://img.shields.io/badge/Bootstrap-5-7952B3?logo=bootstrap&logoColor=white" />
  <img alt="jQuery" src="https://img.shields.io/badge/jQuery-3.7-0769AD?logo=jquery&logoColor=white" />
  <img alt="Platform" src="https://img.shields.io/badge/Platform-Windows-blue" />
</p>

Rol tabanl� (admin / hoca / ��renci) yetkilendirme ile kullan�c�, ders ve kay�t y�netimi yapan bir ASP.NET Web Forms uygulamas�.

## �zellikler

- Oturum (Session) tabanl� kimlik do�rulama
- Roller: `admin`, `hoca`, `ogrenci`
- Admin paneli
  - Kullan�c� y�netimi (listeleme, arama/filtre, d�zenleme, silme, aktif/pasif)
  - Ders y�netimi (listeleme, arama/filtre, d�zenleme, silme)
- D�zenleme i�lemleri i�in ayr� sayfalar
  - `Admin/DerslerDuzenle.aspx`
  - `Admin/KullanicilarDuzenle.aspx`
- Bootstrap 5 ve jQuery ile modern aray�z
- MySQL �zerinden veri y�netimi (MySql.Data)
- Anti-XSRF korumas� (`Site.Master`) ve parametreli sorgular

## Teknoloji Y���n�

- .NET Framework 4.8
- ASP.NET Web Forms
- C# 7.3
- MySQL (MySql.Data / Connector)
- Bootstrap 5, jQuery 3.7

## Proje Yap�s�

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

### �nko�ullar
- Windows + Visual Studio 2019/2022
- .NET Framework 4.8 Developer Pack
- MySQL Server 8.x

### Ad�mlar
1. Depoyu klonlay�n
   ```bash
   git clone https://github.com/AntiFlamer/DersKayitSistemiV1.git
   ```
2. Visual Studio ile ��z�m� a��n
3. `ConnectionStrings.config` i�indeki `UniversiteDB` ba�lant� bilgisini MySQL sunucunuza g�re g�ncelleyin
   ```xml
   <connectionStrings>
     <add name="UniversiteDB"
          connectionString="Server=localhost;Database=universite;Uid=root;Pwd=parola;CharSet=utf8;SslMode=None;" />
   </connectionStrings>
   ```
4. `DersKayitAkademikTakip` projesini ba�lang�� projesi yap�n ve �al��t�r�n

> Not: Varsay�lan tablolar: `Kullanicilar`, `Dersler`, `Kayitlar`. Gerekli �ema sizde farkl�ysa, sorgular� (Admin sayfalar�ndaki `.aspx.cs`) uyarlayabilirsiniz.

## Kullan�m

- Giri� yapt�ktan sonra �st men�de rol�n�ze g�re panel butonlar� g�r�n�r.
- Admin: `Admin/Default.aspx` �zerinden istatistiklere ve mod�llere eri�ir.
- Kullan�c�/Ders d�zenlemeleri ayr� sayfalarda yap�l�r:
  - `Admin/DerslerDuzenle.aspx?ders_kodu=XXX`
  - `Admin/KullanicilarDuzenle.aspx?kullanici_id=ID`

## Ekran G�r�nt�leri (�ste�e Ba�l�)

`docs/` klas�r� alt�na ekran g�r�nt�leri ekleyip burada referans verebilirsiniz:

```
/docs
  ?? login.png
  ?? admin-dashboard.png
  ?? users.png
  ?? courses.png
```

## Geli�tirme Notlar�

- Script ve stil yollar� `ResolveUrl` ve `runat="server"` ile ��z�mlenir. Sanal dizin alt�nda do�ru �al���r.
- Modallar yerine ayr� d�zenleme sayfalar� kullan�lm��t�r (stabil ve bak�m� kolay).
- Sorgularda `AddWithValue` kullan�larak parametre ge�ilir (SQL Injection�a kar�� g�venli).

## Katk�

- PR�lar kabul edilir. L�tfen d�zenlemeleri ayr� branch�te yap�n ve a��klay�c� bir PR mesaj� ekleyin.

## Lisans

Bu repoda lisans dosyas� yoksa, kullan�m ko�ullar� i�in repo sahibine dan���n.
