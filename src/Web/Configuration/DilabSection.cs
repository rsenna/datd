using System.Configuration;

namespace Dataweb.Dilab.Web.Configuration
{
    public class DilabSection : ConfigurationSection
    {
        [ConfigurationProperty("Tenants")]
        public TenantCollection Tenants
        {
            get { return (TenantCollection)base["Tenants"]; }
        }
    }
}