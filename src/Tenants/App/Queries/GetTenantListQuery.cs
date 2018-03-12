using MediatR;
using System.Collections.Generic;
using Tenants.Core.Model;

namespace Tenants.App.Queries
{
    public class GetTenantListQuery : IRequest<List<Tenant>>
    {
        public GetTenantListQuery(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}