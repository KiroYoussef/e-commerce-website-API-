using Electronic.Api.DTO;
using Electronic.Api.Dtos;
using Electronic.Api.Model;
using Electronic.Api.Repository;
using Electronic.Api.Repository.Custom_Repository;

namespace Electronic.Api.Services
{
    public class ProductService : IProductService
    {

        private readonly IProductRepository _productRepository;
        private readonly IBaseRepository<Product> productRepository1;
        private readonly IBaseRepository<SubCategory> subcategoryRepository;
        private readonly IBaseRepository<ApplicationUser> ApplicationUserRepository;
        private readonly ContextDB context;


        public ProductService(IProductRepository repo, IBaseRepository<Product> productRepository, ContextDB _contextDB, IBaseRepository<SubCategory> subcategoryRepository, IBaseRepository<ApplicationUser> ApplicationUserRepositor)
        {
            _productRepository = repo;
            productRepository1 = productRepository;
            this.subcategoryRepository = subcategoryRepository;
            this.ApplicationUserRepository = ApplicationUserRepositor;
            context = _contextDB;

        }


        public void AddNewProduct(ProductDTO productDTO)
        {
            var p = new Product();
            p.price = productDTO.price;
            p.name = productDTO.name;
            p.img = productDTO.img;
            p.processor = productDTO.processor;
            p.Camera = productDTO.Camera;
            p.Ram = productDTO.Ram;
            p.HardDrive = productDTO.HardDrive;
            p.Description = productDTO.Description;
            p.ScreenSize = productDTO.ScreenSize;
            p.Discount = productDTO.Discount;
            p.FirstAprove = false;
            p.UserID = productDTO.UserID;
            p.AdminReview = true;
            p.CountProduct = productDTO.CountProduct;
            p.SubCategoryID = productDTO.SubCategoryID;
            _productRepository.Insert(p);

        }

        public IEnumerable<ProductsGetALLDTO> GetAllProducts(string UserId)
        {
            List<ProductsGetALLDTO> productDTOs = new List<ProductsGetALLDTO>();
            var products = productRepository1.GetAll().Where(e => e.FirstAprove == true) ;
            foreach (Product p in products)
            {
                var FavoriteProduct = context.FvoriteProducts.Where(o => o.UserID == UserId && o.ProductID == p.Id).Select(u => u.iD).FirstOrDefault();

                ProductsGetALLDTO productsGet = new ProductsGetALLDTO();

                productsGet.Id = p.Id;
                productsGet.price = p.price;
                productsGet.name = p.name;
                productsGet.priceAfterDisc = p.price + (p.price * p.Discount / 100);
                productsGet.img = "https://localhost:7096/Img/Products/" + p.img;
                productsGet.Discount = p.Discount;
                productsGet.SubCategoryName = subcategoryRepository.GetById(p.SubCategoryID).name;
                productsGet.IsFavorite = FavoriteProduct;
                productsGet.active = p.Active;
                productsGet.userId = p.UserID;
                //productsGet.FavoriteProductID = (context.FvoriteProducts.Where(o => o.UserID == UserId).Select(u => u.ProductID == p.Id).FirstOrDefault() == true) ? (context.FvoriteProducts.Where(o => o.UserID == UserId && o.ProductID == p.Id).Select(u => u.iD).FirstOrDefault()) : -1;
                productDTOs.Add(productsGet);
            }
            return productDTOs;
        }
        public IEnumerable<ProductsGetALLDTO> GetAllProducts()
        {
            List<ProductsGetALLDTO> productDTOs = new List<ProductsGetALLDTO>();
            var products = productRepository1.GetAllwhere(e => e.FirstAprove == false);
            foreach (Product p in products)
            {

                ProductsGetALLDTO productsGet = new ProductsGetALLDTO();

                productsGet.Id = p.Id;
                productsGet.price = p.price;
                productsGet.name = p.name;
                productsGet.priceAfterDisc = p.price + (p.price * p.Discount / 100);
                productsGet.img = "https://localhost:7096/Img/Products/" + p.img;
                productsGet.Discount = p.Discount;
                productsGet.FirstAprove = p.FirstAprove;
                productsGet.SubCategoryName = subcategoryRepository.GetById(p.SubCategoryID).name;

                //productsGet.FavoriteProductID = (context.FvoriteProducts.Where(o => o.UserID == UserId).Select(u => u.ProductID == p.Id).FirstOrDefault() == true) ? (context.FvoriteProducts.Where(o => o.UserID == UserId && o.ProductID == p.Id).Select(u => u.iD).FirstOrDefault()) : -1;
                productDTOs.Add(productsGet);
            }
            return productDTOs;
        }
        public ProductDetailsDTO GetProductByID(int Id)
        {

            var p = _productRepository.GetById(Id);
            if (p == null)
            {
                return null;
            }
            var SellerName = context.Users.Where(w => w.Id == p.UserID).Select(i => i.UserName).FirstOrDefault();
            var SubCategory = subcategoryRepository.GetById(p.SubCategoryID);
            var pDTO = new ProductDetailsDTO
            {
                Id = p.Id,
                price = p.price,
                name = p.name,
                img = "https://localhost:7096/Img/Products/" + p.img,
                processor = p.processor,
                Camera = p.Camera,
                Ram = p.Ram,
                HardDrive = p.HardDrive,
                Description = p.Description,
                ScreenSize = p.ScreenSize,
                Discount = p.Discount,
                SellerName = SellerName,
                CountProduct = p.CountProduct,
                SubCategoryID = p.SubCategoryID,
                SubCategoryName = subcategoryRepository.GetById(p.SubCategoryID).name
            };
            return pDTO;
        }

        public IEnumerable<AllProductsSellerDTO> GetProductsByUserId(string Id)
        {
            var products = _productRepository.GetProductsByUserId(Id);
            var ProdctDtos = new List<AllProductsSellerDTO>();
            foreach (var p in products)
            {
                ProdctDtos.Add(new AllProductsSellerDTO
                {
                    Id = p.Id,
                    price = p.price,
                    name = p.name,
                    img = "https://localhost:7096/img/Products/" + p.img,
                    SubCategoryName = subcategoryRepository.GetById(p.SubCategoryID).name,
                    Description = p.Description,
                    Discount = p.Discount,
                    CountProduct = p.CountProduct,
                    Active = Convert.ToString(p.Active)

                });
            }
            return ProdctDtos;
        }

        public IEnumerable<ProductDTO> GetTopAddedProducts()
        {
            var products = _productRepository.GetTopAdded();
            var ProdctDtos = new List<ProductDTO>();
            foreach (var p in products)
            {
                ProdctDtos.Add(new ProductDTO
                {
                    Id = p.Id,
                    price = p.price,
                    name = p.name,
                    img = p.img,
                    processor = p.processor,
                    Camera = p.Camera,
                    Ram = p.Ram,
                    HardDrive = p.HardDrive,
                    Description = p.Description,
                    ScreenSize = p.ScreenSize,
                    Discount = p.Discount,
                    UserID = p.UserID,
                    CountProduct = p.CountProduct,

                    SubCategoryID = p.SubCategoryID
                });
            }
            return ProdctDtos;
        }

        public bool RemoveProduct(int Id)
        {
            var product = _productRepository.GetById(Id);
            if (product != null)
            {
                _productRepository.Delete(product);
                return true;
            }
            return false;
        }

        public void UpdateProduct(ProductDTO productDTO)
        {
            //var p = new Product();
            var p = _productRepository.GetById(productDTO.Id);
            p.Id = productDTO.Id;
            p.price = productDTO.price;
            p.name = productDTO.name;
            if (productDTO.img.Length>1)
            {
                p.img = productDTO.img;
            }
            else
            {
                p.img = p.img;
            }
            p.processor = productDTO.processor;
            p.Camera = productDTO.Camera;
            p.Ram = productDTO.Ram;
            p.HardDrive = productDTO.HardDrive;
            p.Description = productDTO.Description;
            p.ScreenSize = productDTO.ScreenSize;
            p.Discount = productDTO.Discount;
            p.SubCategoryID = productDTO.SubCategoryID;
            p.UserID = productDTO.UserID;
            p.CountProduct = productDTO.CountProduct;
            p.AdminReview = false;
            p.FirstAprove = true;
            _productRepository.Update(p);


        }


        public bool EditActiveProduct(User_ProductDTO Data, int status)
        {


            var Product = productRepository1.GetById(n => n.Id == Data.ProductId, o => o.UserID == Data.UserId);
            if (Product != null && status == 1)
            {
                Product.Active = true;
                _productRepository.Update(Product);

            }
            else if (Product != null && status == 0)
            {
                Product.Active = false;
                _productRepository.Update(Product);


            }
            else
            {
                return false;

            }

            return true;


        }
        public bool UpdateProductApprove(int ProdId, bool firstApprove)
        {
            var Prod = productRepository1.GetById(ProdId);
            if (Prod != null)
            {
                Prod.FirstAprove = firstApprove;
                _productRepository.Update(Prod);
                return true;
            }
            return false;
        }

        public IEnumerable<ProductsGetALLDTO> GellAllProductNeedReview()
        {
            List<ProductsGetALLDTO> productDTOs = new List<ProductsGetALLDTO>();
            var productsNeedReview = productRepository1.GetAll().Where(e => e.AdminReview == false);
            foreach (var product in productsNeedReview)
            {
                ProductsGetALLDTO OnlyProduct = new ProductsGetALLDTO();
                OnlyProduct.Id = product.Id;
                OnlyProduct.price = product.price;
                OnlyProduct.name = product.name;
                OnlyProduct.priceAfterDisc = product.price + (product.price * product.Discount / 100);
                OnlyProduct.img = "https://localhost:7096/Img/Products/" + product.img;
                OnlyProduct.Discount = product.Discount;
                OnlyProduct.FirstAprove = product.FirstAprove;
                OnlyProduct.SubCategoryName = subcategoryRepository.GetById(product.SubCategoryID).name;
                productDTOs.Add(OnlyProduct);

            }
            return productDTOs;
        }

    }
}
