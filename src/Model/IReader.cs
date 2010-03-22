using System;

namespace Dataweb.Dilab.Model
{
    public interface IReader : IDisposable
    {
        ICommand Command { get; set; }

        bool ReadRecord();

        T? ReadOptional<T>(string name) where T : struct;
        string ReadOptional(string name);
        T ReadRequired<T>(string name);
        string ReadRequired(string name);
    }
}