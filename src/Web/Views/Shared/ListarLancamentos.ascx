<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Lancamento>>" %>
<%@ Import Namespace="Dataweb.Dilab.Web"%>
<%@ Import Namespace="Dataweb.Dilab.Model.DataTransfer"%>

<% if (Model.Count() == 0) { %>

    Não há lançamentos cadastrados.

<% } else { %>
    <table>
        <tr>
            <th>
                Número
            </th>
            <th>
                Vencimento
            </th>
            <th>
                Total
            </th>
            <th>
                Pagamento
            </th>
        </tr>

        <% foreach (var item in Model) { %>

            <tr>
                <td>
                    <%= Html.Encode(item.Numero) %>
                </td>
                <td>
                    <%= Html.EncodeDate(item.Vencimento) %>
                </td>
                <td>
                    <%= Html.EncodeCurrency(item.Total)%>
                </td>
                <td>
                    <% if (item.Pagamento != null) { %>
                        <%= Html.EncodeDate(item.Pagamento) %>
                    <% } else if (item.Vencimento.Date < DateTime.Today) { %>
                        <span class="pagamentoAtrasado">(atrasado)</span>
                    <% } else { %>
                        <span class="pagamentoAberto">(aberto)</span>
                    <% } // end if item.Pagamento %>
                </td>
            </tr>

        <% } // end foreach %>
    </table>
<% } // end if %>
