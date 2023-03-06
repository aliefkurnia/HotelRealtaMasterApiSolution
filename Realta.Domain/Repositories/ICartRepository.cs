using Realta.Domain.Entities;

namespace Realta.Domain.Repositories
{
    public interface ICartRepository
    {
        Task<IEnumerable<Cart>> GetAllAsync();
        void Insert(Cart cart);
        void UpdateQty(Cart cart, out bool status);
        void Remove(int id, out bool status);
    }
}
