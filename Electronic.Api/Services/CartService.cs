using Electronic.Api.DTO;
using Electronic.Api.Model;
using Electronic.Api.Repository;

namespace Electronic.Api.Services
{
    public class CartService : ICartService
    {
        private readonly IBaseRepository<SubCategory> subcategoryRepository;
        private readonly IBaseRepository<Cart> baseRepository;
        private readonly IBaseRepository<Product> productRepository;

        public CartService(IBaseRepository<SubCategory> subcategoryRepository,
            IBaseRepository<Cart> baseRepository,
            IBaseRepository<Product> ProductRepository)
        {
            this.subcategoryRepository = subcategoryRepository;
            this.baseRepository = baseRepository;
            productRepository = ProductRepository;
        }


        public bool AddCart(string iduser, int idprodect)
        {
            Cart cart = new Cart
            {
                ProductID = idprodect,
                UserID = iduser,
                Quantity = 1,
            };
            Cart result = baseRepository.Insert(cart);
            return true;
        }

        public bool AddCartByDetails(string iduser, int idprodect, int count)
        {
            Cart cart = new Cart
            {
                ProductID = idprodect,
                UserID = iduser,
                Quantity = count,
            };
            Cart result = baseRepository.Insert(cart);
            if (result == null)
            {

                return false;
            }


            return true;
        }

        public bool AddCountProduct(string iduser, int idprodect, int pluscount)
        {
            Cart cart = baseRepository.GetById(e => e.UserID == iduser, r => r.ProductID == idprodect);
            cart.Quantity = pluscount;
            baseRepository.Update(cart);
            return true;
        }

        public bool ChangeQuentityAndCheckMax(string iduser, int idprodect)
        {
            Cart cart = baseRepository.GetById(e => e.UserID == iduser, r => r.ProductID == idprodect);
            Product product = productRepository.GetById(idprodect);
            if (cart.Quantity == product.CountProduct)
            {
                return false; ;
            }

            cart.Quantity += 1;
            baseRepository.Update(cart);
            return true; ;
        }

        public bool ChangeQuentityAndCheckMax(string iduser, int idprodect, int count)
        {
            Cart cart = baseRepository.GetById(e => e.UserID == iduser, r => r.ProductID == idprodect);
            Product product = productRepository.GetById(idprodect);
            if (cart.Quantity == product.CountProduct)
            {
                return false;
            }
            long result = cart.Quantity + count;
            if (result >= product.CountProduct)
            {
                long res2 = result - product.CountProduct;

                cart.Quantity += (int)(count - res2);
            }
            else
            {
                cart.Quantity += count;
            }


            baseRepository.Update(cart);
            return true; ;
        }

        public bool CheckAny(string iduser, int idprodect)
        {

            return baseRepository.CheckAny(e => e.UserID == iduser, r => r.ProductID == idprodect);
        }

        public int CountCart(string iduser)
        {
            return baseRepository.Count(e => e.UserID == iduser);
        }

        public bool DeleteCartProduct(string iduser, int idprodect)
        {
            Cart cart = baseRepository.GetById(e => e.UserID == iduser, r => r.ProductID == idprodect);
            if (cart == null)
            {
                return false;

            }
            baseRepository.Delete(cart);
            return true;
        }

        public IEnumerable<CartProductDTO> GetAll(string iduser)
        {
            var result = baseRepository.GetAll(e => e.UserID == iduser);
            List<CartProductDTO> products = new List<CartProductDTO>();
            foreach (var item in result)
            {
                var prod = productRepository.GetById(item.ProductID);
                CartProductDTO dTO = new CartProductDTO();

                dTO.Id = prod.Id;
                dTO.img = "https://localhost:7096/Img/Products/" + prod.img;
                dTO.name = prod.name;
                dTO.price = prod.price * item.Quantity;
                dTO.countProductMax = prod.CountProduct;
                dTO.Quantity = item.Quantity;
                dTO.SubCategoryName = subcategoryRepository.GetById(prod.SubCategoryID).name;

                products.Add(dTO);
            }
            return products;
        }
    }
}