using Microsoft.EntityFrameworkCore;
using StationeryShop.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace StationeryShop.Data
{
    public class StationeryShopDbContext : IdentityDbContext
    {
        public StationeryShopDbContext(DbContextOptions<StationeryShopDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "Bút Viết", Description = "Các loại bút bi, bút mực, bút chì" },
                new Category { CategoryId = 2, CategoryName = "Sổ Tay & Giấy", Description = "Sổ tay, giấy in, giấy note" },
                new Category { CategoryId = 3, CategoryName = "Dụng Cụ Văn Phòng", Description = "Kéo, kẹp ghim, băng keo" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    ProductId = 1,
                    Name = "Bút bi Thiên Long 027",
                    Price = 3000M,
                    ShortDescription = "Loại bút phổ biến nhất",
                    StockQuantity = 100,
                    IsBestSeller = true,
                    ImageUrl = "/images/products/but-bi-thien-long.jpg",
                    CategoryId = 1 
                },
                new Product
                {
                    ProductId = 2,
                    Name = "Giấy A4 Double A 70gsm",
                    Price = 65000M,
                    ShortDescription = "Tập 500 tờ giấy in chất lượng cao",
                    StockQuantity = 50,
                    IsBestSeller = true,
                    ImageUrl = "/images/products/giay-a4-double-a.jpg",
                    CategoryId = 2 
                },
                new Product
                {
                    ProductId = 3,
                    Name = "Sổ tay Klong A5",
                    Price = 25000M,
                    ShortDescription = "Sổ ghi chép bìa cứng",
                    StockQuantity = 75,
                    IsBestSeller = false,
                    ImageUrl = "/images/products/so-tay-klong.jpg",
                    CategoryId = 2 
                },
                 new Product
                 {
                     ProductId = 4,
                     Name = "Kéo văn phòng Deli",
                     Price = 15000M,
                     ShortDescription = "Kéo sắc bén, tay cầm nhựa",
                     StockQuantity = 80,
                     IsBestSeller = false,
                     ImageUrl = "/images/products/keo-deli.jpg",
                     CategoryId = 3
                 },
                 new Product
                 {
                     ProductId = 5,
                     Name = "Băng keo trong 4.8 cm",
                     Price = 12000M,
                     ShortDescription = "Băng keo dán thùng tiện lợi",
                     LongDescription = "Băng keo trong suốt, độ dính cao, dùng để dán thùng, đóng gói hàng hóa. Chiều rộng 4.8 cm.",
                     StockQuantity = 200,
                     IsBestSeller = false,
                     ImageUrl = "/images/products/bang-keo-trong.jpg",
                     CategoryId = 3
                 }
            );
        }
    }
}
