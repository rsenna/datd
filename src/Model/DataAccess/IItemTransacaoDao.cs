using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.DataAccess
{
    public interface IItemTransacaoDao : IDataAccessBase<ItemTransacao>
    {
        ItemTransacao[] FindByTransacao(int codEmpresa, int codTransacao);
        ItemTransacao[] Insert(ItemTransacao[] dtos);
    }
}
