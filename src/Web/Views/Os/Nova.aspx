<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<OsNovaViewModel>" %>
<%@ Import Namespace="Dataweb.Dilab.Web.Model"%>

<asp:Content ID="headContent" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" language="javascript" src="../../Scripts/jquery.rules.os.nova.js" ></script>
</asp:Content>

<asp:Content ID="titleContent" ContentPlaceHolderID="TitleContent" runat="server">
	Criar
</asp:Content>

<asp:Content ID="mainContent" ContentPlaceHolderID="MainContent" runat="server">
    <form id="formNovaOs" action="" method="post">
        <h2>Nova OS</h2>

        <div id="divNumeroOS">
            <h3>Digite o seu número da OS:</h3>
            <input type="text" name="numeroOS" />
        </div>

        <div id="divLentes">
            <div id="divOD">
                <h3>Olho Direito</h3>

                <div class="divcom1">
                    <label for="familiaOD">Família:</label><br />
                    <select name="familiaOD" id="familiaOD" class="familia">
                        <option value="" selected="selected">(Selecione uma família)</option>
                        <% if (Model != null && Model.Familias != null) foreach(var familia in Model.Familias) { %>
                            <option value="<%=familia.CodFamilia %>"><%=familia.Descricao %></option>
                        <% } // end-foreach %>
                    </select><br />
                </div>

                <div id="divLongeOD" class="divcom3">
                    <h4>Longe</h4>
                    <div class="div1de3">
                        <label for="esfOD">Esf.:</label><br />
                        <input type="text" name="esfLongeOD" id="esfOD" class="required numeric" format="n1x3c3S" />
                    </div>
                    <div class="div1de3">
                        <label for="cilOD">Cil.:</label><br />
                        <input type="text" name="cilLongeOD" id="cilOD" class="required numeric" format="n1x3c3S" />
                    </div>
                    <div class="div1de3">
                        <label for="eixoOD">Eixo:</label><br />
                        <input type="text" name="eixoLongeOD" id="eixoOD" class="required numeric" format="p3x3c0S" max="180" />
                    </div>
                </div>
                <br clear="all" />
                
                <div class="divcom1">
                    <label for="adicaoOD">Adição:</label><br />
                    <input type="text" name="adicaoOD" id="adicaoOD" class="required numeric" format="n6x3c2S" /><br />
                </div>

                <div id="divPertoOD" class="divcom3">
                    <h4>Perto</h4>
                    <div class="div1de3">
                        <label for="esfOD">Esf.:</label><br />
                        <input type="text" name="esfPertoOD" id="Text1" class="required numeric" format="n1x3c3S" />
                    </div>
                    <div class="div1de3">
                        <label for="cilOD">Cil.:</label><br />
                        <input type="text" name="cilPertoOD" id="Text2" class="required numeric" format="n1x3c3S" />
                    </div>
                    <div class="div1de3">
                        <label for="eixoOD">Eixo:</label><br />
                        <input type="text" name="eixoPertoOD" id="Text3" class="required numeric" format="p3x3c0S" max="180" />
                    </div>
                </div>
                <br clear="all" />

                <div class="divcom2">
                    <div class="div1de2">
                        <label for="dnpOD">D.N.P.:</label><br />
                        <input type="text" name="dnpOD" id="dnpOD" class="required numeric" format="p2x3c1S" /><br />
                    </div>
                    <div class="div1de2">
                        <label for="altOD">Alt.:</label><br />
                        <input type="text" name="altOD" id="altOD" class="required numeric" format="n6x3c2S" /><br />
                    </div>
                </div>
                <br clear="all" />

                <div class="divcom1">
                    <label for="descricaoLenteOD">Descrição da Lente:</label><br />
                    <input type="text" name="descricaoLenteOD" id="descricaoLenteOD" />
                </div>
            </div>

            <div id="divOE">
                <h3>Olho Esquerdo</h3>

                <div class="divcom1">
                    <label for="familiaOE">Família:</label><br />
                    <select name="familiaOE" id="familiaOE" class="familia">
                        <option value="" selected="selected">(Selecione uma família)</option>
                        <% if (Model != null && Model.Familias != null) foreach(var familia in Model.Familias) { %>
                            <option value="<%=familia.CodFamilia %>"><%=familia.Descricao %></option>
                        <% } // end-foreach %>
                    </select><br />
                </div>

                <div id="divLongeOE" class="divcom3">
                    <h4>Longe</h4>
                    <div class="div1de3">
                        <label for="esfOE">Esf.:</label><br />
                        <input type="text" name="esfLongeOE" id="Text4" class="required numeric" format="n1x3c3S" />
                    </div>
                    <div class="div1de3">
                        <label for="cilOE">Cil.:</label><br />
                        <input type="text" name="cilLongeOE" id="Text5" class="required numeric" format="n1x3c3S" />
                    </div>
                    <div class="div1de3">
                        <label for="eixoOE">Eixo:</label><br />
                        <input type="text" name="eixoLongeOE" id="Text6" class="required numeric" format="p3x3c0S" max="180" />
                    </div>
                </div>
                <br clear="all" />
                
                <div class="divcom1">
                    <label for="adicaoOE">Adição:</label><br />
                    <input type="text" name="adicaoOE" id="Text7" class="required numeric" format="n6x3c2S" /><br />
                </div>

                <div id="divPertoOE" class="divcom3">
                    <h4>Perto</h4>
                    <div class="div1de3">
                        <label for="esfOE">Esf.:</label><br />
                        <input type="text" name="esfPertoOE" id="Text8" class="required numeric" format="n1x3c3S" />
                    </div>
                    <div class="div1de3">
                        <label for="cilOE">Cil.:</label><br />
                        <input type="text" name="cilPertoOE" id="Text9" class="required numeric" format="n1x3c3S" />
                    </div>
                    <div class="div1de3">
                        <label for="eixoOE">Eixo:</label><br />
                        <input type="text" name="eixoPertoOE" id="Text10" class="required numeric" format="p3x3c0S" max="180" />
                    </div>
                </div>
                <br clear="all" />

                <div class="divcom2">
                    <div class="div1de2">
                        <label for="dnpOE">D.N.P.:</label><br />
                        <input type="text" name="dnpOE" id="Text11" class="required numeric" format="p2x3c1S" /><br />
                    </div>
                    <div class="div1de2">
                        <label for="altOE">Alt.:</label><br />
                        <input type="text" name="altOE" id="Text12" class="required numeric" format="n6x3c2S" /><br />
                    </div>
                </div>
                <br clear="all" />

                <div class="divcom1">
                    <label for="descricaoLenteOE">Descrição da Lente:</label><br />
                    <input type="text" name="descricaoLenteOE" id="Text13" />
                </div>
            </div>
        </div>
        
        <div id="divDp">
            <label for="dp">D.P.:</label><br />
            <input type="text" name="dp" id="dp" />
        </div>

        <div id="divParam">
            <h3>Parâmetros</h3>
            <div class="div1de6">
                <label for="ta">T.A.:</label><br />
                <input type="text" name="ta" id="ta" />
            </div>
            <div class="div1de6">
                <label for="aa">A.A.:</label><br />
                <input type="text" name="aa" id="aa" />
            </div>
            <div class="div1de6">
                <label for="md">M.D.:</label><br />
                <input type="text" name="md" id="md" />
            </div>
            <div class="div1de6">
                <label for="eixo">Eixo:</label><br />
                <input type="text" name="eixo" id="eixo" class="required numeric" format="p3x3c0S" max="180" />
            </div>
            <div class="div1de6">
                <label for="ponte">Ponte:</label><br />
                <input type="text" name="ponte" id="ponte" />
            </div>
            <div class="div1de6">
                <label for="diametro">&Oslash;:</label><br />
                <input type="text" name="diametro" id="diametro" />
            </div>
        </div>
        <br clear="all" />

        <div id="divInfo">
            <h3>Informações Adicionais</h3>

            <div class="divcom2">
                <div class="div1de2">
                    Armação:<br />
                    <input type="text" name="armacao" id="armacao" />
                </div>
                <div class="div1de2">
                    Material da Armação:<br />
                    <select name="materialArmacao" id="materialArmacao">
                        <option value="" selected="selected">(Selecione um material)</option>
                        <% if (Model != null && Model.Familias != null) foreach(var material in Model.Materiais) { %>
                            <option value="<%=material.CodMaterial %>"><%=material.Descricao%></option>
                        <% } // end-foreach %>
                    </select>
                </div>
            </div>
            <br clear="all" />

            <div class="divcom2">
                <div class="div1de2">
                    Observação da Armação:<br />
                    <textarea name="observacaoArmacao" id="observacaoArmacao" cols="0" rows="0"></textarea>
                </div>
                <div class="div1de2">
                    Observação Geral:<br />
                    <textarea name="observacaoGeral" id="observacaoGeral" cols="0" rows="0"></textarea>
                </div>
            </div>
        </div>
        <br clear="all" />

        <h3>Serviços</h3>
        <div id="divServicos">
            <div id="divMsgSemServicos">Selecione uma família para obter os serviços disponíveis.</div>
            <div id="divCheckServicos">
                <div id="divServicosOD">
                    Serviços para <b><label id="lbFamiliaOD">FAMILIA OD</label></b>:<br /><br />
                    <div id="divServicosODChecks"></div>
                </div>
                <div id="divServicosOE">
                    Serviços para <b><label id="lbFamiliaOE">FAMILIA OE</label></b>:<br /><br />
                    <div id="divServicosOEChecks"></div>
                </div>
            </div>
        </div>
        <br clear="all" />

        <input type="submit" value="OK" />
    </form>
</asp:Content>
