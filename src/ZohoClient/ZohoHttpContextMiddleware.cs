using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using ZohoToInsightIntegrator.Contract.Contracts;
using ZohoToInsightIntegrator.Contract.Utility;
using ZohoToInsightIntegrator.Contract.ZohoClientContracts;

namespace ZohoToInsightIntegrator.ZohoClient
{
    public class ZohoHttpContextMiddleware : DelegatingHandler
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IZohoConnectionApi _zohoConnectionApi;

        public ZohoHttpContextMiddleware(
            IUnitOfWork unitOfWork,
            IZohoConnectionApi zohoConnectionApi)
        {
            _unitOfWork = unitOfWork;
            _zohoConnectionApi = zohoConnectionApi;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {

            var response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode == HttpStatusCode.Unauthorized && !request.Properties.ContainsKey("requestCount"))
            {
                request.Properties.TryGetValue(Common.AccountKey, out var accountId);

                var integratingCredential = await _unitOfWork
                    .IntegratingCredentialRepository
                    .SingleOrDefaultAsync(ic => ic.AccountId == Guid.Parse(accountId.ToString()), cancellationToken);

                var refreshedAccess = await _zohoConnectionApi.RefreshZohoClientAcessTokenAsync(
                    integratingCredential.ZohoClientId, integratingCredential.ZohoClientSecret, integratingCredential.ZohoRefreshToken, cancellationToken);
                integratingCredential.ZohoAccessToken = refreshedAccess.AccessToken;

                await _unitOfWork.CommitAsync(cancellationToken);
                request.Headers.Authorization = new AuthenticationHeaderValue(Common.ZohoAuthScheme, refreshedAccess.AccessToken);
                request.Properties.Add(new KeyValuePair<string, object>("refreshCount", 1));

                response = await base.SendAsync(request, cancellationToken);

            }
            return response;
        }
    }
}
