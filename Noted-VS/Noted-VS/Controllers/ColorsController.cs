using Microsoft.AspNetCore.Mvc;
using Noted.Repositories;

namespace Noted.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorsController : ControllerBase
    {
        private readonly ColorsRepository _colorsRepository;
        public ColorsController(ColorsRepository colorsRepository)
        {
            _colorsRepository = colorsRepository;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            try
            {
                var colors = await _colorsRepository.GetAllAsync(cancellationToken);
                return Ok(colors);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
