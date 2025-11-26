using System;
using System.Collections.Generic;

namespace PharmacyStore.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public string CustomerName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Total { get; set; }

        // danh sách chi tiết đơn
        public virtual ICollection<OrderDetail> Items { get; set; } = new List<OrderDetail>();
    }
}
