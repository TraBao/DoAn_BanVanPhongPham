using Microsoft.AspNetCore.Mvc;
using StationeryShop.Interfaces;
using StationeryShop.Models;
using StationeryShop.Repositories.Interfaces;

namespace StationeryShop.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IShoppingCart _shoppingCart;

        public OrderController(IOrderRepository orderRepository, IShoppingCart shoppingCart)
        {
            _orderRepository = orderRepository;
            _shoppingCart = shoppingCart;
        }

        public IActionResult Checkout()
        {
            var items = _shoppingCart.GetShoppingCartItems();

            if (items.Count == 0)
            {
                ModelState.AddModelError("", "Giỏ hàng của bạn đang trống, hãy thêm sản phẩm trước khi thanh toán.");
                return RedirectToAction("Index", "ShoppingCart");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            var items = _shoppingCart.GetShoppingCartItems();
            if (items.Count == 0)
            {
                ModelState.AddModelError("", "Giỏ hàng của bạn đã trống.");
            }

            if (ModelState.IsValid)
            {
                _orderRepository.CreateOrder(order);
                _shoppingCart.ClearCart();
                HttpContext.Session.SetInt32("CartItemsCount", 0);

                return RedirectToAction("CheckoutComplete");
            }

            return View(order);
        }
        public IActionResult CheckoutComplete()
        {
            ViewBag.CheckoutCompleteMessage = "Cảm ơn bạn đã đặt hàng! Đơn hàng của bạn sẽ sớm được xử lý.";
            return View();
        }
    }
}