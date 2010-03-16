using System.Collections.Generic;
using Dataweb.Dilab.Model.DataAccess;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.Mock.DataAccess
{
    public class PacoteCreditoDao : DataAccessBase<PacoteCredito>, IPacoteCreditoDao
    {
        public override PacoteCredito InitDto(PacoteCredito dto)
        {
            dto.CodPacoteCredito = GenerateCode(10);
            dto.Descricao = GenerateName(2);
            dto.Quantidade = GenerateInt32(100);

            return dto;
        }

        public PacoteCredito FindByPrimaryKey(int codCliente, string codPacoteCliente)
        {
            return InitDto(new PacoteCredito());
        }

        public IEnumerable<PacoteCredito> FindAll(int codCliente)
        {
            return base.FindAll();
        }
    }
}