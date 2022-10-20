using System.ComponentModel.DataAnnotations.Schema;

namespace Electronic.Api.Model
{
    public class Comment
    {
        public int Id { get; set; }
        public string comment { get; set; }
        public DateTime CreateDate { get; set; }= DateTime.Now;
        [ForeignKey("Product")]
        public int ProductID { get; set; }
        public virtual Product? Product { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserID { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
