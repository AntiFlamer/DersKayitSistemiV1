<%@ Page Title="Ders İstatistikleri" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DersIstatistik.aspx.cs" Inherits="DersKayitAkademikTakip.Hoca.DersIstatistik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid mt-4">
        <h2><i class="fas fa-chart-bar"></i> Verdiğim Dersler - İstatistik</h2>
        <a href="Default.aspx" class="btn btn-secondary btn-sm mb-3"><i class="fas fa-arrow-left"></i> Geri</a>
        <hr />

        <asp:GridView ID="gvDersIstatistik" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered" EmptyDataText="İstatistik bulunmamaktadır.">
            <Columns>
                <asp:BoundField DataField="ders_kodu" HeaderText="Ders Kodu" />
                <asp:BoundField DataField="ders_adi" HeaderText="Ders Adı" />
                <asp:BoundField DataField="kredi" HeaderText="Kredi" ItemStyle-CssClass="text-center" />
                <asp:BoundField DataField="akts_kredi" HeaderText="AKTS" ItemStyle-CssClass="text-center" />
                <asp:BoundField DataField="ogrenci_sayisi" HeaderText="Öğrenci Sayısı" ItemStyle-CssClass="text-center" />
                <asp:BoundField DataField="ortalama" HeaderText="Ortalama" DataFormatString="{0:F2}" ItemStyle-CssClass="text-center" />
                <asp:BoundField DataField="gecen_yuzde" HeaderText="Geçme Oranı (%)" DataFormatString="{0:F1}" ItemStyle-CssClass="text-center" />
                <asp:BoundField DataField="kalan_yuzde" HeaderText="Kalma Oranı (%)" DataFormatString="{0:F1}" ItemStyle-CssClass="text-center" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
