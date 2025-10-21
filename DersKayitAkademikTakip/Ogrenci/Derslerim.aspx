<%@ Page Title="Kay�tl� Derslerim" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Derslerim.aspx.cs" Inherits="DersKayitAkademikTakip.Ogrenci.Derslerim" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid mt-4">
        <h2><i class="fas fa-book-open"></i> Kay�tl� Derslerim</h2>
        <hr />
        <asp:GridView ID="gvKay�tlar" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered"
            DataKeyNames="kayit_id" OnRowCommand="gvKay�tlar_RowCommand" EmptyDataText="Herhangi bir kay�t bulunamad�.">
            <Columns>
                <asp:BoundField DataField="ders_kodu" HeaderText="Ders Kodu" />
                <asp:BoundField DataField="ders_adi" HeaderText="Ders Ad�" />
                <asp:BoundField DataField="kredi" HeaderText="Kredi" ItemStyle-CssClass="text-center" />
                <asp:TemplateField HeaderText="Durum">
                    <ItemTemplate>
                        <%# Container.DataItem != null ? (string)Eval("durum_label") : "" %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="��lemler" ItemStyle-Width="140px">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnIptal" runat="server" CommandName="Iptal" CommandArgument='<%# Eval("kayit_id") %>' CssClass="btn btn-danger btn-sm" Text="Kay�t �ptal" OnClientClick="return confirm('Kayd� iptal etmek istedi�inize emin misiniz?');" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
