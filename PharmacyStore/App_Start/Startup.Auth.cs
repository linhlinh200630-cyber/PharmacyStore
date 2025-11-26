using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using PharmacyStore.Models;

namespace PharmacyStore  // <--- QUAN TRỌNG: Phải là PharmacyStore, bỏ .App_Start đi nếu có
{
    public partial class Startup
    {
        // Đây chính là hàm mà file Startup.cs của bạn đang tìm kiếm
        public void ConfigureAuth(IAppBuilder app)
        {
            // Cấu hình db context, user manager và signin manager cho mỗi request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // Cho phép ứng dụng dùng cookie để lưu thông tin đăng nhập
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"), // Đường dẫn trang đăng nhập
                Provider = new CookieAuthenticationProvider
                {
                    // Kiểm tra bảo mật mỗi 30 phút
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });
        }
    }
}