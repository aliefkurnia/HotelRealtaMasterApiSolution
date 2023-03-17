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
    public static class RepositoryCountryExtensions
    {
        public static IQueryable<Country> SearchCountry(this IQueryable<Country> country, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return country;

            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();

            return country.Where(p => 
                p.CountryName.ToLower().Contains(lowerCaseSearchTerm) ||
                p.CountryId.ToString().Contains(lowerCaseSearchTerm)
                );
        }

        public static IQueryable<Country> Sort(this IQueryable<Country> country, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return country.OrderBy(e => e.CountryName);

            var orderParams = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(Country).GetProperties(BindingFlags.Public | BindingFlags.Instance);
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
                return country.OrderBy(e => e.CountryName);

            return country.OrderBy(orderQuery);
        }
    }
}
