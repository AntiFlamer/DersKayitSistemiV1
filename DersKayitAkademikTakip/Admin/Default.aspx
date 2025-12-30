<%@ Page Title="Admin Paneli" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DersKayitAkademikTakip.Admin.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid mt-4">
        <div class="row">
            <div class="col-md-12">
                <h2><i class="fas fa-tachometer-alt"></i> Admin Dashboard</h2>
                <hr />
            </div>
        </div>

        <!-- İSTATİSTİK KARTLARI -->
        <div class="row">
            <div class="col-md-3">
                <div class="card text-white bg-primary mb-3">
                    <div class="card-body">
                        <h5 class="card-title">Toplam Kullanıcı</h5>
                        <h2 class="card-text" id="toplamKullanici" runat="server">0</h2>
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="card text-white bg-success mb-3">
                    <div class="card-body">
                        <h5 class="card-title">Toplam Ders</h5>
                        <h2 class="card-text" id="toplamDers" runat="server">0</h2>
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="card text-white bg-warning mb-3">
                    <div class="card-body">
                        <h5 class="card-title">Toplam Kayıt</h5>
                        <h2 class="card-text" id="toplamKayit" runat="server">0</h2>
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="card text-white bg-info mb-3">
                    <div class="card-body">
                        <h5 class="card-title">Aktif Dönem</h5>
                        <h4 class="card-text" id="aktifDonem" runat="server">-</h4>
                    </div>
                </div>
            </div>
        </div>

        <!-- HIZLI ERİŞİM BUTONLARI -->
        <div class="row mt-4">
            <div class="col-md-12">
                <h4>Hızlı Erişim</h4>
                <div class="d-grid gap-2 d-md-block">
                    <a href="KullaniciEkle.aspx" class="btn btn-primary me-2">
                        <i class="fas fa-user-plus"></i> Yeni Kullanıcı Ekle
                    </a>
                    <a href="DersEkle.aspx" class="btn btn-success me-2">
                        <i class="fas fa-book"></i> Yeni Ders Ekle
                    </a>
                    <a href="Kullanicilar.aspx" class="btn btn-info me-2">
                        <i class="fas fa-users"></i> Kullanıcıları Yönet
                    </a>
                    <a href="Dersler.aspx" class="btn btn-secondary me-2">
                        <i class="fas fa-list"></i> Dersleri Yönet
                    </a>
                    <a href="AkademikTakvim.aspx" class="btn btn-warning">
                        <i class="fas fa-calendar-alt"></i> Akademik Takvim
                    </a>
                </div>
            </div>
        </div>

        <!-- AKADEMİK TAKVİM DURUM KARTI -->
        <div class="row mt-4">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-header bg-dark text-white">
                        <i class="fas fa-calendar-check"></i> Akademik Takvim Durumu
                    </div>
                    <div class="card-body" id="takvimDurum" runat="server">
                        <p class="text-muted">Yükleniyor...</p>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>