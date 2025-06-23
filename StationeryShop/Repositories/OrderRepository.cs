using Microsoft.EntityFrameworkCore;
using StationeryShop.Data;
using StationeryShop.Interfaces;
using StationeryShop.Models;
using StationeryShop.Repositories.Interfaces;

namespace StationeryShop.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly StationeryShopDbContext _context;
        private readonly IShoppingCart _shoppingCart;

        public OrderRepository(StationeryShopDbContext context, IShoppingCart shoppingCart)
        {
            _context = context;
            _shoppingCart = shoppingCart;
        }
        public void CreateOrder(Order order)
        {
            order.OrderPlaced = DateTime.Now;
            var shoppingCartItems = _shoppingCart.GetShoppingCartItems();
            order.OrderTotal = _shoppingCart.GetShoppingCartTotal();
            order.OrderDetails = new List<OrderDetail>();

            foreach (var item in shoppingCartItems)
            {
                var orderDetail = new OrderDetail
                {
                    Quantity = item.Amount,
                    ProductId = item.Product.ProductId,
                    Price = item.Product.Price 
                };
                order.OrderDetails.Add(orderDetail);
                var productInDb = _context.Products.Find(item.Product.ProductId);

                if (productInDb != null)
                {
                    productInDb.StockQuantity -= item.Amount;
                }
            }

            _context.Orders.Add(order);
            _context.SaveChanges();
        }
        public IEnumerable<Order> GetOrdersByUserId(string userId)
        {
            var orders = _context.Orders
                                 .Where(o => o.UserId == userId)
                                 .Include(o => o.OrderDetails)
                                 .ThenInclude(od => od.Product)
                                 .OrderByDescending(o => o.OrderPlaced);

            return orders;
        }
    }
}