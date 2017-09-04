using System;
using Microsoft.Practices.Unity;
using Memento;
using Memento.Messaging;
using Memento.Messaging.Postie;
using Memento.Persistence;
using Memento.Persistence.EmbeddedRavenDB;
using Merp.Accountancy.CommandStack.Sagas;
using Merp.Accountancy.CommandStack.Services;
using Merp.Accountancy.QueryStack.Denormalizers;
using Merp.Web.Site.Areas.Accountancy.WorkerServices;
using Merp.Registry.CommandStack.Sagas;
using Merp.Registry.QueryStack.Denormalizers;
using Merp.Web.Site.Areas.Registry.WorkerServices;
using Raven.Database.Server;
using Raven.Client.Embedded;
using Memento.Messaging.Postie.Unity;

namespace Merp.Web.Site.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your types here
            container.RegisterType<ITypeResolver, UnityTypeResolver>(new InjectionConstructor(container));
            container.RegisterType<IBus, InMemoryBus>();
            container.RegisterType<IEventDispatcher, InMemoryBus>();
            container.RegisterType<IRepository, Memento.Persistence.Repository>(new InjectionConstructor(typeof(EmbeddedRavenDbEventStore)));

            NonAdminHttp.EnsureCanListenToWhenInNonAdminContext(8080);
            var documentStore = new EmbeddableDocumentStore
            {
                ConnectionStringName = "EventStore",
                UseEmbeddedHttpServer = true
            };
            documentStore.Configuration.Port = 8080;
            documentStore.Initialize();
            container.RegisterInstance(documentStore);
            container.RegisterType<IEventStore, Memento.Persistence.EmbeddedRavenDB.EmbeddedRavenDbEventStore>(new InjectionConstructor(typeof(EmbeddableDocumentStore), typeof(IEventDispatcher)));

            var bus = container.Resolve<IBus>();
            ConfigureAccountancyBoundedContext(container, bus);
            ConfigureRegistryBoundedContext(container, bus);
        }

        private static void ConfigureAccountancyBoundedContext(IUnityContainer container, IBus bus)
        {
            //Denormalizers
            bus.RegisterHandler<FixedPriceJobOrderDenormalizer>();
            bus.RegisterHandler<IncomingInvoiceDenormalizer>();
            bus.RegisterHandler<InvoiceDenormalizer>();
            bus.RegisterHandler<OutgoingInvoiceDenormalizer>();
            bus.RegisterHandler<TimeAndMaterialJobOrderDenormalizer>();

            //Handlers
            

            //Sagas
            bus.RegisterSaga<FixedPriceJobOrderSaga>();
            bus.RegisterSaga<IncomingInvoiceSaga>();
            bus.RegisterSaga<OutgoingInvoiceSaga>();
            bus.RegisterSaga<TimeAndMaterialJobOrderSaga>();

            //Services
            container.RegisterType<IJobOrderNumberGenerator, JobOrderNumberGenerator>();
            container.RegisterType<IOutgoingInvoiceNumberGenerator, OutgoingInvoiceNumberGenerator>();

            //Types
            container.RegisterType<Merp.Accountancy.QueryStack.IDatabase, Merp.Accountancy.QueryStack.Database>();

            //Worker Services
            container.RegisterType<InvoiceControllerWorkerServices, InvoiceControllerWorkerServices>();
            container.RegisterType<JobOrderControllerWorkerServices, JobOrderControllerWorkerServices>();
        }

        private static void ConfigureRegistryBoundedContext(IUnityContainer container, IBus bus)
        {
            //Denormalizers
            bus.RegisterHandler<PersonDenormalizer>();
            bus.RegisterHandler<CompanyDenormalizer>();

            //Handlers

            //Sagas
            bus.RegisterSaga<CompanySaga>();
            bus.RegisterSaga<PersonSaga>();

            //Types
            container.RegisterType<Merp.Registry.QueryStack.IDatabase, Merp.Registry.QueryStack.Database>();

            //Worker Services
            container.RegisterType<PersonControllerWorkerServices, PersonControllerWorkerServices>();
        }
    }
}
