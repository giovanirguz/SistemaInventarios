using System.Collections.Generic;
using System.IO;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Metadata;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using SharpArch.Data.NHibernate;
using SharpArch.Testing.NHibernate;
using SistemaInventarios.Data.NHibernateMaps;

namespace SistemaInventarios.Tests.NhibernateMaps
{
    [TestFixture]
    [System.ComponentModel.Category("DB Tests")]
    public class MappingIntegrationTests
    {
        [SetUp]
        public virtual void SetUp()
        {
            NHibernateSession.SessionFactory = null;
            NHibernateSession.Storage = null;

            string[] mappingAssemblies = RepositoryTestsHelper.GetMappingAssemblies();
            NHibernateSession.Init(new SimpleSessionStorage(factoryKey), mappingAssemblies,
                new AutoPersistenceModelGenerator().Generate(), LIVE_DB_CONFIG_PATH);
        }

        [Test]
        public void CanConfirmDatabaseMatchesMappings()
        {
            IDictionary<string, IClassMetadata> allClassMetadata =
                NHibernateSession.SessionFactories[factoryKey].GetAllClassMetadata();

            foreach (KeyValuePair<string, IClassMetadata> entry in allClassMetadata)
            {
                NHibernateSession.CurrentFor(factoryKey).CreateCriteria(entry.Value.GetMappedClass(EntityMode.Poco))
                     .SetMaxResults(0).List();
            }
        }

        [Test]
        public void WriteMappingsToFile()
        {
            var mappings = new AutoPersistenceModelGenerator().Generate();
            mappings.CompileMappings();
            mappings.WriteMappingsTo(DB_FOLDER_PATH);
        }

        [Test]
        public void ExportSchemaToFile()
        {
            PrepareSchemaExport(true).Execute(true, false, false, null, new StreamWriter(DB_FOLDER_PATH + "DbSchema.sql"));
        }

        [TearDown]
        public virtual void TearDown()
        {
            if (NHibernateSession.Storages[factoryKey].Session != null)
            {
                NHibernateSession.Storages[factoryKey].Session.Dispose();
            }

            NHibernateSession.SessionFactory = null;
            NHibernateSession.Storage = null;
        }

        private static Configuration GetDatabaseCfg(bool useLiveDb)
        {
            var mappingAssemblies = RepositoryTestsHelper.GetMappingAssemblies();
            return useLiveDb
                       ? NHibernateSession.Init(new SimpleSessionStorage(), mappingAssemblies,
                                                new AutoPersistenceModelGenerator().Generate(),
                                                LIVE_DB_CONFIG_PATH)
                       :
                           NHibernateSession.Init(new SimpleSessionStorage(), mappingAssemblies,
                                                  new AutoPersistenceModelGenerator().Generate());
        }

        private static SchemaExport PrepareSchemaExport(bool useLiveDb)
        {
            return new SchemaExport(GetDatabaseCfg(useLiveDb));
        }

        private static SchemaUpdate PrepareSchemaUpdate(bool useLiveDb)
        {
            return new SchemaUpdate(GetDatabaseCfg(useLiveDb));
        }

        private const string LIVE_DB_CONFIG_PATH = "../../../../SistemaInventarios/SistemaInventarios.UI/NHibernate.config";
        //private const string LIVE_VAL_CONFIG_PATH = "../../../../app/DI.Colef.Sia.Web/NHvalidator.config";
        private const string DB_FOLDER_PATH = "../../../../SistemaInventarios/db/";

        string factoryKey = "nhibernate.tests_using_live_database";
    }
}