using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Realta.Domain.Entities;

namespace Realta.Persistence.Repositories.RepositoryExtensions
{
    public static class RepositoryPolicyExtensions
    {
        public static IQueryable<Policy> SearchPolicy(this IQueryable<Policy> policy, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return policy;

            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();

            return policy.Where(p => 
                p.PoliName.ToLower().Contains(lowerCaseSearchTerm) ||
                p.PoliId.ToString().Contains(lowerCaseSearchTerm)
                );
        }

        public static IQueryable<Policy> Sort(this IQueryable<Policy> policy, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return policy.OrderBy(e => e.PoliName);

            var orderParams = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(Policy).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var orderQueryBuilder = new StringBuilder();

            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                    continue;

                var propertyFromQueryName = param.Split(" ")[0];
                var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

                if (objectProperty == null)
                    continue;

                var direction = param.EndsWith(" desc") ? "descending" : "ascending";
                orderQueryBuilder.Append($"{objectProperty.Name} {direction}, ");
            }
             
            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');
            if (string.IsNullOrWhiteSpace(orderQuery))
                return policy.OrderBy(e => e.PoliName);

            return policy.OrderBy(orderQuery);
        }
    }
}
