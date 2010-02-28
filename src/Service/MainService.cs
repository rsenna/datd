using System.ServiceModel;
using System.ServiceProcess;
using Dataweb.Dilab.Model.Wcf;

namespace Dataweb.Dilab.Service
{
    public partial class MainService : ServiceBase
    {
        private ServiceHost clienteServiceHost;
        private ServiceHost produtoServiceHost;

        public MainService()
        {
            InitializeComponent();
            ServiceName = "DatawebDilabService";
        }

        protected override void OnStart(string[] args)
        {
            Dispose(ref clienteServiceHost);
            Dispose(ref produtoServiceHost);

            clienteServiceHost = new ServiceHost(typeof (ClienteService));
            produtoServiceHost = new ServiceHost(typeof (ProdutoService));

            clienteServiceHost.Open();
            produtoServiceHost.Open();
        }

        protected override void OnStop()
        {
            Dispose(ref clienteServiceHost);
            Dispose(ref produtoServiceHost);
        }

        private static void Dispose(ref ServiceHost host)
        {
            if (host == null)
            {
                return;
            }

            host.Close();
            host = null;
        }
    }
}