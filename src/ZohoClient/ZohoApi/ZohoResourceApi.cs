using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using ZohoToInsightIntegrator.Contract.Contracts;
using ZohoToInsightIntegrator.Contract.DataModels.Zoho.Field;
using ZohoToInsightIntegrator.Contract.DataModels.Zoho.Module;
using ZohoToInsightIntegrator.Contract.Models;
using ZohoToInsightIntegrator.Contract.Utility;
using ZohoToInsightIntegrator.Contract.ZohoClientContracts;

namespace ZohoToInsightIntegrator.ZohoClient.ZohoApi
{
    public class ZohoResourceApi : IZohoResourceApi
    {
        private readonly ZohoHttpClient _zohoHttpClient;
        private readonly IUnitOfWork _unitOfWork;

        public ZohoResourceApi(
            ZohoHttpClient zohoHttpClient,
            IUnitOfWork unitOfWork)
        {
            _zohoHttpClient = zohoHttpClient;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ZohoAvailableModuleDto>> GetZohoAvailableModulesAsync(string accountId, CancellationToken cancellationToken)
        {
            var account = await _unitOfWork.IntegratingCredentialRepository.GetZohoClientAccessDapperAsync(accountId, cancellationToken);
            var url =  $"{ account.ApiDomain}/{IntegratingEndpoints.ZohoAllModules }";
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, url);
            httpRequest.Properties.Add(Common.AccountKey, accountId);
            httpRequest.SetResourceRequestingHeaders(account.ClientId, account.ClientSecret,
                Common.ZohoAuthScheme, account.AccessToken);

            var response = await _zohoHttpClient.HttpClient.SendAsync(httpRequest, cancellationToken);
            var result = await response.Content.ReadFromJsonAsync<module>(null, cancellationToken);
            return result?.Modules;
            
        }

        public async Task<IEnumerable<Section>> GetZohoModuleFieldsAsync(string accountId,
            string moduleApiName, CancellationToken cancellationToken)
        {
            var account = await _unitOfWork.IntegratingCredentialRepository.GetZohoClientAccessDapperAsync(accountId, cancellationToken);
            var url = $"{ account.ApiDomain}/{IntegratingEndpoints.ZohoModuleFields}{moduleApiName}";
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, url);
            httpRequest.Properties.Add(Common.AccountKey, accountId);
            httpRequest.SetResourceRequestingHeaders(account.ClientId, account.ClientSecret,
                Common.ZohoAuthScheme, account.AccessToken);

            var response = await _zohoHttpClient.HttpClient.SendAsync(httpRequest, cancellationToken);
            var result = await response.Content.ReadFromJsonAsync<Layout>(null, cancellationToken);

            return result?.Layouts.Where(l => l.LayoutName == "Standard");
            //.SelectMany(s => s.Sections)
            //.SelectMany(f => f.Fields).Where(a => a.FieldApiName != "Record_Image");

            //return result?.Layouts.Where(l => l.LayoutName == "Standard" 
            //                                  && l.Sections.Where(s=>s.Fields
            //                                      .Where(f=>f.FieldApiName != "Record_Image")));


        }
    }
}
