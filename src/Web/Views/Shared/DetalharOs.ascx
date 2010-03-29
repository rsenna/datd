<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ComprasDetalhar>" %>
<%@ Import Namespace="Dataweb.Dilab.Web.Models"%>

<div id="divLentes">
    <div id="divOD" class="coluna">
        <h4>Olho Direito</h4>

        <div id="divLongeOD" class="divcom3">
            <h4>Longe</h4>
            <div class="div1de3">
                <label>Esf.:</label><br />
                <span class="field"><%= Model.OrdemServico.LenteOd.LongeEsf %></span>
            </div>
            <div class="div1de3">
                <label>Cil.:</label><br />
                <span class="field"><%= Model.OrdemServico.LenteOd.LongeCil %></span>
            </div>
            <div class="div1de3">
                <label>Eixo:</label><br />
                <span class="field"><%= Model.OrdemServico.LenteOd.LongeEixo %></span>
            </div>
        </div>
        <br clear="all" />
        <br clear="all" />
        
        <div class="divcom1">
            <label>Adição:</label><br />
            <span class="field"><%= Model.OrdemServico.LenteOd.Adicao %></span>
        </div>
        <br clear="all" />

        <div id="divPertoOD" class="divcom3">
            <h4>Perto</h4>
            <div class="div1de3">
                <label>Esf.:</label><br />
                <span class="field"><%= Model.OrdemServico.LenteOd.PertoEsf %></span>
            </div>
            <div class="div1de3">
                <label>Cil.:</label><br />
                <span class="field"><%= Model.OrdemServico.LenteOd.PertoCil %></span>
            </div>
            <div class="div1de3">
                <label>Eixo:</label><br />
                <span class="field"><%= Model.OrdemServico.LenteOd.PertoEixo %></span>
            </div>
        </div>
        <br clear="all" />
        <br clear="all" />

        <div class="divcom2">
            <div class="div1de2">
                <label>D.N.P.:</label><br />
                <span class="field"><%= Model.OrdemServico.LenteOd.Dnp %></span>
            </div>
            <div class="div1de2">
                <label>Alt.:</label><br />
                <span class="field"><%= Model.OrdemServico.LenteOd.Alt %></span>
            </div>
        </div>
    </div>

    <div id="divOE" class="coluna">
        <h4>Olho Esquerdo</h4>

        <div id="divLongeOE" class="divcom3">
            <h4>Longe</h4>
            <div class="div1de3">
                <label>Esf.:</label><br />
                <span class="field"><%= Model.OrdemServico.LenteOe.LongeEsf %></span>
            </div>
            <div class="div1de3">
                <label>Cil.:</label><br />
                <span class="field"><%= Model.OrdemServico.LenteOe.LongeCil %></span>
            </div>
            <div class="div1de3">
                <label>Eixo:</label><br />
                <span class="field"><%= Model.OrdemServico.LenteOe.LongeEixo %></span>
            </div>
        </div>
        <br clear="all" />
        <br clear="all" />

        <div class="divcom1">
            <label>Adição:</label><br />
            <span class="field"><%= Model.OrdemServico.LenteOe.Adicao %></span>
        </div>
        <br clear="all" />

        <div id="divPertoOE" class="divcom3">
            <h4>Perto</h4>
            <div class="div1de3">
                <label>Esf.:</label><br />
                <span class="field"><%= Model.OrdemServico.LenteOe.PertoEsf %></span>
            </div>
            <div class="div1de3">
                <label>Cil.:</label><br />
                <span class="field"><%= Model.OrdemServico.LenteOe.PertoCil %></span>
            </div>
            <div class="div1de3">
                <label>Eixo:</label><br />
                <span class="field"><%= Model.OrdemServico.LenteOe.PertoEixo %></span>
            </div>
        </div>
        <br clear="all" />
        <br clear="all" />

        <div class="divcom2">
            <div class="div1de2">
                <label>D.N.P.:</label><br />
                <span class="field"><%= Model.OrdemServico.LenteOe.Dnp %></span>
            </div>
            <div class="div1de2">
                <label>Alt.:</label><br />
                <span class="field"><%= Model.OrdemServico.LenteOe.Alt %></span>
            </div>
        </div>
    </div>
</div>
<br clear="all" />
<br clear="all" />

<div id="divDp">
    <label>D.P.:</label><br />
    <span class="field"><%= Model.OrdemServico.Dp %></span>
</div>
<br clear="all" />

<div id="divParam">
    <h4>Parâmetros</h4>
    <div class="div1de6">
        <label>T.A.:</label><br />
        <span class="field"><%= Model.OrdemServico.Ta %></span>
    </div>
    <div class="div1de6">
        <label>A.A.:</label><br />
        <span class="field"><%= Model.OrdemServico.Aa %></span>
    </div>
    <div class="div1de6">
        <label>M.D.:</label><br />
        <span class="field"><%= Model.OrdemServico.Md %></span>
    </div>
    <div class="div1de6">
        <label>Eixo:</label><br />
        <span class="field"><%= Model.OrdemServico.Eixo %></span>
    </div>
    <div class="div1de6">
        <label>Ponte:</label><br />
        <span class="field"><%= Model.OrdemServico.Ponte %></span>
    </div>
    <div class="div1de6">
        <label>&Oslash;:</label><br />
        <span class="field"><%= Model.OrdemServico.Diametro %></span>
    </div>
</div>
<br clear="all" />

<div id="divInfo">
    <h4>Informações Adicionais</h4>
    <div class="coluna" id="divColunaE">
        <div class="divcom1">
            <label>Armação:</label><br />
            <span class="field coluna"><%= Model.OrdemServico.DescricaoArmacao %></span>
            <br clear="all" />
            <br />
            <label>Observação da Armação:</label><br />
            <span class="field coluna"><%= Model.OrdemServico.ObservacaoArmacao %></span>
        </div>
    </div>
    <div class="coluna" id="divColunaD">
        <div class="divcom1">
            <label>Material da Armação:</label><br />
            <span class="field coluna"><%= Model.Material %></span>
            <br clear="all" />
            <br />
            <label>Observação Geral:</label><br />
            <span class="field coluna"><%= Model.OrdemServico.Observacao %></span>
        </div>
    </div>
</div>
<br clear="all" />