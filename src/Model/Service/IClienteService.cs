using System.Collections.Generic;
using System.ServiceModel;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.Service
{
    [ServiceContract]
    public interface IClienteService : IService
    {
        [OperationContract]
        Cliente FindByLogin(string login);

        [OperationContract]
        bool ValidateLogin(string login, string senha);

        [OperationContract]
        [FaultContract(typeof (DatawebFault))]
        void ChangePassword(string login, string currentPassword, string newPassword);

        [OperationContract]
        void ChangeEmail(string login, string emailNotificacao, bool receberNotificacao);

        [OperationContract]
        PacoteCredito FindPacoteCredito(int codCliente, string codPacoteCliente);

        [OperationContract]
        IEnumerable<PacoteCredito> FindAllPacoteCredito(int codCliente);

        [OperationContract]
        IEnumerable<PacoteHistorico> FindAllPacoteHistorico(int codCliente, string codPacoteCliente);
    }
}