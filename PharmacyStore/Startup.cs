using Microsoft.Owin;
using Owin;

// Dòng này cực kỳ quan trọng, nó chỉ định đây là class khởi chạy
[assembly: OwinStartup(typeof(PharmacyStore.Startup))]

namespace PharmacyStore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Gọi hàm cấu hình đăng nhập từ file Startup.Auth.cs
            ConfigureAuth(app);
        }
    }
}