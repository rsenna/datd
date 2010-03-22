using System;
using System.Reflection;

namespace Dataweb.Dilab.Model
{
    public static class DataAccessFactory
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

        private static T CreateModelObject<T>()
        {
            var asm = Assembly.Load(AssemblyName);
            var types = asm.GetTypes();

            foreach (var type in types)
            {
                if (type.IsClass && !type.IsAbstract && typeof(T).IsAssignableFrom(type))
                {
                    var instance = Activator.CreateInstance(type);

                    if (instance != null)
                    {
                        return (T) instance;
                    }
                }
            }

            throw new ArgumentException(string.Format("[{0}] é um tipo desconhecido.", typeof(T).Name));
        }

        public static T CreateDao<T>(ISession session, QueryDepth depth)
            where T : IDataAccessBase
        {
            try
            {
                var instance = CreateModelObject<T>();

                instance.Session = session;
                instance.Depth = depth;

                return instance;
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException("Tipo de DAO desconhecido.", typeof(T).Name, ex);
            }
        }

        public static T CreateDao<T>(IDataAccessBase parent) where T : IDataAccessBase
        {
            return CreateDao<T>(parent.Session, parent.GetDetailDepth());
        }

        public static ISession CreateSession()
        {
            return CreateModelObject<ISession>();
        }
    }
}
