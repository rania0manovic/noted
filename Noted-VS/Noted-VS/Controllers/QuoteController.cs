using Microsoft.AspNetCore.Mvc;
using Noted.Data;
using Noted.Repositories;
using Noted.Security;
using Noted.ViewModels;

namespace Noted.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class QuoteController : ControllerBase
    {
        private readonly QuotesRepository _quotesRepository;
        public QuoteController(QuotesRepository quotesRepository)
        {
            _quotesRepository = quotesRepository;
        }

        [HttpPost]
        public async Task<ActionResult> AddAsync([FromBody] QuoteAddVM quote, CancellationToken cancellationToken = default)
        {
            try
            {
                await _quotesRepository.AddAsync(quote, cancellationToken);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await _quotesRepository.GetRandomAsync(cancellationToken);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddDailyAsync([FromBody] UserQuoteAddVM userQuote, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await _quotesRepository.AddDailyAsync(userQuote, cancellationToken);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetDailyAsync(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await _quotesRepository.GetByUserIdAsync(id, cancellationToken);
                if (response == null)
                    return NotFound();
                else return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> UpdateDailyAsync([FromBody] UserQuoteAddVM userQuote, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await _quotesRepository.UpdateForUserAsync(userQuote, cancellationToken);
                if (response == null)
                    return NotFound();
                else return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}