using Electronic.Api.DTO;
using Electronic.Api.Dtos;
using Electronic.Api.Model;
using Electronic.Api.Repository;
using Electronic.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Electronic.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService cartService;

        public CartController(ICartService cartService)
        {
            this.cartService = cartService;
        }

        [HttpGet("GetAll/{iduser}")]
        public IActionResult GetAll(string iduser)
        {
            var products = cartService.GetAll(iduser);
            if (products == null)
            {
                return BadRequest("Dont Data");
            }
            return Ok(products);

        }


        [HttpPost("AddCart/{iduser}/{idprodect}")]
        public IActionResult AddCart(string iduser, int idprodect)
        {
            bool check = cartService.CheckAny(iduser, idprodect);
            if (check)
            {
                bool result = cartService.ChangeQuentityAndCheckMax(iduser, idprodect);
                if (!result)
                {
                    return BadRequest("the maximum number of product available");

                }
                return Ok("Add Cart successfully");

            };

            var addcart = cartService.AddCart(iduser, idprodect);

            if (!addcart)
            {
                return BadRequest("Add Cart Faild");
            }


            return Ok("Add Cart successfully");
        }


        [HttpPost("AddCartByDetails/{iduser}/{idprodect}/{count}")]
        public IActionResult AddCartByDetails(string iduser, int idprodect, int count)
        {

            bool check = cartService.CheckAny(iduser, idprodect);
            if (check)
            {
                bool result = cartService.ChangeQuentityAndCheckMax(iduser, idprodect, count);
                if (!result)
                {
                    return BadRequest("the maximum number of product available");

                }
                return Ok("Add Cart successfully");

            };


            bool addcart = cartService.AddCartByDetails(iduser, idprodect, count);

            if (!addcart)
            {
                return BadRequest("Add Cart Faild");
            }


            return Ok("Add Cart successfully");
        }


        [HttpGet("CountCart/{iduser}")]
        public IActionResult CountCart(string iduser)
        {

            return Ok(cartService.CountCart(iduser));

        }


        [HttpPost("AddCountProduct/{iduser}/{idprodect}/{pluscount}")]
        public IActionResult AddCountProduct(string iduser, int idprodect, int pluscount)
        {
            var update = cartService.AddCountProduct(iduser, idprodect, pluscount);
            if (!update) { return BadRequest(); }
            return Ok();

        }



        [HttpDelete("DeleteCartProduct/{iduser}/{idprodect}")]
        public IActionResult DeleteCartProduct(string iduser, int idprodect)
        {
            var cart = cartService.DeleteCartProduct(iduser, idprodect);
            if (!cart)
            {
                return BadRequest("There is no product");

            }
            return Ok("Deleted successfully");

        }




    }
}
