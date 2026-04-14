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
        public IActionResult Login([FromBody] LoginRequest login)
        {
            string role = null;
            if(login.username == "admin" && login.password == "1235")
            {
                role = "Admin";
            }
            else if(login.username == "user" && login.password == "1235")
            {
                role = "User";
            }
            if(role == null)
            {
                return Unauthorized();
            }

            var token = GenerateToken(login.username, role);
            return Ok(new { token = role });
        }
    }
}
