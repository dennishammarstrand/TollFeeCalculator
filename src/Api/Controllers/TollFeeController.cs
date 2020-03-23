using Domain.Interfaces.Services;
using Domain.Models.QueryModels;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TollFeeController : ControllerBase
    {
        private readonly ITollService _tollService;
        public TollFeeController(ITollService tollService)
        {
            _tollService = tollService;
        }

        [HttpGet]
        public IActionResult GetTotalTollFeeAsync([FromQuery]TollFeeQueryData tollFeeQueryData)
        {
            var tollFee = tollFeeQueryData.Dates.Length > 1 ? 
                          _tollService.GetTotalTollFee(tollFeeQueryData.Vehicle, tollFeeQueryData.Dates) : 
                          _tollService.GetTollFeeForDate(tollFeeQueryData.Dates[0], tollFeeQueryData.Vehicle);
            return Ok(tollFee);
        }
    }
}