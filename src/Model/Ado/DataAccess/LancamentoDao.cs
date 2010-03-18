using System;
using System.Collections.Generic;
using System.Data;
using Dataweb.Dilab.Model.DataAccess;
using Dataweb.Dilab.Model.DataTransfer;
using Dataweb.Dilab.Model.Ado;

namespace Dataweb.Dilab.Model.Ado.DataAccess
{
    public class LancamentoDao : DataAccessBase<Lancamento>, ILancamentoDao
    {
        public override Lancamento InitDto(IDataRecord reader, Lancamento dto)
        {
            /*
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
            */

            return dto;
        }

        public override IEnumerable<Lancamento> FindAll()
        {
            throw new NotImplementedException();
        }

        // TODO: ---
        public IEnumerable<Lancamento> FindAll(int codCliente)
        {
            throw new NotImplementedException();
        }

        // TODO: ---
        public IEnumerable<Lancamento> FindAll(int codCliente, int codFatura)
        {
            throw new NotImplementedException();
        }

        public override Lancamento FindByPrimaryKey(object pk)
        {
            return FindByPrimaryKey(Convert.ToInt32(pk));
        }

        // TODO: ---
        public Lancamento FindByPrimaryKey(int codLancamento)
        {
            throw new NotImplementedException();
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
