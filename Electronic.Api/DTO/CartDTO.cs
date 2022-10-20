namespace Electronic.Api.DTO
{
    public class CartDTO
    {
        public int Id { get; set; }
        public int Quantity { get; set; }

        public string UserID { get; set; }

        public int ProductID { get; set; }
    }
}
