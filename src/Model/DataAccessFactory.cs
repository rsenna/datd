using System;

namespace Dataweb.Dilab.Model
{
    public class DataAccessFactory
    {
        private static string assemblyName;

        public static string AssemblyName
        {
            get { return assemblyName; }

            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    assemblyName = value;
                }
            }
        }

        private const string ASSEMBLY_NAME_DEFAULT = "Dataweb.Dilab.Model.Ado";

        static DataAccessFactory()
        {
            assemblyName = ASSEMBLY_NAME_DEFAULT;
        }

        private static object CreateDataAccessObject(string className)
        {
            var fullClassName = string.Format("{0}.{1}", AssemblyName, className);
            var objectHandle = Activator.CreateInstance(AssemblyName, fullClassName);

            if (objectHandle != null)
            {
                var instance = objectHandle.Unwrap();
                return instance;
            }

            return null;
        }

        public static T CreateDao<T>(ISession session) where T : IDataAccessBase
        {
            var interfaceName = typeof(T).Name;
            var className = interfaceName.Remove(0, 1);
            var instance = CreateDataAccessObject(string.Format("DataAccess." + className));

            if (instance != null && instance is T)
            {
                ((T) instance).Session = session;
                return (T) instance;
            }

            throw new ArgumentException("Tipo de DAO desconhecido.", interfaceName);
        }

        public static ISession CreateSession()
        {
            return (ISession) CreateDataAccessObject("Session");
        }
    }
}
