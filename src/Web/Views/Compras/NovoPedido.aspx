<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ComprasNovoPedido>" %>
<%@ Import Namespace="Dataweb.Dilab.Web.Models"%>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" language="javascript">
        // Global utilizada por jquery.rules.os.nova.js; contém o caminho do gif de loader ajax:
        ajaxLoaderGifUrl = '<%= ResolveUrl("~/Content/img/ajax-loader.gif") %>';
    </script>
    <script type="text/javascript" language="javascript" src="<%= ResolveUrl("~/Scripts/jquery.rules.js") %>"></script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Novo Pedido
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Novo Pedido</h2>

    Produtos Disponíveis
    <div id="divProdutosDisponiveis" class="container">
        <div id="divSelecaoFamilia">
            Família<br />
            <select id="selFiltroFamilia">
                <option value="" selected="selected">(Selecione uma família)</option>
                <% foreach(var familia in Model.Familias) { %>
                    <option value="<%=familia.CodFamilia %>"><%=familia.Descricao %></option>
                <% } // end-foreach %>
            </select>
        </div>
        <div id="divSelecaoProduto">
            Produto<br />
            <select id="selFiltroProduto">
                <option value="" selected="selected">(Selecione um produto)</option>
            </select>
        </div>
        <div id="divSelecaoQtd">
            Quantidade<br />
            <input type="text" maxlength="3" value="0" id="itxFiltroQuantidade" class="numeric" />
        </div>
        <input type="button" value="Adicionar" id="ibtAdicionar" />
    </div>
    <br />
    <form id="frmNovoPedido" method="post" action="#">
        Itens
        <div id="divItens" class="container">
            <span id="message">Adicione produtos ao pedido através do painel superior.</span>
            <input type="hidden" id="alertProduto" value="Este produto já foi adicionado." />
            <table id="tabItens">
                <tr>
                    <th>Família</th>
                    <th>Produto</th>
                    <th>Qtd</th>
                    <th>&nbsp;</th>
                </tr>
                <%/*
                   * A tr abaixo é usada como template via javascript. Os
                   * valores {*} são substituídos da seguinte forma:
                   * {0}: código do produto
                   * {1}: código do produto (somente digitos)
                   * {2}: descrição da família
                   * {3}: descrição do produto
                   * {4}: quantidade do produto
                   */%>
                <tr class="template">
                    <td>
                        <input type="hidden" name="produto_{1}" value="{0}" />
                        {2}
                    </td>
                    <td>
                        {3}
                    </td>
                    <td>
                        <input type="text" class="itxQtd" name="qtd_{1}" value="{4}" class="numeric" />
                    </td>
                    <td class="tdExcluir" id="tdExcluir_{1}">
                        <a href="#" class="linkExcluir">Excluir</a>
                    </td>
                </tr>
            </table>
        </div>
        <br />
        Observações<br />
        <textarea rows="4" cols="40" id="txaObservacoes"></textarea>
        <br />
        <input type="submit" value="Finalizar Pedido" />
    </form>
</asp:Content>

