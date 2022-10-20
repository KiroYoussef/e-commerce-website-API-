using Electronic.Api.Model;

namespace Electronic.Api.Repository.Custom_Repository
{
    public interface IProductImagesRepository:IBaseRepository<ProductImages>
    {
        IEnumerable<ProductImages> GetImagesByProductId(int ProductId);
    }
}
