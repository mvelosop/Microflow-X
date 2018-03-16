using FluentValidation;
using Tenants.App.Commands;

namespace Tenants.App.Validators
{
    public class RemoveTenantCommandValidator : AbstractValidator<RemoveTenantCommand>
    {
        public RemoveTenantCommandValidator()
        {
            //RuleFor(c => c.Id).NotEmpty();
            //RuleFor(c => c.UpdateToken).NotEmpty();
        }
    }
}