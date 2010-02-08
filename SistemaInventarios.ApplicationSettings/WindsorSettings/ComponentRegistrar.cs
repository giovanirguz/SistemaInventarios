using System.Collections.Generic;
using System.Collections.ObjectModel;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using SharpArch.Core.CommonValidator;
using SharpArch.Core.NHibernateValidator.CommonValidatorAdapter;
using SharpArch.Core.PersistenceSupport;
using SharpArch.Core.PersistenceSupport.NHibernate;
using SharpArch.Data.NHibernate;
using SharpArch.Web.Castle;

namespace SistemaInventarios.ApplicationSettings.WindsorSettings
{
    public class ComponentRegistrar
    {
        public static void AddComponentsTo(IWindsorContainer container)
        {
            AddGenericRepositoriesTo(container);
            AddCustomRepositoriesTo(container);
            AddApplicationServicesTo(container);
            AddMappersTo(container);

            container.AddComponent("validator",
                                   typeof(IValidator), typeof(Validator));
            container.AddComponent("validationResult",
                                   typeof (IValidationResult), typeof (ValidationResult));
            container.AddComponent("collection",
                                   typeof (ICollection<>), typeof (Collection<>));

        }

        private static void AddApplicationServicesTo(IWindsorContainer container)
        {
            container.Register(
                AllTypes.Pick()
                    .FromAssemblyNamed("SistemaInventarios.ApplicationServices")
                    .WithService.FirstInterface());
        }

        private static void AddMappersTo(IWindsorContainer container)
        {
            container.Register(
                AllTypes.Pick()
                    .FromAssemblyNamed("SistemaInventarios.UI")
                    .If(x => x.Name.EndsWith("Mapper"))
                    .WithService.FirstNonGenericCoreInterface("SistemaInventarios.UI.Mappers"));
        }

        private static void AddCustomRepositoriesTo(IWindsorContainer container)
        {
            container.Register(
                AllTypes.Pick()
                    .FromAssemblyNamed("SistemaInventarios.Data")
                    .WithService.FirstNonGenericCoreInterface("SistemaInventarios.Model"));
        }

        private static void AddGenericRepositoriesTo(IWindsorContainer container)
        {
            container.AddComponent("entityDuplicateChecker",
                                   typeof(IEntityDuplicateChecker), typeof(EntityDuplicateChecker));
            container.AddComponent("repositoryType",
                                   typeof(IRepository<>), typeof(Repository<>));
            container.AddComponent("nhibernateRepositoryType",
                                   typeof(INHibernateRepository<>), typeof(NHibernateRepository<>));
            container.AddComponent("repositoryWithTypedId",
                                   typeof(IRepositoryWithTypedId<,>), typeof(RepositoryWithTypedId<,>));
            container.AddComponent("nhibernateRepositoryWithTypedId",
                                   typeof(INHibernateRepositoryWithTypedId<,>), typeof(NHibernateRepositoryWithTypedId<,>));
        }
    }
}