<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<NotaFiscal>>" %>
<%@ Import Namespace="Dataweb.Dilab.Web"%>
<%@ Import Namespace="Dataweb.Dilab.Model.DataTransfer"%>

<% if (Model.Count() == 0) { %>

    Não há notas fiscais cadastradas.

<% } else { %>
    <table>
        <tr>
            <th>
                Número
            </th>
            <th>
                Data
            </th>
            <th>
                Total
            </th>
            <th></th>
            <th></th>
        </tr>

        <% var totalGeral = 0m; foreach (var item in Model) { totalGeral += item.Total; %>

            <tr>
                <td>
                    <%= Html.Encode(item.Numero) %>
                </td>
                <td>
                    <%= Html.EncodeDate(item.Data) %>
                </td>
                <td>
                    <%=Html.EncodeCurrency(item.Total)%>
                </td>
                <td>
                    <%= Html.ActionLink("Detalhe", "DetalharNotaFiscal", new {codNotaFiscal = item.CodNotaFiscal})%>
                </td>
                <td>
                    <%= item.Nfe? Html.ActionLink("NFE", "NotaFiscalEletronica", new { codNotaFiscal = item.CodNotaFiscal }) : "&nbsp;" %>
                </td>
            </tr>

        <% } // end foreach %>
        <tr class="summary">
            <td>&nbsp;</td>
            <td class="title">Total:</td>
            <td><%= Html.EncodeCurrency(totalGeral) %></td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
<% } // end if %>
