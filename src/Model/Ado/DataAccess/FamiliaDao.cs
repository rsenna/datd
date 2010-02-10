using System;
using System.Data;
using Dataweb.Dilab.Model.DataAccess;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.Ado.DataAccess
{
    public class FamiliaDao : DataAccessBase<Familia>, IFamiliaDao
    {
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
            throw new NotImplementedException();
        }

        public override void Update(Familia dto)
        {
            throw new NotImplementedException();
        }

        public Familia[] FindAll()
        {
            return FindAll(SQL_STMT_FIND_ALL);
        }
    }
}