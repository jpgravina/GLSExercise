using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using OilPriceTrend.Models;
using OilPriceTrend.Services;
using System.Globalization;

namespace OilPriceTrend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OilPriceTrendController : ControllerBase
    {
        private readonly IOilPriceService _oilPriceService;

        public OilPriceTrendController(IOilPriceService oilPriceService)
        {
            _oilPriceService = oilPriceService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        [HttpGet("GetOilPriceTrend")]
        public async Task<IActionResult> GetOilPriceTrend(string fromDate, string toDate)
        {
            DateTime startDate, endDate;
            string dateFormat = "yyyy-MM-dd";

            if (!DateTime.TryParseExact(fromDate, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate) ||
                !DateTime.TryParseExact(toDate, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate))
            {
                return BadRequest(new { error = "Dates must be in the format "+dateFormat });
            }

            var prices = await _oilPriceService.GetOilPricesAsync(startDate, endDate);

            var response = new OilPriceResponse
            {
                JsonRpc = "2.0",
                Id = 1,
                Result = new OilPriceResult { Prices = prices }
            };

            return Ok(response);
        }
    }
}
