using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dataweb.Dilab.Model.DataAccess;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.Mock.DataAccess
{
    public class CompraDetalheDao : DataAccessBase<CompraDetalhe>, ICompraDetalheDao
    {
        public override CompraDetalhe FetchDto()
        {
            throw new NotImplementedException();
        }
    }
}
