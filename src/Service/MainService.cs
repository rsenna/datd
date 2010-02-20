using System.ServiceModel;
using System.ServiceProcess;
using Dataweb.Dilab.Model.Wcf;

namespace Dataweb.Dilab.Service
{
    public partial class MainService : ServiceBase
    {
        private ServiceHost clienteServiceHost;
        private ServiceHost ordemServicoServiceHost;

        public MainService()
        {
            InitializeComponent();
            ServiceName = "DatawebDilabService";
        }

        protected override void OnStart(string[] args)
        {
            Dispose(ref clienteServiceHost);
            Dispose(ref ordemServicoServiceHost);

            clienteServiceHost = new ServiceHost(typeof (ClienteService));
            ordemServicoServiceHost = new ServiceHost(typeof (OrdemServicoService));

            clienteServiceHost.Open();
            ordemServicoServiceHost.Open();
        }

        protected override void OnStop()
        {
            Dispose(ref clienteServiceHost);
            Dispose(ref ordemServicoServiceHost);
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