using Microsoft.AspNetCore.Mvc;

namespace DanmarksRadioREST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly string key = "1235";
        public IActionResult Index()
        {
            return View();
        }
    }
}
