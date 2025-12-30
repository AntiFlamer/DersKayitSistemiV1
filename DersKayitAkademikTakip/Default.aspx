<%@ Page Title="Ana Sayfa" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DersKayitAkademikTakip._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <!-- Hero Section -->
        <section class="py-5 text-center bg-light rounded-3 mb-5">
            <div class="container py-4">
                <h1 class="display-4 fw-bold text-primary">
                    <i class="fas fa-university me-3"></i>Üniversite Ders Kayıt Sistemi
                </h1>
                <p class="lead text-muted mb-4">
                    Akademik hayatınızı kolaylaştıran, modern ve kullanıcı dostu ders kayıt platformu.
                    Ders seçiminden not takibine kadar tüm işlemlerinizi tek bir yerden yönetin.
                </p>
                <div class="d-grid gap-2 d-sm-flex justify-content-sm-center">
                    <a href="Account/Login.aspx" class="btn btn-primary btn-lg px-4">
                        <i class="fas fa-sign-in-alt me-2"></i>Giriş Yap
                    </a>
                    <a href="AkademikTakvimGoruntule.aspx" class="btn btn-outline-secondary btn-lg px-4">
                        <i class="fas fa-calendar-alt me-2"></i>Akademik Takvim
                    </a>
                </div>
            </div>
        </section>

        <!-- Features Section -->
        <section class="mb-5">
            <h2 class="text-center mb-4 text-dark">
                <i class="fas fa-star text-warning me-2"></i>Sistem Özellikleri
            </h2>
            <div class="row g-4">
                <div class="col-md-4">
                    <div class="card h-100 border-0 shadow-sm text-center">
                        <div class="card-body p-4">
                            <div class="feature-icon bg-primary bg-gradient text-white rounded-circle d-inline-flex align-items-center justify-content-center mb-3" style="width: 64px; height: 64px;">
                                <i class="fas fa-book-open fa-2x"></i>
                            </div>
                            <h4 class="card-title">Kolay Ders Kaydı</h4>
                            <p class="card-text text-muted">
                                Açılan dersleri görüntüleyin, ders programınızı oluşturun ve tek tıkla kayıt olun. 
                                Kontenjan durumunu anlık takip edin.
                            </p>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="card h-100 border-0 shadow-sm text-center">
                        <div class="card-body p-4">
                            <div class="feature-icon bg-success bg-gradient text-white rounded-circle d-inline-flex align-items-center justify-content-center mb-3" style="width: 64px; height: 64px;">
                                <i class="fas fa-chart-line fa-2x"></i>
                            </div>
                            <h4 class="card-title">Not Takibi</h4>
                            <p class="card-text text-muted">
                                Vize, final ve bütünleme notlarınızı anlık olarak görüntüleyin. 
                                Genel not ortalamanızı (GNO) hesaplayın.
                            </p>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="card h-100 border-0 shadow-sm text-center">
                        <div class="card-body p-4">
                            <div class="feature-icon bg-info bg-gradient text-white rounded-circle d-inline-flex align-items-center justify-content-center mb-3" style="width: 64px; height: 64px;">
                                <i class="fas fa-users fa-2x"></i>
                            </div>
                            <h4 class="card-title">Akademik Danışmanlık</h4>
                            <p class="card-text text-muted">
                                Öğretim görevlileri ders kayıtlarınızı onaylasın. 
                                Akademik sürecinizi birlikte yönetin.
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </section>

        <!-- User Types Section -->
        <section class="mb-5">
            <h2 class="text-center mb-4 text-dark">
                <i class="fas fa-users-cog text-primary me-2"></i>Kullanıcı Rolleri
            </h2>
            <div class="row g-4">
                <div class="col-lg-4">
                    <div class="card border-success h-100">
                        <div class="card-header bg-success text-white">
                            <i class="fas fa-graduation-cap me-2"></i>Öğrenci
                        </div>
                        <div class="card-body">
                            <ul class="list-unstyled">
                                <li class="mb-2"><i class="fas fa-check text-success me-2"></i>Ders kaydı yapma</li>
                                <li class="mb-2"><i class="fas fa-check text-success me-2"></i>Kayıtlı dersleri görüntüleme</li>
                                <li class="mb-2"><i class="fas fa-check text-success me-2"></i>Not bilgilerini takip etme</li>
                                <li class="mb-2"><i class="fas fa-check text-success me-2"></i>Ders programı oluşturma</li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="col-lg-4">
                    <div class="card border-primary h-100">
                        <div class="card-header bg-primary text-white">
                            <i class="fas fa-chalkboard-teacher me-2"></i>Öğretim Görevlisi
                        </div>
                        <div class="card-body">
                            <ul class="list-unstyled">
                                <li class="mb-2"><i class="fas fa-check text-primary me-2"></i>Ders kayıt onayı</li>
                                <li class="mb-2"><i class="fas fa-check text-primary me-2"></i>Not girişi yapma</li>
                                <li class="mb-2"><i class="fas fa-check text-primary me-2"></i>Öğrenci listesi görüntüleme</li>
                                <li class="mb-2"><i class="fas fa-check text-primary me-2"></i>Ders istatistikleri</li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="col-lg-4">
                    <div class="card border-danger h-100">
                        <div class="card-header bg-danger text-white">
                            <i class="fas fa-user-shield me-2"></i>Yönetici
                        </div>
                        <div class="card-body">
                            <ul class="list-unstyled">
                                <li class="mb-2"><i class="fas fa-check text-danger me-2"></i>Kullanıcı yönetimi</li>
                                <li class="mb-2"><i class="fas fa-check text-danger me-2"></i>Ders ekleme/düzenleme</li>
                                <li class="mb-2"><i class="fas fa-check text-danger me-2"></i>Sistem ayarları</li>
                                <li class="mb-2"><i class="fas fa-check text-danger me-2"></i>Raporlama</li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </section>

        <!-- Statistics Section -->
        <section class="py-5 bg-dark text-white rounded-3 mb-5">
            <div class="container">
                <div class="row text-center">
                    <div class="col-md-3 col-6 mb-3 mb-md-0">
                        <div class="display-5 fw-bold text-primary">
                            <i class="fas fa-users"></i>
                        </div>
                        <p class="lead mb-0">Öğrenci</p>
                    </div>
                    <div class="col-md-3 col-6 mb-3 mb-md-0">
                        <div class="display-5 fw-bold text-info">
                            <i class="fas fa-chalkboard"></i>
                        </div>
                        <p class="lead mb-0">Ders</p>
                    </div>
                    <div class="col-md-3 col-6">
                        <div class="display-5 fw-bold text-success">
                            <i class="fas fa-user-tie"></i>
                        </div>
                        <p class="lead mb-0">Öğretim Görevlisi</p>
                    </div>
                    <div class="col-md-3 col-6">
                        <div class="display-5 fw-bold text-warning">
                            <i class="fas fa-building"></i>
                        </div>
                        <p class="lead mb-0">Bölüm</p>
                    </div>
                </div>
            </div>
        </section>

        <!-- Quick Access Section -->
        <section class="mb-5">
            <h2 class="text-center mb-4 text-dark">
                <i class="fas fa-bolt text-warning me-2"></i>Hızlı Erişim
            </h2>
            <div class="row justify-content-center">
                <div class="col-md-10">
                    <div class="card border-0 shadow">
                        <div class="card-body p-4">
                            <div class="d-flex flex-wrap justify-content-center gap-3">
                                <a href="Account/Login.aspx" class="btn btn-outline-primary btn-lg">
                                    <i class="fas fa-sign-in-alt me-2"></i>Giriş Yap
                                </a>
                                <a href="AkademikTakvimGoruntule.aspx" class="btn btn-outline-info btn-lg">
                                    <i class="fas fa-calendar-alt me-2"></i>Akademik Takvim
                                </a>
                                <a href="Contact.aspx" class="btn btn-outline-success btn-lg">
                                    <i class="fas fa-envelope me-2"></i>İletişim
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </main>

</asp:Content>
