using FluentValidation;
using System.Linq;
using Tenants.App.Commands;

namespace Tenants.App.Validators
{
    public class RemoveTenantCommandValidator : AbstractValidator<RemoveTenantCommand>
    {
        public RemoveTenantCommandValidator()
        {
            RuleFor(c => c.Id).NotEmpty();
            RuleFor(c => c.ConcurrencyToken).Must(NotBeEmpty).WithMessage($"'ConcurrencyToken' should not be empty.");
        }

        private bool NotBeEmpty(byte[] concurrencyToken)
        {
            return concurrencyToken.Length > 0 && concurrencyToken.Any(v => v > 0);
        }
    }
}