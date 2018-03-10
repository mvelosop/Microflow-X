using System;

namespace Tenants.App.Base
{
    public abstract class TenantDataCommand
    {
        private readonly TenantData _data;

        protected TenantDataCommand(TenantData data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));

            if (string.IsNullOrWhiteSpace(data.Email)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(data.Email));
            if (string.IsNullOrWhiteSpace(data.Name)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(data.Name));

            _data = data;
        }

        public virtual string Email => _data.Email;

        public virtual string Name => _data.Name;
    }
}