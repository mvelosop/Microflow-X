using Autofac;
using Autofac.Extensions.DependencyInjection;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Logging;
using Serilog;
using Tenants.Infrastructure.Setup;

namespace Tenants.Specs
{
    public class Startup
    {
        public const string ContainerKey = "Container";
        public const string ScopeKey = "Scope";

        private static readonly object Lock = new object();
        private static Startup _startup;

        public Startup()
        {
            Configuration = SetupConfiguration();

            IServiceCollection services = new ServiceCollection();

            ConfigureLogging(services);
            ConfigureServices(services);
        }

        private void ConfigureLogging(IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.ConfigurationSection(Configuration.GetSection("Logging"))
                .WriteTo.Console()
                .WriteTo.Seq("http://localhost:5341/")
                .CreateLogger();

            services.AddLogging(builder => builder.AddSerilog());
        }

        public IContainer Container { get; private set; }

        public TenantsDbSetup DbSetup { get; private set; }

        public IConfigurationRoot Configuration { get; }

        public static Startup Create()
        {
            lock (Lock)
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
            MediatorSetup.ConfigureMediator(builder);

            builder.RegisterModule(new TenantsContainerSetup(DbSetup));
        }

        private TenantsDbSetup ConfigureDabatase()
        {
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];

            var dbSetup = new TenantsDbSetup(connectionString);

            dbSetup.ConfigureDatabase(migrateDatabase: true);

            return dbSetup;
        }

        private IConfigurationRoot SetupConfiguration()
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

            var builder = new ContainerBuilder();

            builder.Populate(services);

            ConfigureContainer(builder);

            Container = builder.Build();
        }
    }
}