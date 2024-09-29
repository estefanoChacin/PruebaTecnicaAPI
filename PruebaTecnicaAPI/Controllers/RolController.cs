using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PruebaAPI.BLL.Contract;
using PruebaAPI.DTO;

namespace PruebaAPI.Controllers
{
    [Authorize(Roles = "ADMIN")]
    [Route("api/rol")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly IRolService _rolService;

        public RolController(IRolService rolService)
        {
            _rolService = rolService;
        }



        [HttpGet("getAllRols")]
        public async Task<IActionResult> GetAllRol()
        {
            var response = new ResponseHTTP<List<RolDTO>>();
            try
            {
                response.Content = await _rolService.GeAll();
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


        [HttpGet("getRolId")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = new ResponseHTTP<RolDTO>();
            try
            {
                response.Content = await _rolService.Get(id);
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

        [HttpPost("createRol")]
        public async Task<IActionResult> CreateRol(RolDTO rol)
        {
            var response = new ResponseHTTP<RolDTO>();
            try
            {
                response.Content = await _rolService.Create(rol);
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

        [HttpPut("UpdateRol")]
        public async Task<IActionResult> UpdateRol(RolDTO rol)
        {
            var response = new ResponseHTTP<RolDTO>();
            try
            {
                response.status = true;
                response.Content = await _rolService.Update(rol);
                return Ok(response);

            }
            catch (Exception e)
            {
                response.status = false;
                response.message = e.Message;
                return NotFound(response);
            }
        }


        [HttpDelete("deleteRol")]
        public async Task<IActionResult> DeleteRol(int id)
        {
            var response = new ResponseHTTP<RolDTO>();
            try
            {
                response.status = await _rolService.Delete(id);
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
