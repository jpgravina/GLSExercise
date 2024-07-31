using RestSharp;

namespace OilPriceTrend.Services
{
    using RestSharp;

    public interface IHttpClient
    {
        Task<RestResponse> ExecuteAsync(RestRequest request);
    }

}
