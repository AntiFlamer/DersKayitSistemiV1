<%@ Page Title="Ders Yönetimi" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dersler.aspx.cs" Inherits="DersKayitAkademikTakip.Admin.Dersler" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid mt-4">
        <div class="row">
            <div class="col-md-12">
                <div class="card shadow">
                    <div class="card-header bg-primary text-white">
                        <h4><i class="fas fa-book"></i> Ders Yönetimi</h4>
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
                            DataKeyNames="ders_id" OnRowCommand="gvDersler_RowCommand" OnRowDataBound="gvDersler_RowDataBound"
                            EmptyDataText="Ders bulunamadý.">
                            <Columns>
                                <asp:BoundField DataField="ders_id" HeaderText="ID" ItemStyle-Width="50px" />
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
                                        <asp:LinkButton ID="btnDuzenle" runat="server" CommandName="Duzenle" CommandArgument='<%# Eval("ders_id") %>'
                                            CssClass="btn btn-warning btn-sm" Text="Düzenle" />
                                        <asp:LinkButton ID="btnSil" runat="server" CommandName="Sil" CommandArgument='<%# Eval("ders_id") %>'
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

    <!-- DÜZENLEME MODAL -->
    <div class="modal fade" id="editModal" tabindex="-1" aria-labelledby="editModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="editModalLabel">Ders Düzenle</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <asp:HiddenField ID="hfDersId" runat="server" />
                    
                    <div class="row mb-3">
                        <div class="col-md-6">
                            <label class="form-label">Ders Kodu <span class="text-danger">*</span></label>
                            <asp:TextBox ID="txtEditDersKodu" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-md-6">
                            <label class="form-label">Ders Adý <span class="text-danger">*</span></label>
                            <asp:TextBox ID="txtEditDersAdi" runat="server" CssClass="form-control" />
                        </div>
                    </div>

                    <div class="row mb-3">
                        <div class="col-md-3">
                            <label class="form-label">Kredi <span class="text-danger">*</span></label>
                            <asp:TextBox ID="txtEditKredi" runat="server" CssClass="form-control" TextMode="Number" />
                        </div>
                        <div class="col-md-3">
                            <label class="form-label">AKTS Kredi <span class="text-danger">*</span></label>
                            <asp:TextBox ID="txtEditAktsKredi" runat="server" CssClass="form-control" TextMode="Number" />
                        </div>
                        <div class="col-md-3">
                            <label class="form-label">Kontenjan <span class="text-danger">*</span></label>
                            <asp:TextBox ID="txtEditKontenjan" runat="server" CssClass="form-control" TextMode="Number" />
                        </div>
                        <div class="col-md-3">
                            <label class="form-label">Dönem <span class="text-danger">*</span></label>
                            <asp:TextBox ID="txtEditDersDonemi" runat="server" CssClass="form-control" placeholder="Örn: Güz 2024" />
                        </div>
                    </div>

                    <div class="row mb-3">
                        <div class="col-md-6">
                            <label class="form-label">Hoca</label>
                            <asp:DropDownList ID="ddlEditHoca" runat="server" CssClass="form-control">
                                <asp:ListItem Value="">-- Seçiniz --</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-6">
                            <label class="form-label">Ders Tipi <span class="text-danger">*</span></label>
                            <asp:DropDownList ID="ddlEditDersTipi" runat="server" CssClass="form-control">
                                <asp:ListItem Value="">-- Seçiniz --</asp:ListItem>
                                <asp:ListItem Value="zorunlu">Zorunlu</asp:ListItem>
                                <asp:ListItem Value="secmeli">Seçmeli</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Ýptal</button>
                    <asp:Button ID="btnGuncelle" runat="server" Text="Güncelle" CssClass="btn btn-primary" OnClick="btnGuncelle_Click" />
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function showEditModal() {
            var modal = new bootstrap.Modal(document.getElementById('editModal'));
            modal.show();
        }
    </script>
</asp:Content>
