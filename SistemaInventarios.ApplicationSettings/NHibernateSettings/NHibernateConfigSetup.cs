using System.IO;
using System.Reflection;
using SharpArch.Data.NHibernate;
using SistemaInventarios.Data.NHibernateMaps;
using SistemaInventarios.Data.NHibernateMaps.Conventions;

namespace SistemaInventarios.ApplicationSettings.NHibernateSettings
{
    public class NHibernateConfigSetup
    {
        private const string directoryPath = "C:\\NHibernateSettings\\";
        private const string nHibernateFileName = "NHibernate.config";
        private const string nHibernateValidatiorFileName = "NHvalidator.config";
        private const string nhibernateConfigFilePath = directoryPath + nHibernateFileName;
        private const string nHibernateValidatorFilePath = directoryPath + nHibernateValidatiorFileName;

        public bool VerifyNHConfigFileExist()
        {
            Directory.CreateDirectory(directoryPath);

            return File.Exists(directoryPath + nHibernateFileName);
        }

        public void InitializeNHibernate()
        {
            NHibernateInitializer.Instance().InitializeNHibernateOnce(
                InitializeNHibernateSession);
        }

        public void InitializeNHibernateSession()
        {
            NHibernateSession.SessionFactory = null;
            NHibernateSession.Storage = null;


            NHibernateSession.Init(new SimpleSessionStorage(),
                                   new[] { Assembly.GetAssembly(typeof(HasManyConvention)).Location },
                                   new AutoPersistenceModelGenerator().Generate(), nhibernateConfigFilePath, nHibernateValidatorFilePath);
        }
    }
}