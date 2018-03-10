using Autofac;
using Domion.Base;
using Domion.Testing.Assertions;
using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Tenants.App.Base;
using Tenants.App.Commands;
using Tenants.Core.Model;
using Tenants.Core.Repositories;
using Tenants.Data.Extensions;
using Tenants.Data.Repositories;
using Tenants.Specs.Helpers;

namespace Tenants.Specs.Bindings
{
    [Binding]
    public sealed class StepDefinitions
    {
        // For additional details on SpecFlow step definitions see http://go.specflow.org/doc-stepdef

        private readonly ScenarioContext _scenarioContext;

        public StepDefinitions(
            ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"tenant ""(.*)"" does not exist")]
        public async Task GivenTenantDoesNotExist(string email)
        {
            await EnsureTenantDoesNotExistAsync(email);
        }

        [Given(@"these tenants don't exist:")]
        public async Task GivenTheseTenantsDonTExist(Table table)
        {
            IEnumerable<Tenant> tenantList = table.CreateSet<Tenant>();

            foreach (var tenant in tenantList)
            {
                await EnsureTenantDoesNotExistAsync(tenant.Email);
            }
        }

        [Then(@"I can't add another tenant ""(.*)"", ""(.*)""")]
        public async Task ThenICanTAddAnotherTenant(string email, string name)
        {
            CommandResult<Tenant> result = await AddTenantCommandAsync(new TenantData(email, name));

            result.Succeeded.Should().BeFalse();
            result.ValidationResults.Should().ContainErrorMessage(TenantRepository.DuplicateByNameError);
        }

        [Then(@"when querying for ""(.*)"" tenants I get these:")]
        public async Task ThenWhenQueryingForTenantsIGetThese(string name, Table table)
        {
            var repo = Resolve<ITenantRepository>();

            var list = await repo.Query(t => t.Name.StartsWith(name)).ToListAsync();

            table.CompareToSet(list);
        }

        [When(@"I add tenant ""(.*)"", ""(.*)""")]
        public async Task WhenIAddTenant(string email, string name)
        {
            await AddTenantAsync(new TenantData(email, name));
        }

        [Given(@"I add tenants:")]
        [When(@"I add tenants:")]
        public async Task WhenIAddTenants(Table table)
        {
            var dataList = table.CreateSet<TenantData>();

            foreach (var data in dataList)
            {
                await AddTenantAsync(data);
            }
        }

        [When(@"I modify the tenants like so:")]
        public async Task WhenIModifyTheTenantsLikeSo(Table table)
        {
            var dataList = table.CreateSet<TenantSpecData>();

            var repo = Resolve<ITenantRepository>();

            var mediator = Resolve<IMediator>();

            foreach (TenantSpecData data in dataList)
            {
                var entity = await repo.FindByEmailAsync(data.FindEmail);

                var result = await mediator.Send(new ModifyTenantCommand(entity.Id, data, entity.UpdateToken));

                result.Succeeded.Should().BeTrue();
            }
        }

        private async Task<Tenant> AddTenantAsync(TenantData data)
        {
            CommandResult<Tenant> result = await AddTenantCommandAsync(data);

            result.ValidationResults.Should().BeEmpty();
            result.Succeeded.Should().BeTrue();

            return result.Value;
        }

        private Task<CommandResult<Tenant>> AddTenantCommandAsync(TenantData data)
        {
            var mediator = Resolve<IMediator>();

            return mediator.Send(new AddTenantCommand(data));
        }

        private async Task EnsureTenantDoesNotExistAsync(string email)
        {
            var repo = Resolve<ITenantRepository>();

            var tenant = await repo.FindByEmailAsync(email);

            if (tenant == null) return;

            List<ValidationResult> errors = await repo.TryDeleteAsync(tenant);

            errors.Should().BeEmpty();

            await repo.SaveChangesAsync();
        }

        private T Resolve<T>() where T : class
        {
            return _scenarioContext.Get<ILifetimeScope>(Startup.ScopeKey)?.Resolve<T>();
        }
    }
}