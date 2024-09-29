using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PruebaAPI.BLL.Contract;
using PruebaAPI.DTO;

namespace PruebaAPI.Controllers
{
    [Authorize(Roles = "ADMIN")]
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet("getAllUsers")]
        public async Task<IActionResult> GetAllUser()
        {
            var response = new ResponseHTTP<List<UserDTO>>();
            try
            {
                response.Content = await _userService.GeAll();
                response.status = true;

                return Ok(response);
            }
            catch (Exception e)
            {
                response.status = false;
                response.Content = new List<UserDTO>();
                response.message = e.Message;
                return NotFound(response);
            }
        }


        [HttpGet("geUserId")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = new ResponseHTTP<UserDTO>();
            try
            {
                response.Content = await _userService.Get(id);
                response.status = true;
                return Ok(response);
            }
            catch (Exception e)
            {
                response.status = false;
                response.Content = new UserDTO();
                response.message = e.Message;
                return NotFound(response);
            }
        }

        [HttpPost("createUser")]
        public async Task<IActionResult> CreateUser(UserDTO user)
        {
            var response = new ResponseHTTP<UserDTO>();
            try
            {
                response.Content = await _userService.Create(user);
                response.status = true;
                return Ok(response);

            }
            catch (Exception e)
            {
                response.status = false;
                response.Content = new UserDTO();
                response.message = e.Message;
                return NotFound(response);
            }
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser(UserDTO user)
        {
            var response = new ResponseHTTP<UserDTO>();
            try
            {
                response.status =true;
                response.Content = await _userService.Update(user);
                return Ok(response);

            }
            catch (Exception e)
            {
                response.status = false;
                response.Content = new UserDTO();
                response.message = e.Message;
                return NotFound(response);
            }
        }


        [HttpDelete("deleteUser")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var response = new ResponseHTTP<UserDTO>();
            try
            {
                response.status = await _userService.Delete(id);
                return Ok(response);

            }
            catch (Exception e)
            {
                response.status = false;
                response.Content = new UserDTO();
                response.message = e.Message;
                return NotFound(response);
            }
        }

    }
}
