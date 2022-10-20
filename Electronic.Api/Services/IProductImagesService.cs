using Electronic.Api.Dtos;

namespace Electronic.Api.Services
{
    public interface IProductImagesService
    {
        ProductImagesDTO GetProductImagesByImageID(int Id);
        IEnumerable<ProductImagesDTO> GetAllProductsImages();
        void AddNewProductImages(ProductImagesDTO productDTO);
        bool RemoveProductImages(int ProductId);
        bool DeleteProductImageByImgId(int imgId);
        void UpdateProductImages(int Id, ProductImagesDTO productDTO);
        IEnumerable<ProductImagesDTO> GetImagesByProductId(int Id);
    }
}
