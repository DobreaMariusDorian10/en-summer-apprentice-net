using TSM.Models;

namespace TSM.Repositories
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAll();
        Task<Order> GetById(int id);
        int Add(Order @order);
        void Update(Order @order);
        void Delete(Order orderEntity);
    }
}
