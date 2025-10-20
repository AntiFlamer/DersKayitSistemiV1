<%@ Page Title="Kullanıcı Yönetimi" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Kullanicilar.aspx.cs" Inherits="DersKayitAkademikTakip.Admin.Kullanicilar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid mt-4">
        <div class="row">
            <div class="col-md-12">
                <div class="card shadow">
                    <div class="card-header bg-info text-white d-flex justify-content-between align-items-center">
                        <h4 class="mb-0"><i class="fas fa-users"></i> Kullanıcı Yönetimi</h4>
                        <a href="Default.aspx" class="btn btn-light btn-sm">
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

                        <!-- ARAMA VE FİLTRELEME -->
                        <div class="row mb-3">
                            <div class="col-md-4">
                                <div class="input-group">
                                    <asp:TextBox ID="txtArama" runat="server" CssClass="form-control" placeholder="İsim, soyisim veya e-posta ara..." />
                                    <asp:Button ID="btnAra" runat="server" Text="Ara" CssClass="btn btn-outline-secondary" OnClick="btnAra_Click" />
                                </div>
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddlRolFiltre" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlRolFiltre_SelectedIndexChanged">
                                    <asp:ListItem Value="">Tüm Roller</asp:ListItem>
                                    <asp:ListItem Value="admin">Admin</asp:ListItem>
                                    <asp:ListItem Value="hoca">Öğretim Görevlisi</asp:ListItem>
                                    <asp:ListItem Value="ogrenci">Öğrenci</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddlAktiflik" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlAktiflik_SelectedIndexChanged">
                                    <asp:ListItem Value="">Tümü</asp:ListItem>
                                    <asp:ListItem Value="1">Aktif</asp:ListItem>
                                    <asp:ListItem Value="0">Pasif</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2 text-end">
                                <asp:Button ID="btnYenile" runat="server" Text="Yenile" CssClass="btn btn-secondary" OnClick="btnYenile_Click" />
                            </div>
                        </div>

                        <!-- KULLANICI LİSTESİ -->
                        <asp:GridView ID="gvKullanicilar" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered"
                            DataKeyNames="kullanici_id" OnRowCommand="gvKullanicilar_RowCommand" OnRowDataBound="gvKullanicilar_RowDataBound"
                            EmptyDataText="Kullanıcı bulunamadı.">
                            <Columns>
                                <asp:BoundField DataField="kullanici_id" HeaderText="ID" ItemStyle-Width="50px" />
                                <asp:BoundField DataField="tc_kimlik" HeaderText="TC Kimlik" />
                                <asp:BoundField DataField="ad" HeaderText="Ad" />
                                <asp:BoundField DataField="soyad" HeaderText="Soyad" />
                                <asp:BoundField DataField="email" HeaderText="E-posta" />
                                <asp:TemplateField HeaderText="Rol">
                                    <ItemTemplate>
                                        <span class='badge <%# GetRoleBadgeClass(Eval("rol").ToString()) %>'>
                                            <%# GetRoleDisplayName(Eval("rol").ToString()) %>
                                        </span>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Durum">
                                    <ItemTemplate>
                                        <span class='badge <%# (bool)Eval("aktif") ? "bg-success" : "bg-danger" %>'>
                                            <%# (bool)Eval("aktif") ? "Aktif" : "Pasif" %>
                                        </span>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="İşlemler" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnAktifPasif" runat="server" CommandName="AktifPasif" CommandArgument='<%# Eval("kullanici_id") %>'
                                            CssClass='btn btn-sm <%# (bool)Eval("aktif") ? "btn-warning" : "btn-success" %>'
                                            Text='<%# (bool)Eval("aktif") ? "Pasif Yap" : "Aktif Yap" %>' />
                                        <asp:LinkButton ID="btnSil" runat="server" CommandName="Sil" CommandArgument='<%# Eval("kullanici_id") %>'
                                            CssClass="btn btn-danger btn-sm" Text="Sil" OnClientClick="return confirm('Bu kullanıcıyı silmek istediğinizden emin misiniz?');" />
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