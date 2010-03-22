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

        private object ReadObject<T>(string name)
        {
            var fieldIndex = DataReader.GetOrdinal(name);

            if (DataReader.IsDBNull(fieldIndex))
            {
                return null;
            }

            var type = typeof (T);

            if (type.IsEnum)
            {
                return Enum.ToObject(type, DataReader.GetValue(fieldIndex));
            }

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
            var result = ReadObject<T>(name);

            if (result == null)
            {
                return null;
            }

            return (T?) Convert.ChangeType(result, typeof (T));
        }

        public string ReadOptional(string name)
        {
            var result = ReadObject<string>(name);
            return Convert.ToString(result);
        }

        public T ReadRequired<T>(string name)
        {
            var result = ReadObject<T>(name);

            if (result == null)
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