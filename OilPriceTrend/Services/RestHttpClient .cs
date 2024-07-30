using RestSharp;

namespace OilPriceTrend.Services
{
    public class HttpClient : IHttpClient
    {
        private readonly RestClient _client;

        public HttpClient(string baseUrl)
        {
            _client = new RestClient(baseUrl);
        }

        public RestResponse Execute(RestRequest request)
        {
            return _client.Execute(request);
        }
    }
}
