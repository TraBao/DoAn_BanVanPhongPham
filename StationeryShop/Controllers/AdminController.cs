using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StationeryShop.Data;
using StationeryShop.Models;
using StationeryShop.Repositories.Interfaces;

namespace StationeryShop.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly StationeryShopDbContext _context;

        public AdminController(IProductRepository productRepository, ICategoryRepository categoryRepository, StationeryShopDbContext context)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _context = context;
        }
        public IActionResult Index()
        {
            ViewData["Layout"] = "~/Views/Admin/Shared/_AdminLayout.cshtml";
            var allProducts = _productRepository.AllProducts;
            return View(allProducts);
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            ViewData["Layout"] = "~/Views/Admin/Shared/_AdminLayout.cshtml";
            ViewBag.Categories = new SelectList(_categoryRepository.AllCategories, "CategoryId", "CategoryName");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = new SelectList(_categoryRepository.AllCategories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> EditProduct(int id)
        {
            ViewData["Layout"] = "~/Views/Admin/Shared/_AdminLayout.cshtml";
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.Categories = new SelectList(_categoryRepository.AllCategories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProduct(int id,
    [Bind("ProductId,Name,ShortDescription,LongDescription,Price,ImageUrl,StockQuantity,IsBestSeller,CategoryId")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var productToUpdate = await _context.Products.FindAsync(id);
                    if (productToUpdate == null)
                    {
                        return NotFound();
                    }

                    productToUpdate.Name = product.Name;
                    productToUpdate.ShortDescription = product.ShortDescription;
                    productToUpdate.LongDescription = product.LongDescription;
                    productToUpdate.Price = product.Price;
                    productToUpdate.ImageUrl = product.ImageUrl;
                    productToUpdate.StockQuantity = product.StockQuantity;
                    productToUpdate.IsBestSeller = product.IsBestSeller;
                    productToUpdate.CategoryId = product.CategoryId;

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Products.Any(e => e.ProductId == product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = new SelectList(_categoryRepository.AllCategories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            ViewData["Layout"] = "~/Views/Admin/Shared/_AdminLayout.cshtml";
            var product = await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null) { return NotFound(); }
            return View(product);
        }

        [HttpPost, ActionName("DeleteProduct")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index)); // Chuyển hướng về trang Index
        }
    }
}