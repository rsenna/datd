namespace Dataweb.Dilab.Model.Wcf
{
    public static class ConfigHelper
    {
        private static string modelAssemblyName;

        public static string ModelAssemblyName
        {
            get
            {
                if (modelAssemblyName == null)
                {
                    modelAssemblyName = System.Configuration.ConfigurationManager.AppSettings["modelAssembly"];
                }

                return modelAssemblyName;
            }
        }
    }
}
