using System.Configuration;

namespace Dataweb.Dilab.Web.Configuration
{
    public class TenantElement : ConfigurationElement
    {
        [ConfigurationProperty("Name", IsRequired = true)]
        public string Name
        {
            get { return (string)this["Name"]; }
            set { this["Name"] = value; }
        }

        [ConfigurationProperty("Url", IsRequired = true)]
        public string Url
        {
            get { return (string)this["Url"]; }
            set { this["Url"] = value; }
        }
    }
}