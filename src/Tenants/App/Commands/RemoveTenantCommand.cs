using Domion.Base;
using MediatR;
using System;
using Tenants.Core.Model;

namespace Tenants.App.Commands
{
    public class RemoveTenantCommand : IRequest<CommandResult>
    {
        public RemoveTenantCommand(Guid id, byte[] updateToken)
        {
            Id = id;
            UpdateToken = updateToken;
        }

        public Guid Id { get; }

        public byte[] UpdateToken { get; }
    }
}