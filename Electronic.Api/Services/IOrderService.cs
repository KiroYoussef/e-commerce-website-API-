using Electronic.Api.DTO;
using Electronic.Api.Model;

namespace Electronic.Api.Services
{
    public interface IOrderService
    {
        Task<OrderDTO> GetOrderById(int Id);
        IEnumerable<Order> GetOrdersByUserId(string UserId);
        Task<IEnumerable<OrderDTO>> GetAllOrders();
        Task<IEnumerable<OrderDTO>> GetAllOrdersByUserId(string UserId);

        Task<IEnumerable<OrderDTO>> GetAllOrdersByDelevary(string UserId);

        void RemoveOrder(int Id);
        void AddNewOrder(string iduser, string payment, string Address, string State);
        void UpdateOrder(OrderDTO ordDTO);
        //int Count();
        bool UpdateStatusOrder(int IdOrder, string Status);
    }
}
