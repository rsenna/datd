using Base=Dataweb.Dilab.Model.DataAccess;

namespace Dataweb.Dilab.Model.Ado.DataAccess
{
    public class OrdemServicoLenteDao : Base.OrdemServicoLenteDao
    {
        // TODO: [STP]
        private const string SQL_STMT_FIND_ALL_BY_COD_EMPRESA_AND_COD_TRANSACAO = @"
            SELECT
                OSOL.cod_empresa,
                OSOL.cod_ordemservicootica,
                OSOL.cod_ordemservicooticalente,
                PROD.cod_produtofamilia,
                OSOL.descricaolente,
                OSOL.longe_esf,
                OSOL.longe_cil,
                OSOL.longe_eixo,
                OSOL.adicao,
                OSOL.perto_esf,
                OSOL.perto_cil,
                OSOL.perto_eixo,
                OSOL.dnp,
                OSOL.alt
            FROM
                otiordemservicootica_lente OSOL
                INNER JOIN produto PROD ON
                    PROD.cod_produto = OSOL.cod_produtolente
            WHERE
                OSOL.cod_empresa = @PCOD_EMPRESA AND
                OSOL.cod_ordemservicootica = @PCOD_ORDEMSERVICOOTICA AND
                OSOL.cod_ordemservicooticalente = @PCOD_ORDEMSERVICOOTICALENTE
        ";

        private const string SQL_STMT_INSERT = @"
            EXECUTE PROCEDURE STP_WEBORDEMSERVICO_ADDLENTE(
                @PCOD_EMPRESA,
                @PCOD_ORDEMSERVICO,
                @PCOD_ORDEMSERVICOOTICALENTE,
                @PDESCRICAOLENTE,
                @PLONGE_ESF,
                @PLONGE_CIL,
                @PLONGE_EIXO,
                @PADICAO,
                @PPERTO_ESF,
                @PPERTO_CIL,
                @PPERTO_EIXO,
                @PDNP,
                @PALT
            )";

        public override string GetStmtFindAllByCodEmpresaAndCodTransacao()
        {
            return SQL_STMT_FIND_ALL_BY_COD_EMPRESA_AND_COD_TRANSACAO;
        }

        public override string GetStmtInsert()
        {
            return SQL_STMT_INSERT;
        }
    }
}