using Domion.Base;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Tenants.Core.Model;
using Tenants.Core.Repositories;

namespace Tenants.App.Commands
{
    public class ModifyTenantCommandHandler : IRequestHandler<ModifyTenantCommand, CommandResult<Tenant>>
    {
        private readonly ITenantRepository _repo;

        public ModifyTenantCommandHandler(ITenantRepository repo)
        {
            _repo = repo;
        }

        public async Task<CommandResult<Tenant>> Handle(ModifyTenantCommand request, CancellationToken cancellationToken)
        {
            Tenant entity = await _repo.FindByIdAsync(request.Id);

            if (entity == null) return new CommandResult<Tenant>(new InvalidOperationException($"Tenant not found (Id={request.Id})"));

            entity.Email = request.Email;
            entity.Name = request.Name;
            entity.UpdateToken = request.UpdateToken;

            _repo.Update(entity);

            await _repo.SaveChangesAsync();

            return new CommandResult<Tenant>(entity);
        }
    }
}