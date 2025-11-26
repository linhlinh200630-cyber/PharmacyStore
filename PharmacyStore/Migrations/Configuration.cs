using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PharmacyStore.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace PharmacyStore.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<PharmacyStore.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true; // Cho phép tự động cập nhật DB
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(PharmacyStore.Models.ApplicationDbContext context)
        {
            // 1. Tạo Role Admin và Customer nếu chưa có
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!roleManager.RoleExists("Admin"))
            {
                roleManager.Create(new IdentityRole("Admin"));
            }
            if (!roleManager.RoleExists("Customer"))
            {
                roleManager.Create(new IdentityRole("Customer"));
            }

            // 2. Tạo tài khoản Admin mặc định
            string email = "admin@pharmacy.com";
            var adminUser = userManager.FindByEmail(email);

            if (adminUser == null)
            {
                adminUser = new ApplicationUser()
                {
                    UserName = "admin",
                    Email = email,
                    // Thêm các thuộc tính khác của bạn nếu có (vd: FullName, Address...)
                };

                // Mật khẩu mặc định là: Admin@123
                var result = userManager.Create(adminUser, "Admin@123");

                if (result.Succeeded)
                {
                    userManager.AddToRole(adminUser.Id, "Admin");
                }
            }
        }
    }
}