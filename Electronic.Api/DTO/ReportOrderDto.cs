namespace Electronic.Api.DTO
{
    public class ReportOrderDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public string Address { get; set; }

        public float Total_Price { get; set; }

        public DateTime Create_Date { get; set; }

        public DateTime OrderPlace_Date { get; set; }
        public string Payment_Type { get; set; }
        public List<ProductsByReportDto> reportDto { get; set; } = new List<ProductsByReportDto>();

    }

}
