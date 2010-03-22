using Base=Dataweb.Dilab.Model.DataAccess;

namespace Dataweb.Dilab.Model.Ado.DataAccess
{
    public class FaturaDao : Base.FaturaDao
    {
        private const string SQL_STMT_FIND_ALL_BY_COD_CLIENTE = @"
            select
                RCOD_FATURATRANSACAO,
                RCOD_PESSOA,
                RNUMEROFATURA,
                RDATAEMISSAO,
                RTOTAL
            from
                STP_WEBCONSULTARFATURAS(@PCOD_CLIENTE)
        ";

        private const string SQL_STMT_FIND_BY_PRIMARY_KEY = @"
            select
                RCOD_FATURATRANSACAO,
                RCOD_PESSOA,
                RNUMEROFATURA,
                RDATAEMISSAO,
                RTOTAL
            from
                STP_WEBCONSULTARFATURA(@PCOD_FATURA)
        ";

        public override string GetStmtFindAllByCodCliente()
        {
            return SQL_STMT_FIND_ALL_BY_COD_CLIENTE;
        }

        public override string GetStmtFindByPrimaryKey()
        {
            return SQL_STMT_FIND_BY_PRIMARY_KEY;
        }
    }
}