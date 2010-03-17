﻿using System.Collections.Generic;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.DataAccess
{
    public interface INotaFiscalDao : IDataAccessBase<NotaFiscal>
    {
        IEnumerable<NotaFiscal> FindAll(int codCliente);
        NotaFiscal FindByPrimaryKey(int codNotaFiscal);
        string GetXml(int codNotaFiscal);
    }
}