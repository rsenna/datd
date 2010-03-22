using System;
using System.Collections.Generic;
using Dataweb.Dilab.Model.DataAccess.Contracts;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.DataAccess
{
    public abstract class MaterialDao : DataAccessBase<Material>, IMaterialDao
    {
        public abstract string GetStmtFindAll();

        public override Material InitDto(IReader reader, Material dto)
        {
            dto.CodMaterial = reader.ReadRequired<int>("cod_oticalentematerial");
            dto.Descricao = reader.ReadRequired("descricao");

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
            return FindAll(GetStmtFindAll());
        }

        public override Material Insert(Material dto)
        {
            throw new NotImplementedException();
        }
    }
}