using Microsoft.AspNetCore.Mvc;
using DanmarksRadioREST.Repo;
using DanmarksRadioREST.Models;
using Microsoft.AspNetCore.Authorization;

namespace DanmarksRadioREST.Controllers
{
    [Authorize]
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
        public ActionResult<IEnumerable<MusicRecord>> GetAll(
        [FromQuery] string? title,
        [FromQuery] string? artist
        )
        {
            IEnumerable<MusicRecord> musicRecords = _musicRepository.GetAll();

            if (!string.IsNullOrWhiteSpace(title))
            {
                musicRecords = musicRecords.Where(m =>
                    !string.IsNullOrEmpty(m.Title) &&
                    m.Title.Contains(title, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(artist))
            {
                musicRecords = musicRecords.Where(m =>
                    !string.IsNullOrEmpty(m.Artist) &&
                    m.Artist.Contains(artist, StringComparison.OrdinalIgnoreCase));
            }

            if (!musicRecords.Any())
            {
                return NoContent(); // 204
            }

            return Ok(musicRecords);
        }
    }
}
