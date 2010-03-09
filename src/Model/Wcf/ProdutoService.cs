using System.Collections.Generic;
using System.ServiceModel;
using Dataweb.Dilab.Model.DataAccess;
using Dataweb.Dilab.Model.DataTransfer;
using Dataweb.Dilab.Model.Service;
using System.Transactions;

namespace Dataweb.Dilab.Model.Wcf
{
    [ServiceBehavior(TransactionIsolationLevel = IsolationLevel.ReadCommitted, InstanceContextMode = InstanceContextMode.PerSession)]
    public class ProdutoService : IProdutoService
    {
        private readonly ISession session;

        public ProdutoService()
        {
            DataAccessFactory.AssemblyName = ConfigHelper.ModelAssemblyName;
            session = DataAccessFactory.CreateSession();
        }

        public void Dispose()
        {
            session.Dispose();
        }

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        private int GetCodClienteByLogin(string login)
        {
            var clienteService = new ClienteService(session); // Obs.: a instância do serviço, neste caso, é LOCAL (e não remota).
            var cliente = clienteService.FindByLogin(login);
            return cliente.CodCliente;
        }

        private Compra[] FindAllCompraByCodCliente(int codCliente)
        {
            var compraDao = DataAccessFactory.CreateDao<ICompraDao>(session);
            return compraDao.FindAll(codCliente, ProfundidadeConsultaTransacao.Transacao);
        }

        private Compra[] FindAllCompraByCodClienteAndReferencia(int codCliente, string referencia)
        {
            var total = FindAllCompraByCodCliente(codCliente);
            var parcial = new List<Compra>();

            foreach (var os in total)
            {
                if (os.Referencia == referencia)
                {
                    parcial.Add(os);
                }
            }

            return parcial.ToArray();
        }

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public Compra[] FindAllCompraByLogin(string login)
        {
            return FindAllCompraByCodCliente(GetCodClienteByLogin(login));
        }

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public Compra[] FindAllCompraByLoginAndReferencia(string login, string referencia)
        {
            return FindAllCompraByCodClienteAndReferencia(GetCodClienteByLogin(login), referencia);
        }

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public Familia[] FindAllFamilia()
        {
            var familiaDao = DataAccessFactory.CreateDao<IFamiliaDao>(session);
            return familiaDao.FindAll();
        }

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public Material[] FindAllMaterial()
        {
            var materialDao = DataAccessFactory.CreateDao<IMaterialDao>(session);
            return materialDao.FindAll();
        }

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public int GetCountFechadas(int codCliente)
        {
            var compraDao = DataAccessFactory.CreateDao<ICompraDao>(session);
            return compraDao.GetCountFechadas(codCliente);
        }

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public int GetCountEmProducao(int codCliente)
        {
            var compraDao = DataAccessFactory.CreateDao<ICompraDao>(session);
            return compraDao.GetCountEmProducao(codCliente);
        }

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public Item[] FindAllServico(int codFamilia)
        {
            var itemDao = DataAccessFactory.CreateDao<IItemDao>(session);
            return itemDao.FindAll(codFamilia, TipoItem.Servico);
        }

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public Item[] FindAllProduto(int codFamilia)
        {
            var itemDao = DataAccessFactory.CreateDao<IItemDao>(session);
            return itemDao.FindAll(codFamilia, TipoItem.Produto);
        }

        private Compra InsertCompra(Compra dto)
        {
            var compraDao = DataAccessFactory.CreateDao<ICompraDao>(session);

            dto = compraDao.Insert(dto);
            compraDao.Close(dto);

            return dto;
        }

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public OrdemServico InsertOrdemServico(OrdemServico dto)
        {
            return (OrdemServico) InsertCompra(dto);
        }

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public Pedido InsertPedido(Pedido dto)
        {
            return (Pedido) InsertCompra(dto);
        }
    }
}