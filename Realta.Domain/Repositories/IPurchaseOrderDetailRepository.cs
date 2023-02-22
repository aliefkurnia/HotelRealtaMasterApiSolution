using Realta.Domain.Entities;

namespace Realta.Domain.Repositories
{
    public interface IPurchaseOrderDetailRepository
    {
        IEnumerable<PurchaseOrderDetail> FindAll();
        Task<IEnumerable<PurchaseOrderDetail>> FindAllAsync(string poNumber);
        PurchaseOrderDetail FindById(int id);
        void Insert(PurchaseOrderDetail purchaseOrderDetail);
        void UpdateQty(PurchaseOrderDetail purchaseOrderDetail);
        void Remove(PurchaseOrderDetail purchaseOrderDetail);
    }
}
