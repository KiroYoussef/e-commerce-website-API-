using System.ComponentModel.DataAnnotations.Schema;

namespace Electronic.Api.Model
{
    public class FvoriteProduct
    {
        public int iD { get; set; }


        [ForeignKey("ApplicationUser")]
        public string? UserID { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }


        [ForeignKey("Product")]
        public int ProductID { get; set; }
        public virtual Product Product { get; set; }

    }
}
