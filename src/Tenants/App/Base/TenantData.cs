using AutoMapper;
using Tenants.Core.Model;

namespace Tenants.App.Base
{
    public class TenantData
    {
        private static readonly IMapper Mapper;

        static TenantData()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Tenant, TenantData>());

            Mapper = config.CreateMapper();
        }

        public TenantData()
        {
        }

        public TenantData(string email, string name)
        {
            Email = email;
            Name = name;
        }

        public virtual string Email { get; set; }

        public virtual string Name { get; set; }

        public static TenantData From(Tenant entity)
        {
            return Mapper.Map<TenantData>(entity);
        }
    }
}