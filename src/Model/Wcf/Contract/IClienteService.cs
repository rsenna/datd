using System.ServiceModel;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.Wcf.Contract
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
    }
}