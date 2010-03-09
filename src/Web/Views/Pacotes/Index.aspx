<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="ViewPage<IEnumerable<PacoteCredito>>" %>
<%@ Import Namespace="System.Web.Mvc" %>
<%@ Import Namespace="Dataweb.Dilab.Model.DataTransfer"%>
<%@ Import Namespace="Dataweb.Dilab.Web"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Dilab Online - Pacotes
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Pacotes</h2>

    <% if (Model.Count() == 0) { %>

        Não há pacotes cadastrados.

    <% } else { %>
        <table>
            <tr>
                <th>
                    Descrição
                </th>
                <th>
                    Quantidade
                </th>
                <th></th>
            </tr>

            <% foreach (var item in Model) { %>

                <tr>
                    <td>
                        <%= Html.Encode(item.Descricao) %>
                    </td>
                    <td>
                        <%= Html.Encode(item.Quantidade) %>
                    </td>
                    <td>
                        <%= Html.ActionLink("Detalhe", "Detalhar", new {codPacoteCliente = item.CodPacoteCredito})%>
                    </td>
                </tr>

            <% } // end foreach %>
        </table>
    <% } // end if %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

