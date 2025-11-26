using Microsoft.AspNet.Identity.EntityFramework; // 1. Thêm thư viện này
using PharmacyStore.Models;
using System.Data.Entity;

namespace PharmacyStore.Models
{
    // 2. QUAN TRỌNG: Sửa DbContext thành IdentityDbContext<ApplicationUser>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        // Constructor sửa lại một chút để tương thích Identity
        public ApplicationDbContext()
            : base("PharmacyStoreDb", throwIfV1Schema: false)
        {
        }

        // 3. Thêm hàm Create() static để file Startup.Auth.cs có thể gọi
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        // --- Các bảng dữ liệu cũ của bạn (GIỮ NGUYÊN) ---
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }
    }
}