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
        public IActionResult Login([FromBody] LoginRequest request)
        {
            string role = null;

            if (request.Username == "admin" && request.Password == "1235")
            {
                role = "Admin";
            }
            else if (request.Username == "user" && request.Password == "1235")
            {
                role = "User";
            }

            if (role == null)
            {
                return Unauthorized();
            }

            string tokenString = GenerateToken(request.Username, role);

            return Ok(new
            {
                token = tokenString,
                role = role
            });
        }

        private string GenerateToken(string username, string role)
        {
            string key = _config["JwtSettings:Key"];
            string issuer = _config["JwtSettings:Issuer"];
            string audience = _config["JwtSettings:Audience"];

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            Claim[] claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role)
            };

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public class LoginRequest
        {
            public string Username { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
        }
    }
}