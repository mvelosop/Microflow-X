using Domion.Data.Base;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tenants.Core.Model;

namespace Tenants.App.Queries
{
    public class GetTenantListQueryHandler : IRequestHandler<GetTenantListQuery, List<Tenant>>
    {
        private readonly IEntityQuery<Tenant> _repo;

        public GetTenantListQueryHandler(IEntityQuery<Tenant> repo)
        {
            _repo = repo;
        }

        public Task<List<Tenant>> Handle(GetTenantListQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return _repo.Query().ToListAsync(cancellationToken: cancellationToken);
            }

            return _repo.Query(t => t.Name.StartsWith(request.Name)).ToListAsync(cancellationToken: cancellationToken);
        }
    }
}