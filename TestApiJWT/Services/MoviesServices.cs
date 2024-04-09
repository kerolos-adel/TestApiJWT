using Microsoft.EntityFrameworkCore;
using TestApiJWT.Models;

namespace TestApiJWT.Services
{
    public class MoviesServices : IMoviesServices
    {
        private readonly ApplicationDbContext context;

        public MoviesServices(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Movie> Add(Movie movie)
        {
            await context.AddAsync(movie);
            context.SaveChanges();
            return movie;   
        }

        public Movie Delete(Movie movie)
        {
            context.Remove(movie);
            context.SaveChanges();
            return movie;
        }

        public async Task<IEnumerable<Movie>> GetAll(byte categoryId = 0)
        {
           return await context.movies
                .Where(x=>x.GategoryId==categoryId||categoryId==0)
                .OrderByDescending(x => x.Rate)
                .Include(x => x.Category)
                .ToListAsync();
        }

        public async Task<Movie> GetById(int id)
        {
            return await context.movies.Include(x => x.Category).SingleOrDefaultAsync(d => d.Id == id);
        }

        public Movie Update(Movie movie)
        {
           context.Update(movie);
            context.SaveChanges();
            return movie;
        }
    }
}
