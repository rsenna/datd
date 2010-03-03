using System.Collections.Generic;
using System.ServiceModel;
using Dataweb.Dilab.Model.DataAccess;
using Dataweb.Dilab.Model.DataTransfer;
using Dataweb.Dilab.Model.Service;
using System.Transactions;

namespace Dataweb.Dilab.Model.Wcf
{
    [ServiceBehavior(TransactionIsolationLevel = IsolationLevel.ReadCommitted)]
    public class ProdutoService : IProdutoService
    {
        private ICompraDao compraDao;
        private IFamiliaDao familiaDao;
        private IMaterialDao materialDao;
        private IServicoDao servicoDao;
        private IProdutoDao produtoDao;
        private IOrdemServicoDao ordemServicoDao;
        private IOrdemServicoLenteDao ordemServicoLenteDao;
        private IPedidoDao pedidoDao;

        public ProdutoService()
        {
            DaoFactory.AssemblyName = ConfigHelper.ModelAssemblyName;
        }

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        private static int GetCodClienteByLogin(string login)
        {
            var clienteService = new ClienteService(); // Obs.: a instância, neste caso, é LOCAL (e não remota).
            var cliente = clienteService.FindByLogin(login);
            return cliente.CodCliente;
        }

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public Compra[] FindAllCompraByCodCliente(int codCliente)
        {
            compraDao = DaoFactory.CreateDao<ICompraDao>();
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
            familiaDao = DaoFactory.CreateDao<IFamiliaDao>();
            return familiaDao.FindAll();
        }

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public Material[] FindAllMaterial()
        {
            materialDao = DaoFactory.CreateDao<IMaterialDao>();
            return materialDao.FindAll();
        }

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public int GetCountFechadas(int codCliente)
        {
            compraDao = DaoFactory.CreateDao<ICompraDao>();
            return compraDao.GetCountFechadas(codCliente);
        }

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public int GetCountEmProducao(int codCliente)
        {
            compraDao = DaoFactory.CreateDao<ICompraDao>();
            return compraDao.GetCountEmProducao(codCliente);
        }

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public Servico[] FindAllServico(int codFamilia)
        {
            servicoDao = DaoFactory.CreateDao<IServicoDao>();
            return servicoDao.FindAll(codFamilia);
        }

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public Produto[] FindAllProduto(int codFamilia)
        {
            produtoDao = DaoFactory.CreateDao<IProdutoDao>();
            return produtoDao.FindAll(codFamilia);
        }

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public OrdemServicoOtica InsertOrdemServico(OrdemServicoOtica dto)
        {
            ordemServicoDao = DaoFactory.CreateDao<IOrdemServicoDao>();
            ordemServicoLenteDao = DaoFactory.CreateDao<IOrdemServicoLenteDao>();

            // Grava parte básica da OS:
            ordemServicoDao.Insert(dto.OrdemServico);

            // Como só agora foi obtido dto.CodOrdemServico e dto.CodEmpresa, atualizo os demais DTO's:
            dto.LenteOd.CodOrdemServico = dto.OrdemServico.CodOrdemServico;
            dto.LenteOd.CodEmpresa = dto.OrdemServico.CodEmpresa;
            dto.LenteOe.CodOrdemServico = dto.OrdemServico.CodOrdemServico;
            dto.LenteOe.CodEmpresa = dto.OrdemServico.CodEmpresa;
            foreach (var servico in dto.Servicos)
            {
                servico.CodOrdemServico = dto.OrdemServico.CodOrdemServico;
                servico.CodEmpresa = dto.OrdemServico.CodEmpresa;
            }

            // TODO: Por enquanto a descrição da lente vai ser igual ao nome da família - isso irá mudar no futuro.
            familiaDao = DaoFactory.CreateDao<IFamiliaDao>();
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
            pedidoDao = DaoFactory.CreateDao<IPedidoDao>();

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