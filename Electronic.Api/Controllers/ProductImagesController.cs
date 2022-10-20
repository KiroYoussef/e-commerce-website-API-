using Electronic.Api.Dtos;
using Electronic.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Electronic.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImagesController : ControllerBase
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IProductImagesService _service;
        public ProductImagesController(IProductImagesService service, IWebHostEnvironment webHostEnvironment)
        {
            _service = service;
            this.webHostEnvironment = webHostEnvironment;
        }
        [HttpGet,Route("GetAllProductImages")]
        public IActionResult GetAllProductsImages()
        {
            return Ok(_service.GetAllProductsImages());
        }
        [HttpGet,Route("GetProductImageByImageID/{Id}")]
        public IActionResult GetProductImagesByImageId(int Id)
        {
            return Ok(_service.GetProductImagesByImageID(Id));
        }
        [HttpGet,Route("GetProductImageByProductID/{ProductId}")]
        public IActionResult GetProductImageByProductID(int ProductId)
        {
            return Ok(_service.GetImagesByProductId(ProductId));
        }
        [HttpPost,Route("AddNewProductImage")]
        public IActionResult AddNewProductImage([FromForm]ProductImagesDTO ImageDTO)
        {
            if (ModelState.IsValid)
            {
                ImageDTO.Name = UploadFile(ImageDTO.image);
                if (ImageDTO.Name != null)
                {
                    _service.AddNewProductImages(ImageDTO);
                    return Ok("Image Added Successfully");
                }
            }
            return BadRequest(ImageDTO);
        }
        [HttpPut,Route("updateProductImage/{ImageId}")]
        public IActionResult UpdateProductImage(int ImageId,ProductImagesDTO ImageDTO)
        {
            if (ModelState.IsValid)
            {
                _service.UpdateProductImages(ImageId, ImageDTO);
                return Ok("Image Updated");
            }
            return BadRequest(ImageDTO);
        }
        [HttpDelete]
        [Route("RemoveProductImages/{ProductId}")]
        public IActionResult DeleteProductImage(int ProductId)
        {
            _service.RemoveProductImages(ProductId);
            return Ok("Deleted Successfully");
        }
        [HttpDelete]
        [Route("RemoveImgByImgId/{ImgId}")]
        public IActionResult DeleteProductImageByImgId(int ImgId)
        {

           if( _service.DeleteProductImageByImgId(ImgId))
            return Ok("Deleted Successfully");
            return BadRequest("Coulnot Delete That Image");
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
