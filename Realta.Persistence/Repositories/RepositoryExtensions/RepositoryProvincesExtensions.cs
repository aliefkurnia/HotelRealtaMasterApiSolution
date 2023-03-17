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
    public static class RepositoryProvincesExtensions
    {
        public static IQueryable<Provinces> SearchProvinces(this IQueryable<Provinces> provinces, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return provinces;

            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();

            return provinces.Where(p =>
                p.ProvName.ToLower().Contains(lowerCaseSearchTerm) ||
                p.ProvId.ToString().Contains(lowerCaseSearchTerm)
                );
        }

        public static IQueryable<Provinces> Sort(this IQueryable<Provinces> provinces, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return provinces.OrderBy(e => e.ProvId);

            var orderParams = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(Provinces).GetProperties(BindingFlags.Public | BindingFlags.Instance);
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
                return provinces.OrderBy(e => e.ProvId);

            return provinces.OrderBy(orderQuery);
        }
    }
}
