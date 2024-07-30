using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json.Linq;
using OilPriceTrend.Controllers;
using OilPriceTrend.Models;
using OilPriceTrend.Services;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OilPriceTrendTest
{
    public class OilPriceTrendTest
    {
        private readonly Mock<IOilPriceService> _mockService;
        private readonly OilPriceTrendController _controller;

        public OilPriceTrendTest()
        {
            _mockService = new Mock<IOilPriceService>();
            _controller = new OilPriceTrendController(_mockService.Object);
        }

        [Fact]
        public async Task GetOilPriceTrend_ReturnsCorrectPrices()
        {
            // Arrange
            var startDate = new DateTime(2020, 01, 01);
            var endDate = new DateTime(2020, 01, 05);
            var prices = new List<OilPrice>
        {
            new OilPrice { DateISO8601 = "2020-01-01", Price = 12.3M },
            new OilPrice { DateISO8601 = "2020-01-02", Price = 13.4M },
            new OilPrice { DateISO8601 = "2020-01-03", Price = 14.5M },
            new OilPrice { DateISO8601 = "2020-01-04", Price = 16.7M },
            new OilPrice { DateISO8601 = "2020-01-05", Price = 18.9M }
        };

            _mockService.Setup(s => s.GetOilPrices(startDate, endDate)).Returns(prices);

            var request = new JObject
            {
                ["id"] = 1,
                ["jsonrpc"] = "2.0",
                ["method"] = "GetOilPriceTrend",
                ["params"] = new JObject
                {
                    ["startDateISO8601"] = "2020-01-01",
                    ["endDateISO8601"] = "2020-01-05"
                }
            };

            var result = await _controller.GetOilPriceTrend(request);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);

            var response = okResult.Value as OilPriceResponse;
            Assert.NotNull(response);

            var resultPrices = response.Result.Prices;
            Assert.NotNull(resultPrices);
            Assert.Equal(5, resultPrices.Count);
            Assert.Equal("2020-01-01", resultPrices[0].DateISO8601);
            Assert.Equal(12.3M, resultPrices[0].Price);
        }

        [Fact]
        public void GetOilPrices_ShouldReturnFilteredPrices()
        {
            // Arrange
            var httpClient = "https://glsitaly-download.s3.eu-central-1.amazonaws.com/MOBILE_APP/BrentDaily/brent-daily.json";
            var service = new OilPriceService(new OilPriceTrend.Services.HttpClient(httpClient)); // Usa l'adapter per RestClient

            var startDate = new DateTime(2020, 8, 24);
            var endDate = new DateTime(2020, 8, 28);

            // Act
            var result = service.GetOilPrices(startDate, endDate);

            // Assert
            Assert.NotNull(result);            
            Assert.Equal(5, result.Count);
            Assert.Contains(result, price => price.DateISO8601 == "2020-08-24" && price.Price == 44.43m);
            Assert.Contains(result, price => price.DateISO8601 == "2020-08-25" && price.Price == 46.01m);
            Assert.Contains(result, price => price.DateISO8601 == "2020-08-26" && price.Price == 45.79m);
            Assert.Contains(result, price => price.DateISO8601 == "2020-08-27" && price.Price == 44.84m);
            Assert.Contains(result, price => price.DateISO8601 == "2020-08-28" && price.Price == 45.22m);
        }
    }
}
