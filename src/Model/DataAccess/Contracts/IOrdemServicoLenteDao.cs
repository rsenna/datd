using System.Collections.Generic;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.DataAccess.Contracts
{
    public interface IOrdemServicoLenteDao : IDataAccessBase<OrdemServicoLente>
    {
        OrdemServicoLente FindByPrimaryKey(int codEmpresa, int codTransacao, TipoLente tipoLente);
        IEnumerable<OrdemServicoLente> FindAll(int codOrdemServico);
    }
}