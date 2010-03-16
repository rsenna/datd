<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ComprasDetalhar>" %>
<%@ Import Namespace="Dataweb.Dilab.Web.Models"%>

<h3><%= Model.IsOrdemServico? "Serviços" : "Produtos" %></h3>
<div id="divItens">
    <ul>
        <% foreach (var item in Model.Transacao.Itens) { %>
            <li><%= item.Descricao %></li>
        <% } // end foreach %>
    </ul>
</div>
