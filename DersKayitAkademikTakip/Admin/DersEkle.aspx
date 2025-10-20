<%@ Page Title="Ders Ekle" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DersEkle.aspx.cs" Inherits="DersKayitAkademikTakip.Admin.DersEkle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid mt-4">
        <div class="row">
            <div class="col-md-8 mx-auto">
                <div class="card shadow">
                    <div class="card-header bg-success text-white">
                        <h4><i class="fas fa-book"></i> Yeni Ders Ekle</h4>
                    </div>
                    <div class="card-body">
                        <asp:Panel ID="SuccessPanel" runat="server" Visible="false" CssClass="alert alert-success">
                            Ders baþarýyla eklendi!
                        </asp:Panel>
                        
                        <asp:Panel ID="ErrorPanel" runat="server" Visible="false" CssClass="alert alert-danger">
                            <asp:Literal ID="ErrorText" runat="server" />
                        </asp:Panel>

                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Ders Kodu <span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtDersKodu" runat="server" CssClass="form-control" MaxLength="10" placeholder="BIL101" />
                                    <small class="form-text text-muted">Örnek: BIL101, MAT201</small>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Ders Adý <span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtDersAdi" runat="server" CssClass="form-control" MaxLength="100" placeholder="Algoritma ve Programlama" />
                                </div>
                            </div>
                        </div>

                        <div class="row mt-3">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Kredi <span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtKredi" runat="server" CssClass="form-control" TextMode="Number" placeholder="3" />
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>AKTS Kredi <span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtAktsKredi" runat="server" CssClass="form-control" TextMode="Number" placeholder="5" />
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Kontenjan <span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtKontenjan" runat="server" CssClass="form-control" TextMode="Number" placeholder="50" />
                                </div>
                            </div>
                        </div>

                        <div class="row mt-3">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Ders Dönemi <span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtDersDonemi" runat="server" CssClass="form-control" MaxLength="20" placeholder="2025-Güz" />
                                    <small class="form-text text-muted">Örnek: 2025-Güz, 2025-Bahar</small>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Ders Tipi <span class="text-danger">*</span></label>
                                    <asp:DropDownList ID="ddlDersTipi" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="">-- Seçiniz --</asp:ListItem>
                                        <asp:ListItem Value="zorunlu">Zorunlu</asp:ListItem>
                                        <asp:ListItem Value="secmeli">Seçmeli</asp:ListItem>
                                        <asp:ListItem Value="mesleki_secmeli">Mesleki Seçmeli</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Öðretim Görevlisi</label>
                                    <asp:DropDownList ID="ddlHoca" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="">-- Seçiniz --</asp:ListItem>
                                    </asp:DropDownList>
                                    <small class="form-text text-muted">Opsiyonel - Boþ býrakýlabilir</small>
                                </div>
                            </div>
                        </div>

                        <div class="d-grid gap-2 d-md-flex justify-content-md-end mt-4">
                            <asp:Button ID="btnGeri" runat="server" Text="Geri" CssClass="btn btn-secondary me-md-2" 
                                       PostBackUrl="~/Admin/Default.aspx" />
                            <asp:Button ID="btnKaydet" runat="server" Text="Dersi Kaydet" 
                                       CssClass="btn btn-success" OnClick="btnKaydet_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
