using System;
using System.Collections.Generic;
using Dataweb.Dilab.Model.DataAccess;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.Mock.DataAccess
{
    public class FaturaDao : DataAccessBase<Fatura>, IFaturaDao
    {
        public override Fatura InitDto(Fatura dto)
        {
            dto.CodFatura = GenerateInt32();
            dto.CodCliente = GenerateInt32();
            dto.Numero = GenerateInt32();
            dto.Data = GenerateDateTime(-5, 5);
            dto.Total = GenerateDecimal(20000);

            if (Depth > QueryDepth.FirstLevel)
            {
                var notaFiscalDao = new NotaFiscalDao {Depth = GetDetailDepth()};
                dto.NotasFiscais = notaFiscalDao.FindAll();

                var lancamentoDao = new LancamentoDao {Depth = GetDetailDepth()};
                dto.Lancamentos = lancamentoDao.FindAll();
            }

            return dto;
        }

        public IEnumerable<Fatura> FindAll(int codCliente)
        {
            return FindAll();
        }

        public override Fatura FindByPrimaryKey(object pk)
        {
            return FindByPrimaryKey(Convert.ToInt32(pk));
        }

        public Fatura FindByPrimaryKey(int codFatura)
        {
            return InitDto(new Fatura());
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
