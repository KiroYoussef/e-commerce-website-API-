namespace Electronic.Api.DTO
{
    public class AllProductsSellerDTO
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string? Description { get; set; }
        public int Discount { get; set; }
        public float price { get; set; }
        public string img { get; set; }
        public string Active { get; set; }
        public long CountProduct { get; set; }
        public string SubCategoryName { get; set; }
    }
}
