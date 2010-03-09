using System;
using System.Data;
using Dataweb.Dilab.Model.DataAccess;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.Ado.DataAccess
{
    public class ItemTransacaoDao : DataAccessBase<ItemTransacao>, IItemTransacaoDao
    {
        // TODO: [STP]
        // TODO: [Detalhe Transacao] Definir consulta dos itens (produtos/serviços) da transação (pedido/ordem de serviço).
        private const string SQL_STMT_FIND_ALL_BY_COD_EMPRESA_AND_COD_TRANSACAO = @"
            A DEFINIR
        ";

        private const string SQL_STMT_INSERT = @"
            EXECUTE PROCEDURE STP_WEBORDEMSERVICO_ADDITEM(
                @PCOD_EMPRESA,
                @PCOD_ORDEMSERVICO,
                @PCOD_ITEM,
                @PQUANTIDADE
            )";

        // TODO: [Detalhe Transacao] Definir campos da consulta dos itens da transação.
        public override ItemTransacao FetchDto(IDataRecord record)
        {
            return new ItemTransacao {
                CodEmpresa = Helper.ReadInt32(record, "").Value,
                CodTransacao = Helper.ReadInt32(record, "").Value,
                CodItem = Helper.ReadString(record, ""),
                Quantidade = Helper.ReadInt32(record, "").Value
            };
        }

        public override ItemTransacao[] FindAll()
        {
            throw new NotImplementedException();
        }

        public override ItemTransacao FindByPrimaryKey(object pk)
        {
            throw new NotImplementedException();
        }

        public ItemTransacao[] FindByTransacao(int codEmpresa, int codTransacao)
        {
            throw new NotImplementedException();
        }

        public override ItemTransacao Insert(ItemTransacao dto)
        {
            Helper.UsingCommand(Session.Connection, c =>
            {
                c.CommandText = SQL_STMT_INSERT;

                Helper.AddParameter(c, "@PCOD_EMPRESA", DbType.Int32, dto.CodEmpresa);
                Helper.AddParameter(c, "@PCOD_ORDEMSERVICO", DbType.Int32, dto.CodTransacao);
                Helper.AddParameter(c, "@PCOD_ITEM", DbType.String, dto.CodItem);
                Helper.AddParameter(c, "@PQUANTIDADE", DbType.Decimal, dto.Quantidade);

                c.ExecuteNonQuery();
            });

            return dto;
        }

        public ItemTransacao[] Insert(ItemTransacao[] dtos)
        {
            foreach (var dto in dtos)
            {
                Insert(dto);
            }

            return dtos;
        }

        public override ItemTransacao Update(ItemTransacao dto)
        {
            throw new NotImplementedException();
        }
    }
}
