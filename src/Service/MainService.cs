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
            if (clienteServiceHost != null)
            {
                clienteServiceHost.Close();
            }

            if (ordemServicoServiceHost != null)
            {
                ordemServicoServiceHost.Close();
            }

            clienteServiceHost = new ServiceHost(typeof (ClienteService));
            ordemServicoServiceHost = new ServiceHost(typeof (OrdemServicoService));

            clienteServiceHost.Open();
            ordemServicoServiceHost.Open();
        }

        protected override void OnStop()
        {
            if (clienteServiceHost != null)
            {
                clienteServiceHost.Close();
                clienteServiceHost = null;
            }

            if (ordemServicoServiceHost != null)
            {
                ordemServicoServiceHost.Close();
                ordemServicoServiceHost = null;
            }
        }
    }
}