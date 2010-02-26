<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<OrdemServicoQuery[]>" %>
<%@ Import Namespace="Dataweb.Dilab.Web"%>
<%@ Import Namespace="Dataweb.Dilab.Model.DataTransfer"%>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Ordens de Servi�o
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Ordens de Servi�o</h2>
    <table>
        <tr>
            <th>N� OS/Entrada</th>
            <th>Refer�ncia</th>
            <th>Etapa</th>
            <th>Previs�o<br />Encerramento</th>
        </tr>
    <% foreach (var item in Model) { %>
        <tr>
            <td><b><%=item.NumeroOrdemServico %></b><br /><%=Html.EncodeDate(item.Emissao) %></td>
            <td><%=item.Referencia %></td>
            <td><%=Html.Encode<TipoEtapa>(item.Etapa) %></td>
            <td><%=Html.EncodeDate(item.Previsao) %></td>
        </tr>
    <% } // end-foreach %>
    </table>
</asp:Content>
