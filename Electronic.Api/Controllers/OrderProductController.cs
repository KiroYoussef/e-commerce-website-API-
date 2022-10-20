using Electronic.Api.DTO;
using Electronic.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Electronic.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderProductController : ControllerBase
    {
        private readonly IOrderService orderService;
        private readonly IOrderProductService orderProductService;
        private readonly IProductService productService;
        public OrderProductController(IOrderService orderService, IOrderProductService orderProductService, IProductService productService)
        {
            this.orderService = orderService;
            this.orderProductService = orderProductService;
            this.productService = productService;
        }

        [HttpGet]
        [Route("GetOrderProductById/{Id}")]
        public async Task<IActionResult> GetOrderProductById(int Id)
        {
            var ordprod = await orderProductService.GetOrderProductById(Id);
            if (ordprod == null)
            {
                return Ok(new { Msg = "No Items Found" });
            }

            return Ok(ordprod);
        }
        [HttpGet]
        public IActionResult GetAllOrderProducts()
        {
            return Ok(orderProductService.GetAllOrderProducts());
        }

        [HttpPost]
        [Route("AddOrderProduct")]
        public IActionResult AddOrderProduct([FromBody] OrderProductDTO ordprodDTO)
        {
            if (ModelState.IsValid)
            {
                orderProductService.AddNewOrderProduct(ordprodDTO);
                return Ok("Order Of Products Added Successfully");
            }
            return BadRequest(ordprodDTO);
        }
        [HttpPut]
        [Route("UpdateOrderProduct/{Id}")]
        public IActionResult UpdateOrderProduct([FromRoute] int Id, [FromBody] OrderProductDTO ordprodDTO)
        {
            if (ModelState.IsValid)
            {
                orderProductService.UpdateOrderProduct(Id, ordprodDTO);
                return Ok("Order Of Products Updated Successfully");
            }
            return BadRequest(ordprodDTO);
        }
        [HttpDelete]
        [Route("RemoveOrderProduct/{Id}")]
        public IActionResult Remove(int Id)
        {
            orderProductService.RemoveOrderProduct(Id);
            return Ok("Order Of Products Removed Successfully");
        }

        [HttpGet]
        [Route("GetOrderProductBySellerId/{UserId}")]
        public IActionResult GetOrderProductBySellerId(string UserId)
        {
            orderProductService.GetOrderProductBySellerId(UserId);
            return Ok(orderProductService.GetOrderProductBySellerId(UserId));
        }


        [HttpGet]
        [Route("UpdateOrderProductApprove/{OrdProdId}/{Approve}")]

        public IActionResult UpdateOrderProductApprove(int OrdProdId, string Approve)
        {
            if (orderProductService.UpdateOrderProductApprove(OrdProdId, Approve))
            {
                return Ok("product Approved");

            }
            return Ok("product Not Found");
        }

    }
}