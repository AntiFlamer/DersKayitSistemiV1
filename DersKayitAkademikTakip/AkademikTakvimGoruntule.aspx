<%@ Page Title="Akademik Takvim" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AkademikTakvimGoruntule.aspx.cs" Inherits="DersKayitAkademikTakip.AkademikTakvimGoruntule" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <!-- Page Header -->
        <section class="py-4 bg-primary text-white rounded-3 mb-5">
            <div class="container text-center">
                <h1 class="display-5 fw-bold">
                    <i class="fas fa-calendar-alt me-3"></i>Akademik Takvim
                </h1>
                <p class="lead mb-0" id="donemBaslik" runat="server">Aktif dönem bilgileri</p>
            </div>
        </section>

        <!-- Takvim Bulunamadý Uyarýsý -->
        <asp:Panel ID="pnlTakvimYok" runat="server" Visible="false">
            <div class="alert alert-warning text-center py-5">
                <i class="fas fa-exclamation-triangle fa-3x mb-3"></i>
                <h4>Aktif Akademik Takvim Bulunamadý</h4>
                <p class="mb-0">Henüz aktif bir akademik takvim tanýmlanmamýþ. Lütfen daha sonra tekrar kontrol edin.</p>
            </div>
        </asp:Panel>

        <!-- Takvim Ýçeriði -->
        <asp:Panel ID="pnlTakvim" runat="server">
            <!-- Dönem Özeti -->
            <div class="row mb-4">
                <div class="col-12">
                    <div class="card border-0 shadow-sm">
                        <div class="card-body text-center py-4">
                            <h3 class="text-primary mb-2" id="akademikYil" runat="server"></h3>
                            <h5 class="text-muted" id="donemAdi" runat="server"></h5>
                            <p class="mb-0">
                                <span class="badge bg-info fs-6 px-3 py-2" id="donemTarihi" runat="server"></span>
                            </p>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Açýk/Kapalý Durumlar -->
            <div class="row mb-4 g-3">
                <div class="col-md-3 col-sm-6">
                    <div class="card h-100 text-center" id="cardDersKayit" runat="server">
                        <div class="card-body">
                            <i class="fas fa-book fa-2x mb-2" id="iconDersKayit" runat="server"></i>
                            <h6 class="card-title mb-1">Ders Kaydý</h6>
                            <span class="badge" id="badgeDersKayit" runat="server"></span>
                        </div>
                    </div>
                </div>
                <div class="col-md-3 col-sm-6">
                    <div class="card h-100 text-center" id="cardVize" runat="server">
                        <div class="card-body">
                            <i class="fas fa-file-alt fa-2x mb-2" id="iconVize" runat="server"></i>
                            <h6 class="card-title mb-1">Vize Dönemi</h6>
                            <span class="badge" id="badgeVize" runat="server"></span>
                        </div>
                    </div>
                </div>
                <div class="col-md-3 col-sm-6">
                    <div class="card h-100 text-center" id="cardFinal" runat="server">
                        <div class="card-body">
                            <i class="fas fa-graduation-cap fa-2x mb-2" id="iconFinal" runat="server"></i>
                            <h6 class="card-title mb-1">Final Dönemi</h6>
                            <span class="badge" id="badgeFinal" runat="server"></span>
                        </div>
                    </div>
                </div>
                <div class="col-md-3 col-sm-6">
                    <div class="card h-100 text-center" id="cardButunleme" runat="server">
                        <div class="card-body">
                            <i class="fas fa-redo fa-2x mb-2" id="iconButunleme" runat="server"></i>
                            <h6 class="card-title mb-1">Bütünleme</h6>
                            <span class="badge" id="badgeButunleme" runat="server"></span>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Detaylý Takvim Tablosu -->
            <div class="card border-0 shadow-sm mb-4">
                <div class="card-header bg-dark text-white">
                    <h5 class="mb-0"><i class="fas fa-table me-2"></i>Detaylý Akademik Takvim</h5>
                </div>
                <div class="card-body p-0">
                    <div class="table-responsive">
                        <table class="table table-hover mb-0">
                            <thead class="table-light">
                                <tr>
                                    <th style="width: 30%;"><i class="fas fa-tasks me-2"></i>Etkinlik</th>
                                    <th style="width: 25%;"><i class="fas fa-play me-2"></i>Baþlangýç</th>
                                    <th style="width: 25%;"><i class="fas fa-stop me-2"></i>Bitiþ</th>
                                    <th style="width: 20%;"><i class="fas fa-info-circle me-2"></i>Durum</th>
                                </tr>
                            </thead>
                            <tbody>
                                <!-- Dönem -->
                                <tr class="table-primary">
                                    <td><strong><i class="fas fa-calendar me-2"></i>Dönem</strong></td>
                                    <td id="tdDonemBaslangic" runat="server">-</td>
                                    <td id="tdDonemBitis" runat="server">-</td>
                                    <td><span class="badge bg-primary">Aktif Dönem</span></td>
                                </tr>
                                <!-- Ders Kaydý -->
                                <tr>
                                    <td><i class="fas fa-book text-success me-2"></i>Ders Kaydý</td>
                                    <td id="tdDersKayitBaslangic" runat="server">-</td>
                                    <td id="tdDersKayitBitis" runat="server">-</td>
                                    <td id="tdDersKayitDurum" runat="server"></td>
                                </tr>
                                <!-- Vize Sýnavlarý -->
                                <tr>
                                    <td><i class="fas fa-file-alt text-info me-2"></i>Vize Sýnavlarý</td>
                                    <td id="tdVizeBaslangic" runat="server">-</td>
                                    <td id="tdVizeBitis" runat="server">-</td>
                                    <td id="tdVizeDurum" runat="server"></td>
                                </tr>
                                <!-- Vize Not Giriþi -->
                                <tr class="table-light">
                                    <td class="ps-4"><small><i class="fas fa-keyboard me-2"></i>Not Giriþi Son Tarih</small></td>
                                    <td>-</td>
                                    <td id="tdVizeNotGiris" runat="server">-</td>
                                    <td id="tdVizeNotGirisDurum" runat="server"></td>
                                </tr>
                                <!-- Final Sýnavlarý -->
                                <tr>
                                    <td><i class="fas fa-graduation-cap text-warning me-2"></i>Final Sýnavlarý</td>
                                    <td id="tdFinalBaslangic" runat="server">-</td>
                                    <td id="tdFinalBitis" runat="server">-</td>
                                    <td id="tdFinalDurum" runat="server"></td>
                                </tr>
                                <!-- Final Not Giriþi -->
                                <tr class="table-light">
                                    <td class="ps-4"><small><i class="fas fa-keyboard me-2"></i>Not Giriþi Son Tarih</small></td>
                                    <td>-</td>
                                    <td id="tdFinalNotGiris" runat="server">-</td>
                                    <td id="tdFinalNotGirisDurum" runat="server"></td>
                                </tr>
                                <!-- Bütünleme Sýnavlarý -->
                                <tr>
                                    <td><i class="fas fa-redo text-danger me-2"></i>Bütünleme Sýnavlarý</td>
                                    <td id="tdButunlemeBaslangic" runat="server">-</td>
                                    <td id="tdButunlemeBitis" runat="server">-</td>
                                    <td id="tdButunlemeDurum" runat="server"></td>
                                </tr>
                                <!-- Bütünleme Not Giriþi -->
                                <tr class="table-light">
                                    <td class="ps-4"><small><i class="fas fa-keyboard me-2"></i>Not Giriþi Son Tarih</small></td>
                                    <td>-</td>
                                    <td id="tdButunlemeNotGiris" runat="server">-</td>
                                    <td id="tdButunlemeNotGirisDurum" runat="server"></td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>

            <!-- Bilgi Notu -->
            <div class="alert alert-info">
                <i class="fas fa-info-circle me-2"></i>
                <strong>Not:</strong> Akademik takvimde belirtilen tarihler resmi tarihlerdir. 
                Ýþlemlerinizi son günlere býrakmadan tamamlamanýz önerilir.
            </div>
        </asp:Panel>
    </main>
</asp:Content>
