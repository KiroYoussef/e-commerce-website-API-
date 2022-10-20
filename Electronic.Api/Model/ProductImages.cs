using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace Electronic.Api.Model
{
    public class ProductImages
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [ForeignKey("Product")]
        public int ProductID { get; set; }

        public virtual Product? Product { get; set; }

    }
}
