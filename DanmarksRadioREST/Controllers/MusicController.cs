using Microsoft.AspNetCore.Mvc;
using DanmarksRadioREST.Repo;
using DanmarksRadioREST.Models;

namespace DanmarksRadioREST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusicController : Controller
    {
        private readonly IMusicRepository _musicRepository;

        public MusicController(IMusicRepository musicRepository)
        {
            _musicRepository = musicRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<MusicRecord>> GetAll()
        {
            var musicRecords = _musicRepository.GetAll();
            if(musicRecords == null)
            {
                return NotFound();
            }
            return Ok(musicRecords);
        }
    }
}
