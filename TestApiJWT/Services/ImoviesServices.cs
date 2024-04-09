using TestApiJWT.Models;

namespace TestApiJWT.Services
{
    public interface IMoviesServices
    {
        Task<IEnumerable<Movie>> GetAll(byte categoryId = 0);
       // List<Movie> GetCategories();
        Task<Movie> GetById(int id);
        Task<Movie> Add(Movie movie);
        Movie Update(Movie movie);
        Movie Delete(Movie movie);
    }
}
