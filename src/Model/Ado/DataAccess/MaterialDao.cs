using System;
using System.Data;
using Dataweb.Dilab.Model.DataAccess;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.Ado.DataAccess
{
    public class MaterialDao : DataAccessBase<Material>, IMaterialDao
    {
        private const string SQL_STMT_FIND_ALL = @"
            SELECT
                cod_oticalentematerial,
                descricao
            FROM
                oticalentematerial
            ORDER BY
                descricao
        ";

        public override Material FetchDto(IDataRecord reader)
        {
            return new Material {
                CodMaterial = Helper.ReadInt32(reader, "cod_oticalentematerial").Value,
                Descricao = Helper.ReadString(reader, "descricao")
            };
        }

        public override Material FindByPrimaryKey(object pk)
        {
            throw new NotImplementedException();
        }

        public override void Update(Material dto)
        {
            throw new NotImplementedException();
        }

        public Material[] FindAll()
        {
            return FindAll(SQL_STMT_FIND_ALL);
        }
    }
}