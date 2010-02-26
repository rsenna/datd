using Dataweb.Dilab.Model.DataAccess;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.Mock.DataAccess
{
    public class PacoteCreditoDao : DataAccessBase<PacoteCredito>, IPacoteCreditoDao
    {
        public override PacoteCredito FetchDto()
        {
            return new PacoteCredito
            {
                CodPacoteCredito = GenerateCode(10),
                Descricao = GenerateName(2),
                Quantidade = GenerateInt32(100)
            };
        }

        public PacoteCredito FindByPrimaryKey(int codCliente, string codPacoteCliente)
        {
            return FetchDto();
        }

        public PacoteCredito[] FindAll(int codCliente)
        {
            return base.FindAll();
        }
    }
}