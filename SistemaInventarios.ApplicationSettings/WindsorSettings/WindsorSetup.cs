using Castle.Windsor;
using CommonServiceLocator.WindsorAdapter;
using Microsoft.Practices.ServiceLocation;

namespace SistemaInventarios.ApplicationSettings.WindsorSettings
{
    public class WindsorSetup
    {
        public void InitializeInterfaces()
        {
            InitializeServiceLocator();
        }

        /// <summary>
        /// Instantiate the container and add all Controllers that derive from 
        /// WindsorController to the container.  Also associate the Controller 
        /// with the WindsorContainer ControllerFactory.
        /// </summary>
        protected virtual void InitializeServiceLocator()
        {
            IWindsorContainer container = new WindsorContainer();
            ComponentRegistrar.AddComponentsTo(container);

            ServiceLocator.SetLocatorProvider(() => new WindsorServiceLocator(container));
        }
    }
}