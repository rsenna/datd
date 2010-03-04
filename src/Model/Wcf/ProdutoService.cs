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

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public Compra[] FindAllCompraByCodCliente(int codCliente)
        {
            var compraDao = DataAccessFactory.CreateDao<ICompraDao>(session);
            return compraDao.FindAll(codCliente);
        }

        public Compra[] FindAllCompraByCodClienteAndReferencia(int codCliente, string referencia)
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
        public Servico[] FindAllServico(int codFamilia)
        {
            var servicoDao = DataAccessFactory.CreateDao<IServicoDao>(session);
            return servicoDao.FindAll(codFamilia);
        }

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public Produto[] FindAllProduto(int codFamilia)
        {
            var produtoDao = DataAccessFactory.CreateDao<IProdutoDao>(session);
            return produtoDao.FindAll(codFamilia);
        }

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public OrdemServicoOtica InsertOrdemServico(OrdemServicoOtica dto)
        {
            var ordemServicoDao = DataAccessFactory.CreateDao<IOrdemServicoDao>(session);
            var ordemServicoLenteDao = DataAccessFactory.CreateDao<IOrdemServicoLenteDao>(session);

            // Grava parte básica da OS:
            ordemServicoDao.Insert(dto.OrdemServico);

            // Como só agora foi obtido dto.CodOrdemServico e dto.CodEmpresa, atualizo os demais DTO's:
            dto.LenteOd.CodOrdemServico = dto.OrdemServico.CodOrdemServico;
            dto.LenteOd.CodEmpresa = dto.OrdemServico.CodEmpresa;
            dto.LenteOe.CodOrdemServico = dto.OrdemServico.CodOrdemServico;
            dto.LenteOe.CodEmpresa = dto.OrdemServico.CodEmpresa;
            // ---
            foreach (var servico in dto.Servicos)
            {
                servico.CodOrdemServico = dto.OrdemServico.CodOrdemServico;
                servico.CodEmpresa = dto.OrdemServico.CodEmpresa;
            }

            // TODO: Por enquanto a descrição da lente vai ser igual ao nome da família - isso irá mudar no futuro.
            var familiaDao = DataAccessFactory.CreateDao<IFamiliaDao>(session);
            var familiaOd = familiaDao.FindByPrimaryKey(dto.LenteOd.CodFamilia);
            var familiaOe = familiaDao.FindByPrimaryKey(dto.LenteOe.CodFamilia);
            dto.LenteOd.Descricao = familiaOd.Descricao;
            dto.LenteOe.Descricao = familiaOe.Descricao;

            // Grava lentes:
            ordemServicoLenteDao.Insert(dto.LenteOd);
            ordemServicoLenteDao.Insert(dto.LenteOe);

            // Grava os serviços que serão executados na OS:
            ordemServicoDao.InsertItens(dto.Servicos);

            // Conclui a ordem de serviço:
            ordemServicoDao.Close(dto.OrdemServico);

            return dto;
        }

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public Pedido InsertPedido(Pedido dto)
        {
            var pedidoDao = DataAccessFactory.CreateDao<IPedidoDao>(session);

            // Grava parte básica do pedido:
            pedidoDao.Insert(dto);

            // Como só agora foi obtido dto.CodPedido e dto.CodEmpresa, atualizo os demais DTO's:
            foreach (var produto in dto.Produtos)
            {
                produto.CodPedido = dto.CodPedido;
                produto.CodEmpresa = dto.CodEmpresa;
            }

            // Grava os produtos registrados no pedido:
            pedidoDao.InsertItens(dto.Produtos);

            return dto;
        }
    }
}