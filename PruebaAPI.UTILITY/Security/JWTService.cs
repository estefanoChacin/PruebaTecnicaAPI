

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PruebaAPI.MODEL;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PruebaAPI.UTILITY.Security
{
    public class JWTService
    {
        private readonly IConfiguration _configuration;

        public JWTService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //---metodo para generar el bearer token 
        public string GenerateJwtToken(User user)
        {
            //--obtener los datos de configuracion
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"];
            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];
            var expiryMinutes = Convert.ToInt32(jwtSettings["ExpiryMinutes"]);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //--definir los claims
            var claims = new[]
            {
            new Claim("IdUser", user.IdUser.ToString()),
            new Claim("name", user.Name),
            new Claim("role", user.Rol.name)
           };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiryMinutes),
                signingCredentials: credentials
            );
            //--retornar token
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
