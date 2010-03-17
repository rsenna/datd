<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="ViewPage<FinanceiroDetalharNotaFiscal>" %>
<%@ Import Namespace="Dataweb.Dilab.Web.Models"%>
<%@ Import Namespace="System.Web.Mvc" %>
<%@ Import Namespace="Dataweb.Dilab.Model.DataTransfer"%>
<%@ Import Namespace="Dataweb.Dilab.Web"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Dilab Online - Financeiro - Detalhar Nota Fiscal
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Detalhar Nota Fiscal</h2>

    <h3>Identificação</h3>

    <label>Número</label><br />
    <span class="field numeric"><%= Model.NotaFiscal.Numero %></span><br />
    <br />
    <label>Data</label><br />
    <span class="field"><%= Html.EncodeDate(Model.NotaFiscal.Data) %></span><br />
    <br />
    <label>Total</label><br />
    <span class="field numeric"><%= Html.EncodeCurrency(Model.NotaFiscal.Total) %></span><br />
    <br />
    <%= Html.ActionLink("NFE", "NotaFiscalEletronica", new {codNotaFiscal = Model.NotaFiscal.CodNotaFiscal})%>
    <br />

    <% Html.RenderPartial("ListarOrdensServico", Model.OrdensServico); %>
    <% Html.RenderPartial("ListarPedidos", Model.Pedidos); %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

