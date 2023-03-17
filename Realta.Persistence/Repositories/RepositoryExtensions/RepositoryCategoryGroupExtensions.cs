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
    public static class RepositoryCategoryGroupExtensions
    {
        public static IQueryable<CategoryGroup> SearchCategoryGroup(this IQueryable<CategoryGroup> categoryGroup, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return categoryGroup;

            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();

            return categoryGroup.Where(p => 
                p.CagroId.ToString().Contains(lowerCaseSearchTerm) ||
                p.CagroName.ToLower().Contains(lowerCaseSearchTerm) ||
                p.CagroType.ToLower().Contains(lowerCaseSearchTerm) 
                );
        }

        public static IQueryable<CategoryGroup> Sort(this IQueryable<CategoryGroup> categoryGroup, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return categoryGroup.OrderBy(e => e.CagroName);

            var orderParams = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(CategoryGroup).GetProperties(BindingFlags.Public | BindingFlags.Instance);
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
                return categoryGroup.OrderBy(e => e.CagroName);

            return categoryGroup.OrderBy(orderQuery);
        }
    }
}
