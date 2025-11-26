using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacyStore.Models
{
    // Ánh xạ Model này tới bảng có tên là "Categories" trong cơ sở dữ liệu
    [Table("Categories")]
    public class Category
    {
        // Khóa chính
        [Key]
        public int CategoryId { get; set; }

        // Tên danh mục (e.g., Thuốc, Chăm sóc cá nhân)
        [Required(ErrorMessage = "Tên danh mục không được để trống")]
        [StringLength(100, ErrorMessage = "Tên danh mục không được vượt quá 100 ký tự")]
        [Display(Name = "Tên Danh Mục")]
        public string Name { get; set; }

        // Trường tùy chọn để lưu đường dẫn/slug thân thiện với SEO (Search Engine Optimization)
        // e.g., "Thực phẩm bảo vệ sức khỏe" -> "thuc-pham-bao-ve-suc-khoe"
        [StringLength(100)]
        [Display(Name = "Slug")]
        public string Slug { get; set; }

        // Trường để lưu Class CSS hoặc đường dẫn ảnh cho biểu tượng (Icon) hiển thị trên Sidebar
        // e.g., "fa fa-pills" (Font Awesome) hoặc "icon-drug" (Class CSS tùy chỉnh)
        [StringLength(50)]
        [Display(Name = "Biểu tượng (Icon Class)")]
        public string IconClass { get; set; }

        // Khóa ngoại (Optional): Dùng cho danh mục đa cấp (Danh mục con)
        // Nếu CategoryId là null, đây là danh mục cha (top-level category)
        [ForeignKey("ParentCategory")]
        public int? ParentId { get; set; }


        // --- Thuộc tính Điều hướng (Navigation Properties) ---

        // Mối quan hệ 1-nhiều: Một Danh mục chứa nhiều Sản phẩm
        // Đây là thuộc tính bạn đã có và nó đúng.
        public virtual ICollection<Product> Products { get; set; }

        // Mối quan hệ tự tham chiếu (Self-Referencing): Dùng cho danh mục cha/con
        // Tham chiếu tới danh mục cha (Parent)
        public virtual Category ParentCategory { get; set; }

        // Tập hợp các danh mục con (Children)
        public virtual ICollection<Category> ChildrenCategories { get; set; }

        // Constructor (Tùy chọn) để khởi tạo các Collection tránh lỗi NullReferenceException
        public Category()
        {
            Products = new HashSet<Product>();
            ChildrenCategories = new HashSet<Category>();
        }
    }
}