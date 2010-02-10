<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<HomeViewModel>" %>
<%@ Import Namespace="Dataweb.Dilab.Web.Model"%>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Bem-vindo ao Dilab Online
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <% if (Model != null) { %>

        <div id="divHome">
            <h2>Bem-vindo, <%= Model.Cliente.Nome %></h2>

            <div id="divIdentificacao">
                <b>CLIENTE:</b> <%= Model.Cliente.Nome %><br />
                <b>CNPJ:</b> <%= Model.Cliente.Cnpj %><br />
                <b>IDENTIFICADOR:</b> <%=Model.Cliente.Identificador %><br />
                <b>Empresa:</b> <%=Model.Cliente.NomeEmpresa %><br />
            </div>

            <div id="divReferencia">
                <% Html.BeginForm("Index", "Os", FormMethod.Get); %>
                    <b>Referência:</b> (Somente O.S. em produção)<br />
                    <input type="text" id="txtReferencia" name="referencia" />
                    <input type="submit" value="Enviar" id="btnReferencia" />
                <% Html.EndForm(); %>
            </div>

            <div id="divResumo">
                Você tem:<br />
                <b><%=Model.Fechadas %></b> encerradas (no dia de hoje).<br />
                <b><%=Model.EmProducao %></b> em produção.
            </div>
        </div>

    <% } %>
</asp:Content>
