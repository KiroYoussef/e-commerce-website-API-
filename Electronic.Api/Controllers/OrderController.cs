using Electronic.Api.DTO;
using Electronic.Api.Model;
using Electronic.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Electronic.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;
        private readonly IOrderProductService orderProductService;
        private readonly IProductService productService;

        public OrderController(IOrderService orderService, IOrderProductService orderProductService, IProductService productService)
        {
            this.orderService = orderService;
            this.orderProductService = orderProductService;
            this.productService = productService;
        }

        [HttpGet]
        [Route("GetOrderById/{Id}")]
        public async Task<IActionResult> GetOrderById(int Id)
        {
            var ord = await orderService.GetOrderById(Id);
            if (ord == null)
            {
                return BadRequest("No Order Found");
            }

            return Ok(ord);
        }
        [HttpGet("GetAllOrders")]
        public async Task<IActionResult> GetAllOrders()
        {
            return Ok(await orderService.GetAllOrders());
        }

        [HttpGet("GetAllOrdersByUserId/{UserId}")]
        public async Task<IActionResult> GetAllOrdersByUserId(string UserId)
        {
            return Ok(await orderService.GetAllOrdersByUserId(UserId));
        }

        [HttpGet]
        [Route("GetOrderByUserId/{UserId}")]
        public IActionResult GetOrderByUserId(string UserId)
        {
            return Ok(orderService.GetOrdersByUserId(UserId));
        }


        [HttpGet("GetAllOrdersByDelevary/{UserId}")]
        public async Task<IActionResult> GetAllOrdersByDelevary(string UserId)
        {
            return Ok(await orderService.GetAllOrdersByDelevary(UserId));
        }


        [HttpPost]
        [Route("AddOrder/{iduser}/{payment}/{Address}/{State}")]
        public IActionResult AddOrder(string iduser, string payment, string Address, string State)
        {
            if (ModelState.IsValid)
            {
                orderService.AddNewOrder(iduser, payment, Address, State);
                return Ok("Order Added Successfully");
            }
            return BadRequest("Faild Order Added ");


        }
        [HttpPut]
        [Route("UpdateOrder")]
        public IActionResult UpdateOrder([FromBody] OrderDTO ordDTO)
        {
            if (ModelState.IsValid)
            {
                orderService.UpdateOrder(ordDTO);
                return Ok("Order Updated Successfully");
            }
            return BadRequest(ordDTO);
        }
        [HttpDelete]
        [Route("RemoveOrder/{Id}")]
        public IActionResult Remove(int Id)
        {
            orderService.RemoveOrder(Id);
            return Ok("Order Removed Successfully");
        }


        [HttpPut]
        [Route("UpdateStatusOrder/{IdOrder}/{Status}")]
        public IActionResult Remove(int IdOrder, string Status)
        {
            bool result = orderService.UpdateStatusOrder(IdOrder, Status);
            if (result)
            {
                return Ok(" Edit Successfully");
            }
            else
            {
                return BadRequest("Order Edit Status Faild");

            }

        }

    }
}

