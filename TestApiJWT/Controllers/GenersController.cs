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
    public class GenersController : ControllerBase
    {
        private readonly ICategoryServices categoryServices;

        public GenersController(ICategoryServices categoryServices)
        {
            this.categoryServices = categoryServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetaAll()
        {
            var geners = await categoryServices.GetAll();
            return Ok(geners);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetaAll(byte id)
        {
            var geners = await categoryServices.GetById(id);
            return Ok(geners);
        }
        [HttpPost]
        public async Task<IActionResult> AddGenere(CategoryModel categoryModel)
        {
            Category category = new Category
            {
                Name = categoryModel.Name,
            };
            foreach (var item in categoryServices.GetCategories())
            {
                if (category.Name == item.Name)
                {
                    return BadRequest("name already exist");
                }
               
            }
            await categoryServices.Add(category);
            return Ok(category);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(byte id, CategoryModel newCategory)
        {
            var category = await categoryServices.GetById(id);
            

            if (category == null)
            {
                return NotFound("no genre was found by id");
            }
            category.Name = newCategory.Name;
            categoryServices.Update(category);
            return Ok(category);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(byte id)
        {
            var category =await categoryServices.GetById(id);
            if(category == null)
            {
                return NotFound("no genre was found by id");
            }
            categoryServices.Delete(category);
            return Ok("deleted");

        }

    }
}
