using PharmacyStore.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PharmacyStore.Models;       // ★ để thấy CartItem, Order, ApplicationDbContext
using System.Data.Entity;
using System;// nếu có Include/EF


namespace PharmacyStore.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();
        private const string CART = "CART";

        // Hiển thị trang xác nhận đơn hàng
        public ActionResult Checkout()
        {
            var cart = Session[CART] as List<CartItem>;
            if (cart == null || !cart.Any())
                return RedirectToAction("Index", "Cart");

            ViewBag.Total = cart.Sum(i => i.Price * i.Quantity);
            return View(cart);
        }

        // Xử lý khi người dùng xác nhận thanh toán
        [HttpPost]
        public ActionResult CheckoutConfirm(string customerName, string address, string phone)
        {
            var cart = Session["CART"] as Cart;
            if (cart == null || !cart.Items.Any())
                return RedirectToAction("Index", "Cart");

            using (var db = new ApplicationDbContext())
            {
                var order = new Order
                {
                    OrderDate = DateTime.Now,
                    CustomerName = customerName,
                    Address = address,
                    Phone = phone,
                    Total = cart.Items.Sum(i => i.Price * i.Quantity)
                };

                db.Orders.Add(order);
                db.SaveChanges(); // để có OrderId

                foreach (var item in cart.Items)
                // cart là Cart (IEnumerable<CartItem>)
                {
                    var orderDetail = new OrderDetail
                    {
                        OrderId = order.OrderId,
                        ProductId = item.ProductId,  // ✅ KHÔNG dùng item.Product.Id
                        Quantity = item.Quantity,
                        UnitPrice = item.Price       // ✅ KHÔNG dùng item.Product.Price
                    };
                    db.OrderDetails.Add(orderDetail);
                }


                db.SaveChanges();

                Session["CART"] = null; // <-- có ngoặc kép
            }

            return RedirectToAction("Success", "Cart");
        }


        // Trang xác nhận đặt hàng thành công
        public ActionResult Success()
        {
            return View();
        }
    }
}
