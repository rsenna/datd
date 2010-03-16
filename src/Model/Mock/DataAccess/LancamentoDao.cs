using System;
using System.Collections.Generic;
using Dataweb.Dilab.Model.DataAccess;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.Mock.DataAccess
{
    public class LancamentoDao : DataAccessBase<Lancamento>, ILancamentoDao
    {
        public override Lancamento InitDto(Lancamento dto)
        {
            dto.CodEmpresa = GenerateInt32();
            dto.CodLancamento = GenerateInt32();
            dto.CodCliente = GenerateInt32();
            dto.CodFatura = GenerateInt32();
            dto.Numero = GenerateInt32();
            dto.Vencimento = GenerateDateTime(-5, 5);

            if (GenerateBoolean())
            {
                dto.Pagamento = GenerateDateTime(-5);
            }

            dto.Total = GenerateDecimal(20000);

            return dto;
        }

        public IEnumerable<Lancamento> FindAll(int codEmpresa)
        {
            return FindAll();
        }

        public override Lancamento FindByPrimaryKey(object pk)
        {
            throw new NotImplementedException();
        }

        public Lancamento FindByPrimaryKey(int codEmpresa, int codLancamento)
        {
            return InitDto(new Lancamento());
        }

        public override Lancamento Insert(Lancamento dto)
        {
            throw new NotImplementedException();
        }

        public override Lancamento Update(Lancamento dto)
        {
            throw new NotImplementedException();
        }
    }
}
