using System.Data.Common;

namespace Dataweb.Dilab.Model.Mock
{
    public sealed class Session : ISession
    {
        public DbConnection Connection { get; set; }
        public void Dispose() {}
    }
}
