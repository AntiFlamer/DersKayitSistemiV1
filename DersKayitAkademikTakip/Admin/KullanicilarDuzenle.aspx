<%@ Page Title="Kullan�c� D�zenle" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="KullanicilarDuzenle.aspx.cs" Inherits="DersKayitAkademikTakip.Admin.KullanicilarDuzenle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <div class="row">
            <div class="col-md-12">
                <div class="card shadow">
                    <div class="card-header bg-info text-white d-flex justify-content-between align-items-center">
                        <h4 class="mb-0"><i class="fas fa-user-edit"></i> Kullan�c� D�zenle</h4>
                        <a href="Kullanicilar.aspx" class="btn btn-light btn-sm">
                            <i class="fas fa-arrow-left"></i> Geri
                        </a>
                    </div>
                    <div class="card-body">
                        <asp:Panel ID="SuccessPanel" runat="server" Visible="false" CssClass="alert alert-success">
                            <asp:Literal ID="SuccessText" runat="server" />
                        </asp:Panel>
                        
                        <asp:Panel ID="ErrorPanel" runat="server" Visible="false" CssClass="alert alert-danger">
                            <asp:Literal ID="ErrorText" runat="server" />
                        </asp:Panel>

                        <div class="row mb-3">
                            <div class="col-md-6">
                                <label class="form-label">TC Kimlik No <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtTC" runat="server" CssClass="form-control bg-light" ReadOnly="true" MaxLength="11" />
                                <small class="text-muted">TC Kimlik numaras� de�i�tirilemez</small>
                            </div>
                            <div class="col-md-6">
                                <label class="form-label">E-posta <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" />
                            </div>
                        </div>

                        <div class="row mb-3">
                            <div class="col-md-6">
                                <label class="form-label">Ad <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtAd" runat="server" CssClass="form-control" MaxLength="50" />
                            </div>
                            <div class="col-md-6">
                                <label class="form-label">Soyad <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtSoyad" runat="server" CssClass="form-control" MaxLength="50" />
                            </div>
                        </div>

                        <div class="row mb-3">
                            <div class="col-md-6">
                                <label class="form-label">Rol <span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlRol" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="ogrenci">��renci</asp:ListItem>
                                    <asp:ListItem Value="hoca">��retim G�revlisi</asp:ListItem>
                                    <asp:ListItem Value="admin">Admin</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-6">
                                <label class="form-label">Yeni �ifre</label>
                                <asp:TextBox ID="txtSifre" runat="server" CssClass="form-control" TextMode="Password" placeholder="Bo� b�rak�l�rsa de�i�mez" />
                                <small class="text-muted">�ifre de�i�tirmek i�in doldurun, de�ilse bo� b�rak�n</small>
                            </div>
                        </div>

                        <div class="form-check mb-3">
                            <asp:CheckBox ID="chkAktif" runat="server" CssClass="form-check-input" />
                            <label class="form-check-label">
                                Kullan�c� Aktif
                            </label>
                        </div>

                        <div class="row">
                            <div class="col-md-12 text-end">
                                <asp:Button ID="btnIptal" runat="server" Text="�ptal" CssClass="btn btn-secondary" OnClick="btnIptal_Click" CausesValidation="false" />
                                <asp:Button ID="btnGuncelle" runat="server" Text="G�ncelle" CssClass="btn btn-primary" OnClick="btnGuncelle_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
