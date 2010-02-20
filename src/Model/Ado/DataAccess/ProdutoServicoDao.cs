using System;
using System.Data;
using Dataweb.Dilab.Model.DataAccess;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.Ado.DataAccess
{
    public class ProdutoServicoDao : DataAccessBase<ProdutoServico>, IProdutoServicoDao
    {
        private const string SQL_STMT_FIND_PRODUTO_SERVICO = @"
            SELECT
                ITEM.cod_item,
                PRFS.obrigatorio,
                ITEM.descricao,
                SERV.servicointerno
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

        public ProdutoServico[] FindAll(int codFamilia)
        {
            ProdutoServico[] result = null;

            Helper.UsingCommand(c => {
                c.CommandText = SQL_STMT_FIND_PRODUTO_SERVICO;
                Helper.AddParameter(c, "@COD_PRODUTOFAMILIA", DbType.Int32, codFamilia);
                result = FetchDtos(c);
            });

            return result;
        }

        public override ProdutoServico[] FindAll()
        {
            throw new NotImplementedException();
        }

        public override ProdutoServico FindByPrimaryKey(object pk)
        {
            throw new NotImplementedException();
        }

        public override ProdutoServico Update(ProdutoServico dto)
        {
            throw new NotImplementedException();
        }

        public override ProdutoServico Insert(ProdutoServico dto)
        {
            throw new NotImplementedException();
        }

        public override ProdutoServico FetchDto(IDataRecord record)
        {
            return new ProdutoServico {
                CodItem = Helper.ReadString(record, "cod_item"),
                Obrigatorio = Helper.ReadBoolean(record, "obrigatorio").Value,
                Descricao = Helper.ReadString(record, "descricao"),
                ServicoInterno = Helper.ReadBoolean(record, "servicointerno").Value
            };
        }
    }
}