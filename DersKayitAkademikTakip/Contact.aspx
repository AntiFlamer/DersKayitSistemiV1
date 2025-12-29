<%@ Page Title="İletişim" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="DersKayitAkademikTakip.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <!-- Page Header -->
        <section class="py-4 bg-success text-white rounded-3 mb-5">
            <div class="container text-center">
                <h1 class="display-5 fw-bold">
                    <i class="fas fa-envelope me-3"></i>İletişim
                </h1>
                <p class="lead mb-0">Sorularınız için bizimle iletişime geçin</p>
            </div>
        </section>

        <div class="row g-5 mb-5">
            <!-- Contact Information -->
            <div class="col-lg-5">
                <div class="card border-0 shadow-sm h-100">
                    <div class="card-header bg-primary text-white">
                        <h4 class="mb-0"><i class="fas fa-address-card me-2"></i>İletişim Bilgileri</h4>
                    </div>
                    <div class="card-body p-4">
                        <div class="d-flex align-items-start mb-4">
                            <div class="flex-shrink-0">
                                <div class="bg-primary text-white rounded-circle d-flex align-items-center justify-content-center" style="width: 50px; height: 50px;">
                                    <i class="fas fa-map-marker-alt"></i>
                                </div>
                            </div>
                            <div class="flex-grow-1 ms-3">
                                <h5 class="mb-1">Adres</h5>
                                <p class="text-muted mb-0">
                                    Üniversite Caddesi No: 1<br />
                                    Merkez Kampüs, Rektörlük Binası<br />
                                    34000 İstanbul, Türkiye
                                </p>
                            </div>
                        </div>

                        <div class="d-flex align-items-start mb-4">
                            <div class="flex-shrink-0">
                                <div class="bg-success text-white rounded-circle d-flex align-items-center justify-content-center" style="width: 50px; height: 50px;">
                                    <i class="fas fa-phone"></i>
                                </div>
                            </div>
                            <div class="flex-grow-1 ms-3">
                                <h5 class="mb-1">Telefon</h5>
                                <p class="text-muted mb-0">
                                    <strong>Santral:</strong> +90 (212) 555 00 00<br />
                                    <strong>Öğrenci İşleri:</strong> +90 (212) 555 00 01
                                </p>
                            </div>
                        </div>

                        <div class="d-flex align-items-start mb-4">
                            <div class="flex-shrink-0">
                                <div class="bg-info text-white rounded-circle d-flex align-items-center justify-content-center" style="width: 50px; height: 50px;">
                                    <i class="fas fa-envelope"></i>
                                </div>
                            </div>
                            <div class="flex-grow-1 ms-3">
                                <h5 class="mb-1">E-posta</h5>
                                <p class="text-muted mb-0">
                                    <strong>Genel:</strong> info@universite.edu.tr<br />
                                    <strong>Destek:</strong> destek@universite.edu.tr<br />
                                    <strong>Öğrenci İşleri:</strong> ogrenciisleri@universite.edu.tr
                                </p>
                            </div>
                        </div>

                        <div class="d-flex align-items-start">
                            <div class="flex-shrink-0">
                                <div class="bg-warning text-white rounded-circle d-flex align-items-center justify-content-center" style="width: 50px; height: 50px;">
                                    <i class="fas fa-clock"></i>
                                </div>
                            </div>
                            <div class="flex-grow-1 ms-3">
                                <h5 class="mb-1">Çalışma Saatleri</h5>
                                <p class="text-muted mb-0">
                                    <strong>Pazartesi - Cuma:</strong> 08:30 - 17:30<br />
                                    <strong>Cumartesi - Pazar:</strong> Kapalı
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Contact Form -->
            <div class="col-lg-7">
                <div class="card border-0 shadow-sm h-100">
                    <div class="card-header bg-dark text-white">
                        <h4 class="mb-0"><i class="fas fa-paper-plane me-2"></i>Bize Ulaşın</h4>
                    </div>
                    <div class="card-body p-4">
                        <div class="alert alert-info" role="alert">
                            <i class="fas fa-info-circle me-2"></i>
                            Sorularınız veya önerileriniz için aşağıdaki formu doldurabilirsiniz.
                        </div>
                        
                        <div class="mb-3">
                            <label for="txtAdSoyad" class="form-label">
                                <i class="fas fa-user me-1"></i>Ad Soyad
                            </label>
                            <input type="text" class="form-control" id="txtAdSoyad" placeholder="Adınız ve soyadınız" disabled />
                        </div>
                        
                        <div class="mb-3">
                            <label for="txtEposta" class="form-label">
                                <i class="fas fa-envelope me-1"></i>E-posta
                            </label>
                            <input type="email" class="form-control" id="txtEposta" placeholder="ornek@universite.edu.tr" disabled />
                        </div>
                        
                        <div class="mb-3">
                            <label for="ddlKonu" class="form-label">
                                <i class="fas fa-tag me-1"></i>Konu
                            </label>
                            <select class="form-select" id="ddlKonu" disabled>
                                <option selected>Konu seçiniz...</option>
                                <option value="1">Ders Kaydı Hakkında</option>
                                <option value="2">Teknik Destek</option>
                                <option value="3">Şifre Sıfırlama</option>
                                <option value="4">Öneri ve Şikayet</option>
                                <option value="5">Diğer</option>
                            </select>
                        </div>
                        
                        <div class="mb-3">
                            <label for="txtMesaj" class="form-label">
                                <i class="fas fa-comment me-1"></i>Mesajınız
                            </label>
                            <textarea class="form-control" id="txtMesaj" rows="4" placeholder="Mesajınızı buraya yazınız..." disabled></textarea>
                        </div>
                        
                        <button type="button" class="btn btn-success btn-lg w-100" disabled>
                            <i class="fas fa-paper-plane me-2"></i>Gönder
                        </button>
                        <small class="text-muted d-block mt-2 text-center">
                            <i class="fas fa-lock me-1"></i>Bu form şu an demo amaçlıdır.
                        </small>
                    </div>
                </div>
            </div>
        </div>

        <!-- Department Contacts -->
        <section class="mb-5">
            <h2 class="text-center mb-4">
                <i class="fas fa-building text-primary me-2"></i>Birim İletişim Bilgileri
            </h2>
            <div class="row g-4">
                <div class="col-md-4">
                    <div class="card text-center border-0 bg-light h-100">
                        <div class="card-body p-4">
                            <i class="fas fa-user-graduate fa-3x text-success mb-3"></i>
                            <h5 class="card-title">Öğrenci İşleri</h5>
                            <p class="card-text text-muted small">
                                Kayıt, transkript, belge talepleri ve öğrenci durumu ile ilgili işlemler.
                            </p>
                            <p class="mb-0">
                                <i class="fas fa-phone me-1"></i>+90 (212) 555 00 01
                            </p>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="card text-center border-0 bg-light h-100">
                        <div class="card-body p-4">
                            <i class="fas fa-laptop fa-3x text-primary mb-3"></i>
                            <h5 class="card-title">Bilgi İşlem</h5>
                            <p class="card-text text-muted small">
                                Sistem erişimi, şifre sıfırlama ve teknik destek talepleri.
                            </p>
                            <p class="mb-0">
                                <i class="fas fa-phone me-1"></i>+90 (212) 555 00 02
                            </p>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="card text-center border-0 bg-light h-100">
                        <div class="card-body p-4">
                            <i class="fas fa-book fa-3x text-warning mb-3"></i>
                            <h5 class="card-title">Akademik İşler</h5>
                            <p class="card-text text-muted small">
                                Ders programları, akademik takvim ve müfredat bilgileri.
                            </p>
                            <p class="mb-0">
                                <i class="fas fa-phone me-1"></i>+90 (212) 555 00 03
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </section>

        <!-- FAQ Section -->
        <section class="mb-5">
            <h2 class="text-center mb-4">
                <i class="fas fa-question-circle text-info me-2"></i>Sıkça Sorulan Sorular
            </h2>
            <div class="row justify-content-center">
                <div class="col-lg-10">
                    <div class="accordion" id="faqAccordion">
                        <div class="accordion-item">
                            <h2 class="accordion-header" id="faq1">
                                <button class="accordion-button" type="button" data-bs-toggle="collapse" data-bs-target="#faqCollapse1" aria-expanded="true" aria-controls="faqCollapse1">
                                    <i class="fas fa-key me-2 text-primary"></i>Şifremi unuttum, ne yapmalıyım?
                                </button>
                            </h2>
                            <div id="faqCollapse1" class="accordion-collapse collapse show" aria-labelledby="faq1" data-bs-parent="#faqAccordion">
                                <div class="accordion-body">
                                    Şifrenizi sıfırlamak için Bilgi İşlem birimiyle iletişime geçebilirsiniz. 
                                    Kimlik doğrulaması yapıldıktan sonra yeni şifreniz e-posta adresinize gönderilecektir.
                                </div>
                            </div>
                        </div>
                        <div class="accordion-item">
                            <h2 class="accordion-header" id="faq2">
                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#faqCollapse2" aria-expanded="false" aria-controls="faqCollapse2">
                                    <i class="fas fa-book-reader me-2 text-success"></i>Ders kaydı ne zaman yapılır?
                                </button>
                            </h2>
                            <div id="faqCollapse2" class="accordion-collapse collapse" aria-labelledby="faq2" data-bs-parent="#faqAccordion">
                                <div class="accordion-body">
                                    Ders kayıtları akademik takvimde belirtilen tarihlerde yapılır. 
                                    Genellikle dönem başlamadan 1-2 hafta önce ders kayıt haftası ilan edilir.
                                </div>
                            </div>
                        </div>
                        <div class="accordion-item">
                            <h2 class="accordion-header" id="faq3">
                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#faqCollapse3" aria-expanded="false" aria-controls="faqCollapse3">
                                    <i class="fas fa-user-check me-2 text-warning"></i>Ders kaydım neden onaylanmadı?
                                </button>
                            </h2>
                            <div id="faqCollapse3" class="accordion-collapse collapse" aria-labelledby="faq3" data-bs-parent="#faqAccordion">
                                <div class="accordion-body">
                                    Ders kaydınız öğretim görevlisi onayı bekliyor olabilir. 
                                    Danışmanınızla iletişime geçerek kayıt durumunuzu öğrenebilirsiniz.
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </main>
</asp:Content>
