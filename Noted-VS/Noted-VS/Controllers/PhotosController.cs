using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Noted.Data;
using Noted.Repositories;

namespace Noted.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly PhotosRepository _photosRepository;
        public PhotosController(PhotosRepository photosRepository)
        {
            _photosRepository = photosRepository;
        }
        [HttpGet("GetProfilePhoto")]
        public async Task<ActionResult> GetProfilePhotoAsync(int userId, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await _photosRepository.GetProfilePhotoAsync(userId, cancellationToken);
                if (response == null)
                    return NotFound();
                else
                {
                    return Ok(File(response.Data, response.ContentType, $"photo-{response.Id}", true));
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("GetHeaderPhoto")]
        public async Task<ActionResult> GetHeaderPhotoAsync(int userId, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await _photosRepository.GetHeaderPhotoAsync(userId);
                if (response == null)
                    return NotFound();
                else
                {
                    return Ok(File(response.Data, response.ContentType, $"photo-{response.Id}", true));
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
