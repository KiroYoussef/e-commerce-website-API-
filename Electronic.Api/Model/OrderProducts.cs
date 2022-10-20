using System.ComponentModel.DataAnnotations.Schema;

namespace Electronic.Api.Model
{
    public class OrderProducts
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public string OrderApprove { get; set; }
        [ForeignKey("Order")]
        public int OrderID { get; set; }

        public virtual Order? Order { get; set; }
        [ForeignKey("Product")]
        public int ProductID { get; set; }

        public virtual Product Product { get; set; }

    }
}
