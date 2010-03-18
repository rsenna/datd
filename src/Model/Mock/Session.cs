using System.Data;

namespace Dataweb.Dilab.Model.Mock
{
    public sealed class Session : ISession
    {
        public IDbConnection Connection { get; set; }
        public void Dispose() {}
    }
}
