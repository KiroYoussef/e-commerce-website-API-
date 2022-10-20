using System.ComponentModel.DataAnnotations.Schema;

namespace Electronic.Api.Model
{
    public class Order
    {
        public int Id { get; set; }
        public float TotalPrice { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        [ForeignKey("ApplicationUser")]
        public string? UserID { get; set; }
        public string? PaymentType { get; set; }
        [ForeignKey("Satus")]
        public int SatusId { get; set; }
        public string? Address { get; set; }
        public string? State { get; set; }

        public string? DeliveryId { get; set; } 
        public virtual SatusHistory? Satus { get; set; }
        public virtual ApplicationUser? ApplicationUser { get; set; }
        public virtual ICollection<OrderProducts>? OrderProducts { get; set; }

    }
}
