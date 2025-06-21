
using StationeryShop.Models;

namespace StationeryShop.ViewModels
{
    public class ShoppingCartViewModel
    {
        public List<ShoppingCartItem> ShoppingCartItems { get; set; } = new List<ShoppingCartItem>();

        public decimal ShoppingCartTotal { get; set; }
    }
}