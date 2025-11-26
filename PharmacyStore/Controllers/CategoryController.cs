using System.Web.Mvc;

namespace PharmacyStore.Controllers
{
    public class CategoryController : Controller
    {
        // ==========================
        // 1. Thuốc
        // ==========================
        public ActionResult Thuoc()
        {
            ViewBag.Title = "Thuốc";
            return View();
        }

        // ==========================
        // 2. Tra cứu bệnh
        // ==========================
        public ActionResult TraCuuBenh()
        {
            ViewBag.Title = "Tra cứu bệnh";
            return View();
        }

        // ==========================
        // 3. Thực phẩm chức năng
        // ==========================
        public ActionResult TPChucNang()
        {
            ViewBag.Title = "Thực phẩm chức năng";
            return View();
        }

        // ==========================
        // 4. Dược mỹ phẩm
        // ==========================
        public ActionResult DuocMyPham()
        {
            ViewBag.Title = "Dược mỹ phẩm";
            return View();
        }

        // ==========================
        // 5. Chăm sóc cá nhân
        // ==========================
        public ActionResult ChamSocCaNhan()
        {
            ViewBag.Title = "Chăm sóc cá nhân";
            return View();
        }

        // ==========================
        // 6. Thiết bị y tế
        // ==========================
        public ActionResult ThietBiYTe()
        {
            ViewBag.Title = "Thiết bị y tế";
            return View();
        }

        // ==========================
        // 7. Tiêm chủng
        // ==========================
        public ActionResult TiemChung()
        {
            ViewBag.Title = "Tiêm chủng";
            return View();
        }
    }
}
