using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PharmacyStore.Models
{
    public class Tag
    {
        [Key]  // 🔑 Bắt buộc phải có
        public int TagId { get; set; }

        public string TagName { get; set; }

        // Navigation
        public virtual ICollection<ProductTag> ProductTags { get; set; }
    }
}