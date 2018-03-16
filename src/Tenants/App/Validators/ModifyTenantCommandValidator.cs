using FluentValidation;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tenants.App.Commands;
using Tenants.Core.Repositories;

namespace Tenants.App.Validators
{
    public class ModifyTenantCommandValidator : TenantDataCommandValidator<ModifyTenantCommand>
    {
        private readonly ITenantRepository _repo;

        public ModifyTenantCommandValidator(ITenantRepository repo)
        {
            _repo = repo;

            RuleFor(c => c.Id).NotEmpty();
            RuleFor(c => c.UpdateToken).NotEmpty();
            RuleFor(c => c.UpdateToken).Must(NotBeEmpty).WithMessage("'UpdateToken' should not be empty.");
            RuleFor(c => c.Email).MustAsync(NotExist).WithMessage("'Email' should not exist.");
        }

        private bool NotBeEmpty(byte[] updateToken)
        {
            return updateToken.Length > 0 && updateToken.Any(v => v > 0);
        }

        private async Task<bool> NotExist(ModifyTenantCommand command, string email, CancellationToken cancellationToken)
        {
            return await _repo.FindByEmailAsync(email, command.Id) == null;
        }
    }
}