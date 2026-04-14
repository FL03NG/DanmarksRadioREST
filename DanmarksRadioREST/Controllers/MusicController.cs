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
        [Authorize(Roles = "Admin,User")]
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
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public ActionResult<MusicRecord> Add([FromBody] MusicRecord musicRecord)
        {
            if (musicRecord == null)
            {
                return BadRequest("Music record cannot be null.");
            }
            MusicRecord? createdRecord = _musicRepository.Add(musicRecord);
            if (createdRecord == null)
            {
                return BadRequest("Invalid music record data.");
            }
            return CreatedAtAction(nameof(GetAll), new { id = createdRecord.Id }, createdRecord);
        }
    }
}
