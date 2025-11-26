using PharmacyStore.Models;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PharmacyStore.Controllers
{
    // KHÔNG dán [Authorize] ở đây để khách còn xem được hàng
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();
        private static readonly string[] _allowed = { ".jpg", ".jpeg", ".png", ".webp", ".gif" };

        // --- CÁC HÀM KHÁCH HÀNG ĐƯỢC XEM ---

        // Ai cũng xem được danh sách
        public ActionResult Index(string q, int? categoryId, decimal? min, decimal? max)
        {
            // ... code cũ giữ nguyên ...
            var query = db.Products.Include(p => p.Category).AsQueryable();
            if (!string.IsNullOrWhiteSpace(q)) query = query.Where(p => p.Name.Contains(q));
            if (categoryId.HasValue) query = query.Where(p => p.CategoryId == categoryId);
            if (min.HasValue) query = query.Where(p => p.Price >= min);
            if (max.HasValue) query = query.Where(p => p.Price <= max);

            ViewBag.Categories = new SelectList(db.Categories.OrderBy(c => c.Name), "CategoryId", "Name", categoryId);
            ViewBag.q = q; ViewBag.min = min; ViewBag.max = max;
            return View(query.OrderBy(p => p.Name).ToList());
        }

        // Ai cũng xem được chi tiết
        public ActionResult Details(int? id)
        {
            // ... code cũ giữ nguyên ...
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var p = db.Products.Include(x => x.Category).FirstOrDefault(x => x.ProductId == id);
            if (p == null) return HttpNotFound();
            return View(p);
        }

        public ActionResult QuickView(int id)
        {
            // ... code cũ giữ nguyên ...
            using (var db = new ApplicationDbContext())
            {
                var p = db.Products.Find(id);
                if (p == null) return HttpNotFound();
                return PartialView("_QuickView", p);
            }
        }

        // --- CÁC HÀM CHỈ ADMIN ĐƯỢC DÙNG (Dán [Authorize] vào đây) ---

        [Authorize(Roles = "Admin")] // <--- Dán vào đây
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name");
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")] // <--- Dán vào đây (cả POST)
        public ActionResult Create(Product product)
        {
            // ... code cũ giữ nguyên ...
            if (product.Upload != null && product.Upload.ContentLength > 0)
                product.ImageUrl = SaveImage(product.Upload);

            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", product.CategoryId);
            return View(product);
        }

        [Authorize(Roles = "Admin")] // <--- Dán vào đây
        public ActionResult Edit(int id)
        {
            // ... code cũ giữ nguyên ...
            var p = db.Products.Find(id); if (p == null) return HttpNotFound();
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", p.CategoryId);
            return View(p);
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")] // <--- Dán vào đây
        public ActionResult Edit(Product product)
        {
            // ... code cũ giữ nguyên ...
            if (product.Upload != null && product.Upload.ContentLength > 0)
                product.ImageUrl = SaveImage(product.Upload);

            if (ModelState.IsValid)
            {
                db.Entry(product).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", product.CategoryId);
            return View(product);
        }

        [Authorize(Roles = "Admin")] // <--- Dán vào đây
        public ActionResult Delete(int id)
        {
            // ... code cũ giữ nguyên ...
            var p = db.Products.Find(id); if (p == null) return HttpNotFound();
            return View(p);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")] // <--- Dán vào đây
        public ActionResult DeleteConfirmed(int id)
        {
            // ... code cũ giữ nguyên ...
            var p = db.Products.Find(id);
            db.Products.Remove(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // Hàm này là nội bộ, không cần Authorize vì user không gọi trực tiếp được
        private string SaveImage(HttpPostedFileBase file)
        {
            // ... code cũ giữ nguyên ...
            var ext = Path.GetExtension(file.FileName).ToLower();
            if (!_allowed.Contains(ext)) throw new System.InvalidOperationException("Chỉ nhận ảnh .jpg,.jpeg,.png,.gif,.webp");
            if (!file.ContentType.StartsWith("image")) throw new System.InvalidOperationException("File không phải ảnh");

            var fileName = System.Guid.NewGuid().ToString("N") + ext;
            var virtualDir = "~/Uploads/Products";
            var physicalDir = Server.MapPath(virtualDir);
            Directory.CreateDirectory(physicalDir);
            var path = Path.Combine(physicalDir, fileName);
            file.SaveAs(path);
            return VirtualPathUtility.ToAbsolute(virtualDir + "/" + fileName);
        }
    }
}