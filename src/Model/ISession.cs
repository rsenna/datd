using System;
using System.Data.Common;

namespace Dataweb.Dilab.Model
{
    public interface ISession : IDisposable
    {
        DbConnection Connection { get; }
    }
}
