namespace Electronic.Api.DTO
{
    public class UserCommentDTO
    {
        public string UserId { get; set; }
        public int ProductId { get; set; }
        public String comment { get; set; }
        public String UserImg { get; set; }
        public String UserName { get; set; }
        public DateTime date { get; set; }


    }
}
