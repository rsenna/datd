using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dataweb.Dilab.Model.DataAccess.Contracts;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.DataAccess
{
    public abstract class FaturaDao : DataAccessBase<Fatura>, IFaturaDao
    {
        public abstract string GetStmtFindAllByCodCliente();
        public abstract string GetStmtFindByPrimaryKey();

        public override Fatura InitDto(IReader reader, Fatura dto)
        {
            // TODO: Fatura: Definir campos a serem lidos.
            dto.CodFatura = reader.ReadRequired<int>("???");
            dto.CodCliente = reader.ReadRequired<int>("???");
            dto.Numero = reader.ReadRequired<int>("???");
            dto.Data = reader.ReadRequired<DateTime>("???");
            dto.Total = reader.ReadRequired<decimal>("???");

            if (Depth > QueryDepth.FirstLevel)
            {
                var notaFiscalDao = DataAccessFactory.CreateDao<INotaFiscalDao>(this);
                dto.NotasFiscais = notaFiscalDao.FindAll(dto.CodCliente, dto.CodFatura).ToArray();

                var lancamentoDao = DataAccessFactory.CreateDao<ILancamentoDao>(this);
                dto.Lancamentos = lancamentoDao.FindAll(dto.CodCliente, dto.CodFatura).ToArray();
            }

            return dto;
        }

        public override IEnumerable<Fatura> FindAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Fatura> FindAll(int codCliente)
        {
            var result = new List<Fatura>();

            using (var c = Session.CreateCommand())
            {
                c.CommandText = GetStmtFindAllByCodCliente();
                c.AddParameter("@PCOD_CLIENTE", DbType.Int32, codCliente);
                InitDtos(c, result);
            }

            return result;
        }

        public override Fatura FindByPrimaryKey(object pk)
        {
            return FindByPrimaryKey(Convert.ToInt32(pk));
        }

        public Fatura FindByPrimaryKey(int codFatura)
        {
            var result = new Fatura();

            using (var c = Session.CreateCommand())
            {
                c.CommandText = GetStmtFindByPrimaryKey();
                c.AddParameter("@PCOD_FATURA", DbType.Int32, codFatura);
                result = InitDto(c, result);
            }

            return result;
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