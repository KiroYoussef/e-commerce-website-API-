using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace Electronic.Api.Model
{
    public class SubCategory
    {
        public int Id { get; set; }
        [Required]
        public string? name { get; set; }
        public virtual ICollection<Product>? Product { get; set; }

        [ForeignKey("Category")]
        public int CategoryID { get; set; }

        public virtual Category Category { get; set; }

    }
}
