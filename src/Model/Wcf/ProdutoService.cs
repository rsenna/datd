using System.Collections.Generic;
using System.ServiceModel;
using Dataweb.Dilab.Model.DataAccess;
using Dataweb.Dilab.Model.DataTransfer;
using Dataweb.Dilab.Model.Service;
using System.Transactions;

namespace Dataweb.Dilab.Model.Wcf
{
    [ServiceBehavior(TransactionIsolationLevel = IsolationLevel.ReadCommitted, InstanceContextMode = InstanceContextMode.PerSession)]
    public sealed class ProdutoService : IProdutoService
    {
        private readonly ISession session;

        public ProdutoService()
        {
            DataAccessFactory.AssemblyName = ConfigHelper.ModelAssemblyName;
            session = DataAccessFactory.CreateSession();
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

        private int GetCodCliente(string login)
        {
            var clienteService = new ClienteService(session); // Obs.: a instância do serviço, neste caso, é LOCAL (e não remota).
            var cliente = clienteService.FindByLogin(login);
            return cliente.CodCliente;
        }

        /// <summary>
        /// Retorna todas as transações da empresa indicada.
        /// </summary>
        /// <param name="codCliente">CodCliente da empresa.</param>
        /// <returns>Array de transações.</returns>
        private IEnumerable<Transacao> FindAllTransacaoByCodCliente(int codCliente)
        {
            var transacaoDao = DataAccessFactory.CreateDao<ITransacaoDao>(session);
            transacaoDao.Depth = QueryDepth.FirstLevel;
            return transacaoDao.FindAll(codCliente);
        }

        private IEnumerable<Transacao> FindAllTransacaoByCodClienteAndReferencia(int codCliente, string referencia)
        {
            var total = FindAllTransacaoByCodCliente(codCliente);
            var parcial = new List<Transacao>();

            foreach (var os in total)
            {
                if (os.Referencia == referencia)
                {
                    parcial.Add(os);
                }
            }

            return parcial.ToArray();
        }

        /// <summary>
        /// Busca todas as transações da empresa logada.
        /// </summary>
        /// <param name="login">Login da empresa.</param>
        /// <returns>Array de transações.</returns>
        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public IEnumerable<Transacao> FindAllTransacaoByLogin(string login)
        {
            return FindAllTransacaoByCodCliente(GetCodCliente(login));
        }

        /// <summary>
        /// Busca todas as transações da empresa logada e com a referência indicada.
        /// </summary>
        /// <param name="login">Login da empresa.</param>
        /// <param name="referencia">Referência a ser buscada.</param>
        /// <returns>Array de transações.</returns>
        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public IEnumerable<Transacao> FindAllTransacaoByLoginAndReferencia(string login, string referencia)
        {
            return FindAllTransacaoByCodClienteAndReferencia(GetCodCliente(login), referencia);
        }

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public IEnumerable<Familia> FindAllFamilia()
        {
            var familiaDao = DataAccessFactory.CreateDao<IFamiliaDao>(session);
            return familiaDao.FindAll();
        }

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public IEnumerable<Material> FindAllMaterial()
        {
            var materialDao = DataAccessFactory.CreateDao<IMaterialDao>(session);
            return materialDao.FindAll();
        }

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public int GetCountFechadas(int codCliente)
        {
            var transacaoDao = DataAccessFactory.CreateDao<ITransacaoDao>(session);
            return transacaoDao.GetCountFechadas(codCliente);
        }

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public int GetCountEmProducao(int codCliente)
        {
            var transacaoDao = DataAccessFactory.CreateDao<ITransacaoDao>(session);
            return transacaoDao.GetCountEmProducao(codCliente);
        }

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public IEnumerable<Item> FindAllServico(int codFamilia)
        {
            var itemDao = DataAccessFactory.CreateDao<IItemDao>(session);
            return itemDao.FindAll(codFamilia, TipoItem.Servico);
        }

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public IEnumerable<Item> FindAllProduto(int codFamilia)
        {
            var itemDao = DataAccessFactory.CreateDao<IItemDao>(session);
            return itemDao.FindAll(codFamilia, TipoItem.Produto);
        }

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public Transacao GetTransacao(int codEmpresa, int codTransacao)
        {
            var transacaoDao = DataAccessFactory.CreateDao<ITransacaoDao>(session);
            transacaoDao.Depth = QueryDepth.Complete;
            return transacaoDao.FindByPrimaryKey(codEmpresa, codTransacao);
        }

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public OrdemServico GetOrdemServico(int codEmpresa, int codTransacao)
        {
            var ordemServicoDao = DataAccessFactory.CreateDao<IOrdemServicoDao>(session);
            ordemServicoDao.Depth = QueryDepth.Complete;
            return ordemServicoDao.FindByPrimaryKey(codEmpresa, codTransacao);
        }

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public Pedido GetPedido(int codEmpresa, int codTransacao)
        {
            var pedidoDao = DataAccessFactory.CreateDao<IPedidoDao>(session);
            pedidoDao.Depth = QueryDepth.Complete;
            return pedidoDao.FindByPrimaryKey(codEmpresa, codTransacao);
        }

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public OrdemServico InsertOrdemServico(OrdemServico dto)
        {
            var dao = DataAccessFactory.CreateDao<IOrdemServicoDao>(session);
            dao.Insert(dto);
            dao.Close(dto);
            return dto;
        }

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public Pedido InsertPedido(Pedido dto)
        {
            var dao = DataAccessFactory.CreateDao<IPedidoDao>(session);
            dao.Insert(dto);
            dao.Close(dto);
            return dto;
        }

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public IEnumerable<Fatura> FindAllFatura(string login)
        {
            var faturaDao = DataAccessFactory.CreateDao<IFaturaDao>(session);
            faturaDao.Depth = QueryDepth.FirstLevel;
            return faturaDao.FindAll(GetCodCliente(login));
        }

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public IEnumerable<Lancamento> FindAllLancamento(string login)
        {
            var lancamentoDao = DataAccessFactory.CreateDao<ILancamentoDao>(session);
            lancamentoDao.Depth = QueryDepth.FirstLevel;
            return lancamentoDao.FindAll(GetCodCliente(login));
        }

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public IEnumerable<NotaFiscal> FindAllNotaFiscal(string login)
        {
            var notaFiscalDao = DataAccessFactory.CreateDao<INotaFiscalDao>(session);
            notaFiscalDao.Depth = QueryDepth.FirstLevel;
            return notaFiscalDao.FindAll(GetCodCliente(login));
        }

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public Fatura GetFatura(int codFatura)
        {
            var faturaDao = DataAccessFactory.CreateDao<IFaturaDao>(session);
            faturaDao.Depth = QueryDepth.SecondLevel;
            return faturaDao.FindByPrimaryKey(codFatura);
        }

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public NotaFiscal GetNotaFiscal(int codNotaFiscal)
        {
            var notaFiscalDao = DataAccessFactory.CreateDao<INotaFiscalDao>(session);
            notaFiscalDao.Depth = QueryDepth.SecondLevel;
            return notaFiscalDao.FindByPrimaryKey(codNotaFiscal);
        }

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public string GetXmlNotaFiscalEletronica(int codNotaFiscal)
        {
            var notaFiscalDao = DataAccessFactory.CreateDao<INotaFiscalDao>(session);
            return notaFiscalDao.GetXml(codNotaFiscal);
        }
    }
}