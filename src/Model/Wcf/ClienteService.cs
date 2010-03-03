using System.ServiceModel;
using System.Transactions;
using Dataweb.Dilab.Model.DataAccess;
using Dataweb.Dilab.Model.DataTransfer;
using Dataweb.Dilab.Model.Service;

namespace Dataweb.Dilab.Model.Wcf
{
    [ServiceBehavior(TransactionIsolationLevel = IsolationLevel.ReadCommitted)]
    public class ClienteService : IClienteService
    {
        private IClienteDao clienteDao;
        private IPacoteCreditoDao pacoteCreditoDao;
        private IPacoteHistoricoDao pacoteHistoricoDao;

        public ClienteService()
        {
            DaoFactory.AssemblyName = ConfigHelper.ModelAssemblyName;
        }

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public Cliente FindByLogin(string login)
        {
            // Determina se login é um IDENTIFICADOR ou um CNPJ:
            int identificador;
            var isIdentificador = int.TryParse(login, out identificador);

            clienteDao = DaoFactory.CreateDao<IClienteDao>();

            return isIdentificador ?
                clienteDao.FindByIdentificador(identificador) :
                clienteDao.FindByCnpj(login);
        }

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public bool ValidateLogin(string login, string senha)
        {
            var cliente = FindByLogin(login);

            if (cliente == null)
            {
                return false;
            }

            return string.Compare(cliente.Senha, senha, true) == 0;
        }

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public void ChangePassword(string login, string currentPassword, string newPassword)
        {
            clienteDao = DaoFactory.CreateDao<IClienteDao>();
            var cliente = FindByLogin(login);

            if (cliente.Senha != currentPassword)
            {
                throw new DatawebFaultException<InvalidPasswordFault>();
            }

            cliente.Senha = newPassword;
            clienteDao.Update(cliente);
        }

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public void ChangeEmail(string login, string emailNotificacao, bool receberNotificacao)
        {
            clienteDao = DaoFactory.CreateDao<IClienteDao>();
            var cliente = FindByLogin(login);

            cliente.EmailNotificacao = emailNotificacao;
            cliente.ReceberNotificacao = receberNotificacao;

            clienteDao.Update(cliente);
        }

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public PacoteCredito[] FindAllPacoteCredito(int codCliente)
        {
            pacoteCreditoDao = DaoFactory.CreateDao<IPacoteCreditoDao>();
            return pacoteCreditoDao.FindAll(codCliente);
        }

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public PacoteCredito FindPacoteCredito(int codCliente, string codPacoteCliente)
        {
            pacoteCreditoDao = DaoFactory.CreateDao<IPacoteCreditoDao>();
            return pacoteCreditoDao.FindByPrimaryKey(codCliente, codPacoteCliente);
        }

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public PacoteHistorico[] FindAllPacoteHistorico(int codCliente, string codPacoteCliente)
        {
            pacoteHistoricoDao = DaoFactory.CreateDao<IPacoteHistoricoDao>();
            return pacoteHistoricoDao.FindAll(codCliente, codPacoteCliente);
        }
    }
}