<%@ Page Title="Kayıtlı Derslerim" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Derslerim.aspx.cs" Inherits="DersKayitAkademikTakip.Ogrenci.Derslerim" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid mt-4">
        <h2><i class="fas fa-book-open"></i> Kayıtlı Derslerim</h2>
        <a href="Default.aspx" class="btn btn-secondary btn-sm mb-3"><i class="fas fa-arrow-left"></i> Geri</a>
        <hr />

        <!-- UYARI PANELİ (Takvim durumu) -->
        <asp:Panel ID="pnlUyari" runat="server" Visible="false" CssClass="alert alert-warning alert-dismissible fade show">
            <asp:Label ID="lblUyari" runat="server"></asp:Label>
            <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
        </asp:Panel>

        <!-- BAŞARI PANELİ -->
        <asp:Panel ID="pnlBasari" runat="server" Visible="false" CssClass="alert alert-success alert-dismissible fade show">
            <asp:Label ID="lblBasari" runat="server"></asp:Label>
            <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
        </asp:Panel>

        <!-- HATA PANELİ -->
        <asp:Panel ID="pnlHata" runat="server" Visible="false" CssClass="alert alert-danger alert-dismissible fade show">
            <asp:Label ID="lblHata" runat="server"></asp:Label>
            <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
        </asp:Panel>

        <asp:GridView ID="gvKayitlar" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered"
            DataKeyNames="kayit_id" OnRowCommand="gvKayitlar_RowCommand" EmptyDataText="Herhangi bir kayıt bulunamadı.">
            <Columns>
                <asp:BoundField DataField="ders_kodu" HeaderText="Ders Kodu" />
                <asp:BoundField DataField="ders_adi" HeaderText="Ders Adı" />
                <asp:BoundField DataField="kredi" HeaderText="Kredi" ItemStyle-CssClass="text-center" />
                <asp:TemplateField HeaderText="Durum">
                    <ItemTemplate>
                        <%# Container.DataItem != null ? (string)Eval("durum_label") : "" %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="İşlemler" ItemStyle-Width="140px">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnIptal" runat="server" CommandName="Iptal" CommandArgument='<%# Eval("kayit_id") %>' 
                            CssClass="btn btn-danger btn-sm" Text="Kayıt İptal" 
                            OnClientClick="return confirm('Kaydı iptal etmek istediğinize emin misiniz?');" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
