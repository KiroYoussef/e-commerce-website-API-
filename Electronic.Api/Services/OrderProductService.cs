using Electronic.Api.DTO;
using Electronic.Api.Model;
using Electronic.Api.Repository;
using Microsoft.AspNetCore.Identity;

namespace Electronic.Api.Services
{
    public class OrderProductService : IOrderProductService
    {
        private readonly IBaseRepository<SubCategory> subCategoryproduct;
        private readonly IBaseRepository<Product> productReposatory;
        private readonly IBaseRepository<Order> orderReposatory;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IBaseRepository<OrderProducts> orderproduct;
        private readonly ContextDB context;
        public OrderProductService(IBaseRepository<SubCategory> SubCategoryproduct, IBaseRepository<Product> productReposatory, IBaseRepository<Order> orderReposatory, UserManager<ApplicationUser> userManager, IBaseRepository<OrderProducts> _orderproduct, ContextDB _contextDB)
        {
            subCategoryproduct = SubCategoryproduct;
            this.productReposatory = productReposatory;
            this.orderReposatory = orderReposatory;
            _userManager = userManager;
            orderproduct = _orderproduct;
            context = _contextDB;

        }

        public void AddNewOrderProduct(OrderProductDTO ordprodDTO)
        {
            var OrdProd = new OrderProducts
            {
                Quantity = ordprodDTO.quantity,
                OrderApprove = ordprodDTO.Order_Approve,
                OrderID = ordprodDTO.Order_ID,
                ProductID = ordprodDTO.Product_ID
            };
            orderproduct.Insert(OrdProd);

        }

        public IEnumerable<OrderProductDTO> GetAllOrderProducts()
        {
            var ordprods = orderproduct.GetAll();
            List<OrderProductDTO> OrdProdDTOs = new List<OrderProductDTO>();
            if (ordprods != null && ordprods.Count() > 0)
            {
                foreach (var ordprod in ordprods)
                {
                    OrdProdDTOs.Add(new OrderProductDTO
                    {
                        ID = ordprod.Id,
                        quantity = ordprod.Quantity,
                        Order_Approve = ordprod.OrderApprove,
                        Order_ID = ordprod.OrderID,
                        Product_ID = ordprod.ProductID

                    });
                }
                return OrdProdDTOs;
            }
            return null;
        }

        public async Task<ReportOrderDto> GetOrderProductById(int Id)
        {
            var ordproduct = orderproduct.GetAllwhere(e => e.OrderID == Id);
            var order = orderReposatory.GetById(Id);
            ApplicationUser user = await _userManager.FindByIdAsync(order.UserID);
            if (ordproduct != null)
            {
                var OrdProdDTO = new ReportOrderDto
                {

                    UserName = user.UserName,
                    Email = user.Email,
                    Phone = user.PhoneNumber,
                    Address = order.State + " " + order.Address,

                    Create_Date = order.CreateDate,
                    OrderPlace_Date = order.CreateDate.AddDays(4),
                    Payment_Type = order.PaymentType,
                    Total_Price = order.TotalPrice,
                };



                foreach (var pro in ordproduct)
                {
                    Product product = productReposatory.GetById(pro.ProductID);
                    ProductsByReportDto dto = new ProductsByReportDto();
                    dto.Order_Approve = pro.OrderApprove;
                    dto.name = product.name;
                    dto.price = product.price * pro.Quantity;
                    dto.img = "https://localhost:7096/Img/Products/" + product.img;
                    dto.Quantity = pro.Quantity;
                    dto.SubCategoryName = subCategoryproduct.GetById(product.SubCategoryID).name;
                    OrdProdDTO.reportDto.Add(dto);
                }



                return OrdProdDTO;
            }
            return null;
        }

        public void RemoveOrderProduct(int Id)
        {
            var OrdProd = orderproduct.GetById(Id);
            if (OrdProd != null)
            {
                orderproduct.Delete(OrdProd);
            }
        }

        public void UpdateOrderProduct(int Id, OrderProductDTO OrdProdDTO)
        {
            var OrdProd = orderproduct.GetById(Id);
            if (OrdProd != null)
            {
                OrdProd.Quantity = OrdProdDTO.quantity;
                OrdProd.OrderApprove = OrdProdDTO.Order_Approve;
                OrdProd.OrderID = OrdProdDTO.Order_ID;
                OrdProd.ProductID = OrdProdDTO.Product_ID;
                orderproduct.Update(OrdProd);
            }
        }
        public bool UpdateOrderProductApprove(int OrdProdId, string Approve)
        {
            var OrdProd = orderproduct.GetById(OrdProdId);
            if (OrdProd != null)
            {
                OrdProd.OrderApprove = Approve;
                orderproduct.Update(OrdProd);
                return true;
            }
            return false;
        }
        public List<OrderProductDTO> GetOrderProductBySellerId(string UserId)
        {
            var ordprods = orderproduct.GetAllwhere(o => o.Product.UserID == UserId);
            List<OrderProductDTO> OrdProdDTOs = new List<OrderProductDTO>();
            if (ordprods != null && ordprods.Count() > 0)
            {
                foreach (var ordprod in ordprods)
                {
                    OrdProdDTOs.Add(new OrderProductDTO
                    {
                        ID = ordprod.Id,
                        quantity = ordprod.Quantity,
                        Order_Approve = ordprod.OrderApprove,
                        Order_ID = ordprod.OrderID,
                        Product_ID = ordprod.ProductID

                    });
                }
                return OrdProdDTOs;
            }
            return null;
        }

    }
}