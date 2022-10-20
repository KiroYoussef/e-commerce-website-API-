using System.ComponentModel.DataAnnotations;

namespace Electronic.Api.DTO
{
    public class OrderProductDTO
    {
        public int ID { get; set; }

        [Required]
        public int quantity { get; set; }

        [Required]
        public string Order_Approve { get; set; }

        [Required]
        public int Order_ID { get; set; }

        [Required]
        public int Product_ID { get; set; }
    }
}
