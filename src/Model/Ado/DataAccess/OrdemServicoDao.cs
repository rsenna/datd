using Base=Dataweb.Dilab.Model.DataAccess;

namespace Dataweb.Dilab.Model.Ado.DataAccess
{
    public class OrdemServicoDao : Base.OrdemServicoDao
    {
        public override string GetStmtFindAll()
        {
            return TransacaoDao.SQL_STMT_FIND_ALL;
        }

        public override string GetStmtFindAllByCodClienteAndCodNotaFiscal()
        {
            return TransacaoDao.SQL_STMT_FIND_ALL_BY_COD_CLIENTE_AND_COD_NOTA_FISCAL;
        }

        public override string GetStmtFindOneByCodEmpresaAndCodTransacao()
        {
            return TransacaoDao.SQL_STMT_FIND_ONE_BY_COD_EMPRESA_AND_COD_TRANSACAO;
        }

        public override string GetStmtInsert()
        {
            return TransacaoDao.SQL_STMT_INSERT;
        }

        public override string GetStmtCountFechadas()
        {
            return TransacaoDao.SQL_STMT_COUNT_FECHADAS;
        }

        public override string GetStmtCountEmProducao()
        {
            return TransacaoDao.SQL_STMT_COUNT_EM_PRODUCAO;
        }

        public override string GetStmtClose()
        {
            return TransacaoDao.SQL_STMT_CLOSE;
        }
    }
}
