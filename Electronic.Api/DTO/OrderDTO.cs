using Electronic.Api.Model;
using System.ComponentModel.DataAnnotations;

namespace Electronic.Api.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        [Required]
        public float Total_Price { get; set; }

        [Required]
        public DateTime Create_Date { get; set; } = new DateTime();
        public DateTime OrderPlace_Date { get; set; } = new DateTime();
        public string? User_Id { get; set; }
        public int Count_Product { get; set; }
        public string Satus { get; set; }

        public string img { get; set; }
        public string Name { get; set; }

        public string? Address { get; set; }
        public string? State { get; set; }

        public string? DeliveryName { get; set; }
        /*[Required]
        public int[] Product_Id { get; set; }
        public List<Product>? Products { get; set; } = new List<Product>();*/

    }
}
