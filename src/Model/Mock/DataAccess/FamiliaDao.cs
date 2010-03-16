using Dataweb.Dilab.Model.DataAccess;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.Mock.DataAccess
{
    public class FamiliaDao : DataAccessBase<Familia>, IFamiliaDao
    {
        public override Familia InitDto(Familia dto)
        {
            dto.CodFamilia = GenerateInt32();
            dto.Descricao = GenerateName(4).ToUpper();

            return dto;
        }
    }
}