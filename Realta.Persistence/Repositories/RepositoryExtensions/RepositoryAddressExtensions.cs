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
    public static class RepositoryAddressExtensions
    {
        public static IQueryable<Address> SearchAddress(this IQueryable<Address> address, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return address;

            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();

            return address.Where(p =>
                p.AddrId.ToString().Contains(lowerCaseSearchTerm) ||
                p.AddrLine1.ToLower().Contains(lowerCaseSearchTerm) ||
                (!string.IsNullOrEmpty(p.AddrLine2) && p.AddrLine2.ToLower().Contains(lowerCaseSearchTerm)) ||
                p.AddrCity.ToLower().Contains(lowerCaseSearchTerm) ||
                p.AddrPostalCode.ToString().Contains(lowerCaseSearchTerm)
            );
        }

        public static IQueryable<Address> Sort(this IQueryable<Address> address, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return address.OrderBy(e => e.AddrId);

            var orderParams = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(Address).GetProperties(BindingFlags.Public | BindingFlags.Instance);
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
                return address.OrderBy(e => e.AddrId);

            return address.OrderBy(orderQuery);
        }
    }
}
