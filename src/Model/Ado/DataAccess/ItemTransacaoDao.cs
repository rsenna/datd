using System;
using System.Collections.Generic;
using System.Data;
using Dataweb.Dilab.Model.DataAccess;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.Ado.DataAccess
{
    public class ItemTransacaoDao : DataAccessBase<ItemTransacao>, IItemTransacaoDao
    {
        // TODO: [STP]
        private const string SQL_STMT_FIND_ALL_BY_COD_EMPRESA_AND_COD_TRANSACAO = @"
            SELECT
                TRAI.cod_empresa,
                TRAI.cod_transacao,
                TRAI.cod_item,
                TRAI.quantidade,
                ITEM.descricao
            FROM
                transacao_item TRAI
                INNER JOIN item ITEM ON
                    ITEM.cod_item = TRAI.cod_item
            WHERE
                TRAI.cod_empresa = @PCOD_EMPRESA AND
                TRAI.cod_transacao = @PCOD_TRANSACAO
        ";

        private const string SQL_STMT_INSERT = @"
            EXECUTE PROCEDURE STP_WEBORDEMSERVICO_ADDITEM(
                @PCOD_EMPRESA,
                @PCOD_ORDEMSERVICO,
                @PCOD_ITEM,
                @PQUANTIDADE
            )";

        public override ItemTransacao InitDto(IDataRecord record, ItemTransacao dto)
        {
            dto.CodEmpresa = Helper.ReadInt32(record, "cod_empresa").Value;
            dto.CodTransacao = Helper.ReadInt32(record, "cod_transacao").Value;
            dto.CodItem = Helper.ReadString(record, "cod_item");
            dto.Quantidade = Helper.ReadInt32(record, "quantidade").Value;
            dto.Descricao = Helper.ReadString(record, "descricao");

            return dto;
        }

        public override IEnumerable<ItemTransacao> FindAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ItemTransacao> FindAll(int codEmpresa, int codTransacao)
        {
            var result = new List<ItemTransacao>();

            Helper.UsingCommand(Session.Connection, c => {
                c.CommandText = SQL_STMT_FIND_ALL_BY_COD_EMPRESA_AND_COD_TRANSACAO;

                Helper.AddParameter(c, "@PCOD_EMPRESA", DbType.Int32, codEmpresa);
                Helper.AddParameter(c, "@PCOD_TRANSACAO", DbType.Int32, codTransacao);

                InitDtos(c, result);
            });

            return result;
        }

        public override ItemTransacao FindByPrimaryKey(object pk)
        {
            throw new NotImplementedException();
        }

        public override ItemTransacao Insert(ItemTransacao dto)
        {
            Helper.UsingCommand(Session.Connection, c => {
                c.CommandText = SQL_STMT_INSERT;

                Helper.AddParameter(c, "@PCOD_EMPRESA", DbType.Int32, dto.CodEmpresa);
                Helper.AddParameter(c, "@PCOD_ORDEMSERVICO", DbType.Int32, dto.CodTransacao);
                Helper.AddParameter(c, "@PCOD_ITEM", DbType.String, dto.CodItem);
                Helper.AddParameter(c, "@PQUANTIDADE", DbType.Decimal, dto.Quantidade);

                c.ExecuteNonQuery();
            });

            return dto;
        }

        public IEnumerable<ItemTransacao> Insert(IEnumerable<ItemTransacao> dtos)
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
