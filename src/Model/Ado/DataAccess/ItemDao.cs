using System;
using System.Collections.Generic;
using System.Data;
using Dataweb.Dilab.Model.DataAccess;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.Ado.DataAccess
{
    public class ItemDao : DataAccessBase<Item>, IItemDao
    {
        // TODO: Como não usa mais tabela estoque, não dá para verificar se estoque.cod_estoquelocal = 1 - verificar se está tudo bem desta forma.
        // TODO: [STP]
        private const string SQL_STMT_FIND_ALL_PRODUTO_BY_COD_FAMILIA = @"
            SELECT
                ITEM.cod_item,
                PROD.codigobarra,
                'F' AS obrigatorio,
                ITEM.descricao,
                'N' AS servicointerno,
                1 AS tipo
            FROM
                item ITEM
                INNER JOIN produto PROD ON
                    ITEM.cod_item = PROD.cod_produto
                INNER JOIN produtofamilia PRFA ON
                    PRFA.cod_produtofamilia = PROD.cod_produtofamilia
            WHERE
                PRFA.cod_produtofamilia = @COD_PRODUTOFAMILIA AND
                ITEM.ativo = 'T'
            ORDER BY
                ITEM.descricao
        ";

        // TODO: [STP]
        private const string SQL_STMT_FIND_ALL_SERVICO_BY_COD_FAMILIA = @"
            SELECT
                ITEM.cod_item,
                NULL AS codigobarra,
                PRFS.obrigatorio,
                ITEM.descricao,
                SERV.servicointerno,
                2 AS tipo
            FROM
                produtofamilia_servico PRFS
                INNER JOIN servico SERV ON
                    SERV.cod_servico = PRFS.cod_servico
                INNER JOIN item ITEM ON
                    ITEM.cod_item = SERV.cod_servico
            WHERE
                PRFS.cod_produtofamilia = @COD_PRODUTOFAMILIA
            ORDER BY
                ITEM.descricao
        ";

        public override Item InitDto(IDataRecord record, Item result)
        {
            result.CodItem = Helper.ReadString(record, "cod_item");
            result.CodBarra = Helper.ReadString(record, "codigobarra");
            result.Obrigatorio = Helper.ReadBoolean(record, "obrigatorio").Value;
            result.Descricao = Helper.ReadString(record, "descricao");
            result.ServicoInterno = Helper.ReadBoolean(record, "servicointerno").Value;
            result.Tipo = (TipoItem) Helper.ReadInt32(record, "tipo").Value;

            return result;
        }

        public override IEnumerable<Item> FindAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Item> FindAll(int codFamilia, TipoItem tipo)
        {
            var result = new List<Item>();
            var proc = tipo == TipoItem.Produto ? SQL_STMT_FIND_ALL_PRODUTO_BY_COD_FAMILIA : SQL_STMT_FIND_ALL_SERVICO_BY_COD_FAMILIA;

            Helper.UsingCommand(Session.Connection, c => {
                c.CommandText = proc;
                Helper.AddParameter(c, "@COD_PRODUTOFAMILIA", DbType.Int32, codFamilia);
                InitDtos(c, result);
            });

            return result;
        }

        public override Item FindByPrimaryKey(object pk)
        {
            throw new NotImplementedException();
        }

        public override Item Update(Item dto)
        {
            throw new NotImplementedException();
        }

        public override Item Insert(Item dto)
        {
            throw new NotImplementedException();
        }
    }
}