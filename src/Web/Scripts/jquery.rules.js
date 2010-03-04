$(document).ready(function() {
    function atualizarServicosFamilia(selectTag, force) {
        function Tags() {
            this.thisSelect = selectTag;

            if (this.thisSelect === '#familiaOD') {
                this.thisDivServicos = '#divServicosOD';
                this.thisDivChecks = '#divServicosODChecks';
                this.thisLabel = '#lbFamiliaOD';

                this.thatSelect = '#familiaOE';
                this.thatDivServicos = '#divServicosOE';
                this.thatDivChecks = '#divServicosOEChecks';
            } else {
                this.thisDivServicos = '#divServicosOE';
                this.thisDivChecks = '#divServicosOEChecks';
                this.thisLabel = '#lbFamiliaOE';

                this.thatSelect = '#familiaOD';
                this.thatDivServicos = '#divServicosOD';
                this.thatDivChecks = '#divServicosODChecks';
            }
        }

        function checkVisibilityServicos() {
            if ($(tags.thatDivServicos).is(':hidden')) {
                var thatSelectedVal = $(tags.thatSelect).val();

                if (thatSelectedVal != '' && thatSelectedVal != $(tags.thisSelect).val()) {
                    $(tags.thatDivServicos).show();
                }
            }

            if ($(tags.thisDivServicos).is(':hidden') && $(tags.thatDivServicos).is(':hidden')) {
                $('#divMsgSemServicos').show();
            } else {
                $('#divMsgSemServicos').hide();
            }
        }

        tags = new Tags();
        $(tags.thisLabel).text($(tags.thisSelect + ' option:selected').text());

        if ($(tags.thisSelect).val() === '') {
            $(tags.thisDivServicos).hide();

            if (force) {
                atualizarServicosFamilia(tags.thatSelect, false);
            } else {
                checkVisibilityServicos();
            }

        } else if ($(tags.thisSelect).val() === $(tags.thatSelect).val() && !force) {
            $(tags.thisDivChecks).html($(tags.thatDivChecks).html());
            $(tags.thisDivServicos).hide();
            checkVisibilityServicos();

        } else {
            $.getJSON(
                'ListarServicos', { familia: $(tags.thisSelect).val() },
                function(json) {
                    var tags = new Tags();
                    var options = [];

                    $.each(json, function(key, value) {
                        var checkbox = $.validator.format(
                            '<input type="checkbox" name="servicos" id="{0}" value="{1}"/><label for="{0}">{2}</label><br/>',
                            'servico' + key,
                            value.CodItem,
                            value.Descricao
                        );
                        options.push(checkbox);
                    });

                    if (options.length === 0) {
                        $(tags.thisDivServicos).hide();

                    } else {
                        var thisDivChecks = $(tags.thisDivChecks);
                        thisDivChecks.empty();
                        thisDivChecks.append(options.join(''));

                        $(tags.thisDivServicos).show();
                    }

                    checkVisibilityServicos();

                    if (force) {
                        atualizarServicosFamilia(tags.thatSelect, false);
                    }
                }
            );
        }
    }

    function genId(value) {
        value = value.toString();
        var validChars = '0123456789';
        var result = '';
        for (var i = 0; i < value.length; i++) {
            var ch = value.charAt(i);
            if (validChars.indexOf(ch) >= 0) {
                result += ch;
            }
        }
        return result;
    }

    function atualizarProdutosFamilia(force) {
        var familias = $('select#selFiltroFamilia');
        var produtos = $('select#selFiltroProduto');

        function clearProdutos() {
            // Remove todos os itens com exceção do primeiro; depois seleciona o primeiro:
            $('select#selFiltroProduto option[value!=""]').remove();
            produtos.val('');
        }

        if (familias.val() === '') {
            clearProdutos();
        } else {
            $.getJSON(
                'ListarProdutos', { familia: familias.val() },
                function(json) {
                    clearProdutos();

                    var options = [];

                    $.each(json, function(key, value) {
                        var option = $.validator.format(
                            '<option value="{0}">{1}</option>',
                            value.CodItem,
                            value.Descricao
                        );
                        options.push(option);
                    });

                    if (options.length !== 0) {
                        produtos.append(options.join(''));
                    }
                }
            );
        }
    }

    function verificarListagemProdutos() {
        var table = $('table#tabItens');
        var message = $('span#message');
        var cRows = $('table#tabItens tr').length;

        if (cRows > 2) { // 2 = header + linha de template
            table.show();
            message.hide();
        } else {
            table.hide();
            message.show();
        }
    }

    function adicionarProduto() {
        var familiaVal = $('select#selFiltroFamilia').val();
        var produtoVal = $('select#selFiltroProduto').val();
        var qtdVal = $('input#itxFiltroQuantidade').val();

        if (familiaVal && produtoVal && qtdVal && qtdVal > 0) {
            var trSelector = 'tr#tr_' + genId(produtoVal);
            var tr = $(trSelector);

            if (!tr.length) {
                var table = $('table#tabItens');
                var message = $('span#message');
                var familiaTxt = $('select#selFiltroFamilia option:selected').text();
                var produtoTxt = $('select#selFiltroProduto option:selected').text();
                var rowTemplate = '<tr id="tr_{1}">' + $('tr.template').html() + '</tr>';

                var row = $.validator.format(
                    rowTemplate,
                    produtoVal,
                    genId(produtoVal),
                    familiaTxt,
                    produtoTxt,
                    qtdVal
                );

                table.append(row);
                table.show();
                message.hide();
            }

            $(trSelector + ' input.itxQtd')
                .focus()
                .parent()
                    .css('background-color', '#ffffc0')
                    .siblings().css('background-color', '#ffffc0');
        }
    }

    /* Para usar imagem de espera nas chamadas ajax (ajaxLoaderGifUrl deve ser definido na página, antes de incluir jquery.rules.js, com o caminho da imagem):

    $('body').append('<div id="ajaxBusy"><p><img src="' + ajaxLoaderGifUrl + '"></p></div>');
    $('#ajaxBusy').css({
    display: 'none',
    margin: '0px',
    padding: '0px',
    position: 'absolute',
    left: '3px',
    top: '3px',
    width: 'auto'
    });
    */

    $(this).ajaxStart(function() {
        document.body.style.cursor = "wait";
        // $('#ajaxBusy').show();
    }).ajaxStop(function() {
        // $('#ajaxBusy').hide();
        document.body.style.cursor = "default";
    });

    $('input.numeric').each(function() {
        $(this).css('text-align', 'right');
    }).focus(function() {
        $(this).autoNumeric();
    });

    // MENU:
    $("ul.subnav").parent().append("<span></span>");
    $("ul.topnav li span").click(function() {
        $(this).parent().find("ul.subnav").slideDown('fast').show();
        $(this).parent().hover(
            function() {},
            function() { $(this).parent().find("ul.subnav").slideUp('slow'); }
        );
    }).hover(
        function() { $(this).addClass("subhover"); },
        function() { $(this).removeClass("subhover"); }
    );

    // NovaOs.aspx:
    $('form#frmNovaOs').each(function() {
        atualizarServicosFamilia('#familiaOD', true);
    }).validate({
        messages: {
            eixoOD: 'Valor máximo = 180'
        },
        onkeyup: false
    });

    $('select#familiaOD').change(function() {
        atualizarServicosFamilia('#familiaOD', false);
    });

    $('select#familiaOE').change(function() {
        atualizarServicosFamilia('#familiaOE', false);
    });

    // NovoPedido.aspx:
    $('form#frmNovoPedido').each(function() {
        atualizarProdutosFamilia(true);
    });

    $('select#selFiltroFamilia').change(function() {
        atualizarProdutosFamilia(false);
    });

    $('input#ibtAdicionar').click(function() {
        adicionarProduto();
    });

    $('a.linkExcluir').live('click', function() {
        $(this).parent().parent().remove();
        verificarListagemProdutos();
    });

    $('input.itxQtd').live('focusin', function() {
        $(this).parent()
            .css('background-color', '#ffffc0')
            .siblings().css('background-color', '#ffffc0');
    }).live('focusout', function() {
        $(this).parent()
            .css('background-color', 'window')
            .siblings().css('background-color', 'window');
    });

    $('table#tabItens td').live('click', function() {
        $(this)
            .css('background-color', '#ffffc0')
            .siblings()
                .css('background-color', '#ffffc0')
                .find('input.itxQtd').focus();
    });
});
