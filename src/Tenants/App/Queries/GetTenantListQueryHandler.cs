using Domion.Data.Extensions;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tenants.Core.Model;
using Tenants.Core.Repositories;
using Tenants.Data.QuerySpecs;

namespace Tenants.App.Queries
{
    public class GetTenantListQueryHandler : IRequestHandler<GetTenantListQuery, List<Tenant>>
    {
        private readonly ITenantRepository _repo;

        public GetTenantListQueryHandler(ITenantRepository repo)
        {
            _repo = repo;
        }

        public Task<List<Tenant>> Handle(GetTenantListQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return _repo.GetListAsync(cancellationToken);
            }

            return _repo.GetListAsync(new TenantQuerySpec { Name = request.Name }, cancellationToken);
        }
    }
}