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

        <% var totalGeral = 0m; foreach (var item in Model) { totalGeral += item.Total; %>

            <tr>
                <td>
                    <%= Html.Encode(item.Documento) %>
                </td>
                <td>
                    <%= Html.EncodeDate(item.Vencimento) %>
                </td>
                <td>
                    <%= Html.EncodeCurrency(item.Total)%>
                </td>
                <td>
                    <% if (item.Pagamento == null) { %>
                        <% if (item.Vencimento.Date < DateTime.Today) { %>
                            <span class="pendente atrasado">(atrasado)</span>
                        <% } else { %>
                            <span class="pendente ok">(aberto)</span>
                        <% } %>
                    <% } else { %>
                        <% if (item.Vencimento.Date < item.Pagamento.Value.Date) { %>
                            <span class="efetuado atrasado"><%= Html.EncodeDate(item.Pagamento) %></span>
                        <% } else { %>
                            <span class="efetuado ok"><%= Html.EncodeDate(item.Pagamento) %></span>
                        <% } %>
                    <% } // end if item.Pagamento %>
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
