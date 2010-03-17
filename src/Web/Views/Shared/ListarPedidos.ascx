<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Transacao>>" %>
<%@ Import Namespace="Dataweb.Dilab.Web"%>
<%@ Import Namespace="Dataweb.Dilab.Model.DataTransfer"%>

<h3>Pedidos</h3>
<% if (Model.Any()) { %>
    <table summary="Pedidos">
        <tr>
            <th>Nº/Entrada</th>
            <th>Referência</th>
            <th>Etapa</th>
            <th>Previsão<br />Encerramento</th>
            <th>&nbsp;</th>
        </tr>
        <% foreach (var item in Model) { %>
            <tr>
                <td><b><%=item.Numero %></b><br /><%=Html.EncodeDate(item.Emissao) %></td>
                <td><%=item.Referencia %></td>
                <td><%=Html.Encode<TipoEtapa>(item.Etapa) %></td>
                <td><%=Html.EncodeDate(item.Previsao) %></td>
                <td><%= Html.ActionLink("Detalhe", "Detalhar", "Compras", new {codEmpresa = item.CodEmpresa, codTransacao = item.CodTransacao, tipo = TipoTransacao.Pedido}, null)%></td>
            </tr>
        <% } // end-foreach %>
    </table>
<% } else { %>
    Não existem pedidos disponíveis.
<% } %>
