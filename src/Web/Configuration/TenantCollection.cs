using System.Configuration;

namespace Dataweb.Dilab.Web.Configuration
{
    [ConfigurationCollection(typeof(TenantElement), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
    public class TenantCollection : ConfigurationElementCollection
    {
        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.AddRemoveClearMap; }
        }

        public TenantElement this[int index]
        {
            get { return (TenantElement)BaseGet(index); }

            set
            {
                if (BaseGet(index) != null)
                    BaseRemoveAt(index);
                BaseAdd(index, value);
            }
        }

        public void Add(TenantElement element)
        {
            BaseAdd(element);
        }

        public void Clear()
        {
            BaseClear();
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new TenantElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((TenantElement)element).Name;
        }

        public void Remove(TenantElement element)
        {
            BaseRemove(element.Name);
        }

        public void Remove(string name)
        {
            BaseRemove(name);
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }
    }
}