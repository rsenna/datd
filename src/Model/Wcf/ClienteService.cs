using System.Collections.Generic;
using System.ServiceModel;
using System.Transactions;
using Dataweb.Dilab.Model.DataAccess;
using Dataweb.Dilab.Model.DataTransfer;
using Dataweb.Dilab.Model.Service;

namespace Dataweb.Dilab.Model.Wcf
{
    [ServiceBehavior(TransactionIsolationLevel = IsolationLevel.ReadCommitted, InstanceContextMode = InstanceContextMode.PerSession)]
    public sealed class ClienteService : IClienteService
    {
        private readonly ISession session;

        public ClienteService()
        {
            DataAccessFactory.AssemblyName = ConfigHelper.ModelAssemblyName;
            session = DataAccessFactory.CreateSession();
        }

        internal ClienteService(ISession session)
        {
            DataAccessFactory.AssemblyName = ConfigHelper.ModelAssemblyName;
            this.session = session;
        }

        /// <summary>
        /// Fecha a sessão quando o serviço for destruído.
        /// </summary>
        /// <remarks>
        /// Como a classe é sealed, implementação do Dispose pode ser simples.
        /// Ver <see cref="http://www.codeproject.com/KB/cs/idisposable.aspx"/>.
        /// </remarks>
        public void Dispose()
        {
            session.Dispose();
        }

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public Cliente FindByLogin(string login)
        {
            // Determina se login é um IDENTIFICADOR ou um CNPJ:
            int identificador;
            var isIdentificador = int.TryParse(login, out identificador);

            var clienteDao = DataAccessFactory.CreateDao<IClienteDao>(session);
            return isIdentificador? clienteDao.FindByIdentificador(identificador) : clienteDao.FindByCnpj(login);
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
            var clienteDao = DataAccessFactory.CreateDao<IClienteDao>(session);
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
            var clienteDao = DataAccessFactory.CreateDao<IClienteDao>(session);
            var cliente = FindByLogin(login);

            cliente.EmailNotificacao = emailNotificacao;
            cliente.ReceberNotificacao = receberNotificacao;

            clienteDao.Update(cliente);
        }

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public IEnumerable<PacoteCredito> FindAllPacoteCredito(int codCliente)
        {
            var pacoteCreditoDao = DataAccessFactory.CreateDao<IPacoteCreditoDao>(session);
            return pacoteCreditoDao.FindAll(codCliente);
        }

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public PacoteCredito FindPacoteCredito(int codCliente, string codPacoteCliente)
        {
            var pacoteCreditoDao = DataAccessFactory.CreateDao<IPacoteCreditoDao>(session);
            return pacoteCreditoDao.FindByPrimaryKey(codCliente, codPacoteCliente);
        }

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public IEnumerable<PacoteHistorico> FindAllPacoteHistorico(int codCliente, string codPacoteCliente)
        {
            var pacoteHistoricoDao = DataAccessFactory.CreateDao<IPacoteHistoricoDao>(session);
            return pacoteHistoricoDao.FindAll(codCliente, codPacoteCliente);
        }
    }
}