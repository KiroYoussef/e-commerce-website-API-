using Electronic.Api.Dtos;
using Electronic.Api.Model;
namespace Electronic.Api.Services
{
    public interface ICategoryService
    {
        CategoryDTO GetCategoryById(int Id);
        IEnumerable<CategoryDTO> GetAllCategories();
        bool DeletCategory(int id);
        void AddNewCategory(CategoryDTO CatDTO);
        void UpdateCategory(int Id, CategoryDTO CatDTO);
    }
}
