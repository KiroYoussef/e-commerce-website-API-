namespace Electronic.Api.Dtos
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string? name { get; set; }
        public string? Ram { get; set; }
        public string? HardDrive { get; set; }
        public string? Camera { get; set; }
        public string? Description { get; set; }
        public string? processor { get; set; }
        public float? ScreenSize { get; set; }
        public int Discount { get; set; }
        public float price { get; set; }
        public string? img { get; set; }
        public IFormFile? image { get; set; }
        public bool? firstApprove { get; set; } = false;
        public long CountProduct { get; set; }
        public int SubCategoryID { get; set; }
        public string UserID { get; set; }
    }
}
