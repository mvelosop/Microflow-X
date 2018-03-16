using Domion.Base;
using MediatR;
using Tenants.App.Base;
using Tenants.Core.Model;

namespace Tenants.App.Commands
{
    public class AddTenantCommand : TenantCommand, IRequest<CommandResult<Tenant>>
    {
        public AddTenantCommand(TenantData data)
            : base(data)
        {
        }
    }
}