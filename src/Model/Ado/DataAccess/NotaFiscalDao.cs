using System;
using Base=Dataweb.Dilab.Model.DataAccess;

namespace Dataweb.Dilab.Model.Ado.DataAccess
{
    public class NotaFiscalDao : Base.NotaFiscalDao
    {
        // TODO: NotaFiscal: Definir SQL_STMT_FIND_ALL_BY_COD_CLIENTE
        private const string SQL_STMT_FIND_ALL_BY_COD_CLIENTE = "TODO";
        // TODO: NotaFiscal: Definir SQL_STMT_FIND_ALL_BY_COD_CLIENTE_AND_COD_FATURA
        private const string SQL_STMT_FIND_ALL_BY_COD_CLIENTE_AND_COD_FATURA = "TODO";
        // TODO: NotaFiscal: Definir SQL_STMT_FIND_BY_PRIMARY_KEY
        private const string SQL_STMT_FIND_BY_PRIMARY_KEY = "TODO";

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

        // TODO: NotaFiscal: Definir como buscar XML
        public override string GetXml(int codNotaFiscal)
        {
            throw new NotImplementedException();
        }
    }
}