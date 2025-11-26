using System;
using PharmacyStore.Models; // để dùng Product

namespace PharmacyStore.Models
{
    [Serializable]
    public class CartItem
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }   // đơn giá tại thời điểm thêm vào
        public int Quantity { get; set; }

        public decimal LineTotal => Price * Quantity;
    }
}
