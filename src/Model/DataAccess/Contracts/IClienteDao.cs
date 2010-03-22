using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.DataAccess.Contracts
{
    public interface IClienteDao : IDataAccessBase<Cliente>
    {
        Cliente FindByIdentificador(int identificador);
        Cliente FindByCnpj(string cnpj);
    }
}