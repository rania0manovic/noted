using Microsoft.AspNetCore.Mvc;
using Noted.Repositories;
using Noted.ViewModels;

namespace Noted.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly NotesRepository _notesRepository;

        public NoteController(NotesRepository notesRepository)
        {
            _notesRepository = notesRepository;
        }

        [HttpPost]
        public async Task<ActionResult> AddAsync(NoteAddVM note, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await _notesRepository.AddAsync(note, cancellationToken);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("repositoryId")]
        public async Task<ActionResult> GetAsync(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await _notesRepository.GetAsync(id, cancellationToken);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("noteId")]
        public async Task<ActionResult> GetFullAsync(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var response =await _notesRepository.GetFullAsync(id, cancellationToken);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("Edit")]
        public async Task<ActionResult> EditAsync(NoteEditVM note, CancellationToken cancellationToken = default)
        {

            try
            {
                var response = await _notesRepository.EditAsync(note, cancellationToken);
                if (response == null)
                    return NotFound();
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
    }
}
