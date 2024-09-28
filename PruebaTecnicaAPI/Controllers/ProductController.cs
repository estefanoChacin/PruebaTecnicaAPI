using Microsoft.AspNetCore.Mvc;
using PruebaAPI.BLL.Contract;
using PruebaAPI.DTO;

namespace PruebaAPI.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }



        [HttpGet("getAllProducts")]
        public async Task<IActionResult> GetAll()
        {
            var response = new ResponseHTTP<List<ProductDTO>>();
            try
            {
                response.Content = await _productService.GeAll();
                response.status = true;

                return Ok(response);
            }
            catch (Exception e)
            {
                response.status = false;
                response.Content = new List<ProductDTO>();
                response.message = e.Message;
                return NotFound(response);
            }
        }


        [HttpGet("getProductId")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = new ResponseHTTP<ProductDTO>();
            try
            {
                response.Content = await _productService.Get(id);
                response.status = true;
                return Ok(response);
            }
            catch (Exception e)
            {
                response.status = false;
                response.Content = new ProductDTO();
                response.message = e.Message;
                return NotFound(response);
            }
        }

        [HttpPost("createProduct")]
        public async Task<IActionResult> CreateProduct(ProductDTO product)
        {
            var response = new ResponseHTTP<ProductDTO>();
            try
            {
                response.Content = await _productService.Create(product);
                response.status = true;
                return Ok(response);

            }
            catch (Exception e)
            {
                response.status = false;
                response.Content = new ProductDTO();
                response.message = e.Message;
                return NotFound(response);
            }
        }

        [HttpPut("updateProduct")]
        public async Task<IActionResult> UpdateCategory(ProductDTO product)
        {
            var response = new ResponseHTTP<ProductDTO>();
            try
            {
                response.status = await _productService.Update(product);
                return Ok(response);

            }
            catch (Exception e)
            {
                response.status = false;
                response.Content = new ProductDTO();
                response.message = e.Message;
                return NotFound(response);
            }
        }


        [HttpDelete("deleteProduct")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var response = new ResponseHTTP<ProductDTO>();
            try
            {
                response.status = await _productService.Delete(id);
                return Ok(response);

            }
            catch (Exception e)
            {
                response.status = false;
                response.Content = new ProductDTO();
                response.message = e.Message;
                return NotFound(response);
            }
        }
    }
}
