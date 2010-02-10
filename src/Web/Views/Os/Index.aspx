<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<OrdemServico[]>" %>
<%@ Import Namespace="Dataweb.Dilab.Web"%>
<%@ Import Namespace="Dataweb.Dilab.Model.DataTransfer"%>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Ordens de Serviço
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Ordens de Serviço</h2>
    <table>
        <tr>
            <th>Nº OS/Entrada</th>
            <th>Referência</th>
            <th>Etapa</th>
            <th>Previsão<br />Encerramento</th>
        </tr>
    <% foreach (var item in Model) { %>
        <tr>
            <td><b><%=item.NumeroOrdemServico %></b><br /><%=WebHelper.FormatDate(item.Emissao) %></td>
            <td><%=item.Referencia %></td>
            <td><%=item.Etapa %></td>
            <td><%=WebHelper.FormatDate(item.Previsao) %></td>
        </tr>
    <% } // end-foreach %>
    </table>
</asp:Content>
