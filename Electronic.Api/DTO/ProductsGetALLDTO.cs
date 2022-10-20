namespace Electronic.Api.DTO
{
    public class ProductsGetALLDTO
    {
        public int Id { get; set; }
        public string name { get; set; }
        public int Discount { get; set; }
        public float priceAfterDisc { get; set; }
        public float price { get; set; }
        public string img { get; set; }
        public bool? FirstAprove { get; set; }
        public int IsFavorite { get; set; }
        public string SubCategoryName { get; set; }
        public string userId { get; set; }
        public bool active { get; set; }
    }
}
