<%@ Page Title="Kayýt Onay" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="KayitOnay.aspx.cs" Inherits="DersKayitAkademikTakip.Hoca.KayitOnay" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid mt-4">
        <div class="d-flex justify-content-between align-items-center">
            <h2 class="mb-0"><i class="fas fa-user-check"></i> Kayýt Onay</h2>
            <a href="Default.aspx" class="btn btn-light btn-sm">
                <i class="fas fa-arrow-left"></i> Geri
            </a>
        </div>
        <hr />

        <%-- =====================================================
             AJAX - UpdatePanel ACIKLAMASI
             =====================================================
             
             UpdatePanel, sayfa yenilenmeden (partial postback) 
             sadece belirli bir bolumu gunceller.
             
             NASIL CALISIR?
             1. Kullanici "Onayla" veya "Reddet" butonuna tiklar
             2. Normal postback yerine AJAX istegi gonderilir
             3. Sunucu kodu calisir (gvKayitlar_RowCommand)
             4. Sadece UpdatePanel icindeki alan guncellenir
             5. Sayfa yenilenmez, kullanici deneyimi iyilesir
             
             UpdateMode="Conditional" = Sadece kendi icindeki 
             kontroller tetiklendiginde guncellenir
             
             ChildrenAsTriggers="true" = Icindeki tum butonlar
             otomatik olarak AJAX tetikleyicisi olur
             ===================================================== --%>

        <asp:UpdatePanel ID="upKayitlar" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
            <ContentTemplate>
                
                <%-- YUKLENIYOR GOSTERGESI (islem sirasinda gorunur) --%>
                <asp:UpdateProgress ID="upProgress" runat="server" AssociatedUpdatePanelID="upKayitlar">
                    <ProgressTemplate>
                        <div class="alert alert-info">
                            <i class="fas fa-spinner fa-spin me-2"></i> Islem yapiliyor, lutfen bekleyin...
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>

                <%-- BASARI MESAJI --%>
                <asp:Panel ID="SuccessPanel" runat="server" Visible="false" CssClass="alert alert-success mt-3 alert-dismissible fade show">
                    <i class="fas fa-check-circle me-2"></i>
                    <asp:Label ID="SuccessText" runat="server" />
                    <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                </asp:Panel>

                <%-- HATA MESAJI --%>
                <asp:Panel ID="ErrorPanel" runat="server" Visible="false" CssClass="alert alert-danger mt-3 alert-dismissible fade show">
                    <i class="fas fa-exclamation-circle me-2"></i>
                    <asp:Label ID="ErrorText" runat="server" />
                    <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                </asp:Panel>

                <%-- BEKLEYEN KAYIT SAYISI --%>
                <div class="mb-3">
                    <span class="badge bg-warning text-dark fs-6">
                        <i class="fas fa-clock me-1"></i>
                        Bekleyen Kayit: <asp:Label ID="lblBekleyenSayisi" runat="server" Text="0" />
                    </span>
                </div>

                <%-- KAYITLAR TABLOSU --%>
                <asp:GridView ID="gvKayitlar" runat="server" 
                    AutoGenerateColumns="False" 
                    CssClass="table table-striped table-bordered table-hover" 
                    EmptyDataText="Onay bekleyen kayit bulunmamaktadir." 
                    OnRowCommand="gvKayitlar_RowCommand"
                    DataKeyNames="kayit_id">
                    <Columns>
                        <asp:BoundField DataField="ders_kodu" HeaderText="Ders Kodu" ItemStyle-Width="120px" />
                        <asp:BoundField DataField="ders_adi" HeaderText="Ders Adi" />
                        <asp:BoundField DataField="ogrenci_adi" HeaderText="Ogrenci" />
                        <asp:BoundField DataField="kayit_tarihi" HeaderText="Kayit Tarihi" DataFormatString="{0:dd.MM.yyyy HH:mm}" ItemStyle-Width="150px" />
                        <asp:TemplateField HeaderText="Islemler" ItemStyle-Width="180px" ItemStyle-CssClass="text-center">
                            <ItemTemplate>
                                <%-- ONAYLA BUTONU --%>
                                <asp:Button ID="btnOnayla" runat="server" 
                                    Text="Onayla" 
                                    CommandName="Onayla" 
                                    CommandArgument='<%# Eval("kayit_id") %>' 
                                    CssClass="btn btn-success btn-sm"
                                    OnClientClick="return confirm('Bu kaydi onaylamak istediginize emin misiniz?');" />
                                
                                <%-- REDDET BUTONU --%>
                                <asp:Button ID="btnReddet" runat="server" 
                                    Text="Reddet" 
                                    CommandName="Reddet" 
                                    CommandArgument='<%# Eval("kayit_id") %>' 
                                    CssClass="btn btn-danger btn-sm ms-1"
                                    OnClientClick="return confirm('Bu kaydi reddetmek istediginize emin misiniz?');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataRowStyle CssClass="text-center text-muted py-4" />
                </asp:GridView>

            </ContentTemplate>
        </asp:UpdatePanel>

        <%-- =====================================================
             AJAX SONRASI BOOTSTRAP ALERT'LERI YENIDEN AKTIF ET
             
             UpdatePanel guncellendikten sonra Bootstrap'in 
             dismiss butonlari calismayabilir. Bu script ile
             her AJAX guncellemesinden sonra yeniden aktif edilir.
             ===================================================== --%>
        <script type="text/javascript">
            // Sayfa ilk yuklendiginde ve her AJAX guncellemesinden sonra calisir
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                // Bootstrap alert dismiss butonlarini yeniden aktif et
                var alertList = document.querySelectorAll('.alert-dismissible');
                alertList.forEach(function (alert) {
                    new bootstrap.Alert(alert);
                });
            });
        </script>

    </div>
</asp:Content>
