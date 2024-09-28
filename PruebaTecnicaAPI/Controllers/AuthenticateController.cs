using Microsoft.AspNetCore.Mvc;
using PruebaAPI.BLL.Contract;
using PruebaAPI.DTO;

namespace PruebaAPI.Controllers
{
    [Route("api/Authenticate")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthenticateController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("getBearerToken")]
        public async Task<IActionResult> Authenticate(LoginDTO login)
        {
            var response = new ResponseHTTP<Object>();
            try
            {
                //metodo que valida al usuario y devuelve el token
                string tokenAccess = await _userService.AuthenticateUser(login);
                response.status = true;
                response.Content = new { bearerToken = tokenAccess };
                return Ok(response);
            }
            catch (Exception e)
            {
                response.status = true;
                response.Content = new { bearerToken = "" };
                response.message = e.Message;
                return NotFound(response);
            }

        }
    }
}
