<%@ Page Title="Ders Yönetimi" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dersler.aspx.cs" Inherits="DersKayitAkademikTakip.Admin.Dersler" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid mt-4">
        <div class="row">
            <div class="col-md-12">
                <div class="card shadow">
                    <div class="card-header bg-primary text-white d-flex justify-content-between align-items-center">
                        <h4 class="mb-0"><i class="fas fa-book"></i> Ders Yönetimi</h4>
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

                        <!-- ARAMA VE FÝLTRELEME -->
                        <div class="row mb-3">
                            <div class="col-md-4">
                                <div class="input-group">
                                    <asp:TextBox ID="txtArama" runat="server" CssClass="form-control" placeholder="Ders kodu veya adý ara..." />
                                    <asp:Button ID="btnAra" runat="server" Text="Ara" CssClass="btn btn-outline-secondary" OnClick="btnAra_Click" />
                                </div>
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddlDersTipiFiltre" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDersTipiFiltre_SelectedIndexChanged">
                                    <asp:ListItem Value="">Tüm Ders Tipleri</asp:ListItem>
                                    <asp:ListItem Value="zorunlu">Zorunlu</asp:ListItem>
                                    <asp:ListItem Value="secmeli">Seçmeli</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddlHocaFiltre" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlHocaFiltre_SelectedIndexChanged">
                                    <asp:ListItem Value="">Tüm Hocalar</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2 text-end">
                                <asp:Button ID="btnYenile" runat="server" Text="Yenile" CssClass="btn btn-secondary" OnClick="btnYenile_Click" />
                            </div>
                        </div>

                        <!-- DERS LÝSTESÝ -->
                        <asp:GridView ID="gvDersler" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered"
                            DataKeyNames="ders_kodu" OnRowCommand="gvDersler_RowCommand"
                            EmptyDataText="Ders bulunamadý.">
                            <Columns>
                                <asp:BoundField DataField="ders_kodu" HeaderText="Ders Kodu" />
                                <asp:BoundField DataField="ders_adi" HeaderText="Ders Adý" />
                                <asp:BoundField DataField="kredi" HeaderText="Kredi" ItemStyle-Width="70px" ItemStyle-CssClass="text-center" />
                                <asp:BoundField DataField="akts_kredi" HeaderText="AKTS" ItemStyle-Width="70px" ItemStyle-CssClass="text-center" />
                                <asp:BoundField DataField="kontenjan" HeaderText="Kontenjan" ItemStyle-Width="90px" ItemStyle-CssClass="text-center" />
                                <asp:BoundField DataField="ders_donemi" HeaderText="Dönem" ItemStyle-Width="100px" />
                                <asp:TemplateField HeaderText="Hoca">
                                    <ItemTemplate>
                                        <%# Eval("hoca_adi") != DBNull.Value ? Eval("hoca_adi").ToString() : "<span class='text-muted'>Atanmamýþ</span>" %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ders Tipi">
                                    <ItemTemplate>
                                        <span class='badge <%# GetDersTipiBadgeClass(Eval("ders_tipi").ToString()) %>'>
                                            <%# GetDersTipiDisplayName(Eval("ders_tipi").ToString()) %>
                                        </span>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ýþlemler" ItemStyle-Width="180px">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDuzenle" runat="server" CommandName="Duzenle" CommandArgument='<%# Eval("ders_kodu") %>'
                                            CssClass="btn btn-warning btn-sm" Text="Düzenle" />
                                        <asp:LinkButton ID="btnSil" runat="server" CommandName="Sil" CommandArgument='<%# Eval("ders_kodu") %>'
                                            CssClass="btn btn-danger btn-sm" Text="Sil" OnClientClick="return confirm('Bu dersi silmek istediðinizden emin misiniz?');" />
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
