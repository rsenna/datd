using Base=Dataweb.Dilab.Model.DataAccess;

namespace Dataweb.Dilab.Model.Ado.DataAccess
{
    public class ItemTransacaoDao : Base.ItemTransacaoDao
    {
        // TODO: [STP]
        private const string SQL_STMT_FIND_ALL_BY_COD_EMPRESA_AND_COD_TRANSACAO = @"
            SELECT
                TRAI.cod_empresa,
                TRAI.cod_transacao,
                TRAI.cod_item,
                TRAI.quantidade,
                ITEM.descricao
            FROM
                transacao_item TRAI
                INNER JOIN item ITEM ON
                    ITEM.cod_item = TRAI.cod_item
            WHERE
                TRAI.cod_empresa = @PCOD_EMPRESA AND
                TRAI.cod_transacao = @PCOD_TRANSACAO
        ";

        private const string SQL_STMT_INSERT = @"
            EXECUTE PROCEDURE STP_WEBORDEMSERVICO_ADDITEM(
                @PCOD_EMPRESA,
                @PCOD_ORDEMSERVICO,
                @PCOD_ITEM,
                @PQUANTIDADE
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
