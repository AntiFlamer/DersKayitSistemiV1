<%@ Page Title="Hakkında" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="DersKayitAkademikTakip.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div class="row justify-content-center mt-5">
            <div class="col-md-8">
                <div class="card border-0 shadow-sm">
                    <div class="card-body p-5 text-center">
                        <i class="fas fa-graduation-cap fa-4x text-primary mb-4"></i>
                        <h2 class="mb-4">Hakkında</h2>
                        <p class="lead text-muted">
                            Bu proje, <strong>eğitim amaçlı</strong> geliştirilmiş bir üniversite ders kayıt sistemi örneğidir.
                        </p>
                        <hr class="my-4" />
                        <p class="text-muted mb-0">
                            ASP.NET Web Forms, MySQL ve Bootstrap teknolojileri kullanılarak oluşturulmuştur.
                        </p>
                    </div>
                </div>
            </div>
        </div>
    </main>
</asp:Content>
