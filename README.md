# Üniversite Ders Kayýt ve Akademik Takip Sistemi

<p align="center">
  <img alt=".NET Framework" src="https://img.shields.io/badge/.NET%20Framework-4.8-5C2D91?logo=.net&logoColor=white" />
  <img alt="ASP.NET Web Forms" src="https://img.shields.io/badge/ASP.NET-Web%20Forms-512BD4?logo=dotnet&logoColor=white" />
  <img alt="C#" src="https://img.shields.io/badge/C%23-7.3-239120?logo=csharp&logoColor=white" />
  <img alt="MySQL" src="https://img.shields.io/badge/MySQL-8.x-4479A1?logo=mysql&logoColor=white" />
  <img alt="Bootstrap" src="https://img.shields.io/badge/Bootstrap-5-7952B3?logo=bootstrap&logoColor=white" />
  <img alt="Font Awesome" src="https://img.shields.io/badge/Font%20Awesome-6.5-528DD7?logo=fontawesome&logoColor=white" />
  <img alt="jQuery" src="https://img.shields.io/badge/jQuery-3.7-0769AD?logo=jquery&logoColor=white" />
  <img alt="Platform" src="https://img.shields.io/badge/Platform-Windows-blue" />
</p>

<p align="center">
  <strong>Rol tabanlý (Admin / Öðretim Görevlisi / Öðrenci) yetkilendirme ile kullanýcý, ders ve kayýt yönetimi yapan kapsamlý bir ASP.NET Web Forms uygulamasý.</strong>
</p>

---

## Ýçindekiler

- [Özellikler](#özellikler)
- [Teknoloji Yýðýný](#teknoloji-yýðýný)
- [Proje Yapýsý](#proje-yapýsý)
- [Kurulum](#kurulum)
- [Veritabaný Þemasý](#veritabaný-þemasý)
- [Kullaným](#kullaným)
- [Ekran Görüntüleri](#ekran-görüntüleri)
- [Geliþtirme Notlarý](#geliþtirme-notlarý)
- [Katký](#katký)
- [Lisans](#lisans)

---

## Özellikler

### Kimlik Doðrulama ve Yetkilendirme
- Oturum (Session) tabanlý kimlik doðrulama
- E-posta veya kullanýcý numarasý ile giriþ
- Rol bazlý sayfa eriþim kontrolü
- Otomatik yönlendirme (rol bazlý dashboard)

### Kullanýcý Rolleri

| Rol | Yetkiler |
|-----|----------|
| **Admin** | Tam sistem yönetimi, kullanýcý ve ders CRUD iþlemleri |
| **Öðretim Görevlisi (Hoca)** | Ders kayýt onayý, not giriþi, ders istatistikleri |
| **Öðrenci** | Ders kaydý, kayýtlý dersleri görüntüleme, not takibi |

### Admin Paneli
- **Kullanýcý Yönetimi**
  - Kullanýcý listeleme, arama ve filtreleme
  - Yeni kullanýcý ekleme (otomatik kullanýcý numarasý üretimi)
  - Kullanýcý düzenleme ve silme
  - Aktif/Pasif durum yönetimi
- **Ders Yönetimi**
  - Ders listeleme, arama ve filtreleme
  - Yeni ders ekleme
  - Ders düzenleme ve silme
  - Öðretim görevlisi atama
- **Akademik Takvim**
  - Akademik dönem kaynaklý tarih aralýklarýný merkezi olarak yönetme
  - Aktif dönem tanýmlama (tek aktif kayýt)
  - Admin dashboard üzerinden süreç durumlarýný (ders kayýt / not giriþi) gösterme
- **Dashboard**
  - Toplam kullanýcý, ders ve kayýt istatistikleri
  - Rol bazlý kullanýcý daðýlýmý

### Öðretim Görevlisi Paneli
- **Ders Ýstatistikleri**
  - Verilen derslerin listesi
  - Kayýtlý öðrenci sayýlarý
  - Kontenjan durumu
- **Kayýt Onay**
  - Bekleyen ders kayýtlarýný görüntüleme
  - Kayýt onaylama/reddetme
- **Not Giriþi**
  - Vize, Final, Bütünleme not giriþi
  - Öðrenci bazlý not güncelleme

### Öðrenci Paneli
- **Ders Kaydý**
  - Açýk dersleri görüntüleme
  - Kontenjan kontrolü ile ders kaydý
  - Kayýt durumu takibi (Beklemede/Onaylý/Reddedildi)
- **Kayýtlý Derslerim**
  - Kayýtlý derslerin listesi
  - Ders detaylarý ve öðretim görevlisi bilgisi
- **Notlarým**
  - Vize, Final, Bütünleme notlarý
  - Genel Not Ortalamasý (GNO)

### Ýletiþim Formu (SMTP E-posta Gönderimi)
- **SMTP Protokolü ile E-posta Gönderimi**
  - Web.config üzerinden yapýlandýrýlabilir SMTP ayarlarý
  - Gmail, Outlook veya özel SMTP sunucu desteði
  - SSL/TLS þifreli güvenli baðlantý
  - HTML formatýnda profesyonel e-posta þablonu
  - Otomatik Reply-To adresi ayarlama
  - Konu kategorisi seçimi (Genel Bilgi, Teknik Destek, Öneri/Þikayet, Diðer)
  - Form doðrulama (Ad Soyad, E-posta, Mesaj)
  - Baþarý/Hata mesajlarý ile kullanýcý geri bildirimi

### Arayüz
- Bootstrap 5 ile modern ve responsive tasarým
- Font Awesome 6.5 ikonlarý
- Animasyonlu kart ve buton efektleri
- Mobil uyumlu navbar ve layout

---

## Teknoloji Yýðýný

| Kategori | Teknoloji |
|----------|-----------|
| **Backend** | .NET Framework 4.8, ASP.NET Web Forms, C# 7.3 |
| **Veritabaný** | MySQL 8.x (MySql.Data Connector) |
| **Frontend** | Bootstrap 5, jQuery 3.7, Font Awesome 6.5 |
| **E-posta** | SMTP Protokolü (System.Net.Mail) |
| **Güvenlik** | Session tabanlý auth, Parametreli sorgular, Anti-XSRF |

---

## Proje Yapýsý

```
DersKayitAkademikTakip/
|-- Account/
|   |-- Login.aspx(.cs)                # Giriþ sayfasý
|   +-- Logout.aspx(.cs)               # Çýkýþ iþlemi
|
|-- Admin/
|   |-- Default.aspx(.cs)              # Admin dashboard
|   |-- AkademikTakvim.aspx(.cs)       # Akademik takvim CRUD
|   |-- Kullanicilar.aspx(.cs)         # Kullanýcý listesi
|   |-- KullaniciEkle.aspx(.cs)        # Yeni kullanýcý ekleme
|   |-- KullanicilarDuzenle.aspx(.cs)  # Kullanýcý düzenleme
|   |-- Dersler.aspx(.cs)              # Ders listesi
|   |-- DersEkle.aspx(.cs)             # Yeni ders ekleme
|   |-- DerslerDuzenle.aspx(.cs)       # Ders düzenleme
|   |-- AdminBasePage.cs               # Admin sayfa base class
|   +-- Web.config                     # Admin klasör ayarlarý
|
|-- Hoca/
|   |-- Default.aspx(.cs)              # Hoca dashboard
|   |-- DersIstatistik.aspx(.cs)       # Ders istatistikleri
|   |-- KayitOnay.aspx(.cs)            # Kayýt onaylama
|   |-- NotGirisi.aspx(.cs)            # Not giriþi
|   +-- HocaBasePage.cs                # Hoca sayfa base class
|
|-- Ogrenci/
|   |-- Default.aspx(.cs)              # Öðrenci dashboard
|   |-- DersKayit.aspx(.cs)            # Ders kaydý
|   |-- Derslerim.aspx(.cs)            # Kayýtlý dersler
|   +-- Notlarim.aspx(.cs)             # Not görüntüleme
|
|-- App_Code/
|   |-- AkademikTakvimHelper.cs         # Akademik takvim kontrolleri ve model
|   +-- CustomRoleProvider.cs           # Özel rol saðlayýcý
|
|-- App_Start/
|   |-- BundleConfig.cs                 # Script/CSS bundle
|   |-- RouteConfig.cs                  # URL routing
|   +-- IdentityConfig.cs               # Identity ayarlarý
|
|-- Content/
|   |-- bootstrap.min.css               # Bootstrap stilleri
|   +-- Site.css                        # Özel stiller
|
|-- Scripts/
|   |-- jquery-3.7.1.min.js             # jQuery
|   +-- bootstrap.bundle.min.js         # Bootstrap JS
|
|-- AkademikTakvimGoruntule.aspx(.cs)   # Aktif akademik takvim görüntüleme
|-- Default.aspx(.cs)                   # Ana sayfa
|-- About.aspx(.cs)                     # Hakkýnda sayfasý
|-- Contact.aspx(.cs)                   # Ýletiþim sayfasý (SMTP e-posta)
|-- Site.Master(.cs)                    # Ana þablon
|-- Web.config                          # Ana konfigürasyon
|-- ConnectionStrings.config            # DB baðlantý bilgileri
+-- Global.asax(.cs)                    # Uygulama yaþam döngüsü
```

---

## Kurulum

### Önkoþullar
- Windows 10/11
- Visual Studio 2019/2022
- .NET Framework 4.8 Developer Pack
- MySQL Server 8.x
- MySQL Connector/NET

### Adýmlar

1. **Depoyu klonlayýn**
   ```bash
   git clone https://github.com/AntiFlamer/DersKayitSistemiV1.git
   cd DersKayitSistemiV1
   ```

2. **Visual Studio ile açýn**
   - `DersKayitAkademikTakip.sln` dosyasýný açýn

3. **Veritabaný baðlantýsýný ayarlayýn**

   `ConnectionStrings.config` dosyasýný düzenleyin:
   ```xml
   <connectionStrings>
     <add name="UniversiteDB"
          connectionString="Server=localhost;Database=universite;Uid=root;Pwd=YOUR_PASSWORD;CharSet=utf8;SslMode=None;"
          providerName="MySql.Data.MySqlClient" />
   </connectionStrings>
   ```

4. **SMTP Ayarlarýný Yapýlandýrýn**

   `Web.config` dosyasýna aþaðýdaki SMTP ayarlarýný ekleyin:
   ```xml
   <appSettings>
     <!-- SMTP E-posta Ayarlarý -->
     <add key="SmtpHost" value="smtp.gmail.com" />
     <add key="SmtpPort" value="587" />
     <add key="SmtpUser" value="your-email@gmail.com" />
     <add key="SmtpPass" value="your-app-password" />
     <add key="SmtpEnableSsl" value="true" />
     <add key="AdminEmail" value="admin@universite.edu.tr" />
   </appSettings>
   ```

   > **Not:** Gmail kullanýyorsanýz, "Uygulama Þifreleri" oluþturmanýz gerekir. Google Hesabý -> Güvenlik -> 2 Adýmlý Doðrulama -> Uygulama Þifreleri bölümünden oluþturabilirsiniz.

5. **Veritabaný tablolarýný oluþturun** (aþaðýdaki þemaya bakýn)

6. **Projeyi çalýþtýrýn**
   - `F5` veya `Ctrl+F5` ile baþlatýn

---

## Veritabaný Þemasý

### Kullanýcýlar Tablosu
```sql
CREATE TABLE Kullanicilar (
    kullanici_id INT AUTO_INCREMENT PRIMARY KEY,
    tc_kimlik VARCHAR(11) NOT NULL,
    ad VARCHAR(50) NOT NULL,
    soyad VARCHAR(50) NOT NULL,
    email VARCHAR(100) NOT NULL UNIQUE,
    sifre VARCHAR(255) NOT NULL,
    rol ENUM('admin', 'hoca', 'ogrenci') NOT NULL,
    kullanici_no VARCHAR(20) NOT NULL UNIQUE,
    aktif TINYINT(1) DEFAULT 1,
    kayit_tarihi DATETIME DEFAULT CURRENT_TIMESTAMP
);
```

### Dersler Tablosu
```sql
CREATE TABLE Dersler (
    ders_id INT AUTO_INCREMENT PRIMARY KEY,
    ders_kodu VARCHAR(20) NOT NULL UNIQUE,
    ders_adi VARCHAR(100) NOT NULL,
    kredi INT NOT NULL,
    akts_kredi INT NOT NULL,
    kontenjan INT NOT NULL,
    ders_donemi VARCHAR(20),
    ders_tipi ENUM('zorunlu', 'secmeli') DEFAULT 'zorunlu',
    hoca_id INT,
    aktif TINYINT(1) DEFAULT 1,
    FOREIGN KEY (hoca_id) REFERENCES Kullanicilar(kullanici_id)
);
```

### Kayýtlar Tablosu
```sql
CREATE TABLE Kayitlar (
    kayit_id INT AUTO_INCREMENT PRIMARY KEY,
    ogrenci_id INT NOT NULL,
    ders_id INT NOT NULL,
    kayit_tarihi DATETIME DEFAULT CURRENT_TIMESTAMP,
    durum ENUM('beklemede', 'onaylandi', 'reddedildi') DEFAULT 'beklemede',
    vize_notu DECIMAL(5,2),
    final_notu DECIMAL(5,2),
    butunleme_notu DECIMAL(5,2),
    harf_notu VARCHAR(2),
    FOREIGN KEY (ogrenci_id) REFERENCES Kullanicilar(kullanici_id),
    FOREIGN KEY (ders_id) REFERENCES Dersler(ders_id)
);
```

### Akademik Takvim Tablosu
```sql
CREATE TABLE akademiktakvim (
    takvim_id INT AUTO_INCREMENT PRIMARY KEY,
    donem_adi VARCHAR(100) NOT NULL,
    akademik_yil VARCHAR(20) NOT NULL,
    donem_tipi ENUM('Guz', 'Bahar', 'Yaz') NOT NULL,

    ders_kayit_baslangic DATE NULL,
    ders_kayit_bitis DATE NULL,

    vize_baslangic DATE NULL,
    vize_bitis DATE NULL,
    vize_not_giris_bitis DATE NULL,

    final_baslangic DATE NULL,
    final_bitis DATE NULL,
    final_not_giris_bitis DATE NULL,

    butunleme_baslangic DATE NULL,
    butunleme_bitis DATE NULL,
    butunleme_not_giris_bitis DATE NULL,

    donem_baslangic DATE NULL,
    donem_bitis DATE NULL,

    aktif TINYINT(1) DEFAULT 0,
    olusturma_tarihi DATETIME DEFAULT CURRENT_TIMESTAMP
);
```

### Notlar Tablosu
```sql
CREATE TABLE Notlar (
    not_id INT AUTO_INCREMENT PRIMARY KEY,
    kayit_id INT NOT NULL,
    not_tipi ENUM('vize', 'final', 'butunleme') NOT NULL,
    puan DECIMAL(5,2) NOT NULL,
    giris_tarihi DATETIME DEFAULT CURRENT_TIMESTAMP,
    giren_hoca_id INT NOT NULL,
    guncelleme_tarihi DATETIME NULL ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (kayit_id) REFERENCES Kayitlar(kayit_id) ON DELETE CASCADE,
    FOREIGN KEY (giren_hoca_id) REFERENCES Kullanicilar(kullanici_id),
    UNIQUE KEY unique_kayit_not_tipi (kayit_id, not_tipi)
);
```

### Örnek Veriler
```sql
-- Admin kullanýcý
INSERT INTO Kullanicilar (tc_kimlik, ad, soyad, email, sifre, rol, kullanici_no)
VALUES ('12345678901', 'Admin', 'User', 'admin@universite.edu.tr', 'admin123', 'admin', '25000001');

-- Öðretim Görevlisi
INSERT INTO Kullanicilar (tc_kimlik, ad, soyad, email, sifre, rol, kullanici_no)
VALUES ('12345678902', 'Ahmet', 'Yýlmaz', 'ahmet.yilmaz@universite.edu.tr', 'hoca123', 'hoca', '25100001');

-- Öðrenci
INSERT INTO Kullanicilar (tc_kimlik, ad, soyad, email, sifre, rol, kullanici_no)
VALUES ('12345678903', 'Mehmet', 'Demir', 'mehmet.demir@universite.edu.tr', 'ogrenci123', 'ogrenci', '25200001');
```

---

## Kullaným

### Giriþ Yapma
1. `/Account/Login.aspx` sayfasýna gidin
2. E-posta veya kullanýcý numarasý ile giriþ yapýn
3. Rol bazlý otomatik yönlendirme:
   - **Admin** -> `/Admin/Default.aspx`
   - **Hoca** -> `/Hoca/Default.aspx`
   - **Öðrenci** -> `/Ogrenci/Default.aspx`

### Admin Ýþlemleri
- **Kullanýcý Ekle**: Admin Panel -> Kullanýcý Ekle
- **Kullanýcý Düzenle**: Kullanýcýlar -> Düzenle butonu
- **Ders Ekle**: Admin Panel -> Ders Ekle
- **Ders Düzenle**: Dersler -> Düzenle butonu
- **Akademik Takvim**: Admin Panel -> Akademik Takvim

### Öðretim Görevlisi Ýþlemleri
- **Kayýt Onaylama**: Hoca Panel -> Kayýt Onay
- **Not Giriþi**: Hoca Panel -> Not Giriþi

### Öðrenci Ýþlemleri
- **Ders Kaydý**: Öðrenci Panel -> Yeni Ders Kaydý
- **Notlarý Görüntüleme**: Öðrenci Panel -> Notlarým

### Ýletiþim Formu Kullanýmý
1. `/Contact.aspx` sayfasýna gidin
2. Ad Soyad, E-posta adresinizi girin
3. Konu kategorisi seçin (Genel Bilgi, Teknik Destek, vb.)
4. Mesajýnýzý yazýn ve "Gönder" butonuna týklayýn
5. Mesajýnýz SMTP üzerinden yönetici e-postasýna iletilecektir

---

## Ekran Görüntüleri

Aþaðýda projenin çeþitli bölümlerine ait ekran görüntüleri yer almaktadýr:

### 1. Ana Sayfa
![Ana Sayfa](docs/screenshots/anasayfa.png)

### 2. Giriþ Sayfasý
![Giriþ Sayfasý](docs/screenshots/giris_sayfasi.png)

### 3. Admin Panel
![Admin Panel](docs/screenshots/admin_panel.png)

### 4. Akademik Takvim
![Akademik Takvim](docs/screenshots/akademiktakvim.png)

### 5. Hoca Panel
![Hoca Panel](docs/screenshots/hoca_panel.png)

### 6. Öðrenci Panel
![Öðrenci Panel](docs/screenshots/ogrenci_panel.png)

### 7. Hakkýnda
![Hakkýnda](docs/screenshots/hakkinda.png)

### 8. Ýletiþim
![Ýletiþim](docs/screenshots/iletisim.png)

---

## Geliþtirme Notlarý

### Güvenlik
- Tüm veritabaný sorgularý **parametreli** (`AddWithValue`) - SQL Injection korumasý
- Session tabanlý kimlik doðrulama
- Rol bazlý sayfa eriþim kontrolü (`AdminBasePage`, `HocaBasePage`)
- Anti-XSRF token desteði (`Site.Master`)

### E-posta Gönderimi
- **SMTP Protokolü**: `System.Net.Mail` namespace'i kullanýlarak e-posta gönderimi
- **Yapýlandýrma**: Tüm SMTP ayarlarý `Web.config` dosyasýndan okunur
- **Güvenlik**: SSL/TLS þifrelemesi ile güvenli baðlantý
- **HTML Þablon**: Profesyonel görünümlü HTML formatýnda e-posta içeriði
- **Hata Yönetimi**: Try-catch bloklarý ile hata yakalama ve kullanýcý bildirimi

### Mimari
- **BasePage Pattern**: Admin ve Hoca sayfalarý için özel base class'lar
- **Master Page**: Ortak layout ve navbar (`Site.Master`)
- **Ayrý Düzenleme Sayfalarý**: Modal yerine dedicated edit sayfalarý (daha stabil)

### URL Çözümleme
- `ResolveUrl("~/")` kullanýmý - sanal dizin altýnda doðru çalýþýr
- `runat="server"` ile dinamik URL'ler

### Stil ve Script
- Bundle yapýlandýrmasý (`BundleConfig.cs`)
- CDN üzerinden Font Awesome
- Özel stiller `Site.css` içinde

---

## Katký

1. Bu depoyu fork edin
2. Feature branch oluþturun (`git checkout -b feature/YeniOzellik`)
3. Deðiþikliklerinizi commit edin (`git commit -m 'Yeni özellik eklendi'`)
4. Branch'e push edin (`git push origin feature/YeniOzellik`)
5. Pull Request açýn

---

## Lisans

Bu proje eðitim amaçlý geliþtirilmiþtir. Kullaným koþullarý için repo sahibiyle iletiþime geçin.

---

<p align="center">
  <strong>Geliþtirici:</strong> <a href="https://github.com/AntiFlamer">AntiFlamer</a>
</p>

<p align="center">
  ? Bu projeyi beðendiyseniz yýldýz vermeyi unutmayýn! ?
</p>
