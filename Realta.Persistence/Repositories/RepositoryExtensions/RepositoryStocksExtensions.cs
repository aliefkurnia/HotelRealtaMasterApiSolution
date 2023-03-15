using Realta.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;


namespace Realta.Persistence.Repositories.RepositoryExtensions
{
    public static class RepositoryStocksExtensions
    {
        public static IQueryable<Stocks> SearchStock(this IQueryable<Stocks> stocks, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return stocks;

            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();

            return stocks.Where(p => p.StockName.ToLower().Contains(lowerCaseSearchTerm));
        }

        public static IQueryable<Stocks> Sort(this IQueryable<Stocks> stocks, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return stocks.OrderBy(e => e.StockName);

            var orderParams = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(Stocks).GetProperties(BindingFlags.Public | BindingFlags.Instance);
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
                return stocks.OrderBy(e => e.StockName);

            return stocks.OrderBy(orderQuery);
        }


    }
}
