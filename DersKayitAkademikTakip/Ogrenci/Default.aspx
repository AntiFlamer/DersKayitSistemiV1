<%@ Page Title="��renci Paneli" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DersKayitAkademikTakip.Ogrenci.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid mt-4">
        <div class="row">
            <div class="col-md-12">
                <h2><i class="fas fa-graduation-cap"></i> ��renci Paneli</h2>
                <p class="text-muted">Ho� geldiniz, <strong><asp:Literal ID="litOgrenciAd" runat="server" /></strong></p>
                <hr />
            </div>
        </div>

        <!-- �STAT�ST�K KARTLARI -->
        <div class="row">
            <div class="col-md-3">
                <div class="card text-white bg-primary mb-3">
                    <div class="card-body">
                        <h5 class="card-title"><i class="fas fa-book"></i> Kay�tl� Dersler</h5>
                        <h2 class="card-text" id="toplamKayitliDers" runat="server">0</h2>
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="card text-white bg-success mb-3">
                    <div class="card-body">
                        <h5 class="card-title"><i class="fas fa-check"></i> Onayl� Dersler</h5>
                        <h2 class="card-text" id="onayliDersler" runat="server">0</h2>
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="card text-white bg-warning mb-3">
                    <div class="card-body">
                        <h5 class="card-title"><i class="fas fa-clock"></i> Bekleyen Kay�tlar</h5>
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

        <!-- HIZLI ER���M -->
        <div class="row mt-4">
            <div class="col-md-12">
                <h4>H�zl� Eri�im</h4>
                <div class="d-grid gap-2 d-md-block">
                    <a href="DersKayit.aspx" class="btn btn-primary me-2">
                        <i class="fas fa-plus-circle"></i> Yeni Ders Kayd�
                    </a>
                    <a href="Derslerim.aspx" class="btn btn-success me-2">
                        <i class="fas fa-book-open"></i> Kay�tl� Derslerim
                    </a>
                    <a href="Notlarim.aspx" class="btn btn-info me-2">
                        <i class="fas fa-chart-bar"></i> Notlar�m
                    </a>
                </div>
            </div>
        </div>

        <!-- SON KAYITLAR -->
        <div class="row mt-4">
            <div class="col-md-12">
                <div class="card shadow">
                    <div class="card-header bg-secondary text-white">
                        <h5><i class="fas fa-history"></i> Son Ders Kay�tlar�m</h5>
                    </div>
                    <div class="card-body">
                        <asp:GridView ID="gvSonKayitlar" runat="server" AutoGenerateColumns="false" 
                            CssClass="table table-striped table-hover" EmptyDataText="Hen�z ders kayd�n�z bulunmamaktad�r.">
                            <Columns>
                                <asp:BoundField DataField="ders_kodu" HeaderText="Ders Kodu" />
                                <asp:BoundField DataField="ders_adi" HeaderText="Ders Ad�" />
                                <asp:BoundField DataField="kredi" HeaderText="Kredi" ItemStyle-CssClass="text-center" />
                                <asp:BoundField DataField="hoca_adi" HeaderText="��retim G�revlisi" />
                                <asp:BoundField DataField="kayit_tarihi" HeaderText="Kay�t Tarihi" DataFormatString="{0:dd.MM.yyyy HH:mm}" />
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
