using RestSharp;

namespace OilPriceTrend.Services
{
    using RestSharp;

    public interface IHttpClient
    {
        RestResponse Execute(RestRequest request);
    }

}
