using System.Collections.Generic;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.DataAccess
{
    public interface ITransacaoDao : ITransacaoDao<Transacao>
    {
        int GetCountFechadas(int codCliente);
        int GetCountEmProducao(int codCliente);
    }

    public interface ITransacaoDao<T> : IDataAccessBase<T>
        where T: Transacao
    {
        IEnumerable<T> FindAll(int codCliente);
        T FindByPrimaryKey(int codEmpresa, int codTransacao);
        T Close(T dto);
    }

}