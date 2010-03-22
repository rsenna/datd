using Base=Dataweb.Dilab.Model.DataAccess;

namespace Dataweb.Dilab.Model.Ado.DataAccess
{
    public class PacoteHistoricoDao : Base.PacoteHistoricoDao
    {
        private const string SQL_STMT_FIND_BY_COD_CLIENTE_AND_COD_PACOTE = @"
            SELECT
                RDATA,
                RNUMEROORDEMSERVICO,
                RQUANTIDADE,
                RTOTALCOMPRA,
                RTIPO
            FROM
                STP_WEBCONSULTARPACOTE(@PCODCLIENTE, @PCODPACOTE)
        ";

        public override string GetStmtFindByCodClienteAndCodPacote()
        {
            return SQL_STMT_FIND_BY_COD_CLIENTE_AND_COD_PACOTE;
        }
    }
}