<%@ Page Title="Kullanıcı Ekle" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="KullaniciEkle.aspx.cs" Inherits="DersKayitAkademikTakip.Admin.KullaniciEkle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid mt-4">
        <div class="row">
            <div class="col-md-8 mx-auto">
                <div class="card shadow">
                    <div class="card-header bg-primary text-white">
                        <h4><i class="fas fa-user-plus"></i> Yeni Kullanıcı Ekle</h4>
                    </div>
                    <div class="card-body">
                        <asp:Panel ID="SuccessPanel" runat="server" Visible="false" CssClass="alert alert-success">
                            Kullanıcı başarıyla eklendi!
                        </asp:Panel>
                        
                        <asp:Panel ID="ErrorPanel" runat="server" Visible="false" CssClass="alert alert-danger">
                            <asp:Literal ID="ErrorText" runat="server" />
                        </asp:Panel>

                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>TC Kimlik No</label>
                                    <asp:TextBox ID="txtTC" runat="server" CssClass="form-control" MaxLength="11" placeholder="12345678901" />
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Rol</label>
                                    <asp:DropDownList ID="ddlRol" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="ogrenci">Öğrenci</asp:ListItem>
                                        <asp:ListItem Value="hoca">Öğretim Görevlisi</asp:ListItem>
                                        <asp:ListItem Value="admin">Admin</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="row mt-3">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Ad</label>
                                    <asp:TextBox ID="txtAd" runat="server" CssClass="form-control" placeholder="Ad" />
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Soyad</label>
                                    <asp:TextBox ID="txtSoyad" runat="server" CssClass="form-control" placeholder="Soyad" />
                                </div>
                            </div>
                        </div>

                        <div class="row mt-3">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>E-posta</label>
                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" placeholder="ornek@universite.edu.tr" />
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Şifre</label>
                                    <asp:TextBox ID="txtSifre" runat="server" CssClass="form-control" TextMode="Password" placeholder="Şifre" />
                                </div>
                            </div>
                        </div>

                        <div class="d-grid gap-2 d-md-flex justify-content-md-end mt-4">
                            <asp:Button ID="btnGeri" runat="server" Text="Geri" CssClass="btn btn-secondary me-md-2" 
                                       PostBackUrl="~/Admin/Default.aspx" />
                            <asp:Button ID="btnKaydet" runat="server" Text="Kullanıcıyı Kaydet" 
                                       CssClass="btn btn-success" OnClick="btnKaydet_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>