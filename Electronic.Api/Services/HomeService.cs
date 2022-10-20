using Electronic.Api.DTO;
using Electronic.Api.Dtos;
using Electronic.Api.Model;
using Electronic.Api.Repository;
using Electronic.Api.Repository.Custom_Repository;

namespace Electronic.Api.Services
{
    public class HomeService : IHomeService
    {
        private readonly IProductRepository product2AddRepository;
        private readonly IBaseRepository<Product> productRepository;
        private readonly IBaseRepository<Category> categoryRepository;
        private readonly IBaseRepository<SubCategory> subcategoryRepository;
        private readonly ContextDB context;

        public HomeService(ContextDB _contextDB, IProductRepository product2addRepository, IBaseRepository<Product> productRepository, IBaseRepository<Category> categoryRepository, IBaseRepository<SubCategory> subcategoryRepository)
        {
            product2AddRepository = product2addRepository;
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
            this.subcategoryRepository = subcategoryRepository;
            context = _contextDB;

        }




        public IEnumerable<ProductsGetALLDTO> GetAllAccessories(string UserID)
        {
            var cat = categoryRepository.GetByFirst(e => e.name.ToLower() == "Accessories".ToLower());
            var sub = subcategoryRepository.GetAll(e => e.CategoryID == cat.Id).Select(e => e.Id);
            List<ProductsGetALLDTO> Products = new List<ProductsGetALLDTO>();
            foreach (var item in sub)
            {
                Products.AddRange(product2AddRepository.GetAll(e => e.SubCategoryID == item && e.Active == true && e.FirstAprove == true).Select(b => new ProductsGetALLDTO
                {
                    //var FavoriteProduct = ;

                    Id = b.Id,
                    img = "https://localhost:7096/Img/Products/" + b.img,
                    Discount = b.Discount,
                    name = b.name,
                    price = b.price,
                    priceAfterDisc = b.price + (b.price * b.Discount / 100),
                    SubCategoryName = subcategoryRepository.GetById(b.SubCategoryID).name,
                    IsFavorite = context.FvoriteProducts.Where(o => o.UserID == UserID && o.ProductID == b.Id).Select(u => u.iD).FirstOrDefault()
                })
                );
            }


            return Products;
        }

        public IEnumerable<ProductsGetALLDTO> GetAllLabtops(string UserID)
        {
            var cat = categoryRepository.GetByFirst(e => e.name.ToLower() == "Labtops".ToLower());
            var sub = subcategoryRepository.GetAll(e => e.CategoryID == cat.Id).Select(e => e.Id);
            List<ProductsGetALLDTO> Products = new List<ProductsGetALLDTO>();
            foreach (var item in sub)
            {
                Products.AddRange(product2AddRepository.GetAll(e => e.SubCategoryID == item && e.Active==true && e.FirstAprove==true).Select(b => new ProductsGetALLDTO
                {

                    Id = b.Id,
                    img = "https://localhost:7096/Img/Products/" + b.img,
                    Discount = b.Discount,
                    name = b.name,
                    price = b.price,
                    priceAfterDisc = b.price + (b.price * b.Discount / 100),
                    SubCategoryName = subcategoryRepository.GetById(b.SubCategoryID).name,
                    IsFavorite = context.FvoriteProducts.Where(o => o.UserID == UserID && o.ProductID == b.Id).Select(u => u.iD).FirstOrDefault()

                })
                );
            }


            return Products;
        }

        public IEnumerable<ProductsGetALLDTO> GetAllPhones(string UserID)
        {

            var cat = categoryRepository.GetByFirst(e => e.name.ToLower() == "Phones".ToLower());
            var sub = subcategoryRepository.GetAll(e => e.CategoryID == cat.Id).Select(e => e.Id);

            List<ProductsGetALLDTO> Products = new List<ProductsGetALLDTO>();
            foreach (var item in sub)
            {
                Products.AddRange(product2AddRepository.GetAll(e => e.SubCategoryID == item && e.Active == true && e.FirstAprove == true).Select(b => new ProductsGetALLDTO
                {

                    Id = b.Id,
                    img = "https://localhost:7096/Img/Products/" + b.img,
                    Discount = b.Discount,
                    name = b.name,
                    price = b.price,
                    priceAfterDisc = b.price + (b.price * b.Discount / 100),
                    SubCategoryName = subcategoryRepository.GetById(b.SubCategoryID).name,
                    IsFavorite = context.FvoriteProducts.Where(o => o.UserID == UserID && o.ProductID == b.Id).Select(u => u.iD).FirstOrDefault()

                })
                );
            }


            return Products;

        }





        public IEnumerable<ProductsGetALLDTO> GetAllProductsNew()
        {
            return product2AddRepository.GetTopAdded().Select(b => new ProductsGetALLDTO
            {

                Id = b.Id,
                img = "https://localhost:7096/Img/Products/" + b.img,
                Discount = b.Discount,
                name = b.name,
                price = b.price,
                priceAfterDisc = b.price + (b.price * b.Discount / 100),
                SubCategoryName = subcategoryRepository.GetById(b.SubCategoryID).name,

            });
        }












        public IEnumerable<ProductsGetALLDTO> GetAllProductByIdSubCategory(int id, string UserID)
        {


            return product2AddRepository.GetAll(e => e.SubCategoryID == id).Select(b => new ProductsGetALLDTO
            {

                Id = b.Id,
                img = "https://localhost:7096/Img/Products/" + b.img,
                Discount = b.Discount,
                name = b.name,
                price = b.price,
                priceAfterDisc = b.price + (b.price * b.Discount / 100),
                SubCategoryName = subcategoryRepository.GetById(b.SubCategoryID).name,
                IsFavorite = context.FvoriteProducts.Where(o => o.UserID == UserID && o.ProductID == b.Id).Select(u => u.iD).FirstOrDefault()

            }
                 );

        }


        public IEnumerable<SubCatgoryDto> GetAllSubCategoryByIdCaty(int id)
        {
            return subcategoryRepository.GetAll(e => e.CategoryID == id).Select(
                e => new SubCatgoryDto
                {
                    Id = e.Id,
                    CategoryID = e.CategoryID,
                    name = e.name
                });
        }



        public IEnumerable<ProductsGetALLDTO> ByIdCateoryAllPoduct(int id, string UserID)
        {
            var cat = categoryRepository.GetById(id);
            var sub = subcategoryRepository.GetAll(e => e.CategoryID == cat.Id).Select(e => e.Id);
            List<ProductsGetALLDTO> Products = new List<ProductsGetALLDTO>();
            foreach (var item in sub)
            {
                Products.AddRange(product2AddRepository.GetAll(e => e.SubCategoryID == item).Select(b => new ProductsGetALLDTO
                {

                    Id = b.Id,
                    img = "https://localhost:7096/Img/Products/" + b.img,
                    Discount = b.Discount,
                    name = b.name,
                    price = b.price,
                    priceAfterDisc = b.price + (b.price * b.Discount / 100),
                    SubCategoryName = subcategoryRepository.GetById(b.SubCategoryID).name,
                    IsFavorite = context.FvoriteProducts.Where(o => o.UserID == UserID && o.ProductID == b.Id).Select(u => u.iD).FirstOrDefault()

                })
                );
            }


            return Products;
        }




    }
}

