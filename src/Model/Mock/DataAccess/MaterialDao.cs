using Dataweb.Dilab.Model.DataAccess;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.Mock.DataAccess
{
    public class MaterialDao : DataAccessBase<Material>, IMaterialDao
    {
        public override Material FetchDto()
        {
            return new Material {
                CodMaterial = GenerateInt32(),
                Descricao = GenerateText(100)
            };
        }
    }
}