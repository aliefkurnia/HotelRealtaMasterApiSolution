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
    public static class RepositoryServiceTaskExtensions
    {
        public static IQueryable<ServiceTask> SearchServiceTasks(this IQueryable<ServiceTask> serviceTasks, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return serviceTasks;

            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();

            return serviceTasks.Where(p =>
                p.SetaName.ToLower().Contains(lowerCaseSearchTerm) ||
                p.SetaId.ToString().Contains(lowerCaseSearchTerm)
                );
        }

        public static IQueryable<ServiceTask> Sort(this IQueryable<ServiceTask> serviceTasks, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return serviceTasks.OrderBy(e => e.SetaId);

            var orderParams = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(ServiceTask).GetProperties(BindingFlags.Public | BindingFlags.Instance);
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
                return serviceTasks.OrderBy(e => e.SetaId);

            return serviceTasks.OrderBy(orderQuery);
        }
    }
}
