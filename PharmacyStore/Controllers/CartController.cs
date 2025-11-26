using System.Linq;
using System.Web.Mvc;
using PharmacyStore.Models;

namespace PharmacyStore.Controllers
{
    public class CartController : Controller
    {
        // Khởi tạo DB Context để dùng cho việc lấy thông tin sản phẩm
        private ApplicationDbContext db = new ApplicationDbContext();
        const string CART = "CART";

        // Hàm lấy giỏ hàng từ Session
        private Cart GetCart()
        {
            var cart = Session[CART] as Cart;
            if (cart == null)
            {
                cart = new Cart();
                Session[CART] = cart;
            }
            return cart;
        }

        // GET: /Cart
        // Ai cũng xem được giỏ hàng, không cần đăng nhập
        public ActionResult Index()
        {
            return View(GetCart());
        }

        // POST/GET: /Cart/Add/1015?qty=1&returnUrl=/Home/Index
        public ActionResult Add(int id, int qty = 1, string returnUrl = null)
        {
            // Tìm sản phẩm trong DB
            var p = db.Products.Find(id);
            if (p != null)
            {
                var item = new CartItem
                {
                    ProductId = p.ProductId,
                    Name = p.Name,
                    ImageUrl = p.ImageUrl,
                    Price = p.Price
                };
                // Thêm vào giỏ
                GetCart().Add(item, qty);
            }

            // Quay lại trang cũ hoặc về trang giỏ hàng
            if (!string.IsNullOrEmpty(returnUrl)) return Redirect(returnUrl);
            return RedirectToAction("Index");
        }

        // POST: /Cart/Update
        [HttpPost]
        public ActionResult Update(int id, int qty)
        {
            GetCart().Update(id, qty);
            return RedirectToAction("Index");
        }

        // GET: /Cart/Remove/1015
        public ActionResult Remove(int id)
        {
            GetCart().Remove(id);
            return RedirectToAction("Index");
        }

        // GET: /Cart/Clear
        public ActionResult Clear()
        {
            GetCart().Clear();
            return RedirectToAction("Index");
        }

        // Partial hiển thị mini-cart ở header (nếu bạn dùng)
        [ChildActionOnly]
        public ActionResult _CartSummary()
        {
            return PartialView(GetCart());
        }

        // --- KHU VỰC THANH TOÁN (BẮT BUỘC ĐĂNG NHẬP) ---

        // GET: /Cart/Checkout
        [Authorize] // <--- Dòng này bắt buộc người dùng phải đăng nhập mới vào được trang thanh toán
        [HttpGet]
        public ActionResult Checkout()
        {
            var cart = GetCart();
            // Nếu giỏ trống thì đá về trang chủ hoặc trang giỏ
            if (cart.IsEmpty) return RedirectToAction("Index");

            // Có thể lấy thông tin user đang đăng nhập để điền sẵn vào form (tùy chỉnh sau)
            // var userEmail = User.Identity.Name; 

            return View(cart);
        }

        // POST: /Cart/Checkout
        [HttpPost, ValidateAntiForgeryToken]
        [Authorize] // <--- Bắt buộc đăng nhập khi submit đơn hàng
        public ActionResult Checkout(string customerName, string address, string phone)
        {
            // 1. Code lưu đơn hàng vào bảng Order và OrderDetails sẽ viết ở đây
            // ...

            // 2. Sau khi lưu xong thì xóa giỏ hàng
            GetCart().Clear();

            // 3. Chuyển hướng đến trang thông báo thành công
            return RedirectToAction("Success");
        }

        public ActionResult Success()
        {
            return View();
        }

        // Giải phóng tài nguyên DB khi xong request
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}