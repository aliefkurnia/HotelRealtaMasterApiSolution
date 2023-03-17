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
    public static class RepositoryRegionsExtensions
    {
        public static IQueryable<Regions> SearchRegions(this IQueryable<Regions> regions, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return regions;

            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();

            return regions.Where(p =>
                p.RegionName.ToLower().Contains(lowerCaseSearchTerm) ||
                p.RegionCode.ToString().Contains(lowerCaseSearchTerm)
                );
        }

        public static IQueryable<Regions> Sort(this IQueryable<Regions> regions, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return regions.OrderBy(e => e.RegionCode);

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
                return regions.OrderBy(e => e.RegionCode);

            return regions.OrderBy(orderQuery);
        }
    }
}
