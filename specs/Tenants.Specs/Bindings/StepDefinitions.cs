using Autofac;
using Domion.Base;
using Domion.Testing.Assertions;
using FluentAssertions;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Tenants.App.Base;
using Tenants.App.Commands;
using Tenants.App.Queries;
using Tenants.Core.Model;
using Tenants.Core.Repositories;
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
            var command = new AddTenantCommand(new TenantData(email, name));

            CommandResult<Tenant> result = await GetTenantCommandResult(command);

            result.Succeeded.Should().BeFalse();
            result.ValidationResults.Should().ContainErrorMessage(TenantRepository.DuplicateByEmailError);
        }

        [Then(@"I get error ""(.*)"" when I try to add these tenants:")]
        public async Task ThenIGetErrorWhenITryToAddTheseTenants(string errorMessage, Table table)
        {
            var dataList = table.CreateSet<TenantData>();

            foreach (var data in dataList)
            {
                var command = new AddTenantCommand(data);

                CommandResult<Tenant> result = await GetTenantCommandResult(command);

                result.Succeeded.Should().BeFalse();
                result.ValidationMessages.Should().Contain(errorMessage);
            }
        }

        [Then(@"I get error ""(.*)"" when I try to modify tenants like so:")]
        public async Task ThenIGetErrorWhenITryToModifyTenantsLikeSo(string errorMessage, Table table)
        {
            var dataList = table.CreateSet<TenantSpecData>();

            var repo = Resolve<ITenantRepository>();

            foreach (TenantSpecData data in dataList)
            {
                var entity = await repo.FindByEmailAsync(data.FindEmail);

                var command = new ModifyTenantCommand(entity.Id, data, entity.UpdateToken);

                CommandResult<Tenant> result = await GetTenantCommandResult(command);

                result.Succeeded.Should().BeFalse();
                result.ValidationMessages.Should().Contain(errorMessage);
            }
        }

        [Then(@"I get error ""(.*)"" when I try to modify tenants without control properties like so:")]
        public async Task ThenIGetErrorWhenITryToModifyTenantsWithoutControlPropertiesLikeSo(string errorMessage, Table table)
        {
            var dataList = table.CreateSet<TenantSpecData>();

            foreach (TenantSpecData data in dataList)
            {
                var updateToken = new byte[8];

                var command = new ModifyTenantCommand(Guid.Empty, data, updateToken);

                CommandResult<Tenant> result = await GetTenantCommandResult(command);

                result.Succeeded.Should().BeFalse();
                result.ValidationMessages.Should().Contain(errorMessage);
            }
        }

        [Then(@"I get error ""(.*)"" when I try to remove tenants without control properties like so:")]
        public async Task ThenIGetErrorWhenITryToRemoveTenantsWithoutControlPropertiesLikeSo(string errorMessage, Table table)
        {
            var dataList = table.CreateSet<TenantSpecData>();

            foreach (TenantSpecData data in dataList)
            {
                var updateToken = new byte[8];

                var command = new RemoveTenantCommand(Guid.Empty, updateToken);

                CommandResult result = await GetTenantCommandResult(command);

                result.Succeeded.Should().BeFalse();
                result.ValidationMessages.Should().Contain(errorMessage);
            }
        }

        [Then(@"I get error ""(.*)"" when trying to modify tenant's email from ""(.*)"" to ""(.*)"":")]
        public async Task ThenIGetWhenTryingToModifyTenantSEmailFromTo(string errorMessage, string findEmail, string modifyEmail)
        {
            string errorText = GetErrorText(errorMessage);

            var repo = Resolve<ITenantRepository>();

            var entity = await repo.FindByEmailAsync(findEmail);

            entity.Should().NotBeNull();

            var data = TenantData.From(entity);
            data.Email = modifyEmail;

            var command = new ModifyTenantCommand(entity.Id, data, entity.UpdateToken);

            CommandResult<Tenant> result = await GetTenantCommandResult(command);

            result.Succeeded.Should().BeFalse();
            result.ValidationResults.Should().ContainErrorMessage(errorText);
        }

        [Then(@"when querying for ""(.*)"" tenants I get these:")]
        public async Task ThenWhenQueryingForTenantsIGetThese(string name, Table table)
        {
            var mediator = Resolve<IMediator>();

            var list = await mediator.Send(new GetTenantListQuery(name));

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

                result.ValidationResults.Should().BeEmpty();
                result.Succeeded.Should().BeTrue();
            }
        }

        [When(@"I remove these tenants:")]
        public async Task WhenIRemoveTheseTenants(Table table)
        {
            var dataList = table.CreateSet<TenantSpecData>();

            var repo = Resolve<ITenantRepository>();

            var mediator = Resolve<IMediator>();

            foreach (TenantSpecData data in dataList)
            {
                var entity = await repo.FindByEmailAsync(data.FindEmail);

                var result = await mediator.Send(new RemoveTenantCommand(entity.Id, entity.UpdateToken));

                result.ValidationResults.Should().BeEmpty();
                result.Succeeded.Should().BeTrue();
            }
        }

        private async Task<Tenant> AddTenantAsync(TenantData data)
        {
            var command = new AddTenantCommand(data);

            CommandResult<Tenant> result = await GetTenantCommandResult(command);

            result.ValidationResults.Should().BeEmpty();
            result.Succeeded.Should().BeTrue();

            return result.Value;
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

        private string GetErrorText(string errorMessage)
        {
            var propInfo = typeof(TenantRepository).GetField(errorMessage);

            return (string)propInfo.GetValue(null);
        }

        private async Task<CommandResult> GetTenantCommandResult(IRequest<CommandResult> request)
        {
            var mediator = Resolve<IMediator>();

            var response = await mediator.Send(request);

            return response;
        }

        private async Task<CommandResult<Tenant>> GetTenantCommandResult(IRequest<CommandResult<Tenant>> request)
        {
            var mediator = Resolve<IMediator>();

            var response = await mediator.Send(request);

            return response;
        }

        private T Resolve<T>() where T : class
        {
            return _scenarioContext.Get<ILifetimeScope>(Startup.ScopeKey)?.Resolve<T>();
        }
    }
}