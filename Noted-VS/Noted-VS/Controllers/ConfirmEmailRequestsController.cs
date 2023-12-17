using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Noted.Data;
using Noted.Repositories;

namespace Noted.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfirmEmailRequestsController : ControllerBase
    {
        private readonly ConfirmEmailRequestsRepository _confirmEmailRequestsRepository;

        public ConfirmEmailRequestsController(ConfirmEmailRequestsRepository confirmEmailRequestsRepository)
        {
            _confirmEmailRequestsRepository = confirmEmailRequestsRepository;
        }

        [HttpGet]
        public async Task<ActionResult> IsValidAsync(string token, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await _confirmEmailRequestsRepository.IsValidAsync(token, cancellationToken);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
    }
}
