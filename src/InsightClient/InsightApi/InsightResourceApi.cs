using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZohoToInsightIntegrator.Contract.Contracts;
using ZohoToInsightIntegrator.Contract.InsightClientContracts;
using ZohoToInsightIntegrator.Contract.Utility;

namespace ZohoToInsightIntegrator.InsightClient.InsightApi
{
    public class InsightResourceApi : IInsightResourceApi
    {
        private readonly InsightHttpClient _httpClient;
        private readonly IUnitOfWork _unitOfWork;

        public InsightResourceApi(
            InsightHttpClient httpClient,
            IUnitOfWork unitOfWork)
        {
            _httpClient = httpClient;
            _unitOfWork = unitOfWork;
        }
        public async Task<string> GetAllData(string accountId, string moduleName, CancellationToken cancellationToken)
        {
            var account = await _unitOfWork
                .IntegratingCredentialRepository
                .GetInsightClientAccessDapperAsync(accountId, cancellationToken);

            var url = $"{ account.ApiDomain}/Services/{moduleName}/List";
           
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, url);
            httpRequest.SetInsightResourceRequestingHeaders(account.ClientId, account.ClientSecret);
            
            var response = await _httpClient.HttpClient.SendAsync(httpRequest, cancellationToken);

            var result = await response.Content.ReadAsStringAsync();

            return result;
        }
    }
}
