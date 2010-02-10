function atualizarServicosFamilia($selectTag) {
    function checkVisibility() {
        if ($($otherDivServicosTag).is(':hidden')) {
            $otherSelectedVal = $($otherSelectTag).val();

            if ($otherSelectedVal != '' && $otherSelectedVal != $($selectTag).val()) {
                $($otherDivServicosTag).show();
            }
        }

        if ($($divServicosTag).is(':hidden') && $($otherDivServicosTag).is(':hidden')) {
            $('#divMsgSemServicos').show();
        } else {
            $('#divMsgSemServicos').hide();
        }
    }

    if ($selectTag == '#familiaOD') {
        $divServicosTag = '#divServicosOD';
        $divChecksTag = '#divServicosODChecks';
        $labelTag = '#lbFamiliaOD';
        $otherSelectTag = '#familiaOE';
        $otherDivServicosTag = '#divServicosOE';
        $otherDivChecksTag = '#divServicosOEChecks';
    } else {
        $divServicosTag = '#divServicosOE';
        $divChecksTag = '#divServicosOEChecks';
        $labelTag = '#lbFamiliaOE';
        $otherSelectTag = '#familiaOD';
        $otherDivServicosTag = '#divServicosOD';
        $otherDivChecksTag = '#divServicosODChecks';
    }

    $($labelTag).text($($selectTag + ' option:selected').text());

    if ($($selectTag).val() == '') {
        $($divServicosTag).hide();
        checkVisibility();

    } else if ($($selectTag).val() == $($otherSelectTag).val()) {
        $($divChecksTag).html($($otherDivChecksTag).html());
        $($divServicosTag).hide();
        checkVisibility();

    } else {
        $.getJSON(
            '/Os/ListarServicos', { familia: $($selectTag).val() },
            function(json) {
                options = [];

                $.each(json, function(key, value) {
                    $checkbox = $.validator.format(
                        '<input type="checkbox" name="servicos" id="{0}" value="{1}"/><label for="{0}">{2}</label><br/>',
                        'servico' + key,
                        value.CodItem,
                        value.Descricao
                    );
                    options.push($checkbox);
                });

                if (options.length == 0) {
                    $($divServicosTag).hide();

                } else {
                    $divChecks = $($divChecksTag);
                    $divChecks.empty();
                    $divChecks.append(options.join(''));

                    $($divServicosTag).show();
                }

                checkVisibility();
            }
        );
    }
}

$(document).ready(function() {
    $('input.numeric').each(function() {
        $(this).css('TEXT-ALIGN', 'right');
    });

    $('input.numeric').focus(function() {
        $(this).autoNumeric();
    });

    $('#formNovaOs').ready(function() {
        $('#divMsgSemServicos').show();
        $('#divServicosOD').hide();
        $('#divServicosOE').hide();
    });
    
    $('#formNovaOs').validate({
        messages: {
            eixoOD: 'Valor máximo = 180'
        },
        onkeyup: false
    });

    $('select#familiaOD').change(function() {
        atualizarServicosFamilia('#familiaOD');
    });

    $('select#familiaOE').change(function() {
        atualizarServicosFamilia('#familiaOE');
    });
});
