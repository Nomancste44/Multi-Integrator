using System;
using System.Threading;
using System.Threading.Tasks;
using ZohoToInsightIntegrator.Contract.Contracts;
using ZohoToInsightIntegrator.Contract.DataModels.Insight.Connection;
using ZohoToInsightIntegrator.Contract.DataModels.Integrator.Account;
using ZohoToInsightIntegrator.Contract.DataModels.Zoho.SetConnection;
using ZohoToInsightIntegrator.Contract.Models;

namespace ZohoToInsightIntegrator.Contract.DataContracts
{
    public interface IIntegratingCredentialRepository : IRepository<IntegratingCredential>
    {
        Task<ZohoClientAccessDto> GetZohoClientAccessDapperAsync(string accountId, CancellationToken cancellationToken);
        Task<InsightClientAccessDto> GetInsightClientAccessDapperAsync(string accountId, CancellationToken cancellationToken);
        Task<IntegratingCredential> GetIntegratingCredentialWithAccountAsync(string integratingCredentialId, CancellationToken cancellationToken);
        Task<IntegratingCredentialDto> GetIntegratingCredentialsAsync(string accountId, CancellationToken cancellationToken);
    }
}
