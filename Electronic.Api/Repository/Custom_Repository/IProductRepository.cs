using Electronic.Api.Model;

namespace Electronic.Api.Repository.Custom_Repository
{
    public interface IProductRepository:IBaseRepository<Product>
    {
        IEnumerable<Product> GetTopAdded();
        IEnumerable<Product> GetProductsByUserId(string UserId);
    }
}
