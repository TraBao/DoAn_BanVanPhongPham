using Microsoft.AspNetCore.Mvc;
using StationeryShop.Data;
using StationeryShop.Models;
using StationeryShop.Repositories.Interfaces;

namespace StationeryShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly StationeryShopDbContext _context;
        public HomeController(IProductRepository productRepository, StationeryShopDbContext context)
        {
            _productRepository = productRepository;
            _context = context;
        }

        public IActionResult Index()
        {
            var bestSellers = _productRepository.BestSellers;
            return View(bestSellers);
        }
        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Contact(ContactMessage contactMessage)
        {
            if (ModelState.IsValid)
            {
                contactMessage.MessageSent = DateTime.Now;

                _context.ContactMessages.Add(contactMessage);
                _context.SaveChanges();
                return RedirectToAction("ContactSuccess");
            }

            return View(contactMessage);
        }
        public IActionResult ContactSuccess()
        {
            ViewBag.ContactSuccessMessage = "Cảm ơn bạn đã liên hệ! Chúng tôi sẽ phản hồi sớm nhất có thể.";
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}