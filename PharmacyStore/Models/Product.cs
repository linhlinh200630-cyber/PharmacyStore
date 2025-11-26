using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace PharmacyStore.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }      // ← dùng Name, KHÔNG phải ProductName
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Unit { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        [NotMapped]
        public HttpPostedFileBase Upload { get; set; }
        public virtual ICollection<ProductTag> ProductTags { get; set; } = new List<ProductTag>();
     
    }
}
