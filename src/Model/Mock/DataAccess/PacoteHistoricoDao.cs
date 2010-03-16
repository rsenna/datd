using System.Collections.Generic;
using Dataweb.Dilab.Model.DataAccess;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.Mock.DataAccess
{
    public class PacoteHistoricoDao : DataAccessBase<PacoteHistorico>, IPacoteHistoricoDao
    {
        public override PacoteHistorico InitDto(PacoteHistorico dto)
        {
            dto.Data = GenerateDateTime(-10);
            dto.NumeroOS = GenerateInt32();
            dto.Quantidade = GenerateInt32(100);
            dto.Valor = GenerateDecimal(10000);
            dto.Tipo = (TipoPacoteHistorico)GenerateInt32(1, 2);

            return dto;
        }

        public IEnumerable<PacoteHistorico> FindAll(int codCliente, string codPacoteCliente)
        {
            return base.FindAll();
        }
    }
}