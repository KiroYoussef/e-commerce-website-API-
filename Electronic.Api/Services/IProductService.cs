using Electronic.Api.DTO;
using Electronic.Api.Dtos;
using Electronic.Api.Model;

namespace Electronic.Api.Services
{
    public interface IProductService
    {
        ProductDetailsDTO GetProductByID(int Id);
        IEnumerable<ProductsGetALLDTO> GetAllProducts(string UserId);
        IEnumerable<ProductsGetALLDTO> GetAllProducts();
        void AddNewProduct(ProductDTO productDTO);
        bool RemoveProduct(int Id);
        void UpdateProduct(ProductDTO productDTO);
        IEnumerable<ProductDTO> GetTopAddedProducts();
        IEnumerable<AllProductsSellerDTO> GetProductsByUserId(string Id);
        bool EditActiveProduct(User_ProductDTO Data, int status);
        bool UpdateProductApprove(int prod, int fapprove);
        //public IEnumerable<ProductsGetALLDTO> GellAllProductNeedReview();

    }
}
