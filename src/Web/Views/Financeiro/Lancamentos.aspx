<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="ViewPage<IEnumerable<Lancamento>>" %>
<%@ Import Namespace="System.Web.Mvc" %>
<%@ Import Namespace="Dataweb.Dilab.Model.DataTransfer"%>
<%@ Import Namespace="Dataweb.Dilab.Web"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Dilab Online - Financeiro - Meus Lançamentos
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Meus Lançamentos</h2>
    <% Html.RenderPartial("ListarLancamentos", Model); %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

