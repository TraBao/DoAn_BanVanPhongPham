using StationeryShop.Models;

namespace StationeryShop.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> AllCategories { get; }
    }
}