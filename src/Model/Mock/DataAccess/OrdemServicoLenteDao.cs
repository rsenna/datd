using System;
using Dataweb.Dilab.Model.DataAccess;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.Mock.DataAccess
{
    public class OrdemServicoLenteDao : DataAccessBase<OrdemServicoLente>, IOrdemServicoLenteDao
    {
        public override OrdemServicoLente FindByPrimaryKey(object pk)
        {
            throw new NotImplementedException();
        }

        public override OrdemServicoLente Update(OrdemServicoLente dto)
        {
            throw new NotImplementedException();
        }

        public OrdemServicoLente[] FindAll(int codOrdemServico)
        {
            throw new NotImplementedException();
        }

        public override OrdemServicoLente Insert(OrdemServicoLente dto)
        {
            return dto;
        }

        public override OrdemServicoLente FetchDto()
        {
            throw new NotImplementedException();
        }
    }
}