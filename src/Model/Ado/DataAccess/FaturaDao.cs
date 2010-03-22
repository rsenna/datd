using Base=Dataweb.Dilab.Model.DataAccess;

namespace Dataweb.Dilab.Model.Ado.DataAccess
{
    public class FaturaDao : Base.FaturaDao
    {
        // TODO: Fatura: Definir SQL_STMT_FIND_ALL_BY_COD_CLIENTE
        private const string SQL_STMT_FIND_ALL_BY_COD_CLIENTE = "TODO";
        // TODO: Fatura: Definir SQL_STMT_FIND_BY_PRIMARY_KEY
        private const string SQL_STMT_FIND_BY_PRIMARY_KEY = "TODO";

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