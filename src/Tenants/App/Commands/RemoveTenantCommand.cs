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
            UpdateToken = updateToken ?? throw new ArgumentNullException(nameof(updateToken));

            if (updateToken.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(updateToken));
        }

        public Guid Id { get; }

        public byte[] UpdateToken { get; }
    }
}