using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Electronic.Api.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string Address { get; set; }
        public string Img { get; set; }
        public virtual ICollection<FvoriteProduct> FvoriteProduct { get; set; }
        public virtual ICollection<Cart> cart { get; set; }
        public virtual ICollection<Order> Order { get; set; }
        public virtual ICollection<Product> Product { get; set; }

        public virtual ICollection<Comment> Comment { get; set; }

    }
}
