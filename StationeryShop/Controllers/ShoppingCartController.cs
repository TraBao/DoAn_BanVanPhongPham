using Microsoft.AspNetCore.Mvc;
using StationeryShop.Models;
using StationeryShop.Repositories.Interfaces;
using StationeryShop.ShoppingCart;
using StationeryShop.ViewModels;
using StationeryShop.Interfaces;

namespace StationeryShop.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IShoppingCart _shoppingCart; 

        public ShoppingCartController(IProductRepository productRepository, IShoppingCart shoppingCart) 
        {
            _productRepository = productRepository;
            _shoppingCart = shoppingCart;
        }
        public ViewResult Index()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            var total = _shoppingCart.GetShoppingCartTotal();

            var shoppingCartViewModel = new ShoppingCartViewModel
            {
                ShoppingCartItems = items,
                ShoppingCartTotal = total
            };

            return View(shoppingCartViewModel);
        }

        public RedirectToActionResult AddToShoppingCart(int productId)
        {
            var selectedProduct = _productRepository.GetProductById(productId);

            if (selectedProduct != null)
            {
                _shoppingCart.AddToCart(selectedProduct, 1);
            }
            var itemCount = _shoppingCart.GetShoppingCartItems().Count;
            HttpContext.Session.SetInt32("CartItemsCount", itemCount);
            return RedirectToAction("Index");
        }
        public RedirectToActionResult RemoveFromShoppingCart(int productId)
        {
            var selectedProduct = _productRepository.GetProductById(productId);

            if (selectedProduct != null)
            {
                _shoppingCart.RemoveFromCart(selectedProduct);
            }
            var itemCount = _shoppingCart.GetShoppingCartItems().Count;
            HttpContext.Session.SetInt32("CartItemsCount", itemCount);
            return RedirectToAction("Index");
        }
    }
}
