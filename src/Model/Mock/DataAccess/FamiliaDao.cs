using Dataweb.Dilab.Model.DataAccess;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.Mock.DataAccess
{
    public class FamiliaDao : DataAccessBase<Familia>, IFamiliaDao
    {
        public override Familia FetchDto()
        {
            return new Familia {
                CodFamilia = GenerateInt32(),
                Descricao = GenerateText(100)
            };
        }
    }
}