using System.ComponentModel.DataAnnotations;

namespace Electronic.Api.Model
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        public string name { get; set; }
        public virtual ICollection<SubCategory> SubCategory { get; set; }

    }
}
