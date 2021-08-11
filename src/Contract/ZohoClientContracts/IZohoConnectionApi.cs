using System.Threading;
using System.Threading.Tasks;
using ZohoToInsightIntegrator.Contract.DataModels.Zoho.SetConnection;

namespace ZohoToInsightIntegrator.Contract.ZohoClientContracts
{
    public interface IZohoConnectionApi
    {
        Task<ZohoClientAccessDto> SetZohoClientAccessAsync(string clientId, string clientSecret, string code, CancellationToken cancellationToken);
        Task<ZohoClientAccessDto> RefreshZohoClientAcessTokenAsync(string cliendId, string clientSecret,string zohoRefreshToken, CancellationToken cancellationToken);
       
    }
}
