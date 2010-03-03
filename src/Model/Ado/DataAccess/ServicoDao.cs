using System;
using System.Data;
using Dataweb.Dilab.Model.DataAccess;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.Ado.DataAccess
{
    public class ServicoDao : DataAccessBase<Servico>, IServicoDao
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

        public Servico[] FindAll(int codFamilia)
        {
            Servico[] result = null;

            Helper.UsingCommand(Connection, c => {
                c.CommandText = SQL_STMT_FIND_PRODUTO_SERVICO;
                Helper.AddParameter(c, "@COD_PRODUTOFAMILIA", DbType.Int32, codFamilia);
                result = FetchDtos(c);
            });

            return result;
        }

        public override Servico[] FindAll()
        {
            throw new NotImplementedException();
        }

        public override Servico FindByPrimaryKey(object pk)
        {
            throw new NotImplementedException();
        }

        public override Servico Update(Servico dto)
        {
            throw new NotImplementedException();
        }

        public override Servico Insert(Servico dto)
        {
            throw new NotImplementedException();
        }

        public override Servico FetchDto(IDataRecord record)
        {
            return new Servico {
                CodItem = Helper.ReadString(record, "cod_item"),
                Obrigatorio = Helper.ReadBoolean(record, "obrigatorio").Value,
                Descricao = Helper.ReadString(record, "descricao"),
                ServicoInterno = Helper.ReadBoolean(record, "servicointerno").Value
            };
        }
    }
}