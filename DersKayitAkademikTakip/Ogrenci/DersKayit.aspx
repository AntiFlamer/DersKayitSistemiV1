<%@ Page Title="Ders Kaydý" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DersKayit.aspx.cs" Inherits="DersKayitAkademikTakip.Ogrenci.DersKayit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid mt-4">
        <div class="row">
            <div class="col-md-12">
                <h2><i class="fas fa-plus-circle"></i> Yeni Ders Kaydý</h2>
                <a href="Default.aspx" class="btn btn-secondary btn-sm mb-3"><i class="fas fa-arrow-left"></i> Geri</a>
                <hr />
            </div>
        </div>

        <asp:Panel ID="SuccessPanel" runat="server" Visible="false" CssClass="alert alert-success">
            <asp:Literal ID="SuccessText" runat="server" />
        </asp:Panel>
        <asp:Panel ID="ErrorPanel" runat="server" Visible="false" CssClass="alert alert-danger">
            <asp:Literal ID="ErrorText" runat="server" />
        </asp:Panel>

        <div class="row mb-3">
            <div class="col-md-6">
                <div class="input-group">
                    <asp:TextBox ID="txtArama" runat="server" CssClass="form-control" placeholder="Ders kodu veya adý ara..." />
                    <asp:Button ID="btnAra" runat="server" Text="Ara" CssClass="btn btn-outline-secondary" OnClick="btnAra_Click" />
                </div>
            </div>
        </div>

        <asp:GridView ID="gvDersler" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered"
            DataKeyNames="ders_kodu" OnRowCommand="gvDersler_RowCommand" EmptyDataText="Uygun ders bulunamadý.">
            <Columns>
                <asp:BoundField DataField="ders_kodu" HeaderText="Ders Kodu" />
                <asp:BoundField DataField="ders_adi" HeaderText="Ders Adý" />
                <asp:BoundField DataField="kredi" HeaderText="Kredi" ItemStyle-CssClass="text-center" />
                <asp:BoundField DataField="akts_kredi" HeaderText="AKTS" ItemStyle-CssClass="text-center" />
                <asp:BoundField DataField="kontenjan" HeaderText="Kontenjan" ItemStyle-CssClass="text-center" />
                <asp:TemplateField HeaderText="Öðretim Görevlisi">
                    <ItemTemplate>
                        <%# Eval("hoca_adi") ?? "-" %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Ýþlem" ItemStyle-Width="120px">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnKaydol" runat="server" CommandName="Kaydol" CommandArgument='<%# Eval("ders_kodu") %>'
                            CssClass="btn btn-primary btn-sm" Text="Kaydol" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
