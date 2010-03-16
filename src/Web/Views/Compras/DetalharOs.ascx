<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ComprasDetalhar>" %>
<%@ Import Namespace="Dataweb.Dilab.Web.Models"%>

<div id="divLentes">
    <div id="divOD" class="coluna">
        <h3>Olho Direito</h3>

        <div id="divLongeOD" class="divcom3">
            <h4>Longe</h4>
            <div class="div1de3">
                <span class="title">Esf.:</span><br />
                <%= Model.OrdemServico.LenteOd.LongeEsf %>
            </div>
            <div class="div1de3">
                <span class="title">Cil.:</span><br />
                <%= Model.OrdemServico.LenteOd.LongeCil %>
            </div>
            <div class="div1de3">
                <span class="title">Eixo:</span><br />
                <%= Model.OrdemServico.LenteOd.LongeEixo %>
            </div>
        </div>
        <br clear="all" />
        <br clear="all" />
        
        <div class="divcom1">
            <span class="title">Adição:</span><br />
            <%= Model.OrdemServico.LenteOd.Adicao %>
        </div>

        <div id="divPertoOD" class="divcom3">
            <h4>Perto</h4>
            <div class="div1de3">
                <span class="title">Esf.:</span><br />
                <%= Model.OrdemServico.LenteOd.PertoEsf %>
            </div>
            <div class="div1de3">
                <span class="title">Cil.:</span><br />
                <%= Model.OrdemServico.LenteOd.PertoCil %>
            </div>
            <div class="div1de3">
                <span class="title">Eixo:</span><br />
                <%= Model.OrdemServico.LenteOd.PertoEixo %>
            </div>
        </div>
        <br clear="all" />
        <br clear="all" />

        <div class="divcom2">
            <div class="div1de2">
                <span class="title">D.N.P.:</span><br />
                <%= Model.OrdemServico.LenteOd.Dnp %>
            </div>
            <div class="div1de2">
                <span class="title">Alt.:</span><br />
                <%= Model.OrdemServico.LenteOd.Alt %>
            </div>
        </div>
    </div>

    <div id="divOE" class="coluna">
        <h3>Olho Esquerdo</h3>

        <div id="divLongeOE" class="divcom3">
            <h4>Longe</h4>
            <div class="div1de3">
                <span class="title">Esf.:</span><br />
                <%= Model.OrdemServico.LenteOe.LongeEsf %>
            </div>
            <div class="div1de3">
                <span class="title">Cil.:</span><br />
                <%= Model.OrdemServico.LenteOe.LongeCil %>
            </div>
            <div class="div1de3">
                <span class="title">Eixo:</span><br />
                <%= Model.OrdemServico.LenteOe.LongeEixo %>
            </div>
        </div>
        <br clear="all" />
        <br clear="all" />

        <div class="divcom1">
            <span class="title">Adição:</span><br />
            <%= Model.OrdemServico.LenteOe.Adicao %>
        </div>

        <div id="divPertoOE" class="divcom3">
            <h4>Perto</h4>
            <div class="div1de3">
                <span class="title">Esf.:</span><br />
                <%= Model.OrdemServico.LenteOe.PertoEsf %>
            </div>
            <div class="div1de3">
                <span class="title">Cil.:</span><br />
                <%= Model.OrdemServico.LenteOe.PertoCil %>
            </div>
            <div class="div1de3">
                <span class="title">Eixo:</span><br />
                <%= Model.OrdemServico.LenteOe.PertoEixo %>
            </div>
        </div>
        <br clear="all" />
        <br clear="all" />

        <div class="divcom2">
            <div class="div1de2">
                <span class="title">D.N.P.:</span><br />
                <%= Model.OrdemServico.LenteOe.Dnp %>
            </div>
            <div class="div1de2">
                <span class="title">Alt.:</span><br />
                <%= Model.OrdemServico.LenteOe.Alt %>
            </div>
        </div>
    </div>
</div>
<br clear="all" />
<br clear="all" />

<div id="divDp">
    <span class="title">D.P.:</span><br />
    <%= Model.OrdemServico.Dp %>
</div>
<br clear="all" />

<div id="divParam">
    <h3>Parâmetros</h3>
    <div class="div1de6">
        <span class="title">T.A.:</span><br />
        <%= Model.OrdemServico.Ta %>
    </div>
    <div class="div1de6">
        <span class="title">A.A.:</span><br />
        <%= Model.OrdemServico.Aa %>
    </div>
    <div class="div1de6">
        <span class="title">M.D.:</span><br />
        <%= Model.OrdemServico.Md %>
    </div>
    <div class="div1de6">
        <span class="title">Eixo:</span><br />
        <%= Model.OrdemServico.Eixo %>
    </div>
    <div class="div1de6">
        <span class="title">Ponte:</span><br />
        <%= Model.OrdemServico.Ponte %>
    </div>
    <div class="div1de6">
        <span class="title">&Oslash;:</span><br />
        <%= Model.OrdemServico.Diametro %>
    </div>
</div>
<br clear="all" />

<div id="divInfo">
    <h3>Informações Adicionais</h3>
    <div class="coluna">
        <div class="divcom1">
            <span class="title">Armação:</span><br />
            <%= Model.OrdemServico.DescricaoArmacao %>
            <br />
            <span class="title">Observação da Armação:</span><br />
            <%= Model.OrdemServico.ObservacaoArmacao %>
        </div>
    </div>
    <div class="coluna">
        <div class="divcom1">
            <span class="title">Material da Armação:</span><br />
            <%= Model.OrdemServico.CodMaterial %><br />
            <br />
            <span class="title">Observação Geral:</span><br />
            <%= Model.OrdemServico.Observacao %><br />
        </div>
    </div>
</div>
<br clear="all" />