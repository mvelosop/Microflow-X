using Autofac;
using Autofac.Extensions.DependencyInjection;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Tenants.Setup;

namespace Tenants.Specs
{
    public class Startup
    {
        public const string ContainerKey = "Container";
        public const string ScopeKey = "Scope";

        private static object _lock = new object();
        private static Startup _startup;

        public Startup()
        {
            Options = ConfigureOptions();

            IServiceCollection services = new ServiceCollection();

            ConfigureServices(services);
        }

        public IContainer Container { get; private set; }

        public TenantsDbSetup DbSetup { get; private set; }

        public IConfigurationRoot Options { get; }

        public static Startup Create()
        {
            lock (_lock)
            {
                if (_startup == null)
                {
                    _startup = new Startup();
                }
            }

            return _startup;
        }

        private void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new TenantsContainerSetup(DbSetup));

            //builder.RegisterAssemblyTypes(GetType().Assembly)
            //    .Where(t => t.Name.EndsWith("Mapper"));
        }

        private TenantsDbSetup ConfigureDabatase()
        {
            string connectionString = Options["ConnectionStrings:DefaultConnection"];

            var dbSetup = new TenantsDbSetup(connectionString);

            dbSetup.ConfigureDatabase(migrateDatabase: true);

            return dbSetup;
        }

        private void ConfigureMediator(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();

            //builder
            //    .RegisterType<Mediator>()
            //    .As<IMediator>()
            //    .InstancePerLifetimeScope();

            // It appears Autofac returns the last registered types first

            //builder.RegisterGeneric(typeof(RequestPostProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            //builder.RegisterGeneric(typeof(RequestPreProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            //builder.RegisterGeneric(typeof(GenericRequestPreProcessor<>)).As(typeof(IRequestPreProcessor<>));
            //builder.RegisterGeneric(typeof(GenericRequestPostProcessor<,>)).As(typeof(IRequestPostProcessor<,>));
            //builder.RegisterGeneric(typeof(GenericPipelineBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            //builder.RegisterGeneric(typeof(ConstrainedRequestPostProcessor<,>)).As(typeof(IRequestPostProcessor<,>));
            //builder.RegisterGeneric(typeof(ConstrainedPingedHandler<>)).As(typeof(INotificationHandler<>));

            //builder.Register<SingleInstanceFactory>(ctx =>
            //{
            //    var c = ctx.Resolve<IComponentContext>();

            //    return t => c.Resolve(t);
            //});

            //builder.Register<MultiInstanceFactory>(ctx =>
            //{
            //    var c = ctx.Resolve<IComponentContext>();

            //    return t => (IEnumerable<object>)c.Resolve(typeof(IEnumerable<>).MakeGenericType(t));
            //});

            // request handlers
            builder
                .Register<SingleInstanceFactory>(ctx =>
                {
                    var c = ctx.Resolve<IComponentContext>();
                    return t => { object o; return c.TryResolve(t, out o) ? o : null; };
                })
                .InstancePerLifetimeScope();

            // notification handlers
            builder
                .Register<MultiInstanceFactory>(ctx =>
                {
                    var c = ctx.Resolve<IComponentContext>();
                    return t => (IEnumerable<object>)c.Resolve(typeof(IEnumerable<>).MakeGenericType(t));
                })
                .InstancePerLifetimeScope();
        }

        private IConfigurationRoot ConfigureOptions()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath))
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            builder.AddEnvironmentVariables();

            IConfigurationRoot configuration = builder.Build();

            return configuration;
        }

        private void ConfigureServices(IServiceCollection services)
        {
            DbSetup = ConfigureDabatase();

            //services.AddMediatR();

            var builder = new ContainerBuilder();

            builder.Populate(services);

            ConfigureMediator(builder);
            ConfigureContainer(builder);

            Container = builder.Build();
        }
    }
}