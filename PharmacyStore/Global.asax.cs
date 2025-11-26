using System.Collections.Generic;
using System.Data.Entity; // Cần cái này cho dòng Database.SetInitializer
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using PharmacyStore.Models; // <--- THÊM DÒNG NÀY ĐỂ SỬA LỖI

namespace PharmacyStore
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Dòng này sẽ hết lỗi sau khi thêm using ở trên
            Database.SetInitializer<ApplicationDbContext>(null);
        }
    }
}