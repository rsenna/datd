using Dataweb.Dilab.Model.DataAccess;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.Mock.DataAccess
{
    public class MaterialDao : DataAccessBase<Material>, IMaterialDao
    {
        public override Material InitDto(Material dto)
        {
            dto.CodMaterial = GenerateInt32();
            dto.Descricao = GenerateName(4).ToUpper();

            return dto;
        }
    }
}