using System;
using System.Collections.Generic;
using System.Data;
using Dataweb.Dilab.Model.DataAccess;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.Ado.DataAccess
{
    public class MaterialDao : DataAccessBase<Material>, IMaterialDao
    {
        // TODO: [STP]
        private const string SQL_STMT_FIND_ALL = @"
            SELECT
                cod_oticalentematerial,
                descricao
            FROM
                oticalentematerial
            ORDER BY
                descricao
        ";

        public override Material InitDto(IDataRecord reader, Material dto)
        {
            dto.CodMaterial = Helper.ReadInt32(reader, "cod_oticalentematerial").Value;
            dto.Descricao = Helper.ReadString(reader, "descricao");

            return dto;
        }

        public override Material FindByPrimaryKey(object pk)
        {
            throw new NotImplementedException();
        }

        public override Material Update(Material dto)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<Material> FindAll()
        {
            return FindAll(SQL_STMT_FIND_ALL);
        }

        public override Material Insert(Material dto)
        {
            throw new NotImplementedException();
        }
    }
}