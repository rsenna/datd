<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="changePasswordTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Minha Conta
</asp:Content>

<asp:Content ID="changePasswordContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Minha Conta</h2>
    <p>
        Utilize o formulário abaixo para alterar detalhes da sua conta.
    </p>
    <p>
        Obs.: Para alterar a senha, e necessário que a nova possua pelo menos <%=Html.Encode(ViewData["PasswordLength"])%> caracteres.
    </p>
    <%= Html.ValidationSummary("A alteração da sua conta <b>não</b> teve sucesso. Por favor, corrija os erros e tente novamente.")%>

    <% using (Html.BeginForm()) { %>
        <div>
            <fieldset>
                <legend>Detalhes da conta</legend>
                <p>
                    <label for="senhaAtual">Endereço de email:</label>
                    <%= Html.TextBox("emailNotificacao") %><br />
                    <%= Html.CheckBox("receberNotificacao") %> Receber notificações por email?
                    <%= Html.ValidationMessage("emailNotificacao")%>
                </p>
                <hr />
                <p>
                    <label for="senhaAtual">Senha atual:</label>
                    <%= Html.Password("senhaAtual") %>
                    <%= Html.ValidationMessage("senhaAtual") %>
                </p>
                <p>
                    <label for="senhaNova">Nova senha:</label>
                    <%= Html.Password("senhaNova") %>
                    <%= Html.ValidationMessage("senhaNova") %>
                </p>
                <p>
                    <label for="senhaConfirmacao">Confirme a nova senha:</label>
                    <%= Html.Password("senhaConfirmacao") %>
                    <%= Html.ValidationMessage("senhaConfirmacao") %>
                </p>
                <p>
                    <input type="submit" value="Salvar" />
                </p>
            </fieldset>
        </div>
    <% } %>
</asp:Content>
