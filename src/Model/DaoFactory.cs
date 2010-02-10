using System;

namespace Dataweb.Dilab.Model
{
    public class DaoFactory
    {
        // TODO: ASSEMBLY_NAME deveria vir de um arquivo de configuração.
        private const string ASSEMBLY_NAME = "Dataweb.Dilab.Model.Ado";

        public static T CreateDao<T>() where T : IDataAccessBase
        {
            var interfaceName = typeof(T).Name;
            var className = interfaceName.Remove(0, 1);
            var fullClassName = string.Format("{0}.DataAccess.{1}", ASSEMBLY_NAME, className);
            var objectHandle = Activator.CreateInstance(ASSEMBLY_NAME, fullClassName);

            if (objectHandle != null)
            {
                var instance = objectHandle.Unwrap();

                if (instance != null && instance is T)
                {
                    return (T) instance;
                }
            }

            return default(T);
        }
    }
}
