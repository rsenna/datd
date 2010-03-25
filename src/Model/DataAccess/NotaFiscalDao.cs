using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dataweb.Dilab.Model.DataAccess.Contracts;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.DataAccess
{
    public abstract class NotaFiscalDao : DataAccessBase<NotaFiscal>, INotaFiscalDao
    {
        public abstract string GetStmtFindAllByCodCliente();
        public abstract string GetStmtFindAllByCodClienteAndCodFatura();
        public abstract string GetStmtFindByPrimaryKey();

        public override NotaFiscal InitDto(IReader reader, NotaFiscal dto)
        {
            dto.CodNotaFiscal = reader.ReadRequired<int>("RCOD_NOTAFISCALEMITIDA");
            dto.CodCliente = reader.ReadRequired<int>("RCOD_PESSOA");
            dto.CodFatura = reader.ReadOptional<int>("RCOD_FATURATRANSACAO");
            dto.Numero = reader.ReadRequired<int>("RNUMERONOTAFISCAL");
            dto.Data = reader.ReadRequired<DateTime>("RDATAEMISSAO");
            dto.Total = reader.ReadRequired<decimal>("RTOTAL");
            dto.Nfe = reader.ReadRequired<bool>("RNFE");
            dto.NfeXml = reader.ReadOptional("RNFEXML");

            if (Depth > QueryDepth.FirstLevel)
            {
                // Lê as transações associadas à nota fiscal:
                var transacaoDao = DataAccessFactory.CreateDao<ITransacaoDao>(this);
                dto.Transacoes = transacaoDao.FindAll(dto.CodCliente, dto.CodNotaFiscal).ToArray();
            }

            return dto;
        }

        public override IEnumerable<NotaFiscal> FindAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<NotaFiscal> FindAll(int codCliente)
        {
            var result = new List<NotaFiscal>();

            using (var c = Session.CreateCommand())
            {
                c.CommandText = GetStmtFindAllByCodCliente();

                c.AddParameter("@PCOD_CLIENTE", DbType.Int32, codCliente);

                InitDtos(c, result);
            }

            return result;
        }

        public IEnumerable<NotaFiscal> FindAll(int codCliente, int codFatura)
        {
            var result = new List<NotaFiscal>();

            using (var c = Session.CreateCommand())
            {
                c.CommandText = GetStmtFindAllByCodClienteAndCodFatura();

                c.AddParameter("@PCOD_CLIENTE", DbType.Int32, codCliente);
                c.AddParameter("@PCOD_FATURA", DbType.Int32, codFatura);

                InitDtos(c, result);
            }

            return result;
        }

        public override NotaFiscal FindByPrimaryKey(object pk)
        {
            return FindByPrimaryKey(Convert.ToInt32(pk));
        }

        public NotaFiscal FindByPrimaryKey(int codNotaFiscal)
        {
            var result = new NotaFiscal();

            using (var c = Session.CreateCommand())
            {
                c.CommandText = GetStmtFindByPrimaryKey();
                c.AddParameter("@PCOD_NOTAFISCAL", DbType.Int32, codNotaFiscal);
                result = InitDto(c, result);
            }

            return result;
        }

        public override NotaFiscal Insert(NotaFiscal dto)
        {
            throw new NotImplementedException();
        }

        public override NotaFiscal Update(NotaFiscal dto)
        {
            throw new NotImplementedException();
        }
    }
}