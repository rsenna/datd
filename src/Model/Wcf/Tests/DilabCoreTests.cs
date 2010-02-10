using System;
using System.Configuration;
using System.Data;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using NUnit.Framework;

namespace Dataweb.Dilab.Model.Wcf.Tests
{
    [TestFixture]
    public class DilabCoreTests
    {
        // https://blogs.msdn.com/mrtechnocal/archive/2009/11/09/hacking-connection-string-settings.aspx
        private static FieldInfo elementField;
        private static FieldInfo collectionField;

        private static void EnsureFieldInfo()
        {
            if (elementField != null)
            {
                return;
            }

            new ReflectionPermission(ReflectionPermissionFlag.MemberAccess | ReflectionPermissionFlag.RestrictedMemberAccess).Assert();

            const BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;

            try
            {
                elementField = typeof (ConfigurationElement).GetField("_bReadOnly", flags);
                collectionField = typeof (ConfigurationElementCollection).GetField("bReadOnly", flags);
            }
            finally
            {
                CodeAccessPermission.RevertAssert();
            }

            if (elementField == null)
            {
                throw new MemberAccessException(string.Format(null, "The operation to access field '{0}' failed.", "_bReadOnly"));
            }

            if (collectionField == null)
            {
                throw new MemberAccessException(string.Format(null, "The operation to access field '{0}' failed.", "bReadOnly"));
            }
        }

        private static void SetElementIsReadOnly(ConfigurationElement element, bool readOnly)
        {
            elementField.SetValue(element, readOnly);
        }

        private static void SetCollectionIsReadOnly(ConfigurationElementCollection collection, bool readOnly)
        {
            SetElementIsReadOnly(collection, readOnly);
            collectionField.SetValue(collection, readOnly);
        }

        private static void AddConnectionString(ConnectionStringSettings settings)
        {
            if (!ConfigurationManager.ConnectionStrings.IsReadOnly())
            {
                ConfigurationManager.ConnectionStrings.Add(settings);
                return;
            }

            EnsureFieldInfo();
            new ReflectionPermission(ReflectionPermissionFlag.MemberAccess | ReflectionPermissionFlag.RestrictedMemberAccess).Assert();
            try
            {
                SetCollectionIsReadOnly(ConfigurationManager.ConnectionStrings, false);
                ConfigurationManager.ConnectionStrings.Add(settings);
                SetCollectionIsReadOnly(ConfigurationManager.ConnectionStrings, true);
            }
            finally
            {
                CodeAccessPermission.RevertAssert();
            }
        }

        [TestFixtureSetUp]
        public void Setup()
        {
            // Fake a .config file
            var dataSet = ConfigurationManager.GetSection("system.data") as DataSet;
            if (dataSet != null)
            {
                dataSet.Tables[0].Rows.Add(
                    "FirebirdClient Data Provider",
                    ".Net Framework Data Provider for Firebird",
                    "FirebirdSql.Data.FirebirdClient",
                    "FirebirdSql.Data.FirebirdClient.FirebirdClientFactory, FirebirdSql.Data.FirebirdClient, Version=2.5.1.0, Culture=neutral, PublicKeyToken=3750abcc3150b00c"
                );
            }

            AddConnectionString(new ConnectionStringSettings("DilabDatabase",
                @"server=SERVER;database=D:\Share\Firebird\Sturmer.dataweb;username=SYSDBA;password=masterkey",
                "FirebirdSql.Data.FirebirdClient"));
        }

        [Test]
        public void CanCheckPassword()
        {
            var service = new ClienteService();
            Assert.IsNull(service.FindByLogin("0"));

            var userById = service.FindByLogin("1630");
            Assert.IsNotNull(userById);
            Assert.AreEqual(userById.Identificador, 1630);
            Assert.AreEqual(userById.Cnpj, "05859449000123");

            var userByCnpj = service.FindByLogin("5859449000123");
            Assert.IsNotNull(userByCnpj);
            Assert.AreEqual(userByCnpj.Identificador, 1630);
            Assert.AreEqual(userByCnpj.Cnpj, "05859449000123");
        }

        [Test]
        public void CanGetByLogin()
        {
            var service = new ClienteService();

            //Console.WriteLine(service.GetConnectionString());
        }
    }
}