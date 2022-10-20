namespace Electronic.Api.DTO
{
    public class FavoriteProductsDTO
    {
    
            public int Id { get; set; }
            public int FavoriteID { get; set; }
            public string name { get; set; }
            public string Description { get; set; }
            public float Discount { get; set; }
            public float price { get; set; }
            public string img { get; set; }
            public string SubCategoryName { get; set; }
}
}
