using Dataweb.Dilab.Model.DataAccess;
using Dataweb.Dilab.Model.DataTransfer;
using Dataweb.Dilab.Model.Service;

namespace Dataweb.Dilab.Model.Wcf
{
    // TODO: Servico generico - renomear
    public class ClienteService : IClienteService
    {
        private IClienteDao clienteDao;
        private IPacoteCreditoDao pacoteCreditoDao;
        private IPacoteHistoricoDao pacoteHistoricoDao;

        public ClienteService()
        {
            DaoFactory.AssemblyName = ConfigHelper.ModelAssemblyName;
        }

        public Cliente FindByLogin(string login)
        {
            clienteDao = DaoFactory.CreateDao<IClienteDao>();

            // Determina se login é um IDENTIFICADOR ou um CNPJ:
            int identificador;
            var isIdentificador = int.TryParse(login, out identificador);

            return isIdentificador?
                clienteDao.FindByIdentificador(identificador) :
                clienteDao.FindByCnpj(login);
        }

        public bool ValidateLogin(string login, string senha)
        {
            var cliente = FindByLogin(login);

            if (cliente == null)
            {
                return false;
            }

            return string.Compare(cliente.Senha, senha, true) == 0;
        }

        public void ChangePassword(string login, string currentPassword, string newPassword)
        {
            var cliente = FindByLogin(login);

            if (cliente.Senha != currentPassword)
            {
                throw new DatawebFaultException<InvalidPasswordFault>();
            }

            cliente.Senha = newPassword;
            clienteDao.Update(cliente);
        }

        public void ChangeEmail(string login, string emailNotificacao, bool receberNotificacao)
        {
            var cliente = FindByLogin(login);

            cliente.EmailNotificacao = emailNotificacao;
            cliente.ReceberNotificacao = receberNotificacao;

            clienteDao.Update(cliente);
        }

        public PacoteCredito[] FindAllPacoteCredito(int codCliente)
        {
            pacoteCreditoDao = DaoFactory.CreateDao<IPacoteCreditoDao>();
            return pacoteCreditoDao.FindAll(codCliente);
        }

        public PacoteCredito FindPacoteCredito(int codCliente, string codPacoteCliente)
        {
            pacoteCreditoDao = DaoFactory.CreateDao<IPacoteCreditoDao>();
            return pacoteCreditoDao.FindByPrimaryKey(codCliente, codPacoteCliente);
        }

        public PacoteHistorico[] FindAllPacoteHistorico(int codCliente, string codPacoteCliente)
        {
            pacoteHistoricoDao = DaoFactory.CreateDao<IPacoteHistoricoDao>();
            return pacoteHistoricoDao.FindAll(codCliente, codPacoteCliente);
        }
    }
}