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

        <% foreach (var item in Model) { %>

            <tr>
                <td>
                    <%= Html.Encode(item.Numero) %>
                </td>
                <td>
                    <%= Html.EncodeDate(item.Data) %>
                </td>
                <td>
                    <%= Html.EncodeCurrency(item.Total)%>
                </td>
                <td>
                    <%= Html.ActionLink("Detalhe", "DetalharNotaFiscal", new {codNotaFiscal = item.CodNotaFiscal})%>
                </td>
                <td>
                    <%= Html.ActionLink("NFE", "NotaFiscalEletronica", new { codNotaFiscal = item.CodNotaFiscal })%>
                </td>
            </tr>

        <% } // end foreach %>
    </table>
<% } // end if %>
