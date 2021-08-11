using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using ZohoToInsightIntegrator.Contract.DataModels.Zoho.SetConnection;
using ZohoToInsightIntegrator.Contract.Utility;
using ZohoToInsightIntegrator.Contract.ZohoClientContracts;

namespace ZohoToInsightIntegrator.ZohoClient.ZohoApi
{
    public class ZohoConnectionApi : IZohoConnectionApi
    {
        private readonly HttpClient _httpClient;
        public ZohoConnectionApi(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }
        public async Task<ZohoClientAccessDto> SetZohoClientAccessAsync(string clientId, string clientSecret,string code, CancellationToken cancellationToken)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, IntegratingEndpoints.ZohoAuth)
                .SetZohoAccessTokenRequestingHeaders(clientId, clientSecret, code);
            var response = await _httpClient.SendAsync(httpRequest, cancellationToken);
            if(!response.IsSuccessStatusCode) throw new UnauthorizedAccessException(response.Content.ToString());
            var result = await response.Content.ReadFromJsonAsync<ZohoClientAccessDto>(null,cancellationToken);
            result.ClientId = clientId; result.ClientSecret = clientSecret;
            return result;
        }

        public async Task<ZohoClientAccessDto> RefreshZohoClientAcessTokenAsync(
            string clientId, string clientSecret, string zohoRefreshToken, CancellationToken cancellationToken)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, IntegratingEndpoints.ZohoAuth)
                .SetTokenRefresingHeaders(clientId, clientSecret, zohoRefreshToken);
            var response = await _httpClient.SendAsync(httpRequest, cancellationToken);
            if(!response.IsSuccessStatusCode) throw new UnauthorizedAccessException(response.Content.ToString());
            var result = await response.Content.ReadFromJsonAsync<ZohoClientAccessDto>();
            result.ClientId = clientId; result.ClientSecret = clientSecret;
            result.RefreshToken = zohoRefreshToken;
            return result;
        }
        
    }
}
