using Electronic.Api.Dtos;
using Electronic.Api.Model;
using Electronic.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Electronic.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly IWebHostEnvironment webHostEnvironment;
        public ProductController(IProductService s, IWebHostEnvironment webHostEnvironment)
        {
            _service = s;
            this.webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        [Route("GetProductByProductId/{Id}")]
        public IActionResult GetProductById(int Id)
        {
            var p = _service.GetProductByID(Id);
            if (p == null)
                return BadRequest("No Item Found");
            return Ok(p);
        }
        [HttpGet,Route("GetAllProducts")]
        public IActionResult GetAllProducts(string? UserId)
        {
            return Ok(_service.GetAllProducts(UserId));
        }
        [HttpGet]
        [Route("GetProductByUserId")]
        public IActionResult GetProductByUserId(string? UserId)
        {
            return Ok(_service.GetProductsByUserId(UserId));
        }
        [HttpGet]
        [Route("GetTopAddedProducts")]
        public IActionResult GetTopAddedProducts()
        {
            return Ok(_service.GetTopAddedProducts());
        }
        [HttpPost]
        [Route("AddNewProduct")]
        public IActionResult AddProduct([FromForm] ProductDTO pDTO)
        {
            if (ModelState.IsValid)
            {
                pDTO.img = UploadFile(pDTO.image);
                _service.AddNewProduct(pDTO);
                return Ok("Product Added Successfully");
            }
            return BadRequest(pDTO);
        }
       
        [HttpPut("UpdateProduct")]
       
        public IActionResult UpdateProduct([FromForm] ProductDTO pDTO)
        {
            if (ModelState.IsValid)
            {
                if (pDTO.img.Length>1)
                {
                    pDTO.img = UploadFile(pDTO.image);
                }
                _service.UpdateProduct(pDTO);
                return Ok("You Updated That Product");
            }
            return BadRequest(pDTO);
        }
        [HttpDelete]
        [Route("RemoveProduct/{Id}")]
        
        public IActionResult Remove(int Id)
        {
           if(_service.RemoveProduct(Id))
              return Ok("Product Removed Successfully");
            return BadRequest("No Product To Delete");
        }
        [HttpGet]
        [Route("RemoveProduct/{UserId}/{ProductId}/{status}")]
        public IActionResult EditActiveProduct([FromRoute] int status,[FromRoute] User_ProductDTO Data)
        {
            if (_service.EditActiveProduct(Data, status)) {
                return Ok("Product Removed Successfully");
            }
            return BadRequest("No Product To Delete");

        }

        [HttpPut]
        [Route("UpdateProductApprove/{ProductId}/{approve}")]
        public IActionResult UpdateProductApprove(int ProdId, bool firstApprove)
        {
            if (_service.UpdateProductApprove(ProdId, firstApprove))
            {
                return Ok("product Approved");

            }
            return Ok("product Not Found");
        }
        private string UploadFile(IFormFile Image)
        {
            string fileName = null;
            if (Image != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "img/Products");

                fileName = Guid.NewGuid().ToString() + "_" + Image.FileName;
                string filePath = Path.Combine(uploadsFolder, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Image.CopyTo(fileStream);
                    fileStream.Close();
                }
            }
            return fileName;


        }

    }
}
