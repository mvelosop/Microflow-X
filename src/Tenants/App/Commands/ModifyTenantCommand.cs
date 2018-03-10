using System;
using Domion.Base;
using MediatR;
using Tenants.App.Base;
using Tenants.Core.Model;

namespace Tenants.App.Commands
{
    public class ModifyTenantCommand : TenantDataCommand, IRequest<CommandResult<Tenant>>
    {
        public ModifyTenantCommand(Guid id, TenantData data, byte[] updateToken)
            : base(data)
        {
            Id = id;
            UpdateToken = updateToken ?? throw new ArgumentNullException(nameof(updateToken));

            if (updateToken.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(updateToken));
        }

        public Guid Id { get; }

        public byte[] UpdateToken { get; }
    }
}