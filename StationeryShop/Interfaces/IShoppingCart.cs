using StationeryShop.Models;

namespace StationeryShop.Interfaces
{
    public interface IShoppingCart
    {
        void AddToCart(Product product, int quantity);
        int RemoveFromCart(Product product);
        List<ShoppingCartItem> GetShoppingCartItems();
        void ClearCart();
        decimal GetShoppingCartTotal();

        List<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
