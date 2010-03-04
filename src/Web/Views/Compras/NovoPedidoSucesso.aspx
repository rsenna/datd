<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Dilab Online - Compras - Novo Pedido
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Pedido foi cadastrado com sucesso.</h2>
    <h3>Número: <%= ViewData["num"] %></h3>
</asp:Content>
