using System;
using System.Collections.Generic;
using System.Data;
using Dataweb.Dilab.Model.DataAccess.Contracts;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.DataAccess
{
    public abstract class LancamentoDao : DataAccessBase<Lancamento>, ILancamentoDao
    {
        public abstract string GetStmtFindAllByCodCliente();
        public abstract string GetStmtFindAllByCodClienteAndCodFatura();
        public abstract string GetStmtFindByPrimaryKey();

        public override Lancamento InitDto(IReader reader, Lancamento dto)
        {
            dto.CodLancamento = reader.ReadRequired<int>("RCOD_LANCAMENTO");
            dto.CodCliente = reader.ReadRequired<int>("RCOD_PESSOA");
            dto.CodFatura = reader.ReadOptional<int>("RCOD_FATURATRANSACAO");
            dto.Documento = reader.ReadRequired<string>("RNUMERODOCUMENTO");
            dto.Vencimento = reader.ReadRequired<DateTime>("RDATAVENCIMENTO");
            dto.Pagamento = reader.ReadOptional<DateTime>("RDATAPAGAMENTO");
            dto.Total = reader.ReadRequired<decimal>("RTOTAL");

            return dto;
        }

        public override IEnumerable<Lancamento> FindAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Lancamento> FindAll(int codCliente)
        {
            var result = new List<Lancamento>();

            using (var c = Session.CreateCommand())
            {
                c.CommandText = GetStmtFindAllByCodCliente();

                c.AddParameter("@PCOD_CLIENTE", DbType.Int32, codCliente);

                InitDtos(c, result);
            }

            return result;
        }

        public IEnumerable<Lancamento> FindAll(int codCliente, int codFatura)
        {
            var result = new List<Lancamento>();

            using (var c = Session.CreateCommand())
            {
                c.CommandText = GetStmtFindAllByCodClienteAndCodFatura();

                c.AddParameter("@PCOD_CLIENTE", DbType.Int32, codCliente);
                c.AddParameter("@PCOD_FATURA", DbType.Int32, codFatura);

                InitDtos(c, result);
            }

            return result;
        }

        public override Lancamento FindByPrimaryKey(object pk)
        {
            return FindByPrimaryKey(Convert.ToInt32(pk));
        }

        public Lancamento FindByPrimaryKey(int codLancamento)
        {
            var result = new Lancamento();

            using (var c = Session.CreateCommand())
            {
                c.CommandText = GetStmtFindByPrimaryKey();
                c.AddParameter("@PCOD_LANCAMENTO", DbType.Int32, codLancamento);
                result = InitDto(c, result);
            }

            return result;
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