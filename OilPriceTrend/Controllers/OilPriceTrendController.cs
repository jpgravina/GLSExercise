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
        public async Task<IActionResult> GetOilPriceTrend([FromBody] JsonRpcRequest jsonRpcRequest)
        {
            var id = jsonRpcRequest.Id;
            var method = jsonRpcRequest.Method;
            var startDate = DateTime.Parse(jsonRpcRequest.Params.StartDate, null, DateTimeStyles.RoundtripKind);
            var endDate = DateTime.Parse(jsonRpcRequest.Params.EndDate, null, DateTimeStyles.RoundtripKind);

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
