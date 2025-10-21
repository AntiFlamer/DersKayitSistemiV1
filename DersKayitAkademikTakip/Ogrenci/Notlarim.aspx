<%@ Page Title="Notlarým" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Notlarim.aspx.cs" Inherits="DersKayitAkademikTakip.Ogrenci.Notlarim" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid mt-4">
        <h2><i class="fas fa-chart-bar"></i> Notlarým</h2>
        <a href="Default.aspx" class="btn btn-secondary btn-sm mb-3"><i class="fas fa-arrow-left"></i> Geri</a>
        <hr />
        <asp:GridView ID="gvNotlar" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered" EmptyDataText="Notunuz bulunmamaktadýr.">
            <Columns>
                <asp:BoundField DataField="ders_kodu" HeaderText="Ders Kodu" />
                <asp:BoundField DataField="ders_adi" HeaderText="Ders Adý" />
                <asp:BoundField DataField="vize" HeaderText="Vize" ItemStyle-CssClass="text-center" />
                <asp:BoundField DataField="final" HeaderText="Final" ItemStyle-CssClass="text-center" />
                <asp:BoundField DataField="ortalama" HeaderText="Ortalama" ItemStyle-CssClass="text-center" />
                <asp:BoundField DataField="harf_notu" HeaderText="Harf Notu" ItemStyle-CssClass="text-center" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
