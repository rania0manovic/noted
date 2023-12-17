using Microsoft.AspNetCore.Mvc;
using Noted.Entities;
using Noted.Repositories;
using Noted.ViewModels;

namespace Noted.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChecklistItemsController : ControllerBase
    {
        private readonly ChecklistItemsRepository _checklistItemsRepository;

        public ChecklistItemsController( ChecklistItemsRepository checklistItemsRepository)
        {
            _checklistItemsRepository = checklistItemsRepository;
        }

        [HttpPost]
        public async Task<ActionResult> AddAsync(ChecklistItemAddVM checklistItem, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await _checklistItemsRepository.AddAsync(checklistItem, cancellationToken);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("Edit")]
        public async Task<ActionResult> EditAsync(ChecklistItemEditVM checklistItem, CancellationToken cancellationToken = default)
        {

            try
            {
                var response = await _checklistItemsRepository.EditAsync(checklistItem, cancellationToken);
                if (response == null)
                    return NotFound();
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
                await _checklistItemsRepository.DeleteAsync(id, cancellationToken);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
      
    }
}
