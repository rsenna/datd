function atualizarServicosFamilia($selectTag, $force) {
    function Tags() {
        this.thisSelect = $selectTag;

        if (this.thisSelect == '#familiaOD') {
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

    function checkVisibility() {
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

    var tags = new Tags();
    $(tags.thisLabel).text($(tags.thisSelect + ' option:selected').text());

    if ($(tags.thisSelect).val() == '') {
        $(tags.thisDivServicos).hide();

        if ($force) {
            atualizarServicosFamilia(tags.thatSelect, false);
        } else {
            checkVisibility();
        }

    } else if ($(tags.thisSelect).val() == $(tags.thatSelect).val() && !$force) {
        $(tags.thisDivChecks).html($(tags.thatDivChecks).html());
        $(tags.thisDivServicos).hide();
        checkVisibility();

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

                if (options.length == 0) {
                    $(tags.thisDivServicos).hide();

                } else {
                    var thisDivChecks = $(tags.thisDivChecks);
                    thisDivChecks.empty();
                    thisDivChecks.append(options.join(''));

                    $(tags.thisDivServicos).show();
                }

                checkVisibility();
                
                if ($force) {
                    atualizarServicosFamilia(tags.thatSelect, false);
                }
            }
        );
    }
}

$(document).ready(function() {
    $('body').append('<div id="ajaxBusy"><p><img src="' + ajaxLoaderGifUrl + '"></p></div>');
    $('#ajaxBusy').css({display: 'none', margin: '0px', padding: '0px', position: 'absolute', left: '3px', top: '3px', width: 'auto'});

    $('input.numeric').each(function() {
        $(this).css('TEXT-ALIGN', 'right');
    });

    $('input.numeric').focus(function() {
        $(this).autoNumeric();
    });

    $('#formNovaOs').ready(function() {
        atualizarServicosFamilia('#familiaOD', true);
    });
    
    $('#formNovaOs').validate({
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
});

$(document).ajaxStart(function() {
    document.body.style.cursor = "wait";
    $('#ajaxBusy').show();
}).ajaxStop(function() {
    $('#ajaxBusy').hide();
    document.body.style.cursor = "default";
});
