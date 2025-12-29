<%@ Page Title="Hakkımızda" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="DersKayitAkademikTakip.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <!-- Page Header -->
        <section class="py-4 bg-primary text-white rounded-3 mb-5">
            <div class="container text-center">
                <h1 class="display-5 fw-bold">
                    <i class="fas fa-info-circle me-3"></i>Hakkımızda
                </h1>
                <p class="lead mb-0">Üniversite Ders Kayıt ve Akademik Takip Sistemi</p>
            </div>
        </section>

        <!-- About Content -->
        <div class="row g-5 mb-5">
            <div class="col-lg-6">
                <div class="card border-0 shadow-sm h-100">
                    <div class="card-body p-4">
                        <h3 class="card-title text-primary">
                            <i class="fas fa-bullseye me-2"></i>Misyonumuz
                        </h3>
                        <p class="card-text text-muted">
                            Üniversite Ders Kayıt Sistemi, öğrencilerin akademik süreçlerini kolaylaştırmak, 
                            öğretim görevlilerinin iş yükünü azaltmak ve yöneticilerin kurumsal verimliliği 
                            artırmasına yardımcı olmak amacıyla geliştirilmiştir.
                        </p>
                        <p class="card-text text-muted">
                            Modern teknolojiler kullanılarak oluşturulan sistemimiz, kullanıcı dostu arayüzü 
                            ve güvenilir altyapısı ile akademik hayatın vazgeçilmez bir parçası olmayı hedeflemektedir.
                        </p>
                    </div>
                </div>
            </div>
            <div class="col-lg-6">
                <div class="card border-0 shadow-sm h-100">
                    <div class="card-body p-4">
                        <h3 class="card-title text-success">
                            <i class="fas fa-eye me-2"></i>Vizyonumuz
                        </h3>
                        <p class="card-text text-muted">
                            Eğitim teknolojileri alanında öncü bir platform olarak, sürekli gelişen ve 
                            yenilenen yapımızla üniversitelerin dijital dönüşümüne katkı sağlamak.
                        </p>
                        <p class="card-text text-muted">
                            Öğrenci memnuniyetini en üst düzeyde tutarak, akademik başarıya giden yolda 
                            güvenilir bir rehber olmayı amaçlıyoruz.
                        </p>
                    </div>
                </div>
            </div>
        </div>

        <!-- Features Detail -->
        <section class="mb-5">
            <h2 class="text-center mb-4">
                <i class="fas fa-cogs text-primary me-2"></i>Sistem Özellikleri
            </h2>
            <div class="row g-4">
                <div class="col-md-6 col-lg-3">
                    <div class="card text-center h-100 border-0 bg-light">
                        <div class="card-body">
                            <i class="fas fa-shield-alt fa-3x text-primary mb-3"></i>
                            <h5 class="card-title">Güvenli Giriş</h5>
                            <p class="card-text small text-muted">
                                Şifreli giriş sistemi ve oturum yönetimi ile verileriniz güvende.
                            </p>
                        </div>
                    </div>
                </div>
                <div class="col-md-6 col-lg-3">
                    <div class="card text-center h-100 border-0 bg-light">
                        <div class="card-body">
                            <i class="fas fa-mobile-alt fa-3x text-success mb-3"></i>
                            <h5 class="card-title">Responsive Tasarım</h5>
                            <p class="card-text small text-muted">
                                Tüm cihazlardan erişim için optimize edilmiş arayüz.
                            </p>
                        </div>
                    </div>
                </div>
                <div class="col-md-6 col-lg-3">
                    <div class="card text-center h-100 border-0 bg-light">
                        <div class="card-body">
                            <i class="fas fa-tachometer-alt fa-3x text-warning mb-3"></i>
                            <h5 class="card-title">Hızlı Performans</h5>
                            <p class="card-text small text-muted">
                                Optimize edilmiş veritabanı sorguları ile hızlı yanıt süreleri.
                            </p>
                        </div>
                    </div>
                </div>
                <div class="col-md-6 col-lg-3">
                    <div class="card text-center h-100 border-0 bg-light">
                        <div class="card-body">
                            <i class="fas fa-user-check fa-3x text-info mb-3"></i>
                            <h5 class="card-title">Kolay Kullanım</h5>
                            <p class="card-text small text-muted">
                                Sezgisel arayüz ile her seviyeden kullanıcı için uygun.
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </section>

        <!-- Technology Stack -->
        <section class="mb-5">
            <div class="card border-0 shadow">
                <div class="card-header bg-dark text-white">
                    <h4 class="mb-0"><i class="fas fa-code me-2"></i>Kullanılan Teknolojiler</h4>
                </div>
                <div class="card-body">
                    <div class="row text-center">
                        <div class="col-md-3 col-6 mb-3">
                            <div class="p-3">
                                <i class="fab fa-microsoft fa-3x text-primary mb-2"></i>
                                <p class="mb-0 fw-bold">ASP.NET</p>
                                <small class="text-muted">Web Forms</small>
                            </div>
                        </div>
                        <div class="col-md-3 col-6 mb-3">
                            <div class="p-3">
                                <i class="fas fa-database fa-3x text-warning mb-2"></i>
                                <p class="mb-0 fw-bold">MySQL</p>
                                <small class="text-muted">Veritabanı</small>
                            </div>
                        </div>
                        <div class="col-md-3 col-6 mb-3">
                            <div class="p-3">
                                <i class="fab fa-bootstrap fa-3x text-purple mb-2" style="color: #7952b3;"></i>
                                <p class="mb-0 fw-bold">Bootstrap 5</p>
                                <small class="text-muted">UI Framework</small>
                            </div>
                        </div>
                        <div class="col-md-3 col-6 mb-3">
                            <div class="p-3">
                                <i class="fab fa-font-awesome fa-3x text-info mb-2"></i>
                                <p class="mb-0 fw-bold">Font Awesome</p>
                                <small class="text-muted">İkonlar</small>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>

        <!-- Timeline -->
        <section class="mb-5">
            <h2 class="text-center mb-4">
                <i class="fas fa-history text-primary me-2"></i>Geliştirme Süreci
            </h2>
            <div class="row justify-content-center">
                <div class="col-lg-8">
                    <div class="card border-0 shadow-sm">
                        <div class="card-body">
                            <ul class="list-group list-group-flush">
                                <li class="list-group-item d-flex align-items-center">
                                    <span class="badge bg-primary rounded-pill me-3">1</span>
                                    <div>
                                        <strong>Analiz ve Planlama</strong>
                                        <p class="mb-0 small text-muted">Gereksinim analizi ve sistem tasarımı</p>
                                    </div>
                                </li>
                                <li class="list-group-item d-flex align-items-center">
                                    <span class="badge bg-success rounded-pill me-3">2</span>
                                    <div>
                                        <strong>Veritabanı Tasarımı</strong>
                                        <p class="mb-0 small text-muted">MySQL veritabanı şeması oluşturma</p>
                                    </div>
                                </li>
                                <li class="list-group-item d-flex align-items-center">
                                    <span class="badge bg-info rounded-pill me-3">3</span>
                                    <div>
                                        <strong>Arayüz Geliştirme</strong>
                                        <p class="mb-0 small text-muted">Bootstrap ile responsive tasarım</p>
