<%@ Page Title="Kayıt Onay" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="KayitOnay.aspx.cs" Inherits="DersKayitAkademikTakip.Hoca.KayitOnay" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid mt-4">
        <div class="d-flex justify-content-between align-items-center">
            <h2 class="mb-0"><i class="fas fa-user-check"></i> Kayıt Onay</h2>
            <a href="Default.aspx" class="btn btn-light btn-sm">
                <i class="fas fa-arrow-left"></i> Geri
            </a>
        </div>
        <hr />
        <asp:GridView ID="gvKayitlar" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered" EmptyDataText="Onay bekleyen kayıt bulunmamaktadır." OnRowCommand="gvKayitlar_RowCommand">
            <Columns>
                <asp:BoundField DataField="ders_kodu" HeaderText="Ders Kodu" />
                <asp:BoundField DataField="ders_adi" HeaderText="Ders Adı" />
                <asp:BoundField DataField="ogrenci_adi" HeaderText="Öğrenci" />
                <asp:BoundField DataField="kayit_tarihi" HeaderText="Kayıt Tarihi" DataFormatString="{0:dd.MM.yyyy HH:mm}" />
                <asp:TemplateField HeaderText="İşlemler">
                    <ItemTemplate>
                        <asp:Button ID="btnOnayla" runat="server" Text="Onayla" CommandName="Onayla" CommandArgument='<%# Eval("kayit_id") %>' CssClass="btn btn-success btn-sm" />
                        <asp:Button ID="btnReddet" runat="server" Text="Reddet" CommandName="Reddet" CommandArgument='<%# Eval("kayit_id") %>' CssClass="btn btn-danger btn-sm ms-1" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <asp:Panel ID="SuccessPanel" runat="server" Visible="false" CssClass="alert alert-success mt-3">
            <asp:Label ID="SuccessText" runat="server" />
        </asp:Panel>
        <asp:Panel ID="ErrorPanel" runat="server" Visible="false" CssClass="alert alert-danger mt-3">
            <asp:Label ID="ErrorText" runat="server" />
        </asp:Panel>
    </div>
</asp:Content>
