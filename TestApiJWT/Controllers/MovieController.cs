using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestApiJWT.Models;
using TestApiJWT.Services;
using TestApiJWT.ViewModel;

namespace TestApiJWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMoviesServices moviesServices; 
        private readonly ICategoryServices categoryServices; 
        private new List<String> allowedExtenstions = new List<string> { ".png",".jpg"};
        private long _maxAllowedPosterSize = 1048576;
        public MovieController(IMoviesServices moviesServices, ICategoryServices categoryServices)
        {
            this.moviesServices = moviesServices;
            this.categoryServices = categoryServices;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm]MovieModel newMovie)
        {
            if(newMovie.Poster == null) 
            {
                return BadRequest("poster is required");
            }
            if (!allowedExtenstions.Contains(Path.GetExtension(newMovie.Poster.FileName).ToLower()))
            {
                return BadRequest("only .png and .jpg images are allowed");
            }
            if (newMovie.Poster.Length > _maxAllowedPosterSize)
            {
                return BadRequest("max allowed size for posret 1MB");

            }
            var isValidCategory = await categoryServices.IsValidCategory(newMovie.GategoryId);
            if (!isValidCategory)
            {
                return BadRequest("invalid category id");
            }
            using var dataStream = new MemoryStream();

            await newMovie.Poster.CopyToAsync(dataStream);

            var movie = new Movie
            {
                Title = newMovie.Title,
                Year = newMovie.Year,
                Poster = dataStream.ToArray(),
                Rate = newMovie.Rate,
                StoreLine = newMovie.StoreLine,
                GategoryId = newMovie.GategoryId,
            };
            moviesServices.Add(movie);
            return Ok(movie);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMovies()
        {
            var movies = await moviesServices.GetAll();
            return Ok(movies);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovieById(int id)
        {
            var movie = await moviesServices.GetById(id);
            if (movie == null)
            {
                return BadRequest("id not found");
            }
            return Ok(movie);
        }
        [HttpGet("GetAllMoviesByCategory")]
        public async Task<IActionResult> GetAllMoviesByCategory(byte categoryId)
        {
            var movieCat = await moviesServices.GetAll(categoryId);
            if(movieCat == null)
            {
                return BadRequest("not found movies in this category");
            }
            return Ok(movieCat);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie =await moviesServices.GetById(id);
            if(id == null)
            {
                return NotFound("movie not found");
            }
            moviesServices.Delete(movie); 
            return Ok(movie);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie(int id, MovieModel newmovie)
        {
            var movie = await moviesServices.GetById(id);
            if (movie == null)
                return NotFound("id not found");
            var isValidCategory = await categoryServices.IsValidCategory(newmovie.GategoryId);
            if (!isValidCategory)
            {
                return BadRequest("invalid category id");
            }

            if (newmovie.Poster!=null)
            {
                if (!allowedExtenstions.Contains(Path.GetExtension(newmovie.Poster.FileName).ToLower()))
                    return BadRequest("only .png and .jpg images are allowed");
                
                if (newmovie.Poster.Length > _maxAllowedPosterSize)
                    return BadRequest("max allowed size for posret 1MB");
                
                using var dataStream = new MemoryStream();
                await newmovie.Poster.CopyToAsync(dataStream);

                movie.Poster = dataStream.ToArray();

            }
            movie.Title = newmovie.Title;
            movie.StoreLine = newmovie.StoreLine;
            movie.Year = newmovie.Year;
            movie.Rate = newmovie.Rate;
            movie.GategoryId = newmovie.GategoryId;
            moviesServices.Update(movie);
            return Ok(movie);

        }


    }
}
