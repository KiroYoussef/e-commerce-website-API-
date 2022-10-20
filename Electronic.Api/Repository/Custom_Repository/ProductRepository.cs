using Electronic.Api.Model;

namespace Electronic.Api.Repository.Custom_Repository
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        
        public ProductRepository(ContextDB _context):base(_context)
        {

        }
        public IEnumerable<Product> GetTopAdded()
        {
            return _context.Products.OrderBy(p => p.CreateDate).Take(10).ToList();

        }
        public IEnumerable<Product> GetProductsByUserId(string UserId)
        {
            return _context.Products.Where(p => p.UserID == UserId);
        }
    }
}
