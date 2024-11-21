using GoldenRaspberryAwards.Api.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace GoldenRaspberryAwards.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AwardsController : ControllerBase
    {
        private readonly IProducerIntervalService _producerIntervalService;

        public AwardsController(IProducerIntervalService producerIntervalService)
        {
            _producerIntervalService = producerIntervalService;
        }

        [HttpGet("producer-intervals")]
        public async Task<IActionResult> GetProducerIntervals()
        {
            var result = await _producerIntervalService.GetProducerAwardIntervalsAsync();
            return Ok(result);
        }
    }
}
