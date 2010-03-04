using System;
using System.Data;
using Dataweb.Dilab.Model.DataAccess;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.Ado.DataAccess
{
    public class ProdutoDao : DataAccessBase<Produto>, IProdutoDao
    {
        // NOTE: Como não usa mais tabela estoque, não dá para verificar se estoque.cod_estoquelocal = 1 - verificar se está tudo bem desta forma.
        private const string SQL_STMT_FIND_ALL_BY_COD_FAMILIA = @"
            SELECT
                item.cod_item,
                produto.codigobarra,
                item.descricao
            FROM
                item
                JOIN produto ON (item.cod_item = produto.cod_produto)
                JOIN produtofamilia ON (produtofamilia.cod_produtofamilia = produto.cod_produtofamilia)
            WHERE
                produtofamilia.cod_produtofamilia = @COD_FAMILIA AND
                item.ativo = 'T'
            ORDER BY
                3
        ";

        public override Produto  FetchDto(IDataRecord record)
        {
            return new Produto {
                CodItem = Helper.ReadString(record, "cod_item"),
                CodBarra = Helper.ReadString(record, "codigobarra"),
                Descricao = Helper.ReadString(record, "descricao")
            };
        }

        public override Produto[] FindAll()
        {
            throw new NotImplementedException();
        }

        public Produto[] FindAll(int codFamilia)
        {
            Produto[] result = null;

            Helper.UsingCommand(Session.Connection, c => {
                c.CommandText = SQL_STMT_FIND_ALL_BY_COD_FAMILIA;
                Helper.AddParameter(c, "@COD_FAMILIA", DbType.Int32, codFamilia);
                result = FetchDtos(c);
            });

            return result;
        }

        public override Produto FindByPrimaryKey(object pk)
        {
            throw new NotImplementedException();
        }

        public override Produto Update(Produto dto)
        {
            throw new NotImplementedException();
        }

        public override Produto Insert(Produto dto)
        {
            throw new NotImplementedException();
        }
    }
}