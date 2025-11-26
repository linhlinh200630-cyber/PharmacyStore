using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PharmacyStore.Models
{
    public class ProductTag
    {
        [Key, Column(Order = 0)]  // 🔑 Khóa 1
        public int ProductId { get; set; }

        [Key, Column(Order = 1)]  // 🔑 Khóa 2
        public int TagId { get; set; }

        // Navigation properties
        public virtual Product Product { get; set; }
        public virtual Tag Tag { get; set; }
    }
}