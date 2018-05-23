using Domion.Base;
using MediatR;
using System;
using Tenants.Core.Model;

namespace Tenants.App.Commands
{
    public class RemoveTenantCommand : IRequest<CommandResult>
    {
        public RemoveTenantCommand(Guid id, byte[] concurrencyToken)
        {
            Id = id;
            ConcurrencyToken = concurrencyToken;
        }

        public Guid Id { get; }

        public byte[] ConcurrencyToken { get; }
    }
}