using Electronic.Api.DTO;

namespace Electronic.Api.Services
{
    public interface IOrderProductService
    {
        Task<ReportOrderDto> GetOrderProductById(int Id);
        IEnumerable<OrderProductDTO> GetAllOrderProducts();
        void RemoveOrderProduct(int Id);
        void AddNewOrderProduct(OrderProductDTO ordprocDTO);
        void UpdateOrderProduct(int Id, OrderProductDTO ordprocDTO);
        List<OrderProductDTO> GetOrderProductBySellerId(string UserId);
        bool UpdateOrderProductApprove(int OrdProdId, string Approve);
    }
}
