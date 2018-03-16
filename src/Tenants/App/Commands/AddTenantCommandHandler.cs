using Domion.Base;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tenants.Core.Model;
using Tenants.Core.Repositories;

namespace Tenants.App.Commands
{
    public class AddTenantCommandHandler : IRequestHandler<AddTenantCommand, CommandResult<Tenant>>
    {
        private readonly ILogger<AddTenantCommandHandler> _logger;
        private readonly ITenantRepository _repo;

        public AddTenantCommandHandler(
            ITenantRepository repo,
            ILogger<AddTenantCommandHandler> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<CommandResult<Tenant>> Handle(AddTenantCommand request, CancellationToken cancellationToken)
        {
            var entity = new Tenant
            {
                Email = request.Email,
                Name = request.Name
            };

            Debugger.Break();

            List<ValidationResult> validationResults = await _repo.TryInsertAsync(entity);

            if (validationResults.Any()) return new CommandResult<Tenant>(validationResults);

            await _repo.SaveChangesAsync();

            return new CommandResult<Tenant>(entity);
        }
    }
}