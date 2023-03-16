using Realta.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Persistence.Repositories.RepositoryExtensions
{
    public static class RepositoryVenProExtension
    {
        public static IQueryable<VendorProduct> Search(this IQueryable<VendorProduct> vendors, string Keyword)
        {
            if (string.IsNullOrWhiteSpace(Keyword))
                return vendors;

            var lowerCaseSearchTerm = Keyword.Trim().ToLower();

            return vendors.Where(p => p.StockName.ToLower().Contains(lowerCaseSearchTerm));
        }

        //variabel belum disesuaikan
        public static IQueryable<Vendor> Sortt(this IQueryable<Vendor> vendors, string orderByQueryString)
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

            return vendors.OrderBy(e => e.VendorName);
        }

    }
}
