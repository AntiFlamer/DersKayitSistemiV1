<%@ Page Title="Ders Düzenle" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DerslerDuzenle.aspx.cs" Inherits="DersKayitAkademikTakip.Admin.DerslerDuzenle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <div class="row">
            <div class="col-md-12">
                <div class="card shadow">
                    <div class="card-header bg-warning text-white d-flex justify-content-between align-items-center">
                        <h4 class="mb-0"><i class="fas fa-edit"></i> Ders Düzenle</h4>
                        <a href="Dersler.aspx" class="btn btn-light btn-sm">
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
                                <label class="form-label">Ders Kodu <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtDersKodu" runat="server" CssClass="form-control bg-light" ReadOnly="true" />
                                <small class="text-muted">Ders kodu deðiþtirilemez</small>
                            </div>
                            <div class="col-md-6">
                                <label class="form-label">Ders Adý <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtDersAdi" runat="server" CssClass="form-control" />
                            </div>
                        </div>

                        <div class="row mb-3">
                            <div class="col-md-3">
                                <label class="form-label">Kredi <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtKredi" runat="server" CssClass="form-control" TextMode="Number" />
                            </div>
                            <div class="col-md-3">
                                <label class="form-label">AKTS Kredi <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtAktsKredi" runat="server" CssClass="form-control" TextMode="Number" />
                            </div>
                            <div class="col-md-3">
                                <label class="form-label">Kontenjan <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtKontenjan" runat="server" CssClass="form-control" TextMode="Number" />
                            </div>
                            <div class="col-md-3">
                                <label class="form-label">Dönem <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtDersDonemi" runat="server" CssClass="form-control" placeholder="Örn: Güz 2024" />
                            </div>
                        </div>

                        <div class="row mb-3">
                            <div class="col-md-6">
                                <label class="form-label">Hoca</label>
                                <asp:DropDownList ID="ddlHoca" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="">-- Seçiniz --</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-6">
                                <label class="form-label">Ders Tipi <span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlDersTipi" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="">-- Seçiniz --</asp:ListItem>
                                    <asp:ListItem Value="zorunlu">Zorunlu</asp:ListItem>
                                    <asp:ListItem Value="secmeli">Seçmeli</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-12 text-end">
                                <asp:Button ID="btnIptal" runat="server" Text="Ýptal" CssClass="btn btn-secondary" OnClick="btnIptal_Click" CausesValidation="false" />
                                <asp:Button ID="btnGuncelle" runat="server" Text="Güncelle" CssClass="btn btn-primary" OnClick="btnGuncelle_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
