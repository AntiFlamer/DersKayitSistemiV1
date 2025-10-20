<%@ Page Title="Ders Y�netimi" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dersler.aspx.cs" Inherits="DersKayitAkademikTakip.Admin.Dersler" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid mt-4">
        <div class="row">
            <div class="col-md-12">
                <div class="card shadow">
                    <div class="card-header bg-primary text-white">
                        <h4><i class="fas fa-book"></i> Ders Y�netimi</h4>
                    </div>
                    <div class="card-body">
                        <asp:Panel ID="SuccessPanel" runat="server" Visible="false" CssClass="alert alert-success">
                            <asp:Literal ID="SuccessText" runat="server" />
                        </asp:Panel>
                        
                        <asp:Panel ID="ErrorPanel" runat="server" Visible="false" CssClass="alert alert-danger">
                            <asp:Literal ID="ErrorText" runat="server" />
                        </asp:Panel>

                        <!-- ARAMA VE F�LTRELEME -->
                        <div class="row mb-3">
                            <div class="col-md-4">
                                <div class="input-group">
                                    <asp:TextBox ID="txtArama" runat="server" CssClass="form-control" placeholder="Ders kodu veya ad� ara..." />
                                    <asp:Button ID="btnAra" runat="server" Text="Ara" CssClass="btn btn-outline-secondary" OnClick="btnAra_Click" />
                                </div>
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddlDersTipiFiltre" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDersTipiFiltre_SelectedIndexChanged">
                                    <asp:ListItem Value="">T�m Ders Tipleri</asp:ListItem>
                                    <asp:ListItem Value="zorunlu">Zorunlu</asp:ListItem>
                                    <asp:ListItem Value="secmeli">Se�meli</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddlHocaFiltre" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlHocaFiltre_SelectedIndexChanged">
                                    <asp:ListItem Value="">T�m Hocalar</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2 text-end">
                                <asp:Button ID="btnYenile" runat="server" Text="Yenile" CssClass="btn btn-secondary" OnClick="btnYenile_Click" />
                            </div>
                        </div>

                        <!-- DERS L�STES� -->
                        <asp:GridView ID="gvDersler" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered"
                            DataKeyNames="ders_id" OnRowCommand="gvDersler_RowCommand" OnRowDataBound="gvDersler_RowDataBound"
                            EmptyDataText="Ders bulunamad�.">
                            <Columns>
                                <asp:BoundField DataField="ders_id" HeaderText="ID" ItemStyle-Width="50px" />
                                <asp:BoundField DataField="ders_kodu" HeaderText="Ders Kodu" />
                                <asp:BoundField DataField="ders_adi" HeaderText="Ders Ad�" />
                                <asp:BoundField DataField="kredi" HeaderText="Kredi" ItemStyle-Width="70px" ItemStyle-CssClass="text-center" />
                                <asp:BoundField DataField="akts_kredi" HeaderText="AKTS" ItemStyle-Width="70px" ItemStyle-CssClass="text-center" />
                                <asp:BoundField DataField="kontenjan" HeaderText="Kontenjan" ItemStyle-Width="90px" ItemStyle-CssClass="text-center" />
                                <asp:BoundField DataField="ders_donemi" HeaderText="D�nem" ItemStyle-Width="100px" />
                                <asp:TemplateField HeaderText="Hoca">
                                    <ItemTemplate>
                                        <%# Eval("hoca_adi") != DBNull.Value ? Eval("hoca_adi").ToString() : "<span class='text-muted'>Atanmam��</span>" %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ders Tipi">
                                    <ItemTemplate>
                                        <span class='badge <%# GetDersTipiBadgeClass(Eval("ders_tipi").ToString()) %>'>
                                            <%# GetDersTipiDisplayName(Eval("ders_tipi").ToString()) %>
                                        </span>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="��lemler" ItemStyle-Width="180px">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDuzenle" runat="server" CommandName="Duzenle" CommandArgument='<%# Eval("ders_id") %>'
                                            CssClass="btn btn-warning btn-sm" Text="D�zenle" />
                                        <asp:LinkButton ID="btnSil" runat="server" CommandName="Sil" CommandArgument='<%# Eval("ders_id") %>'
                                            CssClass="btn btn-danger btn-sm" Text="Sil" OnClientClick="return confirm('Bu dersi silmek istedi�inizden emin misiniz?');" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- D�ZENLEME MODAL -->
    <div class="modal fade" id="editModal" tabindex="-1" aria-labelledby="editModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="editModalLabel">Ders D�zenle</h5>
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
                            <label class="form-label">Ders Ad� <span class="text-danger">*</span></label>
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
                            <label class="form-label">D�nem <span class="text-danger">*</span></label>
                            <asp:TextBox ID="txtEditDersDonemi" runat="server" CssClass="form-control" placeholder="�rn: G�z 2024" />
                        </div>
                    </div>

                    <div class="row mb-3">
                        <div class="col-md-6">
                            <label class="form-label">Hoca</label>
                            <asp:DropDownList ID="ddlEditHoca" runat="server" CssClass="form-control">
                                <asp:ListItem Value="">-- Se�iniz --</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-6">
                            <label class="form-label">Ders Tipi <span class="text-danger">*</span></label>
                            <asp:DropDownList ID="ddlEditDersTipi" runat="server" CssClass="form-control">
                                <asp:ListItem Value="">-- Se�iniz --</asp:ListItem>
                                <asp:ListItem Value="zorunlu">Zorunlu</asp:ListItem>
                                <asp:ListItem Value="secmeli">Se�meli</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">�ptal</button>
                    <asp:Button ID="btnGuncelle" runat="server" Text="G�ncelle" CssClass="btn btn-primary" OnClick="btnGuncelle_Click" />
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
