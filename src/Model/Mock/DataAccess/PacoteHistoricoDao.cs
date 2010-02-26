using Dataweb.Dilab.Model.DataAccess;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.Mock.DataAccess
{
    public class PacoteHistoricoDao : DataAccessBase<PacoteHistorico>, IPacoteHistoricoDao
    {
        public override PacoteHistorico FetchDto()
        {
            return new PacoteHistorico
            {
                Data = GenerateDateTime(-10),
                Quantidade = GenerateInt32(100),
                Valor = GenerateDecimal(10000),
                Tipo = (TipoPacoteHistorico) GenerateInt32(1)
            };
        }

        public PacoteHistorico[] FindAll(int codCliente, string codPacoteCliente)
        {
            return base.FindAll();
        }
    }
}