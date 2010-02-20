using System;
using System.Data;
using Dataweb.Dilab.Model.DataAccess;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.Ado.DataAccess
{
    public class FamiliaDao : DataAccessBase<Familia>, IFamiliaDao
    {
        private const string SQL_STMT_FIND_BY_PRIMARY_KEY = @"
            SELECT
                 PRFA.cod_produtofamilia,
                 PRFA.descricao
            FROM
                produtofamilia PRFA
            WHERE
                PRFA.cod_produtofamilia = @PCOD_PRODUTOFAMILIA
        ";

        private const string SQL_STMT_FIND_ALL = @"
            SELECT
                 PRFA.cod_produtofamilia,
                 PRFA.descricao
            FROM
                produtofamilia PRFA
            WHERE
                PRFA.ativo = 'T'
            ORDER BY
                PRFA.descricao
        ";

        public override Familia FetchDto(IDataRecord reader)
        {
            return new Familia {
                CodFamilia = Helper.ReadInt32(reader, "cod_produtofamilia").Value,
                Descricao = Helper.ReadString(reader, "descricao")
            };
        }

        public override Familia FindByPrimaryKey(object pk)
        {
            Familia result = null;

            Helper.UsingCommand(c =>
            {
                c.CommandText = SQL_STMT_FIND_BY_PRIMARY_KEY;
                Helper.AddParameter(c, "@PCOD_PRODUTOFAMILIA", DbType.Int32, pk);
                result = FetchDto(c);
            });

            return result;
        }

        public override Familia Update(Familia dto)
        {
            throw new NotImplementedException();
        }

        public override Familia[] FindAll()
        {
            return FindAll(SQL_STMT_FIND_ALL);
        }

        public override Familia Insert(Familia dto)
        {
            throw new NotImplementedException();
        }
    }
}