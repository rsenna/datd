using System.Runtime.Serialization;
using System.ServiceModel;

namespace Dataweb.Dilab.Model.Wcf
{
    [DataContract]
    public class DatawebFault
    {
        [DataMember]
        public string Message { get; set; }
    }

    public class InvalidPasswordFault : DatawebFault
    {
        public InvalidPasswordFault()
        {
            Message = "Senha inválida";
        }
    }

    public class DatawebFaultException<TDetail> : FaultException<TDetail>
        where TDetail : DatawebFault, new()
    {
        public DatawebFaultException() : base(new TDetail()) {}
    }
}