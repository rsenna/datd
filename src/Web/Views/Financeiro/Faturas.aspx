<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="ViewPage<IEnumerable<Fatura>>" %>
<%@ Import Namespace="System.Web.Mvc" %>
<%@ Import Namespace="Dataweb.Dilab.Model.DataTransfer"%>
<%@ Import Namespace="Dataweb.Dilab.Web"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Dilab Online - Financeiro - Minhas Faturas
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Minhas Faturas</h2>

    <% if (Model.Count() == 0) { %>

        N�o h� faturas cadastradas.

    <% } else { %>
        <table>
            <tr>
                <th>
                    N�mero
                </th>
                <th>
                    Data
                </th>
                <th>
                    Total
                </th>
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
                        <%= Html.EncodeCurrency(item.Total) %>
                    </td>
                    <td>
                        <%= Html.ActionLink("Detalhe", "DetalharFatura", new {codFatura = item.CodFatura})%>
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

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

