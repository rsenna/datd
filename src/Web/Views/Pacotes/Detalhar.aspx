<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="ViewPage<PacotesDetalhar>" %>
<%@ Import Namespace="Dataweb.Dilab.Web.Models"%>
<%@ Import Namespace="Dataweb.Dilab.Model.DataTransfer"%>
<%@ Import Namespace="Dataweb.Dilab.Web"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
  Detalhar Pacotes
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Histórico de Uso</h2>
    
    <div id="divPacote">
        <h3>Pacote</h3>
        <%=Model.Descricao %>
    </div>

    <div id="divCompras">
        <h3>Compras</h3>

        <% if (Model.Compra == null || Model.Compra.Count() == 0) { %>

            Não há registro de compras efetuadas.

        <% } else { %>

            <table>
                <tr>
                    <th>
                        Data
                    </th>
                    <th>
                        Valor
                    </th>
                    <th>
                        Quantidade
                    </th>
                </tr>

                <% foreach (var item in Model.Compra) { %>

                    <tr>
                        <td>
                            <%= Html.EncodeDate(item.Data)%>
                        </td>
                        <td>
                            <%= item.Valor.HasValue ? Html.EncodeCurrency(item.Valor) : "(Manual)"%>
                        </td>
                        <td>
                            <%= Html.Encode(item.Quantidade)%>
                        </td>
                    </tr>

                <% } // end foreach %>

                <tr class="summary">
                    <td>&nbsp;</td>
                    <td class="title">Total:</td>
                    <td><%= Html.Encode(Model.TotalCompras) %></td>
                </tr>
            </table>
        <% } // end if %>
    </div>

    <div id="divUsos">
        <h3>Usos</h3>

        <% if (Model.Uso == null || Model.Uso.Count() == 0) { %>

            Não há registro de usos efetuados.

        <% } else { %>
            <table>
                <tr>
                    <th>
                        Data
                    </th>
                    <th>
                        Nº O.S.
                    </th>
                    <th>
                        Quantidade
                    </th>
                </tr>

                <% foreach (var item in Model.Uso) { %>

                    <tr>
                        <td>
                            <%= Html.EncodeDate(item.Data)%>
                        </td>
                        <td>
                            <%= Html.Encode(item.NumeroOS)%>
                        </td>
                        <td>
                            <%= Html.Encode(item.Quantidade)%>
                        </td>
                    </tr>

                <% } // end foreach %>

                <tr class="summary">
                    <td>&nbsp;</td>
                    <td class="title">Total:</td>
                    <td><%= Html.Encode(Model.TotalUsos) %></td>
                </tr>
            </table>
        <% } // end if %>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

