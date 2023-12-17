using Microsoft.AspNetCore.Mvc;
using Noted.Repositories;
using Noted.ViewModels;

namespace Noted.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChecklistController : ControllerBase
    {
        private readonly ChecklistsRepository _checklistsRepository;

        public ChecklistController(ChecklistsRepository notesRepository)
        {
            _checklistsRepository = notesRepository;
        }

        [HttpPost]
        public async Task<ActionResult> AddAsync(ChecklistAddVM checklist, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await _checklistsRepository.AddAsync(checklist, cancellationToken);
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
                var response = await _checklistsRepository.GetAsync(id, cancellationToken);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetFullAsync(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var  response = await _checklistsRepository.GetByIdAsync(id, cancellationToken);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("Delete")]
        public async Task<ActionResult> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                await _checklistsRepository.DeleteAsync(id, cancellationToken);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost("EditColor")]
        public async Task<ActionResult> EditColorAsync(int id, string color, CancellationToken cancellationToken = default)
        {
            try
            {
                await _checklistsRepository.EditColor(id, color, cancellationToken);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
       
    }
}
