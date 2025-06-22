using StationeryShop.Models;

namespace StationeryShop.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        void CreateOrder(Order order);
        IEnumerable<Order> GetOrdersByUserId(string userId);
    }
}