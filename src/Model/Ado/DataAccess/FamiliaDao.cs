using System;
using System.Collections.Generic;
using System.Data;
using Dataweb.Dilab.Model.DataAccess;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.Ado.DataAccess
{
    public class FamiliaDao : DataAccessBase<Familia>, IFamiliaDao
    {
        // TODO: [STP]
        private const string SQL_STMT_FIND_BY_PRIMARY_KEY = @"
            SELECT
                PRFA.cod_produtofamilia,
                PRFA.descricao
            FROM
                produtofamilia PRFA
            WHERE
                PRFA.cod_produtofamilia = @PCOD_PRODUTOFAMILIA
        ";

        // TODO: [STP]
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

        public override Familia InitDto(IDataRecord reader, Familia dto)
        {
            dto.CodFamilia = Helper.ReadInt32(reader, "cod_produtofamilia").Value;
            dto.Descricao = Helper.ReadString(reader, "descricao");

            return dto;
        }

        public override Familia FindByPrimaryKey(object pk)
        {
            var result = new Familia();

            Helper.UsingCommand(Session.Connection, c => {
                c.CommandText = SQL_STMT_FIND_BY_PRIMARY_KEY;
                Helper.AddParameter(c, "@PCOD_PRODUTOFAMILIA", DbType.Int32, pk);
                result = InitDto(c, result);
            });

            return result;
        }

        public override Familia Update(Familia dto)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<Familia> FindAll()
        {
            return FindAll(SQL_STMT_FIND_ALL);
        }

        public override Familia Insert(Familia dto)
        {
            throw new NotImplementedException();
        }
    }
}