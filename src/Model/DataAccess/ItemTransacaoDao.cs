using System;
using System.Collections.Generic;
using System.Data;
using Dataweb.Dilab.Model.DataAccess.Contracts;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.DataAccess
{
    public abstract class ItemTransacaoDao : DataAccessBase<ItemTransacao>, IItemTransacaoDao
    {
        public abstract string GetStmtFindAllByCodEmpresaAndCodTransacao();
        public abstract string GetStmtInsert();

        public override ItemTransacao InitDto(IReader reader, ItemTransacao dto)
        {
            dto.CodEmpresa = reader.ReadRequired<int>("cod_empresa");
            dto.CodTransacao = reader.ReadRequired<int>("cod_transacao");
            dto.CodItem = reader.ReadRequired("cod_item");
            dto.Quantidade = reader.ReadRequired<int>("quantidade");
            dto.Descricao = reader.ReadRequired("descricao");

            return dto;
        }

        public override IEnumerable<ItemTransacao> FindAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ItemTransacao> FindAll(int codEmpresa, int codTransacao)
        {
            var result = new List<ItemTransacao>();

            using (var c = Session.CreateCommand())
            {
                c.CommandText = GetStmtFindAllByCodEmpresaAndCodTransacao();

                c.AddParameter("@PCOD_EMPRESA", DbType.Int32, codEmpresa);
                c.AddParameter("@PCOD_TRANSACAO", DbType.Int32, codTransacao);

                InitDtos(c, result);
            }

            return result;
        }

        public override ItemTransacao FindByPrimaryKey(object pk)
        {
            throw new NotImplementedException();
        }

        public override ItemTransacao Insert(ItemTransacao dto)
        {
            using (var c = Session.CreateCommand())
            {
                c.CommandText = GetStmtInsert();

                c.AddParameter("@PCOD_EMPRESA", DbType.Int32, dto.CodEmpresa);
                c.AddParameter("@PCOD_ORDEMSERVICO", DbType.Int32, dto.CodTransacao);
                c.AddParameter("@PCOD_ITEM", DbType.String, dto.CodItem);
                c.AddParameter("@PQUANTIDADE", DbType.Decimal, dto.Quantidade);

                c.Execute();
            }

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