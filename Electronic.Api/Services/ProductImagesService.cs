using Electronic.Api.Dtos;
using Electronic.Api.Model;
using Electronic.Api.Repository.Custom_Repository;
using System.Collections.Generic;

namespace Electronic.Api.Services
{
    public class ProductImagesService : IProductImagesService
    {

        private readonly IProductImagesRepository _repository;
        public ProductImagesService(IProductImagesRepository repo)
        {
            _repository = repo;
        }
        public void AddNewProductImages(ProductImagesDTO productDTO)
        {
            var ProductImgs = new ProductImages();
            ProductImgs.Name = productDTO.Name;
            ProductImgs.ProductID = productDTO.ProductID;
            _repository.Insert(ProductImgs);

        }

        public IEnumerable<ProductImagesDTO> GetAllProductsImages()
        {
            var ProductImgs = _repository.GetAll();
            List<ProductImagesDTO> DTOs = new List<ProductImagesDTO>();
            if (ProductImgs != null && ProductImgs.Count() > 0)
            {
                foreach (var pi in ProductImgs)
                {
                    DTOs.Add(new ProductImagesDTO
                    {
                        Id = pi.Id,
                        ProductID = pi.ProductID,
                        Name = "https://localhost:7096/img/Products/" + pi.Name
                    });
                }
                return DTOs;
            }
            return null;
        }

        public IEnumerable<ProductImagesDTO> GetImagesByProductId(int Id)
        {
            var UserImgs = _repository.GetImagesByProductId(Id);
            if (UserImgs != null && UserImgs.Count() > 0)
            {
                List<ProductImagesDTO> DTOs = new List<ProductImagesDTO>();
                foreach (var UserImg in UserImgs)
                {
                    DTOs.Add(new ProductImagesDTO
                    {
                        Id = UserImg.Id,
                        Name = "https://localhost:7096/img/Products/" + UserImg.Name,
                        ProductID = UserImg.ProductID
                    });
                }
                return DTOs;
            }
            return null;

        }

        public ProductImagesDTO GetProductImagesByImageID(int Id)
        {
            var Img = _repository.GetById(Id);
            if (Img != null)
            {
                var ImgDTO = new ProductImagesDTO
                {
                    Id = Img.Id,
                    Name = Img.Name,
                    ProductID = Img.ProductID
                };
                return ImgDTO;
            }
            return null;
        }

        public bool RemoveProductImages(int ProductId)
        {
            var Imgs = _repository.GetImagesByProductId(ProductId);
            if (Imgs != null && Imgs.Count() > 0)
            {
                _repository.DeleteRange(Imgs);
                return true;
            }
            return false;
        }
        public bool DeleteProductImageByImgId(int imgId)
        {
            var Img = _repository.GetById(imgId);
            if (Img != null)
            {
                _repository.Delete(Img);
                return true;
            }
            return false;
        }

        

        public void UpdateProductImages(int Id, ProductImagesDTO producImagestDTO)
        {
            var pi = _repository.GetById(Id);
            if (pi != null)
            {
                pi.Name = producImagestDTO.Name;
                _repository.Update(pi);
            }
        }
    }
}
