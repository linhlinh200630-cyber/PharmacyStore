using System;
using System.Linq;
using System.Web.Mvc;
using PharmacyStore.Models;   // đảm bảo dòng này có
using System.Data.Entity;
using System.IO;
using System.Web;

namespace PharmacyStore.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Banner = "logo.jpg";
            ViewBag.PromoA = "thuoc.jpg";
            ViewBag.PromoB = "bacsi.jpg";
            ViewBag.Slides = new[] { "salebanner.jpg", "salebanner3.jpg", "salebanner4.jpg", "nhathuoc.jpg" };

            const int TOP_CAT_ID = 1;

            using (var db = new ApplicationDbContext())
            {
                // ✅ Top bán chạy: chỉ lấy sản phẩm có ảnh
                ViewBag.BestProducts = db.Products
                    .Where(p => p.CategoryId == TOP_CAT_ID &&
                                p.ImageUrl != null && p.ImageUrl != "")
                    .OrderByDescending(p => p.ProductId)
                    .Take(8)
                    .ToList();

                // ✅ Sản phẩm mới: chỉ lấy ảnh trong thư mục spnew/
                ViewBag.NewProducts = db.Products
                    .Where(p => p.ImageUrl.StartsWith("spnew/"))
                    .OrderByDescending(p => p.ProductId)
                    .Take(5)
                    .ToList();

                // ✅ Flash Sale: ngẫu nhiên sản phẩm có ảnh
                // FLASH SALE: chọn 5 sản phẩm ngẫu nhiên có ảnh hợp lệ
                // Lọc ở DB: chỉ lấy record có ImageUrl khác null/rỗng
                var candidates = db.Products
                    .Where(p => p.ImageUrl != null && p.ImageUrl != "")
                    .OrderBy(p => Guid.NewGuid())
                    .Take(30) // lấy dư chút để loại file hỏng
                    .ToList();

                // Lọc tiếp ở code: chỉ giữ món có file thật trong /Content/img
                string root = Server.MapPath("~/Content/img");
                bool HasImage(Product p)
                {
                    var safe = (p.ImageUrl ?? "").Replace("\\", "/").TrimStart('/');
                    var path = Path.Combine(root, safe);
                    return System.IO.File.Exists(path);
                }

                var flash = candidates.Where(HasImage).Take(5).ToList();

                // Nếu vẫn thiếu 5, bù từ 'spnew/' hay 'top/'
                if (flash.Count < 5)
                {
                    var need = 5 - flash.Count;
                    var fill = db.Products
                        .Where(p => p.ImageUrl.StartsWith("spnew/") || p.ImageUrl.StartsWith("top/"))
                        .OrderByDescending(p => p.ProductId) // hoặc random tiếp
                        .ToList()
                        .Where(HasImage)
                        .Take(need)
                        .ToList();

                    flash.AddRange(fill);
                }

                ViewBag.FlashSale = flash;


                // ép thay HEART Plus bằng "Dầu xả L'ORÉAL PARIS..." nếu chưa có
                var lorealId = db.Products
                    .Where(p => p.Name.Contains("Dầu xả L'ORÉAL PARIS"))
                    .Select(p => p.ProductId)
                    .FirstOrDefault();

                if (lorealId != 0 && !flash.Any(x => x.ProductId == lorealId))
                {
                    var loreal = db.Products.Find(lorealId);
                    var kick = flash.FirstOrDefault(x => x.Name.Contains("HEART"));
                    if (kick == null) kick = flash.Last();
                    flash.Remove(kick);
                    flash.Insert(0, loreal);
                }

                ViewBag.FlashSale = flash;


            }

            return View();
        }



        // Hiển thị ảnh từ thư mục Content/img/
        public ActionResult Img(string file)
        {
            var root = Server.MapPath("~/Content/img");     // ảnh thật nằm ở đây
            var safe = (file ?? "").Replace("\\", "/").TrimStart('/'); // chống path traversal
            var path = System.IO.Path.Combine(root, safe);

            if (!System.IO.File.Exists(path))
                path = System.IO.Path.Combine(root, "no-image.png");   // fallback

            var mime = MimeMapping.GetMimeMapping(path);
            return File(path, mime);
        }



    }

}
