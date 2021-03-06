﻿using Autofac;
using FluentValidation;

namespace Tenants.Infrastructure.Setup
{
    public class TenantsContainerSetup : Module
    {
        private readonly TenantsDbSetup _dbSetup;

        public TenantsContainerSetup(TenantsDbSetup dbSetup)
        {
            _dbSetup = dbSetup;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => _dbSetup.CreateDbContext())
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(t => t.Name.EndsWith("Validator"))
                .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
                .AsImplementedInterfaces()
                .InstancePerDependency();

            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(t => t.Name.EndsWith("Command") || t.Name.EndsWith("Query"))
                .AsImplementedInterfaces()
                .InstancePerDependency();

            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(t => t.Name.EndsWith("CommandHandler") || t.Name.EndsWith("QueryHandler"))
                .AsImplementedInterfaces()
                .InstancePerDependency();
        }
    }
}