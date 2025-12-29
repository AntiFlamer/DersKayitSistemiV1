<%@ Page Title="Iletisim" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="DersKayitAkademikTakip.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <!-- Page Header -->
        <section class="py-4 bg-success text-white rounded-3 mb-5">
            <div class="container text-center">
                <h1 class="display-5 fw-bold">
                    <i class="fas fa-envelope me-3"></i>Iletisim
                </h1>
                <p class="lead mb-0">Sorulariniz icin bizimle iletisime gecin</p>
            </div>
        </section>

        <div class="row g-5 mb-5">
            <!-- Contact Information -->
            <div class="col-lg-5">
                <div class="card border-0 shadow-sm h-100">
                    <div class="card-header bg-primary text-white">
                        <h4 class="mb-0"><i class="fas fa-address-card me-2"></i>Iletisim Bilgileri</h4>
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
                                    Universite Caddesi No: 1<br />
                                    Merkez Kampus, Rektorluk Binasi<br />
                                    34000 Istanbul, Turkiye
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
                                    <strong>Ogrenci Isleri:</strong> +90 (212) 555 00 01
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
                                    <strong>Destek:</strong> destek@universite.edu.tr
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
                                <h5 class="mb-1">Calisma Saatleri</h5>
                                <p class="text-muted mb-0">
                                    <strong>Pazartesi - Cuma:</strong> 08:30 - 17:30<br />
                                    <strong>Cumartesi - Pazar:</strong> Kapali
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Contact Form - GERCEK FORM -->
            <div class="col-lg-7">
                <div class="card border-0 shadow-sm h-100">
                    <div class="card-header bg-dark text-white">
                        <h4 class="mb-0"><i class="fas fa-paper-plane me-2"></i>Bize Ulasin</h4>
                    </div>
                    <div class="card-body p-4">
                        
                        <!-- BASARI MESAJI -->
                        <asp:Panel ID="pnlSuccess" runat="server" Visible="false" CssClass="alert alert-success">
                            <i class="fas fa-check-circle me-2"></i>
                            <strong>Mesajiniz basariyla gonderildi!</strong> En kisa surede size donecegiz.
                        </asp:Panel>
                        
                        <!-- HATA MESAJI -->
                        <asp:Panel ID="pnlError" runat="server" Visible="false" CssClass="alert alert-danger">
                            <i class="fas fa-exclamation-circle me-2"></i>
                            <asp:Literal ID="litError" runat="server" />
                        </asp:Panel>
                        
                        <!-- FORM ALANLARI -->
                        <asp:Panel ID="pnlForm" runat="server">
                            <div class="alert alert-info" role="alert">
                                <i class="fas fa-info-circle me-2"></i>
                                Sorulariniz veya onerileriniz icin asagidaki formu doldurun.
                            </div>
                            
                            <!-- Ad Soyad -->
                            <div class="mb-3">
                                <label for="txtAdSoyad" class="form-label">
                                    <i class="fas fa-user me-1"></i>Ad Soyad <span class="text-danger">*</span>
                                </label>
                                <asp:TextBox ID="txtAdSoyad" runat="server" CssClass="form-control" 
                                    placeholder="Adiniz ve soyadiniz" MaxLength="100" />
                                <asp:RequiredFieldValidator ID="rfvAdSoyad" runat="server" 
                                    ControlToValidate="txtAdSoyad" 
                                    ErrorMessage="Ad Soyad alani zorunludur." 
                                    CssClass="text-danger small" Display="Dynamic" />
                            </div>
                            
                            <!-- E-posta -->
                            <div class="mb-3">
                                <label for="txtEposta" class="form-label">
                                    <i class="fas fa-envelope me-1"></i>E-posta <span class="text-danger">*</span>
                                </label>
                                <asp:TextBox ID="txtEposta" runat="server" CssClass="form-control" 
                                    TextMode="Email" placeholder="ornek@email.com" MaxLength="100" />
                                <asp:RequiredFieldValidator ID="rfvEposta" runat="server" 
                                    ControlToValidate="txtEposta" 
                                    ErrorMessage="E-posta alani zorunludur." 
                                    CssClass="text-danger small" Display="Dynamic" />
                                <asp:RegularExpressionValidator ID="revEposta" runat="server" 
                                    ControlToValidate="txtEposta" 
                                    ValidationExpression="^[\w\.-]+@[\w\.-]+\.\w+$"
                                    ErrorMessage="Gecerli bir e-posta adresi giriniz." 
                                    CssClass="text-danger small" Display="Dynamic" />
                            </div>
                            
                            <!-- Konu -->
                            <div class="mb-3">
                                <label for="ddlKonu" class="form-label">
                                    <i class="fas fa-tag me-1"></i>Konu <span class="text-danger">*</span>
                                </label>
                                <asp:DropDownList ID="ddlKonu" runat="server" CssClass="form-select">
                                    <asp:ListItem Value="" Text="Konu seciniz..." />
                                    <asp:ListItem Value="Ders Kaydi Hakkinda" Text="Ders Kaydi Hakkinda" />
                                    <asp:ListItem Value="Teknik Destek" Text="Teknik Destek" />
                                    <asp:ListItem Value="Sifre Sifirlama" Text="Sifre Sifirlama" />
                                    <asp:ListItem Value="Oneri ve Sikayet" Text="Oneri ve Sikayet" />
                                    <asp:ListItem Value="Diger" Text="Diger" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvKonu" runat="server" 
                                    ControlToValidate="ddlKonu" 
                                    InitialValue=""
                                    ErrorMessage="Lutfen bir konu seciniz." 
                                    CssClass="text-danger small" Display="Dynamic" />
                            </div>
                            
                            <!-- Mesaj -->
                            <div class="mb-3">
                                <label for="txtMesaj" class="form-label">
                                    <i class="fas fa-comment me-1"></i>Mesajiniz <span class="text-danger">*</span>
                                </label>
                                <asp:TextBox ID="txtMesaj" runat="server" CssClass="form-control" 
                                    TextMode="MultiLine" Rows="5" 
                                    placeholder="Mesajinizi buraya yaziniz..." MaxLength="2000" />
                                <asp:RequiredFieldValidator ID="rfvMesaj" runat="server" 
                                    ControlToValidate="txtMesaj" 
                                    ErrorMessage="Mesaj alani zorunludur." 
                                    CssClass="text-danger small" Display="Dynamic" />
                            </div>
                            
                            <!-- Gonder Butonu -->
                            <asp:Button ID="btnGonder" runat="server" Text="Gonder" 
                                CssClass="btn btn-success btn-lg w-100" 
                                OnClick="btnGonder_Click" />
                            
                            <small class="text-muted d-block mt-2 text-center">
                                <i class="fas fa-shield-alt me-1"></i>Bilgileriniz guvenle saklanir.
                            </small>
                        </asp:Panel>
                        
                    </div>
                </div>
            </div>
        </div>

        <!-- Department Contacts - Kisaltilmis -->
        <section class="mb-5">
            <h2 class="text-center mb-4">
                <i class="fas fa-building text-primary me-2"></i>Birim Iletisim Bilgileri
            </h2>
            <div class="row g-4">
                <div class="col-md-4">
                    <div class="card text-center border-0 bg-light h-100">
                        <div class="card-body p-4">
                            <i class="fas fa-user-graduate fa-3x text-success mb-3"></i>
                            <h5 class="card-title">Ogrenci Isleri</h5>
                            <p class="card-text text-muted small">
                                Kayit, transkript ve belge talepleri.
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
                            <h5 class="card-title">Bilgi Islem</h5>
                            <p class="card-text text-muted small">
                                Sistem erisimi ve teknik destek.
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
                            <h5 class="card-title">Akademik Isler</h5>
                            <p class="card-text text-muted small">
                                Ders programlari ve akademik takvim.
                            </p>
                            <p class="mb-0">
                                <i class="fas fa-phone me-1"></i>+90 (212) 555 00 03
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </main>
</asp:Content>
