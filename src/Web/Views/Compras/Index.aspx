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
        <h3>Ordens de Serviço</h3>
        <% if (Model.OrdensServico.Any()) { %>
            <table>
                <tr>
                    <th>Nº/Entrada</th>
                    <th>Referência</th>
                    <th>Etapa</th>
                    <th>Previsão<br />Encerramento</th>
                    <th>&nbsp;</th>
                </tr>
                <% foreach (var item in Model.OrdensServico) { %>
                    <tr>
                        <td><b><%=item.Numero %></b><br /><%=Html.EncodeDate(item.Emissao) %></td>
                        <td><%=item.Referencia %></td>
                        <td><%=Html.Encode<TipoEtapa>(item.Etapa) %></td>
                        <td><%=Html.EncodeDate(item.Previsao) %></td>
                        <td><%= Html.ActionLink("Detalhe", "Detalhar", new { codEmpresa = item.CodEmpresa, codTransacao = item.CodTransacao })%></td>
                    </tr>
                <% } // end-foreach %>
            </table>
        <% } else { %>
            Não existem ordens de serviço disponíveis.
        <% } %>
        <h3>Pedidos</h3>
        <% if (Model.Pedidos.Any()) { %>
            <table>
                <tr>
                    <th>Nº/Entrada</th>
                    <th>Referência</th>
                    <th>Etapa</th>
                    <th>Previsão<br />Encerramento</th>
                    <th>&nbsp;</th>
                </tr>
                <% foreach (var item in Model.Pedidos) { %>
                    <tr>
                        <td><b><%=item.Numero %></b><br /><%=Html.EncodeDate(item.Emissao) %></td>
                        <td><%=item.Referencia %></td>
                        <td><%=Html.Encode<TipoEtapa>(item.Etapa) %></td>
                        <td><%=Html.EncodeDate(item.Previsao) %></td>
                        <td><%= Html.ActionLink("Detalhe", "Detalhar", new { codEmpresa = item.CodEmpresa, codTransacao = item.CodTransacao })%></td>
                    </tr>
                <% } // end-foreach %>
            </table>
        <% } else { %>
            Não existem pedidos disponíveis.
        <% } %>
    </div>
</asp:Content>
