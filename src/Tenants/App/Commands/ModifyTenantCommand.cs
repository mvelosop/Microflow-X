using System;
using Domion.Base;
using MediatR;
using Tenants.App.Base;
using Tenants.Core.Model;

namespace Tenants.App.Commands
{
    public class ModifyTenantCommand : TenantDataCommand, IRequest<CommandResult<Tenant>>
    {
        public ModifyTenantCommand(Guid id, TenantData data, byte[] concurrencyToken)
            : base(data)
        {
            Id = id;
            ConcurrencyToken = concurrencyToken;
        }

        public Guid Id { get; }

        public byte[] ConcurrencyToken { get; }
    }
}