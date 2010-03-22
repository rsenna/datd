using System.Data;

namespace Dataweb.Dilab.Model.Mock
{
    public sealed class MockSession : ISession
    {
        public IDbConnection Connection { get; set; }

        public void Dispose() {}

        public ICommand CreateCommand()
        {
            return new MockCommand {Session = this};
        }
    }
}
