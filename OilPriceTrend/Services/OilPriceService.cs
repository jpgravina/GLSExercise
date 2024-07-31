using Newtonsoft.Json.Linq;
using OilPriceTrend.Models;
using RestSharp;
using System.Net.Http;

namespace OilPriceTrend.Services
{
    public class OilPriceService : IOilPriceService
    {
        private readonly IHttpClient _httpClient;

        public OilPriceService(IHttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<OilPrice>> GetOilPricesAsync(DateTime startDate, DateTime endDate)
        {
            var request = new RestRequest();
            var response = await _httpClient.ExecuteAsync(request);

            if (!response.IsSuccessful)
            {
                throw new Exception("Could not retrieve oil prices");
            }

            var data = JArray.Parse(response.Content);
            var filteredData = data
                .Where(d => DateTime.Parse(d["Date"].ToString()) >= startDate && DateTime.Parse(d["Date"].ToString()) <= endDate)
                .Select(d => new OilPrice
                {
                    Date = d["Date"].ToString(),
                    Price = (decimal)d["Price"]
                })
                .ToList();

            return filteredData;
        }
    }
}
