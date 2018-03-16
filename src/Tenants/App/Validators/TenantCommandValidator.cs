using FluentValidation;
using Tenants.App.Base;

namespace Tenants.App.Validators
{
    public class TenantDataCommandValidator<T> : AbstractValidator<T>
        where T : TenantDataCommand
    {
        public TenantDataCommandValidator()
        {
            RuleFor(c => c.Email).NotEmpty();
            RuleFor(c => c.Name).NotEmpty();
        }
    }
}