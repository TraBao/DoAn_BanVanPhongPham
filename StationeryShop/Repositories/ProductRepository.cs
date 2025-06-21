using Microsoft.EntityFrameworkCore;
using StationeryShop.Data;
using StationeryShop.Models;
using StationeryShop.Repositories.Interfaces;

namespace StationeryShop.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly StationeryShopDbContext _context;

        public ProductRepository(StationeryShopDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> AllProducts => _context.Products.Include(p => p.Category);

        public IEnumerable<Product> BestSellers => _context.Products.Where(p => p.IsBestSeller).Include(p => p.Category);

        public Product? GetProductById(int productId)
        {
            return _context.Products.FirstOrDefault(p => p.ProductId == productId);
        }
    }
}