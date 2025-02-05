﻿using RestSharp;

namespace OilPriceTrend.Services
{
    public class HttpClientWrapper : IHttpClient
    {
        private readonly RestClient _client;

        public HttpClientWrapper(string baseUrl)
        {
            _client = new RestClient(baseUrl);
        }

        public async Task<RestResponse> ExecuteAsync(RestRequest request)
        {
            return await _client.ExecuteAsync(request);
        }
    }
}
