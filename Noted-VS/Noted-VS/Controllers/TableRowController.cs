using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Noted.Data;
using Noted.Repositories;
using Noted.Security;
using Noted.ViewModels;

namespace Noted.Controllers
{

    [ApiController]
    [Route("[controller]/[action]")]
    public class TableRowController : ControllerBase
    {
        private readonly TableRowsRepository _tableRowsRepository;

        public TableRowController(TableRowsRepository tableRowsRepository)
        {
            _tableRowsRepository = tableRowsRepository;
        }

        [HttpPost]
        public async Task<ActionResult> AddAsync([FromBody] TableRowVM row, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await _tableRowsRepository.AddAsync(row, cancellationToken);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult> DeleteAsync(int rowId, CancellationToken cancellationToken = default)
        {
            try
            {
                await _tableRowsRepository.DeleteAsync(rowId, cancellationToken);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
