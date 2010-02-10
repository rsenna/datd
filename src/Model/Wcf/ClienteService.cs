using Dataweb.Dilab.Model.DataAccess;
using Dataweb.Dilab.Model.DataTransfer;
using Dataweb.Dilab.Model.Wcf.Contract;

namespace Dataweb.Dilab.Model.Wcf
{
    public class ClienteService : IClienteService
    {
        private IClienteDao clienteDao;

        public Cliente FindByLogin(string login)
        {
            clienteDao = DaoFactory.CreateDao<IClienteDao>();
            return clienteDao.FindByLogin(login);
        }

        public bool ValidateLogin(string login, string senha)
        {
            clienteDao = DaoFactory.CreateDao<IClienteDao>();
            var cliente = clienteDao.FindByLogin(login);

            if (cliente == null)
            {
                return false;
            }

            return string.Compare(cliente.Senha, senha, true) == 0;
        }

        public void ChangePassword(string login, string currentPassword, string newPassword)
        {
            clienteDao = DaoFactory.CreateDao<IClienteDao>();
            var cliente = clienteDao.FindByLogin(login);

            if (cliente.Senha != currentPassword)
            {
                throw new DatawebFaultException<InvalidPasswordFault>();
            }

            cliente.Senha = newPassword;
            clienteDao.Update(cliente);
        }

        public void ChangeEmail(string login, string emailNotificacao, bool receberNotificacao)
        {
            clienteDao = DaoFactory.CreateDao<IClienteDao>();
            var cliente = clienteDao.FindByLogin(login);

            cliente.EmailNotificacao = emailNotificacao;
            cliente.ReceberNotificacao = receberNotificacao;

            clienteDao.Update(cliente);
        }
    }
}