using Electronic.Api.Model;
using Electronic.Api.Reporsitories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Electronic.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class favoriteProductController : ControllerBase
    {





        // private readonly context contexttDB;
        private readonly IFavoriteProductService favoriteProducts;
        private readonly ContextDB context;


        public favoriteProductController(IFavoriteProductService _IfavoriteProduct, ContextDB _contextDB)
        {
            this.favoriteProducts = _IfavoriteProduct;
            this.context = _contextDB;

        }



        [HttpGet]
        [Route("/GetFavoriteProduct/{UserId}")]
        public IActionResult GetAllFavoriteProduct(string UserId) { 
            var allPro = favoriteProducts.GetAllByID(UserId);
            if (allPro == null)
            {
                return BadRequest("id not valid");
            }
            else
            {
                return Ok(allPro);

            }
        }

        [HttpGet]
        [Route("checkProduct/{UserId}/{ProductId}")]

        public IActionResult checkProduct([FromRoute]User_ProductDTO UserP)
        {
           var Product =favoriteProducts.checkProduct(UserP);
                return Ok(Product);


        }

        [HttpGet]
        [Route("addFavoriteProduct/{UserId}/{ProductId}")]

        public IActionResult AddNewFavoriteProduct([FromRoute] User_ProductDTO UserP)
        {
            favoriteProducts.AddNew(UserP);
            return Ok( "data saved");

        }

        [HttpGet]
        [Route("remove/{favoId}")]

        public IActionResult delete(int favoId)
        {
            favoriteProducts.delete(favoId);
            return Ok("Item deleted");
        }

        [HttpGet]
        [Route("count/{userID}")]
        public IActionResult count(string userID)
        {
           var count =  favoriteProducts.count(userID);
            return Ok(count);

        }
    }
}
