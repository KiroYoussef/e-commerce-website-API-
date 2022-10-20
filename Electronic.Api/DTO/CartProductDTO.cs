namespace Electronic.Api.DTO
{
    public class CartProductDTO
    {

        public int Id { get; set; }
        public string name { get; set; }
        public float price { get; set; }
        public string img { get; set; }
        public int Quantity { get; set; }
        public long countProductMax { get; set; }
        public string SubCategoryName { get; set; }


    }
}
