using System.Collections.Generic;
using System.Web.Mvc;
using Dataweb.Dilab.Model.DataTransfer;
using Dataweb.Dilab.Web.Model;

namespace Dataweb.Dilab.Web.Controllers
{
    public class PacotesController : ControllerBase
    {
        public ActionResult Index()
        {
            InitWcf();

            var result = ClienteSC.FindAllPacoteCredito(GetCodCliente());

            return View(result);
        }

        public ActionResult Detalhar(string id)
        {
            InitWcf();

            var codCliente = GetCodCliente();
            var viewModel = new OsDetalharPacoteViewModel();
            var pacoteCliente = ClienteSC.FindPacoteCredito(codCliente, id);

            if (pacoteCliente != null)
            {
                var pacotesHistorico = ClienteSC.FindAllPacoteHistorico(codCliente, id);

                var compra = new List<PacoteHistorico>();
                var uso = new List<PacoteHistorico>();

                var totalCompras = 0m;
                var totalUsos = 0m;

                foreach (var pacote in pacotesHistorico)
                {
                    switch (pacote.Tipo)
                    {
                        case TipoPacoteHistorico.Compra:
                            compra.Add(pacote);
                            totalCompras += pacote.Quantidade;
                            break;
                        case TipoPacoteHistorico.Uso:
                            uso.Add(pacote);
                            totalUsos += pacote.Quantidade;
                            break;
                    }
                }

                viewModel.Descricao = pacoteCliente.Descricao;
                viewModel.Compra = compra;
                viewModel.Uso = uso;
                viewModel.TotalCompras = totalCompras;
                viewModel.TotalUsos = totalUsos;
            }

            return View(viewModel);
        }
    }
}
