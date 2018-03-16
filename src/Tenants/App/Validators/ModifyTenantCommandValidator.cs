using System;
using System.Linq;
using FluentValidation;
using Tenants.App.Commands;

namespace Tenants.App.Validators
{
    public class ModifyTenantCommandValidator : TenantCommandValidator<ModifyTenantCommand>
    {
        public ModifyTenantCommandValidator()
        {
            RuleFor(c => c.Id).NotEmpty();
            RuleFor(c => c.UpdateToken).NotEmpty();
            RuleFor(c => c.UpdateToken).Must(NotBeEmpty).WithMessage($"'UpdateToken' should not be empty.");
        }

        private bool NotBeEmpty(byte[] updateToken)
        {
            return updateToken.Length > 0 && updateToken.Any(v => v > 0);
        }
    }
}