using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace Electronic.Api.Model
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string? name { get; set; }
        public string? Ram { get; set; }
        public string? HardDrive { get; set; }
        public string? Camera { get; set; }
        [MaxLength(1000), MinLength(10)]
        public string? Description { get; set; }
        public string? processor { get; set; }
        public float? ScreenSize { get; set; }
        public int Discount { get; set; }
        [Required]
        public float price { get; set; }
        [Required]
        public string img { get; set; }
        [Required]

        public bool Active { get; set; }=true;
        public bool AdminReview { get; set; } = true;

        public bool FirstAprove { get; set; } = false;
        public long CountProduct { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public virtual ICollection<FvoriteProduct> FvoriteProduct { get; set; }
        public virtual ICollection<OrderProducts> OrderProducts { get; set; }
        public virtual ICollection<ProductImages> ProductImages { get; set; }
        public virtual ICollection<Comment>? Comment { get; set; }
        public virtual ICollection<Cart> cart { get; set; }



        [ForeignKey("SubCategory")]
        public int SubCategoryID { get; set; }
        public virtual SubCategory SubCategory { get; set; }



        [ForeignKey("ApplicationUser")]
        public string UserID { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
