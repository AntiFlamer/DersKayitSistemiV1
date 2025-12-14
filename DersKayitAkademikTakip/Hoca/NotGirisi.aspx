<%@ Page Title="Not Giriþi" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NotGirisi.aspx.cs" Inherits="DersKayitAkademikTakip.Hoca.NotGirisi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid mt-4">
        <div class="d-flex justify-content-between align-items-center">
            <h2 class="mb-0"><i class="fas fa-clipboard-list"></i> Not Giriþi</h2>
            <a href="Default.aspx" class="btn btn-light btn-sm">
                <i class="fas fa-arrow-left"></i> Geri
            </a>
        </div>
        <hr />

        <div class="row mb-3">
            <div class="col-md-4">
                <label>Ders Seçiniz:</label>
                <asp:DropDownList ID="ddlDersler" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDersler_SelectedIndexChanged" />
            </div>
        </div>

        <asp:GridView ID="gvOgrenciler" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered" EmptyDataText="Seçili derse ait onaylý öðrenci kaydý bulunmamaktadýr.">
            <Columns>
                <asp:TemplateField HeaderText="Öðrenci">
                    <ItemTemplate>
                        <asp:HiddenField ID="hfOgrenciId" runat="server" Value='<%# Eval("ogrenci_id") %>' />
                        <%# Eval("ogrenci_adi") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Vize">
                    <ItemTemplate>
                        <asp:TextBox ID="txtVize" runat="server" CssClass="form-control" Text='<%# Bind("vize_notu") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Final">
                    <ItemTemplate>
                        <asp:TextBox ID="txtFinal" runat="server" CssClass="form-control" Text='<%# Bind("final_notu") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Bütünleme">
                    <ItemTemplate>
                        <asp:TextBox ID="txtBut" runat="server" CssClass="form-control" Text='<%# Bind("butunleme_notu") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ortalama" HeaderText="Ortalama" ItemStyle-CssClass="text-center" />
                <asp:BoundField DataField="harf_notu" HeaderText="Harf" ItemStyle-CssClass="text-center" />
                <asp:BoundField DataField="durum" HeaderText="Durum" ItemStyle-CssClass="text-center" />
            </Columns>
        </asp:GridView>

        <asp:Button ID="btnKaydet" runat="server" Text="Kaydet" CssClass="btn btn-primary mt-3" OnClick="btnKaydet_Click" />

        <asp:Panel ID="SuccessPanel" runat="server" Visible="false" CssClass="alert alert-success mt-3">
            <asp:Label ID="SuccessText" runat="server" />
        </asp:Panel>
        <asp:Panel ID="ErrorPanel" runat="server" Visible="false" CssClass="alert alert-danger mt-3">
            <asp:Label ID="ErrorText" runat="server" />
        </asp:Panel>
    </div>
</asp:Content>
