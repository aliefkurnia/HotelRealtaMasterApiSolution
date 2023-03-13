using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using System.Reflection;
using Realta.Domain.Entities;

namespace Realta.Persistence.Repositories.RepositoryExtensions
{
    public static class RepositoryVendorExtensions
    {
        public static IQueryable<Vendor> Search(this IQueryable<Vendor> vendors, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return vendors;

            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();

            return vendors.Where(p => p.VendorName.ToLower().Contains(lowerCaseSearchTerm));
        }

        public static IQueryable<Vendor> Sort(this IQueryable<Vendor> vendors, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return vendors.OrderBy(e => e.VendorName);

            var orderParams = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(Vendor).GetProperties(BindingFlags.Public | BindingFlags.Instance);
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
                return vendors.OrderBy(e => e.VendorName);

            return vendors.OrderBy(orderQuery);
        }
    }
}
