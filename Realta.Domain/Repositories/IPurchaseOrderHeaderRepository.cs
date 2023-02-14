using Realta.Domain.Entities;

namespace Realta.Domain.Repositories
{
    public interface IPurchaseOrderHeaderRepository
    {
        IEnumerable<PurchaseOrderHeader> FindAll();
        Task<IEnumerable<PurchaseOrderHeader>> FindAllAsync();
        PurchaseOrderHeader FindById(int id);
        void Insert(PurchaseOrderHeader purchaseOrderHeader);
        void UpdateStatus(PurchaseOrderHeader purchaseOrderHeader);
        void Remove(PurchaseOrderHeader purchaseOrderHeader);
    }
}
