using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PruebaAPI.BLL.Contract;
using PruebaAPI.DTO;

namespace PruebaAPI.Controllers
{
    [Authorize(Roles = "ADMIN,EMPLEADO")]
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController( ICategoryService categoryService)
        {
                _categoryService = categoryService;
        }


        [HttpGet("getAllCategories")]
        public async Task<IActionResult> GetAll()
        {
            var response = new ResponseHTTP<List<CategoryDTO>>();
            try
            {
                response.Content = await _categoryService.GeAll();
                response.status = true;

                return Ok(response);
            }
            catch (Exception e)
            {
                response.status = false;
                response.message = e.Message;
                return NotFound(response);
            }
        }


        [HttpGet("getCategoryId")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = new ResponseHTTP<CategoryDTO>();
            try
            {
                response.Content = await _categoryService.Get(id);
                response.status = true;
                return Ok(response);
            }
            catch (Exception e)
            {
                response.status = false;
                response.message = e.Message;
                return NotFound(response);
            }
        }

        [HttpPost("createCategory")]
        public async Task<IActionResult> CreateCategory(CategoryDTO category)
        {
            var response = new ResponseHTTP<CategoryDTO>();
            try
            {
                response.Content = await _categoryService.Create(category);
                response.status = true;
                return Ok(response);

            }
            catch (Exception e)
            {
                response.status = false;
                response.message = e.Message;
                return NotFound(response);
            }
        }

        [HttpPut("updateCategory")]
        public async Task<IActionResult> UpdateCategory(CategoryDTO category)
        {
            var response = new ResponseHTTP<CategoryDTO>();
            try
            {
                response.status = true;
                response.Content = await _categoryService.Update(category);
                return Ok(response);

            }
            catch (Exception e)
            {
                response.status = false;
                response.message = e.Message;
                return NotFound(response);
            }
        }


        [HttpDelete("deleteCategory")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var response = new ResponseHTTP<CategoryDTO>();
            try
            {
                response.status = await _categoryService.Delete(id);
                return Ok(response);

            }
            catch (Exception e)
            {
                response.status = false;
                response.message = e.Message;
                return NotFound(response);
            }
        }
    }
}
