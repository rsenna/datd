<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ComprasDetalhar>" %>
<%@ Import Namespace="Dataweb.Dilab.Web.Models"%>
<%@ Import Namespace="Dataweb.Dilab.Web"%>
<%@ Import Namespace="Dataweb.Dilab.Model.DataTransfer"%>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Dilab Online - Compras - Minhas Compras - Detalhar
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <div id="divComprasDetalhar">
        <h2>Minhas Compras</h2>
        <h3>Detalhes - <%= Html.Encode<TipoTransacao>(Model.Transacao.Tipo) %></h3>

        <% if (Model != null) { %>

            <div id="divNumeroOS">
                <h3>Número:</h3>
                <%= Model.Transacao.Numero %>
            </div>

            <% if (Model.IsOrdemServico) Html.RenderPartial("DetalharOs", Model); %>
            <% Html.RenderPartial("DetalharItens", Model); %>

        <% } else { %>
            Não há detalhes para apresentar.
        <% } %>
    </div>
</asp:Content>
