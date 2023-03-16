using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;
using Realta.Domain.Entities;

namespace Realta.Persistence.RepositoryExtensions;

public static class RepositoryPurchaseOrderExtensions
{
    public static IQueryable<PurchaseOrderHeader> Search(this IQueryable<PurchaseOrderHeader> purchaseOrder, string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword))
            return purchaseOrder;

        var lowerCaseKeyword = keyword.Trim().ToLower();

        return purchaseOrder.Where(p =>
            p.PoheNumber.ToLower().Contains(lowerCaseKeyword) ||
            p.VendorName.ToLower().Contains(lowerCaseKeyword)
        );
    }

    public static IQueryable<PurchaseOrderHeader> Status(this IQueryable<PurchaseOrderHeader> purchaseOrder, int? status)
    {
        return status == null ? purchaseOrder : purchaseOrder.Where(p => p.PoheStatus == status);
    }

    public static IQueryable<PurchaseOrderHeader> Sort(this IQueryable<PurchaseOrderHeader> purchaseOrder, string orderByQueryString)
    {
        if (string.IsNullOrWhiteSpace(orderByQueryString))
            return purchaseOrder.OrderBy(e => e.PoheNumber);

        var orderParams = orderByQueryString.Trim().Split(',');
        var propertyInfos = typeof(PurchaseOrderHeader).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var orderQueryBuilder = new StringBuilder();

        foreach (var param in orderParams)
        {
            if (string.IsNullOrWhiteSpace(param))
                continue;

            var propertyFromQueryName = param.Split(" ")[0];
            var objectProperty = propertyInfos.FirstOrDefault(pi =>
                pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

            if (objectProperty == null)
                continue;

            var direction = param.EndsWith(" desc") ? "descending" : "ascending";
            orderQueryBuilder.Append($"{objectProperty.Name} {direction}, ");
        }

        var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');
        if (string.IsNullOrWhiteSpace(orderQuery))
            return purchaseOrder.OrderBy(e => e.PoheNumber);

        return purchaseOrder.OrderBy(orderQuery);
    }
}

public static class RepositoryPurchaseOrderDetailExtensions
{
    public static IQueryable<PurchaseOrderDetail> SearchPoDetail(this IQueryable<PurchaseOrderDetail> source, string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword))
            return source;

        var lowerCaseKeyword = keyword.Trim().ToLower();

        return source.Where(po => po.StockName.ToLower().Contains(lowerCaseKeyword));
    }

    public static IQueryable<PurchaseOrderDetail> SortPoDetail(this IQueryable<PurchaseOrderDetail> source, string orderByQueryString)
    {
        if (string.IsNullOrWhiteSpace(orderByQueryString))
            return source.OrderBy(e => e.StockName);
    
        var orderParams = orderByQueryString.Trim().Split(',');
        // var propertyInfos = typeof(PurchaseOrderDetail).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var orderQueryBuilder = new StringBuilder();
    
        foreach (var param in orderParams)
        {
            if (string.IsNullOrWhiteSpace(param))
                continue;
    
            var propertyFromQueryName = param.Split(" ")[0];
            var direction = param.EndsWith(" desc") ? "descending" : "ascending";
            var propertyInfos = typeof(PurchaseOrderDetail).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));
    
            if (objectProperty == null)
                continue;
    
            orderQueryBuilder.Append($"{objectProperty.Name} {direction}, ");
        }
    
        var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');
        if (string.IsNullOrWhiteSpace(orderQuery))
            return source.OrderBy(e => e.StockName);
    
        return source.OrderBy(orderQuery);
    }
}