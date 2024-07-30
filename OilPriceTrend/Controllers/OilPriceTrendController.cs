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

        [HttpPost]
        public async Task<IActionResult> GetOilPriceTrend([FromBody] JObject jsonRpcRequest)
        {
            var id = jsonRpcRequest["id"];
            var method = jsonRpcRequest["method"].ToString();
            var startDate = DateTime.Parse(jsonRpcRequest["params"]["startDateISO8601"].ToString(), null, DateTimeStyles.RoundtripKind);
            var endDate = DateTime.Parse(jsonRpcRequest["params"]["endDateISO8601"].ToString(), null, DateTimeStyles.RoundtripKind);

            if (method != "GetOilPriceTrend")
            {
                return BadRequest(new { jsonrpc = "2.0", id, error = new { code = -32601, message = "Method not found" } });
            }

            var prices = _oilPriceService.GetOilPrices(startDate, endDate);

            var response = new OilPriceResponse
            {
                JsonRpc = "2.0",
                Id = Convert.ToInt32(id),
                Result = new OilPriceResult { Prices = prices }
            };

            return Ok(response);
        }
    }
}
