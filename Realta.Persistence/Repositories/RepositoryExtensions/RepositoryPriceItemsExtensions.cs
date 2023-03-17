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
    public static class RepositoryPriceItemsExtensions
    {
        public static IQueryable<PriceItems> SearchPriceItems(this IQueryable<PriceItems> priceItems, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return priceItems;

            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();

            return priceItems.Where(p => 
                p.PritId.ToString().Contains(lowerCaseSearchTerm) ||
                p.PritName.ToLower().Contains(lowerCaseSearchTerm) ||
                p.PritPrice.ToString().Contains(lowerCaseSearchTerm) ||
                p.PritType.ToLower().Contains(lowerCaseSearchTerm)
                );
        }

        public static IQueryable<PriceItems> Sort(this IQueryable<PriceItems> priceItems, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return priceItems.OrderBy(e => e.PritId);

            var orderParams = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(PriceItems).GetProperties(BindingFlags.Public | BindingFlags.Instance);
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
                return priceItems.OrderBy(e => e.PritId);

            return priceItems.OrderBy(orderQuery);
        }
    }
}
