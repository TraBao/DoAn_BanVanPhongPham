using StationeryShop.Data;
using StationeryShop.Models;
using StationeryShop.Repositories.Interfaces;

namespace StationeryShop.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly StationeryShopDbContext _context;

        public CategoryRepository(StationeryShopDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> AllCategories => _context.Categories.OrderBy(c => c.CategoryName);
    }
}