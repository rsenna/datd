using System;

namespace Dataweb.Dilab.Model
{
    public class DaoFactory
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

        static DaoFactory()
        {
            assemblyName = ASSEMBLY_NAME_DEFAULT;
        }

        public static T CreateDao<T>() where T : IDataAccessBase
        {
            var interfaceName = typeof(T).Name;
            var className = interfaceName.Remove(0, 1);

            var fullClassName = string.Format("{0}.DataAccess.{1}", AssemblyName, className);
            var objectHandle = Activator.CreateInstance(AssemblyName, fullClassName);

            if (objectHandle != null)
            {
                var instance = objectHandle.Unwrap();

                if (instance != null && instance is T)
                {
                    return (T) instance;
                }
            }

            throw new ArgumentException("Tipo de DAO desconhecido.", interfaceName);
        }
    }
}
