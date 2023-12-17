using Microsoft.AspNetCore.Mvc;
using Noted.Repositories;
using Noted.ViewModels;

namespace Noted.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : ControllerBase
    {

        private readonly UsersRepository _usersRepository;

        public UserController(UsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        [HttpPost]
        public async Task<ActionResult> Register([FromBody] UserVM user, CancellationToken cancellationToken = default)
        {
            try
            {
                await _usersRepository.Register(user,cancellationToken);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> EditProfilePhotoAsync(int userId, [FromForm] PhotoAddVM photo, CancellationToken cancellationToken = default)
        {
            try
            {
                await _usersRepository.EditProfilePhotoAsync(userId, photo, cancellationToken);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> EditHeaderPhotoAsync(int userId, [FromForm] PhotoAddVM photo, CancellationToken cancellationToken = default)
        {
            try
            {
                await _usersRepository.EditHeaderPhotoAsync(userId, photo, cancellationToken);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> EditAsync([FromBody] UserVM user, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await _usersRepository.EditAsync(user, cancellationToken);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult> LoginAsync(string email, string password, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await _usersRepository.LoginAsync(email, password, cancellationToken);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }


}
