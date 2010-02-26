﻿using System;
using System.Configuration;
using System.Data;
using System.Data.Common;

namespace Dataweb.Dilab.Model.Ado
{
    public static class Helper
    {
        public const string CONNECTION_STRING_NAME = "DilabDatabase";

        public static ConnectionStringSettings GetConnectionString()
        {
            var connectionString = ConfigurationManager.ConnectionStrings[CONNECTION_STRING_NAME];

            if (connectionString == null)
            {
                throw new ConfigurationErrorsException(String.Format("Connection string '{0}' is undefined.", CONNECTION_STRING_NAME));
            }

            return connectionString;
        }

        public static void AddParameter(DbCommand command, string parameterName, DbType dbtype, object value)
        {
            var parameter = command.CreateParameter();

            parameter.ParameterName = parameterName;
            parameter.DbType = dbtype;
            parameter.Value = value;

            command.Parameters.Add(parameter);
        }

        public static DbParameter AddReturnParameter(DbCommand command, string parameterName, DbType dbtype)
        {
            var parameter = command.CreateParameter();

            parameter.ParameterName = parameterName;
            parameter.DbType = dbtype;
            parameter.Direction = ParameterDirection.Output;

            command.Parameters.Add(parameter);

            return parameter;
        }

        public static void UsingConnection(Action<DbConnection> action)
        {
            var connectionString = GetConnectionString();
            var factory = DbProviderFactories.GetFactory(connectionString.ProviderName);

            if (factory == null)
            {
                throw new ConfigurationErrorsException(String.Format("Could not obtain a factory for provider '{0}'.", connectionString.ProviderName));
            }

            using (var connection = factory.CreateConnection())
            {
                if (connection == null)
                {
                    throw new Exception("Could not obtain a connection from DB factory.");
                }

                connection.ConnectionString = connectionString.ConnectionString;
                connection.Open();

                action(connection);
            }
        }

        public static void UsingTransaction(Action<DbTransaction> action)
        {
            UsingConnection(c => {
                using (var transaction = c.BeginTransaction())
                {
                    action(transaction);
                    transaction.Commit();
                }
            });
        }

        public static void UsingCommand(Action<DbCommand> action)
        {
            UsingTransaction(t => UsingCommand(t, action));
        }

        public static void UsingCommand(DbTransaction t, Action<DbCommand> action)
        {
            using (var command = t.Connection.CreateCommand())
            {
                command.Transaction = t;
                action(command);
            }
        }

        public static object ReadObject(IDataRecord record, string name)
        {
            // O codigo abaixo eh mais extenso que "return record[name]", mas
            // permite identificar mais facilmente algum erro de nome ou tipo
            // de campo:
            var fieldIndex = record.GetOrdinal(name);
            var result = record.GetValue(fieldIndex);
            return result;
        }

        public static int? ReadInt32(IDataRecord record, string name)
        {
            var result = ReadObject(record, name);
            return result == null || result == DBNull.Value? (int?) null : Convert.ToInt32(result);
        }

        public static long? ReadInt64(IDataRecord record, string name)
        {
            var result = ReadObject(record, name);
            return result == null || result == DBNull.Value? (long?) null : Convert.ToInt64(result);
        }

        public static DateTime? ReadDateTime(IDataRecord record, string name)
        {
            var result = ReadObject(record, name);
            return result == null || result == DBNull.Value? (DateTime?) null : Convert.ToDateTime(result);
        }

        public static decimal? ReadDecimal(IDataRecord record, string name)
        {
            var fieldIndex = record.GetOrdinal(name);

            if (record.IsDBNull(fieldIndex))
            {
                return null;
            }

            var result = record.GetDecimal(fieldIndex);
            return result;
        }

        public static string ReadString(IDataRecord record, string name)
        {
            return ReadObject(record, name) as string;
        }

        public static bool? ReadBoolean(IDataRecord record, string name)
        {
            return ToBoolean(ReadString(record, name));
        }

        public static bool? ToBoolean(string value)
        {
            switch (value)
            {
                case "T":
                    return true;
                case "F":
                    return false;
            }

            return null;
        }

        public static string ToString(bool? value)
        {
            if (value == null)
            {
                return null;
            }

            return value.Value? "T" : "F";
        }
    }
}