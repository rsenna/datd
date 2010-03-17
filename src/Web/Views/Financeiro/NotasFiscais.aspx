<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="ViewPage<IEnumerable<NotaFiscal>>" %>
<%@ Import Namespace="System.Web.Mvc" %>
<%@ Import Namespace="Dataweb.Dilab.Model.DataTransfer"%>
<%@ Import Namespace="Dataweb.Dilab.Web"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Dilab Online - Financeiro - Minhas Notas Fiscais
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Minhas Notas Fiscais</h2>
    <% Html.RenderPartial("ListarNotasFiscais", Model); %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

