using System.Collections.Generic;
using Dataweb.Dilab.Model.DataAccess;
using Dataweb.Dilab.Model.DataTransfer;
using Dataweb.Dilab.Model.Wcf.Contract;

namespace Dataweb.Dilab.Model.Wcf
{
    public class OrdemServicoService : IOrdemServicoService
    {
        private IOrdemServicoDao ordemServicoDao;
        private IClienteDao clienteDao;
        private IFamiliaDao familiaDao;
        private IMaterialDao materialDao;
        private IProdutoServicoDao produtoServicoDao;

        private int GetCodClienteByLogin(string login)
        {
            clienteDao = DaoFactory.CreateDao<IClienteDao>();
            var cliente = clienteDao.FindByLogin(login);
            return cliente.CodCliente;
        }

        public OrdemServico[] FindAllByCodCliente(int codCliente)
        {
            ordemServicoDao = DaoFactory.CreateDao<IOrdemServicoDao>();
            return ordemServicoDao.FindAll(codCliente);
        }

        public OrdemServico[] FindAllByCodClienteAndReferencia(int codCliente, string referencia)
        {
            var total = FindAllByCodCliente(codCliente);
            var parcial = new List<OrdemServico>();

            foreach (var os in total)
            {
                if (os.Referencia == referencia)
                {
                    parcial.Add(os);
                }
            }

            return parcial.ToArray();
        }

        public OrdemServico[] FindAllByLogin(string login)
        {
            return FindAllByCodCliente(GetCodClienteByLogin(login));
        }

        public OrdemServico[] FindAllByLoginAndReferencia(string login, string referencia)
        {
            return FindAllByCodClienteAndReferencia(GetCodClienteByLogin(login), referencia);
        }

        public Familia[] FindAllFamilia()
        {
            familiaDao = DaoFactory.CreateDao<IFamiliaDao>();
            return familiaDao.FindAll();
        }

        public Material[] FindAllMaterial()
        {
            materialDao = DaoFactory.CreateDao<IMaterialDao>();
            return materialDao.FindAll();
        }

        public int GetCountFechadas(int codCliente)
        {
            ordemServicoDao = DaoFactory.CreateDao<IOrdemServicoDao>();
            return ordemServicoDao.GetCountFechadas(codCliente);
        }

        public int GetCountEmProducao(int codCliente)
        {
            ordemServicoDao = DaoFactory.CreateDao<IOrdemServicoDao>();
            return ordemServicoDao.GetCountEmProducao(codCliente);
        }

        public ProdutoServico[] FindAllProdutoServico(int? codFamiliaOd, int? codFamiliaOe)
        {
            produtoServicoDao = DaoFactory.CreateDao<IProdutoServicoDao>();
            return produtoServicoDao.FindAll(codFamiliaOd, codFamiliaOe);
        }
    }
}