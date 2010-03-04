<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Dilab Online - Login
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Bem-vindo ao Dilab Online</h2>

    <% if (ViewData["Tenant"] != null) { %>

        <p>
            Para acessar o site, por favor identifique-se:
        </p>
        <% using (Html.BeginForm("Logon", "Account")) { %>
            <div>
                <fieldset>
                    <legend>Informações da sua conta:</legend>
                    <p>
                        <label for="userId">Identificador:</label>
                        <%= Html.TextBox("login") %>
                    </p>
                    <p>
                        <label for="password">Senha:</label>
                        <%= Html.Password("password") %>
                    </p>
                    <p>
                        <%= Html.CheckBox("rememberMe") %> <label class="inline" for="RememberMe">Lembrar minha senha</label>
                    </p>
                    <p>
                        <input type="submit" value="Entrar" />
                    </p>
                </fieldset>
            </div>
        <% } %>

    <% } else { %>
    
        <p>
            Obrigado pelo seu interesse no Dilab Online.
        </p>
        
    <% } %>
    <p>
        Para conhecer mais os nossos produtos, visite <a href="http://www.dataweb.inf.br">www.dataweb.inf.br</a>.
    </p>
</asp:Content>
