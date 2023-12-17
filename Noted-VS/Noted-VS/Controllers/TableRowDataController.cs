using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Noted.Data;
using Noted.Repositories;
using Noted.ViewModels;

namespace Noted.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableRowDataController : ControllerBase
    {
        private readonly TableRowDatasRepository _tableRowDatasRepository;

        public TableRowDataController(TableRowDatasRepository tableRowDatasRepository)
        {
            _tableRowDatasRepository = tableRowDatasRepository;
        }

        [HttpPost]
        public async Task<ActionResult> AddAsync([FromBody] TableRowDataVM rowData, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await _tableRowDatasRepository.AddAsync(rowData, cancellationToken);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("Edit")]
        public async Task<ActionResult> EditAsync([FromBody] TableRowDataEditVM data, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await _tableRowDatasRepository.EditAsync(data, cancellationToken);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("Delete")]
        public async Task<ActionResult> DeleteAsync(int rowId, CancellationToken cancellationToken = default)
        {
            try
            {
                await _tableRowDatasRepository.DeleteAsync(rowId, cancellationToken);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
