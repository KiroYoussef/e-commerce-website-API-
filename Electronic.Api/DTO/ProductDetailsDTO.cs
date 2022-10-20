namespace Electronic.Api.DTO
{
    public class ProductDetailsDTO
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string Ram { get; set; }
        public string HardDrive { get; set; }
        public string Camera { get; set; }
        public string Description { get; set; }
        public string processor { get; set; }
        public float? ScreenSize { get; set; }
        public int Discount { get; set; }
        public float price { get; set; }
        public string img { get; set; }
        public long CountProduct { get; set; }
        public string SubCategoryName { get; set; }
        public int SubCategoryID { get; set; }
        public string SellerName { get; set; }
    }
}
