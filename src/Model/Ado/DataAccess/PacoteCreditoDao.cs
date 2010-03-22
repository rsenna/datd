using Base=Dataweb.Dilab.Model.DataAccess;

namespace Dataweb.Dilab.Model.Ado.DataAccess
{
    public class PacoteCreditoDao : Base.PacoteCreditoDao
    {
        // TODO: [STP]
        private const string SQL_STMT_FIND_BY_PRIMARY_KEY = @"
            SELECT
                PACC.cod_pacote AS rcodpacote,
                PACC.quantidade AS rquantidadedisponivel,
                item.descricao AS rdescricao
            FROM
                pacote_cliente PACC
                INNER JOIN item ON
                    item.cod_item = PACC.cod_pacote
            WHERE
                PACC.cod_cliente = @PCOD_CLIENTE
                AND item.cod_item = @PCOD_PACOTE
        ";

        private const string SQL_STMT_FIND_BY_COD_CLIENTE = @"
            SELECT
                rcodpacote,
                rquantidadedisponivel,
                rdescricao
            FROM
                STP_WEBCONSULTARPACOTES(@PCOD_CLIENTE)
        ";

        public override string GetStmtFindByCodCliente()
        {
            return SQL_STMT_FIND_BY_COD_CLIENTE;
        }

        public override string GetStmtFindByPrimaryKey()
        {
            return SQL_STMT_FIND_BY_PRIMARY_KEY;
        }
    }
}