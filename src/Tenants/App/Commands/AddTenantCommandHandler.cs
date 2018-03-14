using Domion.Base;
using MediatR;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Tenants.Core.Model;
using Tenants.Core.Repositories;

namespace Tenants.App.Commands
{
    public class AddTenantCommandHandler : IRequestHandler<AddTenantCommand, CommandResult<Tenant>>
    {
        private readonly ITenantRepository _repo;
        private readonly ILogger<AddTenantCommandHandler> _logger;

        public AddTenantCommandHandler(ITenantRepository repo, ILogger<AddTenantCommandHandler> logger)
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

            _logger.LogInformation($"----- {nameof(AddTenantCommandHandler)}");

            List<ValidationResult> validationResults = await _repo.TryInsertAsync(entity);

            if (validationResults.Any()) return new CommandResult<Tenant>(validationResults);

            await _repo.SaveChangesAsync();

            return new CommandResult<Tenant>(entity);
        }
    }
}