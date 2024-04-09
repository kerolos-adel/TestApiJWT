 using TestApiJWT.Models;

namespace TestApiJWT.Services
{
    public interface ICategoryServices
    {
        Task<IEnumerable<Category>> GetAll();
        List<Category> GetCategories();
        Task<Category> GetById(byte id);
        Task<Category>Add(Category category);
        Category Update(Category category);
        Category Delete(Category category);
        Task<bool> IsValidCategory(byte id);
    }
}
