using FluentValidation;
using System.Threading;
using System.Threading.Tasks;
using Tenants.App.Commands;
using Tenants.Core.Repositories;

namespace Tenants.App.Validators
{
    public class AddTenantCommandValidator : TenantDataCommandValidator<AddTenantCommand>
    {
        private readonly ITenantRepository _repo;

        public AddTenantCommandValidator(ITenantRepository repo)
        {
            _repo = repo;

            RuleFor(c => c.Email).MustAsync(NotExist).WithMessage(@"'Email' should not exist.");
        }

        private async Task<bool> NotExist(string email, CancellationToken cancellationToken)
        {
            return await _repo.FindByEmailAsync(email) == null;
        }
    }
}