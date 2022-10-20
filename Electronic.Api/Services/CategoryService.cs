using Electronic.Api.Dtos;
using Electronic.Api.Model;
using Electronic.Api.Repository;

namespace Electronic.Api.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IBaseRepository<Category> _categoryRepositroy;
        public CategoryService(IBaseRepository<Category> categoryRepositroy)
        {
            _categoryRepositroy = categoryRepositroy;
        }
        public void AddNewCategory(CategoryDTO CatDTO)
        {
            var Cat = new Category
            {
                name = CatDTO.name
            };
            _categoryRepositroy.Insert(Cat);


        }

        public IEnumerable<CategoryDTO> GetAllCategories()
        {
            var Cats = _categoryRepositroy.GetAll();
            List<CategoryDTO> CatDTOs = new List<CategoryDTO>();
            if (Cats != null && Cats.Count() > 0)
            {
                foreach (var c in Cats)
                {
                    CatDTOs.Add(new CategoryDTO
                    {
                        Id = c.Id,
                        name = c.name,

                    });
                }
                return CatDTOs;
            }
            return null;
        }

        public CategoryDTO GetCategoryById(int Id)
        {
            var cat = _categoryRepositroy.GetById(Id);
            if (cat != null)
            {
                var CatDTO = new CategoryDTO { Id = cat.Id, name = cat.name };
                return CatDTO;
            }
            return null;
        }

        public bool DeletCategory(int id)
        {

            var model = _categoryRepositroy.GetById(id);

            if (model == null)
            {
                return false;
            }
            _categoryRepositroy.Delete(model);
            return true;


        }

        public void UpdateCategory(int Id, CategoryDTO CatDTO)
        {
            var Cat = _categoryRepositroy.GetById(Id);
            if (Cat != null)
            {
                Cat.name = CatDTO.name;
                _categoryRepositroy.Update(Cat);
            }
        }
    }
}
