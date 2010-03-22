using System;
using System.Data;

namespace Dataweb.Dilab.Model.Ado
{
    public sealed class FbReader : IReader
    {
        internal IDataReader DataReader { get; set; }
        public ICommand Command { get; set; }

        public void Dispose()
        {
            DataReader.Dispose();
        }

        public bool ReadRecord()
        {
            return DataReader.Read();
        }

        private object ReadObject(string name)
        {
            var fieldIndex = DataReader.GetOrdinal(name);

            if (DataReader.IsDBNull(fieldIndex))
            {
                return null;
            }

            var type = DataReader.GetFieldType(fieldIndex);

            if (type == typeof(decimal))
            {
                // Decimal precisa ser lido desta forma (GetValue não funciona).
                return DataReader.GetDecimal(fieldIndex);
            }

            if (type == typeof(bool))
            {
                return ToBoolean(DataReader.GetString(fieldIndex));
            }

            return DataReader.GetValue(fieldIndex);
        }

        public T? ReadOptional<T>(string name)
            where T : struct
        {
            var result = ReadObject(name);
            return (T?) Convert.ChangeType(result, typeof (T?));
        }

        public string ReadOptional(string name)
        {
            var result = ReadObject(name);
            return Convert.ToString(result);
        }

        public T ReadRequired<T>(string name)
        {
            var result = ReadObject(name);

            if (result == null || result == DBNull.Value)
            {
                throw new InvalidOperationException(string.Format(
                    "Campo \"{0}\" é obrigatório, mas foi lido um valor nulo.",
                    name
                    ));
            }

            return (T) Convert.ChangeType(result, typeof (T));
        }

        public string ReadRequired(string name)
        {
            return ReadRequired<string>(name);
        }

        internal static bool? ToBoolean(string value)
        {
            switch (value)
            {
                case "S":
                case "T":
                case "Y":
                    return true;
                case "F":
                case "N":
                    return false;
            }

            return null;
        }

        internal static string ToString(bool? value)
        {
            if (value == null)
            {
                return null;
            }

            return value.Value? "T" : "F";
        }
    }
}