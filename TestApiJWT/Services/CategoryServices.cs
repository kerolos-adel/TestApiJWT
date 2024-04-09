using Microsoft.EntityFrameworkCore;
using TestApiJWT.Models;

namespace TestApiJWT.Services
{
    public class CategoryServices : ICategoryServices
    {
        private readonly ApplicationDbContext context;

        public CategoryServices(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Category> Add(Category category)
        {
            await context.categories.AddAsync(category);
            context.SaveChanges();
            return category;
        }

        public Category Delete(Category category)
        {
            context.Remove(category);
            context.SaveChanges();
            return category;
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await context.categories.OrderBy(g=>g.Name).ToListAsync();
           
        }
        public List<Category> GetCategories()
        {
            return context.categories.ToList();

        }

        public async Task<Category> GetById(byte id)
        {
            return await context.categories.SingleOrDefaultAsync(d => d.Id == id);
        }

        public Category Update(Category category)
        {
            context.Update(category);
            context.SaveChanges();
            return category;
        }

        public Task<bool> IsValidCategory(byte id)
        {
            return context.categories.AnyAsync(g => g.Id == id);
        }
    }
}
