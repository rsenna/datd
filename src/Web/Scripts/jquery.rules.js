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
                $('div#divMsgSemServicos').show();
                $('div#divSeparator').hide();
            } else {
                $('div#divMsgSemServicos').hide();

                // Mostra ou esconde separador entre divs:
                if ($(tags.thisDivServicos).is(':visible') && $(tags.thatDivServicos).is(':visible')) {
                    $('div#divSeparator').height(20);
                    $('div#divSeparator').show();
                } else {
                    $('div#divSeparator').hide();
                }
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
                            'servico' + tags.thisSelect.substring(tags.thisSelect.length, tags.thisSelect.length - 2) + key,
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
                .select()
                .parent()
                    .css('background-color', '#ffffc0')
                    .siblings().css('background-color', '#ffffc0');
        }
    }

    /* hacks para o IE:
        - IE não permite options com largura maior que o select - implementado comportamento "auto width" como solução
        - inputs com "width: auto" não respeitam limite de largura do parent
    */
    if ($.browser.msie) {
        // Correção da largura auto de inputs:
        $('input:text').each(function() {
            if ($(this).css('width') === 'auto') {
                var parentWidth = $(this).parent().width();
                $(this).width(parentWidth);
            }
        });
        
        // function to expand the selection box
        var expand = function() {
            var origWidthPx = $(this).width();

            // Duplica o select, para determinar se novo width seria maior que o atual:
            var tempSelect = $(this).clone().css('width', 'auto');
            $('body').append(tempSelect);
            var newWidthPx = tempSelect.width();
            tempSelect.remove();

            // Se select precisa aumentar, executa a mágica necessária:
            if (origWidthPx < newWidthPx) {
                // Traz o select atual para a frente:
                var zmax = 0;
                $(this).siblings().each(function() {
                    var zcur = $(this).css('z-index');
                    zmax = zcur > zmax? zcur : zmax;
                });
                $(this).css('z-index', zmax + 1);

                // capture the original width the first time round
                if ($(this).data('origWidth') == undefined) {
                    $(this).data('origWidth', $(this).css('width'));
                }

                // expand the select
                $(this)
                    .css('position', 'absolute')
                    .css('width', 'auto');
            }
        };

        // function to contract the selection box
        var contract = function() {
            // no hide workaround for IE6 to stop the element contracting when clicked
            if (!$(this).data('noHide')) {
                $(this)
                    .css('position', 'relative')
                    .css('width', $(this).data('origWidth'));

                // Apenas no IE7 ocorre um problema de "diminuição dos selects".
                // Segue aqui um "workaround" pra corrigir isso, nos selects que podem ser alvo do bug:
                if (gIeVersion == 7) {
                    $('select#selFiltroProduto').width(440);
                }
            }
        };

        // set the noHide workaround on focus and blur
        var focus = function() {
            $(this).data('noHide', true)
        };
        var blur = function() {
            $(this).data('noHide', false);
            contract.call(this)
        };

        gIeVersion = $.browser.version.substr(0, 1);

        switch (gIeVersion) {
            case 6:
                $('select.ieAutoWidth')
                    .hover(expand, contract)
                    .focus(focus)
                    .click(focus)
                    .blur(blur)
                    .change(blur);
                break;

            case 7:
            case 8:
            default:
                $('select.ieAutoWidth')
                    .mousedown(expand)
                    .change(contract)
                    .blur(contract);
                break;
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
    $('ul.subnav').parent().append('<span></span>');
    $('ul.topnav li span').click(function() {
        $(this).parent().find('ul.subnav').slideDown('fast').show();
        $(this).parent().hover(
            function() { },
            function() { $(this).parent().find('ul.subnav').slideUp('slow'); }
        );
    }).hover(
        function() { $(this).addClass('subhover'); },
        function() { $(this).removeClass('subhover'); }
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
        $(this)
            .autoNumeric()
            .parent()
                .css('background-color', '#ffffc0')
                .siblings().css('background-color', '#ffffc0');
    }).live('focusout', function() {
        $(this)
            .parent()
                .css('background-color', 'window')
                .siblings().css('background-color', 'window');
    });

    $('table#tabItens td').live('click', function() {
        $(this)
            .css('background-color', '#ffffc0')
            .siblings()
                .css('background-color', '#ffffc0')
                .find('input.itxQtd')
                    .focus()
                    .select();
    });
});
