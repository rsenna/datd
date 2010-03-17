<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="ViewPage<Fatura>" %>
<%@ Import Namespace="System.Web.Mvc" %>
<%@ Import Namespace="Dataweb.Dilab.Model.DataTransfer"%>
<%@ Import Namespace="Dataweb.Dilab.Web"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Dilab Online - Financeiro - Detalhar Fatura
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Detalhar Fatura</h2>

    <h3>Identificação</h3>

    <label>Número</label><br />
    <span class="field numeric"><%= Model.Numero %></span><br />
    <br />
    <label>Data</label><br />
    <span class="field"><%= Html.EncodeDate(Model.Data) %></span><br />
    <br />
    <label>Total</label><br />
    <span class="field numeric"><%= Html.EncodeCurrency(Model.Total) %></span><br />
    <br />
    
    <h3>Notas Fiscais</h3>
    <% Html.RenderPartial("ListarNotasFiscais", Model.NotasFiscais); %>
    
    <h3>Lançamentos</h3>
    <% Html.RenderPartial("ListarLancamentos", Model.Lancamentos); %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

