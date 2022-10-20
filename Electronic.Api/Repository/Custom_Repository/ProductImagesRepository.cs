using Electronic.Api.Model;

namespace Electronic.Api.Repository.Custom_Repository
{
    public class ProductImagesRepository:BaseRepository<ProductImages>,IProductImagesRepository
    {
        public ProductImagesRepository (ContextDB _context) : base(_context)
        {

        }
        public IEnumerable<ProductImages> GetImagesByProductId(int ProductId)
        {
            return _context.ProductImages.Where(imgs => imgs.ProductID == ProductId);
        }
    }
}
