using Microsoft.AspNetCore.Mvc;
using Noted.Repositories;
using Noted.ViewModels;

namespace Noted.Controllers
{

    [ApiController]
    [Route("[controller]/[action]")]
    public class TableController : ControllerBase
    {
        private readonly TablesRepository _tableRepository;

        public TableController(TablesRepository tableRepository, CancellationToken cancellationToken = default)
        {
            _tableRepository = tableRepository;
        }

        [HttpPost]
        public async Task<ActionResult> EditColorAsync(int tableId, string color, CancellationToken cancellationToken = default)
        {
            try
            {
                await _tableRepository.EditColorAsync(tableId, color, cancellationToken);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> EditNameAsync(int tableId, string name, CancellationToken cancellationToken = default)
        {
            try
            {
                await _tableRepository.EditColorAsync(tableId, name, cancellationToken);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddAsync([FromBody] TableAddVM table, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await _tableRepository.AddAsync(table, cancellationToken);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddInitialAsync([FromBody] TableAddVM table, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await _tableRepository.AddInitialAsync(table, cancellationToken);
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
                var response = await _tableRepository.GetAsync(id, cancellationToken);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("tableId")]
        public async Task<ActionResult> GetFullAsync(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await _tableRepository.GetFullAsync(id, cancellationToken);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult> DeleteAsync(int tableId, CancellationToken cancellationToken = default)
        {
            try
            {
                await _tableRepository.DeleteAsync(tableId, cancellationToken);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
