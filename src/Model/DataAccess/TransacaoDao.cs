using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dataweb.Dilab.Model.DataAccess.Contracts;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.DataAccess
{
    public abstract class TransacaoDao<T> : DataAccessBase<T>, ITransacaoDao<T>
        where T : Transacao, new()
    {
        public abstract string GetStmtFindAll();
        public abstract string GetStmtFindAllByCodClienteAndCodNotaFiscal();
        public abstract string GetStmtFindOneByCodEmpresaAndCodTransacao();
        public abstract string GetStmtInsert();
        public abstract string GetStmtCountFechadas();
        public abstract string GetStmtCountEmProducao();
        public abstract string GetStmtClose();

        public override T InitDto(IReader reader, T dto)
        {
            dto.CodEmpresa = reader.ReadRequired<int>("RCODEMPRESA");
            dto.CodTransacao = reader.ReadRequired<int>("RCODTRANSACAO");
            dto.Numero = reader.ReadRequired<int>("RNUMEROORDEMSERVICO");
            dto.Referencia = reader.ReadOptional("RREFERENCIA");
            dto.Emissao = reader.ReadOptional<DateTime>("RDATAHORAEMISSAO");
            dto.Previsao = reader.ReadOptional<DateTime>("RDATAHORAPREVISAO");
            dto.Expedicao = reader.ReadOptional<DateTime>("RDATAHORAEXPEDICAO");
            dto.Etapa = reader.ReadRequired<TipoEtapa>("RCODETAPA");
            dto.AvisoMensagem = reader.ReadOptional("RAVISOMENSAGEM");
            dto.Tipo = reader.ReadRequired<TipoTransacao>("RTIPO");
            dto.Observacao = reader.ReadOptional("ROBSERVACAO");

            if (Depth > QueryDepth.FirstLevel)
            {
                var itemTransacaoDao = DataAccessFactory.CreateDao<IItemTransacaoDao>(this);
                dto.Itens = itemTransacaoDao.FindAll(dto.CodEmpresa, dto.CodTransacao).ToArray();
            }

            return dto;
        }

        public override IEnumerable<T> FindAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> FindAll(int codCliente)
        {
            var result = new List<T>();

            using (var c = Session.CreateCommand())
            {
                c.CommandText = GetStmtFindAll();
                c.AddParameter("@PCOD_CLIENTE", DbType.Int32, codCliente);
                InitDtos(c, result);
            }

            return result;
        }

        public IEnumerable<T> FindAll(int codCliente, int codNotaFiscal)
        {
            var result = new List<T>();

            using (var c = Session.CreateCommand())
            {
                c.CommandText = GetStmtFindAllByCodClienteAndCodNotaFiscal();
                c.AddParameter("@PCOD_CLIENTE", DbType.Int32, codCliente);
                c.AddParameter("@PCOD_NOTAFISCAL", DbType.Int32, codNotaFiscal);
                InitDtos(c, result);
            }

            return result;
        }

        public override T FindByPrimaryKey(object pk)
        {
            throw new NotImplementedException();
        }

        public virtual T FindByPrimaryKey(int codEmpresa, int codTransacao)
        {
            var result = new T();

            using (var c = Session.CreateCommand())
            {
                c.CommandText = GetStmtFindOneByCodEmpresaAndCodTransacao();
                c.AddParameter("@PCOD_EMPRESA", DbType.Int32, codEmpresa);
                c.AddParameter("@PCOD_TRANSACAO", DbType.Int32, codTransacao);
                result = InitDto(c, result);
            }

            return result;
        }

        protected virtual void PrepareParameters(ICommand c, T dto)
        {
            // Parâmetros abaixo só são utilizados para OS, portanto vão em branco:
            c.AddParameter("@PREFERENCIA", DbType.String, DBNull.Value);
            c.AddParameter("@PDESCRICAOARMACAO", DbType.String, DBNull.Value);
            c.AddParameter("@POBSERVACAOARMACAO", DbType.String, DBNull.Value);
            c.AddParameter("@PCOD_OTICALENTEMATERIAL", DbType.Int32, DBNull.Value);
            c.AddParameter("@PTIPOVT", DbType.Int32, (object) 0);
            c.AddParameter("@PTA", DbType.Decimal, DBNull.Value);
            c.AddParameter("@PMD", DbType.Decimal, DBNull.Value);
            c.AddParameter("@PDIAMETRO", DbType.Decimal, DBNull.Value);
            c.AddParameter("@POBSERVACAOLENTE", DbType.String, DBNull.Value);
            c.AddParameter("@PDP", DbType.Decimal, DBNull.Value);
            c.AddParameter("@PAA", DbType.Decimal, DBNull.Value);
            c.AddParameter("@PEIXO", DbType.Decimal, DBNull.Value);
            c.AddParameter("@PPONTE", DbType.Decimal, DBNull.Value);
        }

        public override T Insert(T dto)
        {
            using (var c = Session.CreateCommand())
            {
                c.CommandText = GetStmtInsert();

                // Entrada:
                c.AddParameter("@PCOD_CLIENTE", DbType.Int32, dto.CodCliente);
                c.AddParameter("@POBSERVACAO", DbType.String, dto.Observacao);
                c.AddParameter("@PREFERENCIA", DbType.String, dto.Referencia);

                PrepareParameters(c, dto);

                // Saída:
                c.AddParameter("@RCOD_EMPRESA", DbType.Int32, ParameterDirection.Output);
                c.AddParameter("@RCOD_ORDEMSERVICO", DbType.Int32, ParameterDirection.Output);
                c.AddParameter("@RNUMEROORDEMSERVICO", DbType.Int32, ParameterDirection.Output);

                c.Execute();

                dto.CodEmpresa = (int) c["@RCOD_EMPRESA"];
                dto.CodTransacao = (int) c["@RCOD_ORDEMSERVICO"];
                dto.Numero = (int) c["@RNUMEROORDEMSERVICO"];
            }

            if (dto.Itens != null)
            {
                foreach (var item in dto.Itens)
                {
                    item.CodTransacao = dto.CodTransacao;
                    item.CodEmpresa = dto.CodEmpresa;
                }

                // Grava os serviços que serão executados na OS:
                var itemTransacaoDao = DataAccessFactory.CreateDao<IItemTransacaoDao>(this);
                itemTransacaoDao.Insert(dto.Itens);
            }

            return dto;
        }

        public override T Update(T dto)
        {
            throw new NotImplementedException();
        }

        protected int ExecuteScalarInt32(string sqlStmt, int codCliente)
        {
            using (var c = Session.CreateCommand())
            {
                c.CommandText = sqlStmt;
                c.AddParameter("@PCOD_CLIENTE", DbType.Int32, codCliente);
                return Convert.ToInt32(c.ExecuteScalar());
            }
        }

        public virtual int GetCountFechadas(int codCliente)
        {
            return ExecuteScalarInt32(GetStmtCountFechadas(), codCliente);
        }

        public virtual int GetCountEmProducao(int codCliente)
        {
            return ExecuteScalarInt32(GetStmtCountEmProducao(), codCliente);
        }

        public T Close(T dto)
        {
            using (var c = Session.CreateCommand())
            {
                c.CommandText = GetStmtClose();

                // Entrada:
                c.AddParameter("@PCOD_EMPRESA", DbType.Int32, dto.CodCliente);
                c.AddParameter("@PCOD_ORDEMSERVICO", DbType.Int32, dto.CodTransacao);

                c.Execute();
            }

            return dto;
        }
    }
}