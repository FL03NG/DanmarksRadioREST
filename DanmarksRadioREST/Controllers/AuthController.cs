using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DanmarksRadioREST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IConfiguration _config;
        public AuthController(IConfiguration config)
        {
            _config = config;
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] loginRequest login)
        {
            string role = null;
            if (login.username == "admin" && login.password == "1235")
            {
                role = "Admin";
            }
            else if (login.username == "user" && login.password == "1235")
            {
                role = "User";
            }
            if (role == null)
            {
                return Unauthorized();
            }

            var token = GenerateToken(login.username, role);
            return Ok(new { token = role });
        }
        private string GenerateToken(string username, string role)
        {
            var jwtsettings = _config.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtsettings["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role)
            };
            var token = new JwtSecurityToken(
                issuer: jwtsettings["Issuer"],
                audience: jwtsettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public class loginRequest
        {
            public string username { get; set; }
            public string password { get; set; }
        }
    }
}
