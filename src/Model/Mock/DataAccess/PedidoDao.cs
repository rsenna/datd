using Base=Dataweb.Dilab.Model.DataAccess;

namespace Dataweb.Dilab.Model.Mock.DataAccess
{
    public class PedidoDao : Base.PedidoDao
    {
        public override string GetStmtFindAll()
        {
            return string.Empty;
        }

        public override string GetStmtFindAllByCodClienteAndCodNotaFiscal()
        {
            return string.Empty;
        }

        public override string GetStmtFindOneByCodEmpresaAndCodTransacao()
        {
            return string.Empty;
        }

        public override string GetStmtInsert()
        {
            return string.Empty;
        }

        public override string GetStmtCountFechadas()
        {
            return string.Empty;
        }

        public override string GetStmtCountEmProducao()
        {
            return string.Empty;
        }

        public override string GetStmtClose()
        {
            return string.Empty;
        }
    }
}