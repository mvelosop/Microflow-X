using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Domion.Base;
using Tenants.Core.Model;

namespace Tenants.Data.QuerySpecs
{
    public class TenantQuerySpec : IQuerySpec<Tenant>
    {
        public Expression<Func<Tenant, bool>> WhereExpression
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Name)) return null;

                if (Name.EndsWith("%"))
                {
                    var match = Name.Substring(0, Name.Length - 1);

                    return t => t.Name.StartsWith(match);
                }

                return t => t.Name == Name;
            }
        }

        public string Name { get; set; }
    }
}
