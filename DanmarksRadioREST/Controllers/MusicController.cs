using Microsoft.AspNetCore.Mvc;
using DanmarksRadioREST.Repo;
using DanmarksRadioREST.Models;
using DanmarksRadioREST.Repo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            List<MusicRecord> musicRecords = _musicRepository.GetAll();

            if (musicRecords == null || musicRecords.Count == 0)
            {
                return NoContent();
            }

            return Ok(musicRecords);
        }

        [HttpGet("{id}")]
        public ActionResult<MusicRecord> GetById(int id)
        {
            MusicRecord? musicRecord = _musicRepository.GetById(id);

            if (musicRecord == null)
            {
                return NotFound();
            }

            return Ok(musicRecord);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult<MusicRecord> Add([FromBody] MusicRecord musicRecord)
        {
            if (musicRecord == null)
            {
                return BadRequest();
            }

            MusicRecord createdRecord = _musicRepository.Add(musicRecord);
            return CreatedAtAction(nameof(GetById), new { id = createdRecord.Id }, createdRecord);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult<MusicRecord> Update(int id, [FromBody] MusicRecord musicRecord)
        {
            if (musicRecord == null)
            {
                return BadRequest();
            }

            MusicRecord? updatedRecord = _musicRepository.Update(id, musicRecord);

            if (updatedRecord == null)
            {
                return NotFound();
            }

            return Ok(updatedRecord);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            bool deleted = _musicRepository.Delete(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
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