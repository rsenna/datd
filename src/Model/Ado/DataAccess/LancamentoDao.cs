using Base=Dataweb.Dilab.Model.DataAccess;

namespace Dataweb.Dilab.Model.Ado.DataAccess
{
    public class LancamentoDao : Base.LancamentoDao
    {
        // TODO: Lancamento: Definir SQL_STMT_FIND_BY_PRIMARY_KEY
        private const string SQL_STMT_FIND_BY_PRIMARY_KEY = @"
            select
                RCOD_LANCAMENTO,
                RCOD_PESSOA,
                RCOD_FATURATRANSACAO,
                RNUMERODOCUMENTO,
                RDATAVENCIMENTO,
                RDATAPAGAMENTO,
                RTOTAL
            from
                STP_WEBCONSULTARLANCAMENTO(@PCOD_LANCAMENTO)
        ";

        private const string SQL_STMT_FIND_ALL_BY_COD_CLIENTE_AND_COD_FATURA = @"
            select
                RCOD_LANCAMENTO,
                RCOD_PESSOA,
                RCOD_FATURATRANSACAO,
                RNUMERODOCUMENTO,
                RDATAVENCIMENTO,
                RDATAPAGAMENTO,
                RTOTAL
            from
                STP_WEBCONSULTARLANCAMENTOS(@PCOD_CLIENTE, @PCOD_FATURA)
        ";

        private const string SQL_STMT_FIND_ALL_BY_COD_CLIENTE = @"
            select
                RCOD_LANCAMENTO,
                RCOD_PESSOA,
                RCOD_FATURATRANSACAO,
                RNUMERODOCUMENTO,
                RDATAVENCIMENTO,
                RDATAPAGAMENTO,
                RTOTAL
            from
                STP_WEBCONSULTARLANCAMENTOS(@PCOD_CLIENTE, NULL)
        ";

        public override string GetStmtFindAllByCodCliente()
        {
            return SQL_STMT_FIND_ALL_BY_COD_CLIENTE;
        }

        public override string GetStmtFindAllByCodClienteAndCodFatura()
        {
            return SQL_STMT_FIND_ALL_BY_COD_CLIENTE_AND_COD_FATURA;
        }

        public override string GetStmtFindByPrimaryKey()
        {
            return SQL_STMT_FIND_BY_PRIMARY_KEY;
        }
    }
}
