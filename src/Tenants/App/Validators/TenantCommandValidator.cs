using FluentValidation;
using Tenants.App.Base;

namespace Tenants.App.Validators
{
    public class TenantCommandValidator<T> : AbstractValidator<T>
        where T : TenantDataCommand
    {
        public TenantCommandValidator()
        {
            RuleFor(c => c.Email).NotEmpty();
            RuleFor(c => c.Name).NotEmpty();
        }
    }
}