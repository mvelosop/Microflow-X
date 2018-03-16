using System;

namespace Tenants.App.Base
{
    public abstract class TenantDataCommand
    {
        private readonly TenantData _data;

        protected TenantDataCommand(TenantData data)
        {
            _data = data ?? throw new ArgumentNullException(nameof(data));
        }

        public virtual string Email => _data.Email;

        public virtual string Name => _data.Name;
    }
}