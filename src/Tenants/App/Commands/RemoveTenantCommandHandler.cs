using Domion.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tenants.Core.Model;
using Tenants.Core.Repositories;

namespace Tenants.App.Commands
{
    public class RemoveTenantCommandHandler : IRequestHandler<RemoveTenantCommand, CommandResult>
    {
        private readonly ITenantRepository _repo;

        public RemoveTenantCommandHandler(ITenantRepository repo)
        {
            _repo = repo;
        }

        public async Task<CommandResult> Handle(RemoveTenantCommand request, CancellationToken cancellationToken)
        {
            Tenant entity = await _repo.FindByIdAsync(request.Id);

            if (entity == null) return new CommandResult(new InvalidOperationException($"Tenant not found (Id={request.Id})"));

            entity.UpdateToken = request.UpdateToken;

            List<ValidationResult> validationResults = await _repo.TryDeleteAsync(entity);

            if (validationResults.Any()) return new CommandResult(validationResults);

            await _repo.SaveChangesAsync();

            return new CommandResult(true);
        }
    }
}