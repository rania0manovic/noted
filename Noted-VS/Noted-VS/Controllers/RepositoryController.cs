using Microsoft.AspNetCore.Mvc;
using Noted.Data;
using Noted.Repositories;
using Noted.Security;
using Noted.ViewModels;

namespace Noted.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class RepositoryController : ControllerBase
    {
        private readonly RepositoriesRepository _repositoriesRepository;
        public RepositoryController(RepositoriesRepository repositoriesRepository)
        {
            _repositoriesRepository = repositoriesRepository;
        }

        [HttpPost]
        public async Task<ActionResult> AddAsync([FromBody] RepositoryAddVM repo, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await _repositoriesRepository.AddAsync(repo, cancellationToken);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetAllAsync(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await _repositoriesRepository.GetAllAsync(id, cancellationToken);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> EditAsync(int id, [FromBody] RepositoryAddVM repo, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await _repositoriesRepository.EditAsync(id, repo, cancellationToken);
                if (response == null)
                    return NotFound();
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                await _repositoriesRepository.DeleteAsync(id, cancellationToken);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}