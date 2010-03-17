<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ComprasIndex>" %>
<%@ Import Namespace="Dataweb.Dilab.Web.Models"%>
<%@ Import Namespace="Dataweb.Dilab.Web"%>
<%@ Import Namespace="Dataweb.Dilab.Model.DataTransfer"%>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Dilab Online - Compras - Minhas Compras
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <div id="divCompras">
        <h2>Minhas Compras</h2>
        <% Html.RenderPartial("ListarOrdensServico", Model.OrdensServico); %>
        <% Html.RenderPartial("ListarPedidos", Model.Pedidos); %>
    </div>
</asp:Content>
