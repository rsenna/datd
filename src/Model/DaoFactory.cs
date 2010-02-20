using System;

namespace Dataweb.Dilab.Model
{
    public class DaoFactory
    {
        public static string AssemblyName { get; set; }

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

            return default(T);
        }
    }
}
