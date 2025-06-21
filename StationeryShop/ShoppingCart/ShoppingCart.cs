using Microsoft.EntityFrameworkCore;
using StationeryShop.Data;
using StationeryShop.Interfaces;
using StationeryShop.Models;

namespace StationeryShop.ShoppingCart
{
    public class ShoppingCart : IShoppingCart
    {
        private readonly StationeryShopDbContext _context;
        public string? ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; } = default!;

        private ShoppingCart(StationeryShopDbContext context)
        {
            _context = context;
        }

        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession? session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.Session;
            var context = services.GetService<StationeryShopDbContext>();

            string cartId = session?.GetString("CartId") ?? Guid.NewGuid().ToString();
            session?.SetString("CartId", cartId);

            if (context == null)
            {
                throw new InvalidOperationException("DbContext is not available.");
            }

            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }
        public void AddToCart(Product product, int amount)
        {
            var shoppingCartItem = _context.ShoppingCartItems.SingleOrDefault(
                s => s.Product.ProductId == product.ProductId && s.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Product = product,
                    Amount = amount
                };
                _context.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount += amount;
            }
            _context.SaveChanges();
        }

        public int RemoveFromCart(Product product)
        {
            var shoppingCartItem = _context.ShoppingCartItems.SingleOrDefault(
                s => s.Product.ProductId == product.ProductId && s.ShoppingCartId == ShoppingCartId);

            int remainingAmount = 0;

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    remainingAmount = shoppingCartItem.Amount;
                }
                else
                {
                    _context.ShoppingCartItems.Remove(shoppingCartItem);
                }

                _context.SaveChanges();
            }

            return remainingAmount;
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ??= _context.ShoppingCartItems
                .Where(c => c.ShoppingCartId == ShoppingCartId)
                .Include(s => s.Product)
                .ToList();
        }

        public void ClearCart()
        {
            var cartItems = _context.ShoppingCartItems
                .Where(cart => cart.ShoppingCartId == ShoppingCartId);
            _context.ShoppingCartItems.RemoveRange(cartItems);
            _context.SaveChanges();
        }

        public decimal GetShoppingCartTotal()
        {
            var total = _context.ShoppingCartItems
                .Where(c => c.ShoppingCartId == ShoppingCartId)
                .Select(c => c.Product.Price * c.Amount)
                .Sum();
            return total;
        }
    }
}