using Domion.Base;
using MediatR;
using System;
using Tenants.Core.Model;

namespace Tenants.App.Commands
{
    public class AddTenantCommand : IRequest<CommandResult<Tenant>>
    {
        public AddTenantCommand(string email, string name)
        {
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(email));
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));

            Email = email;
            Name = name;
        }

        public string Email { get; }

        public string Name { get; }
    }
}