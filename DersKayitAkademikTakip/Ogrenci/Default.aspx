<%@ Page Title="Öðrenci Paneli" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DersKayitAkademikTakip.Ogrenci.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid mt-4">
        <div class="row">
            <div class="col-md-12">
                <h2><i class="fas fa-graduation-cap"></i> Öðrenci Paneli</h2>
                <p class="text-muted">Hoþ geldiniz, <strong><asp:Literal ID="litOgrenciAd" runat="server" /></strong></p>
                <hr />
            </div>
        </div>

        <!-- ÝSTATÝSTÝK KARTLARI -->
        <div class="row">
            <div class="col-md-3">
                <div class="card text-white bg-primary mb-3">
                    <div class="card-body">
                        <h5 class="card-title"><i class="fas fa-book"></i> Kayýtlý Dersler</h5>
                        <h2 class="card-text" id="toplamKayitliDers" runat="server">0</h2>
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="card text-white bg-success mb-3">
                    <div class="card-body">
                        <h5 class="card-title"><i class="fas fa-check"></i> Onaylý Dersler</h5>
                        <h2 class="card-text" id="onayliDersler" runat="server">0</h2>
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="card text-white bg-warning mb-3">
                    <div class="card-body">
                        <h5 class="card-title"><i class="fas fa-clock"></i> Bekleyen Kayýtlar</h5>
                        <h2 class="card-text" id="bekleyenKayitlar" runat="server">0</h2>
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="card text-white bg-info mb-3">
                    <div class="card-body">
                        <h5 class="card-title"><i class="fas fa-star"></i> Genel Ortalama</h5>
                        <h2 class="card-text" id="genelOrtalama" runat="server">0.00</h2>
                    </div>
                </div>
            </div>
        </div>

        <!-- HIZLI ERÝÞÝM -->
        <div class="row mt-4">
            <div class="col-md-12">
                <h4>Hýzlý Eriþim</h4>
                <div class="d-grid gap-2 d-md-block">
                    <a href="DersKayit.aspx" class="btn btn-primary me-2">
                        <i class="fas fa-plus-circle"></i> Yeni Ders Kaydý
                    </a>
                    <a href="Derslerim.aspx" class="btn btn-success me-2">
                        <i class="fas fa-book-open"></i> Kayýtlý Derslerim
                    </a>
                    <a href="Notlarim.aspx" class="btn btn-info me-2">
                        <i class="fas fa-chart-bar"></i> Notlarým
                    </a>
                </div>
            </div>
        </div>

        <!-- SON KAYITLAR -->
        <div class="row mt-4">
            <div class="col-md-12">
                <div class="card shadow">
                    <div class="card-header bg-secondary text-white">
                        <h5><i class="fas fa-history"></i> Son Ders Kayýtlarým</h5>
                    </div>
                    <div class="card-body">
                        <asp:GridView ID="gvSonKayitlar" runat="server" AutoGenerateColumns="false" 
                            CssClass="table table-striped table-hover" EmptyDataText="Henüz ders kaydýnýz bulunmamaktadýr.">
                            <Columns>
                                <asp:BoundField DataField="ders_kodu" HeaderText="Ders Kodu" />
                                <asp:BoundField DataField="ders_adi" HeaderText="Ders Adý" />
                                <asp:BoundField DataField="kredi" HeaderText="Kredi" ItemStyle-CssClass="text-center" />
                                <asp:BoundField DataField="hoca_adi" HeaderText="Öðretim Görevlisi" />
                                <asp:BoundField DataField="kayit_tarihi" HeaderText="Kayýt Tarihi" DataFormatString="{0:dd.MM.yyyy HH:mm}" />
                                <asp:TemplateField HeaderText="Durum">
                                    <ItemTemplate>
                                        <%# GetDurumBadge(Eval("durum").ToString()) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
