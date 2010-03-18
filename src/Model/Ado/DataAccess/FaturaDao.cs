using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dataweb.Dilab.Model.DataAccess;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.Ado.DataAccess
{
    // TODO: Implementar classe de fatura
    public class FaturaDao : DataAccessBase<Fatura>, IFaturaDao
    {
        public override Fatura InitDto(IDataRecord record, Fatura dto)
        {
            /*
            dto.CodFatura = GenerateInt32();
            dto.CodCliente = GenerateInt32();
            dto.Numero = GenerateInt32();
            dto.Data = GenerateDateTime(-5, 5);
            dto.Total = GenerateDecimal(20000);
             */

            if (Depth > QueryDepth.FirstLevel)
            {
                var notaFiscalDao = new NotaFiscalDao {Depth = GetDetailDepth()};
                dto.NotasFiscais = notaFiscalDao.FindAll(dto.CodCliente, dto.CodFatura).ToArray();

                var lancamentoDao = new LancamentoDao {Depth = GetDetailDepth()};
                dto.Lancamentos = lancamentoDao.FindAll(dto.CodCliente, dto.CodFatura).ToArray();
            }

            return dto;
        }

        public override IEnumerable<Fatura> FindAll()
        {
            throw new NotImplementedException();
        }

        // TODO: implementação
        public IEnumerable<Fatura> FindAll(int codCliente)
        {
            throw new NotImplementedException();
        }

        public override Fatura FindByPrimaryKey(object pk)
        {
            return FindByPrimaryKey(Convert.ToInt32(pk));
        }

        // TODO: implementação
        public Fatura FindByPrimaryKey(int codFatura)
        {
            throw new NotImplementedException();
        }

        public override Fatura Insert(Fatura dto)
        {
            throw new NotImplementedException();
        }

        public override Fatura Update(Fatura dto)
        {
            throw new NotImplementedException();
        }
    }
}