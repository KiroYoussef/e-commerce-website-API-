namespace Electronic.Api.DTO
{
    public class OrderProductBySellerDTO
    {
        public int ID { get; set; }

        public int quantity { get; set; }

        public string Order_Approve { get; set; }

        public int Order_ID { get; set; }

        public int Product_Id { get; set; }
        public string Product_img { get; set; }


        public string Product_NAME{ get; set; }
    }
}
