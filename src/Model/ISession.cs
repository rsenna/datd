using System;

namespace Dataweb.Dilab.Model
{
    public interface ISession : IDisposable
    {
        ICommand CreateCommand();
    }
}
