<%@ Page Title="Giriş" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="DersKayitAkademikTakip.Account.Login" Async="true" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-md-6">
                <div class="card shadow">
                    <div class="card-header bg-primary text-white text-center">
                        <h4><i class="fas fa-university"></i> Üniversite Giriş Paneli</h4>
                    </div>
                    <div class="card-body">
                        <section id="loginForm">
                            <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                                <div class="alert alert-danger">
                                    <asp:Literal runat="server" ID="FailureText" />
                                </div>
                            </asp:PlaceHolder>
                            
                            <div class="form-group">
                                <asp:Label runat="server" AssociatedControlID="Email" CssClass="form-label">E-posta</asp:Label>
                                <asp:TextBox runat="server" ID="Email" CssClass="form-control" TextMode="Email" placeholder="ornek@universite.edu.tr" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="Email"
                                    CssClass="text-danger" ErrorMessage="E-posta alanı zorunludur." Display="Dynamic" />
                            </div>
                            
                            <div class="form-group mt-3">
                                <asp:Label runat="server" AssociatedControlID="Password" CssClass="form-label">Şifre</asp:Label>
                                <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" placeholder="Şifreniz" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="Password" 
                                    CssClass="text-danger" ErrorMessage="Şifre alanı zorunludur." Display="Dynamic" />
                            </div>
                            
                            <div class="mt-3">
                                <div class="form-check">
                                    <input type="checkbox" id="RememberMe" class="form-check-input" runat="server"/>
                                    <label class="form-check-label" for="MainContent_RememberMe">Beni hatırla</label>
                                </div>
                            </div>
                            
                            <div class="d-grid gap-2 mt-4">
                                <asp:Button runat="server" OnClick="LogIn" Text="Giriş Yap" CssClass="btn btn-primary btn-lg" />
                            </div>
                        </section>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>