namespace PharmacyStore.Models
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }

        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public string ProductName { get; set; }  // chụp tên sản phẩm tại thời điểm mua
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }

        public decimal Subtotal => UnitPrice * Quantity;
    }
}
