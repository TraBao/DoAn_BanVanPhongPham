using StationeryShop.Models;

namespace StationeryShop.Repositories.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> AllProducts { get; }
        IEnumerable<Product> BestSellers { get; }
        Product? GetProductById(int productId);
    }
}