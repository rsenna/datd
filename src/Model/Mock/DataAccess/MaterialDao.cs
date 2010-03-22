using System;
using Dataweb.Dilab.Model.DataTransfer;
using Base=Dataweb.Dilab.Model.DataAccess;

namespace Dataweb.Dilab.Model.Mock.DataAccess
{
    public class MaterialDao : Base.MaterialDao
    {
        public override Material InitDto(IReader reader, Material dto)
        {
            base.InitDto(reader, dto);

            // Sobreescreve a geração default para estes campos:
            dto.Descricao = MockReader.GenerateName(4).ToUpper();

            return dto;
        }

        public override string GetStmtFindAll()
        {
            return string.Empty;
        }
    }
}