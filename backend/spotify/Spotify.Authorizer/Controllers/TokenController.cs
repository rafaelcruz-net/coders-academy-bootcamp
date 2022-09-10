using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Spotify.Authorizer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private string jwtsecret = "ACDt1vR3lXToPQ1g3MyN";
        private string audience = "spotify-api";
        private string issuer = "http://127.0.0.1:80";

        [HttpPost]
        public IActionResult Token([FromForm] string username, [FromForm] string password)
        {
            var isLogged = this.AuthenticateUser(username, password);

            if (!isLogged)
                return Unauthorized();

            return Ok(GenerateToken());
        }

        private bool AuthenticateUser(string username, string password)
        {
            //Vai na base de dados, caso esteja logado return true;

            if (username == "admin" && password == "1234")
                return true;

            return false;
        }

        private string GenerateToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtsecret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Name,"Rafael"),
                new Claim("role","Admin")
            };
            
            var token = new JwtSecurityToken(issuer,
               audience,
               claims,
               expires: DateTime.Now.AddMinutes(15),
               signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

    }
}
