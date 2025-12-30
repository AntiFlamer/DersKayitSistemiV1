<%@ Page Title="Akademik Takvim Yönetimi" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AkademikTakvim.aspx.cs" Inherits="DersKayitAkademikTakip.Admin.AkademikTakvim" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid mt-4">
        <div class="row">
            <div class="col-md-12">
                <h2><i class="fas fa-calendar-alt"></i> Akademik Takvim Yönetimi</h2>
                <hr />
            </div>
        </div>

        <!-- MESAJ PANELLERÝ -->
        <asp:Panel ID="SuccessPanel" runat="server" Visible="false" CssClass="alert alert-success alert-dismissible fade show">
            <asp:Literal ID="SuccessText" runat="server" />
            <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
        </asp:Panel>

        <asp:Panel ID="ErrorPanel" runat="server" Visible="false" CssClass="alert alert-danger alert-dismissible fade show">
            <asp:Literal ID="ErrorText" runat="server" />
            <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
        </asp:Panel>

        <!-- AKTÝF DÖNEM BÝLGÝSÝ -->
        <asp:Panel ID="pnlAktifDonem" runat="server" CssClass="alert alert-info mb-4">
            <h5><i class="fas fa-info-circle"></i> Aktif Dönem</h5>
            <asp:Label ID="lblAktifDonem" runat="server" Text="Aktif dönem yok"></asp:Label>
        </asp:Panel>

        <div class="row">
            <!-- SOL: TAKVÝM LÝSTESÝ -->
            <div class="col-md-4">
                <div class="card">
                    <div class="card-header bg-primary text-white">
                        <i class="fas fa-list"></i> Takvimler
                    </div>
                    <div class="card-body p-0">
                        <asp:GridView ID="gvTakvimler" runat="server" AutoGenerateColumns="false" 
                            CssClass="table table-hover table-striped mb-0" 
                            DataKeyNames="takvim_id"
                            OnSelectedIndexChanged="gvTakvimler_SelectedIndexChanged"
                            OnRowCommand="gvTakvimler_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="donem_adi" HeaderText="Dönem" />
                                <asp:BoundField DataField="akademik_yil" HeaderText="Yýl" />
                                <asp:TemplateField HeaderText="Durum">
                                    <ItemTemplate>
                                        <%# Convert.ToBoolean(Eval("aktif")) ? "<span class='badge bg-success'>Aktif</span>" : "<span class='badge bg-secondary'>Pasif</span>" %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnSec" runat="server" CommandName="Select" CssClass="btn btn-sm btn-outline-primary" ToolTip="Düzenle">
                                            <i class="fas fa-edit"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <div class="p-3 text-center text-muted">Henüz takvim eklenmemiþ.</div>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                    <div class="card-footer">
                        <asp:Button ID="btnYeniTakvim" runat="server" Text="+ Yeni Takvim" CssClass="btn btn-success btn-sm" OnClick="btnYeniTakvim_Click" />
                    </div>
                </div>
            </div>

            <!-- SAÐ: TAKVÝM DÜZENLEME FORMU -->
            <div class="col-md-8">
                <asp:Panel ID="pnlForm" runat="server" CssClass="card">
                    <div class="card-header bg-dark text-white">
                        <i class="fas fa-calendar-plus"></i> 
                        <asp:Label ID="lblFormBaslik" runat="server" Text="Yeni Takvim Ekle"></asp:Label>
                    </div>
                    <div class="card-body">
                        <asp:HiddenField ID="hfTakvimId" runat="server" Value="0" />

                        <!-- DÖNEM BÝLGÝLERÝ -->
                        <div class="row mb-3">
                            <div class="col-md-4">
                                <label class="form-label">Dönem Adý *</label>
                                <asp:TextBox ID="txtDonemAdi" runat="server" CssClass="form-control" placeholder="Örn: 2024-2025 Güz"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <label class="form-label">Akademik Yýl *</label>
                                <asp:TextBox ID="txtAkademikYil" runat="server" CssClass="form-control" placeholder="Örn: 2024-2025"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <label class="form-label">Dönem Tipi *</label>
                                <asp:DropDownList ID="ddlDonemTipi" runat="server" CssClass="form-select">
                                    <asp:ListItem Text="Güz" Value="guz"></asp:ListItem>
                                    <asp:ListItem Text="Bahar" Value="bahar"></asp:ListItem>
                                    <asp:ListItem Text="Yaz" Value="yaz"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <hr />
                        <h5><i class="fas fa-book"></i> Ders Kayýt Tarihleri</h5>
                        <div class="row mb-3">
                            <div class="col-md-6">
                                <label class="form-label">Ders Kayýt Baþlangýç</label>
                                <asp:TextBox ID="txtDersKayitBaslangic" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label class="form-label">Ders Kayýt Bitiþ</label>
                                <asp:TextBox ID="txtDersKayitBitis" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                            </div>
                        </div>

                        <hr />
                        <h5><i class="fas fa-file-alt"></i> Vize Tarihleri</h5>
                        <div class="row mb-3">
                            <div class="col-md-4">
                                <label class="form-label">Vize Baþlangýç</label>
                                <asp:TextBox ID="txtVizeBaslangic" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <label class="form-label">Vize Bitiþ</label>
                                <asp:TextBox ID="txtVizeBitis" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <label class="form-label">Vize Not Giriþ Bitiþ</label>
                                <asp:TextBox ID="txtVizeNotGirisBitis" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                            </div>
                        </div>

                        <hr />
                        <h5><i class="fas fa-graduation-cap"></i> Final Tarihleri</h5>
                        <div class="row mb-3">
                            <div class="col-md-4">
                                <label class="form-label">Final Baþlangýç</label>
                                <asp:TextBox ID="txtFinalBaslangic" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <label class="form-label">Final Bitiþ</label>
                                <asp:TextBox ID="txtFinalBitis" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <label class="form-label">Final Not Giriþ Bitiþ</label>
                                <asp:TextBox ID="txtFinalNotGirisBitis" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                            </div>
                        </div>

                        <hr />
                        <h5><i class="fas fa-redo"></i> Bütünleme Tarihleri</h5>
                        <div class="row mb-3">
                            <div class="col-md-4">
                                <label class="form-label">Bütünleme Baþlangýç</label>
                                <asp:TextBox ID="txtButunlemeBaslangic" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <label class="form-label">Bütünleme Bitiþ</label>
                                <asp:TextBox ID="txtButunlemeBitis" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <label class="form-label">Büt Not Giriþ Bitiþ</label>
                                <asp:TextBox ID="txtButunlemeNotGirisBitis" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                            </div>
                        </div>

                        <hr />
                        <h5><i class="fas fa-calendar"></i> Dönem Tarihleri</h5>
                        <div class="row mb-3">
                            <div class="col-md-6">
                                <label class="form-label">Dönem Baþlangýç</label>
                                <asp:TextBox ID="txtDonemBaslangic" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label class="form-label">Dönem Bitiþ</label>
                                <asp:TextBox ID="txtDonemBitis" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                            </div>
                        </div>

                        <hr />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-check form-switch">
                                    <asp:CheckBox ID="chkAktif" runat="server" CssClass="form-check-input" />
                                    <label class="form-check-label">Bu takvimi aktif yap (diðer takvimler pasif olur)</label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="card-footer">
                        <asp:Button ID="btnKaydet" runat="server" Text="Kaydet" CssClass="btn btn-primary" OnClick="btnKaydet_Click" />
                        <asp:Button ID="btnSil" runat="server" Text="Sil" CssClass="btn btn-danger" OnClick="btnSil_Click" OnClientClick="return confirm('Bu takvimi silmek istediðinize emin misiniz?');" Visible="false" />
                        <asp:Button ID="btnIptal" runat="server" Text="Ýptal" CssClass="btn btn-secondary" OnClick="btnIptal_Click" />
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>
