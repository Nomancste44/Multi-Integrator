using System;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using ZohoToInsightIntegrator.Contract.DataContracts;
using ZohoToInsightIntegrator.Contract.DataModels.Insight.Connection;
using ZohoToInsightIntegrator.Contract.DataModels.Integrator.Account;
using ZohoToInsightIntegrator.Contract.DataModels.Zoho.SetConnection;
using ZohoToInsightIntegrator.Contract.Models;
using ZohoToInsightIntegrator.Data.DbContext;

namespace ZohoToInsightIntegrator.Data.Repository
{
    public class IntegratingCredentialRepository : Repository<IntegratingCredential>, IIntegratingCredentialRepository
    {
        private readonly IntegratorDbContext _dbContext;
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public IntegratingCredentialRepository(
            IntegratorDbContext dbContext,
            ISqlConnectionFactory sqlConnectionFactory) : base(dbContext)
        {
            _dbContext = dbContext;
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<ZohoClientAccessDto> GetZohoClientAccessDapperAsync(string accountId, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetIntegratorOpenConnection();
            var sql = $"SELECT ZohoClientId AS {nameof(ZohoClientAccessDto.ClientId)}, " +
                      $" ZohoClientSecret AS {nameof(ZohoClientAccessDto.ClientSecret)}, " +
                      $"ZohoAccessToken AS {nameof(ZohoClientAccessDto.AccessToken)}, " +
                      $"ZohoApiDomain AS {nameof(ZohoClientAccessDto.ApiDomain)} " +
                      "FROM IntegratingCredentials WHERE IsActive = 1 AND AccountId = @AccoundId";

            return await connection
                .QuerySingleAsync<ZohoClientAccessDto>(sql, new { AccoundId = accountId });
        }

        public async Task<InsightClientAccessDto> GetInsightClientAccessDapperAsync(string accountId, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetIntegratorOpenConnection();
            var sql = $"SELECT InsightClientId AS {nameof(InsightClientAccessDto.ClientId)}, " +
                      $" InsightClientSecret AS {nameof(InsightClientAccessDto.ClientSecret)}, " +
                      $"InsightApiDomain AS {nameof(InsightClientAccessDto.ApiDomain)} " +
                      "FROM IntegratingCredentials WHERE  IsActive = 1 AND AccountId = @AccoundId";

            return await connection
                .QuerySingleAsync<InsightClientAccessDto>(sql, new { AccoundId = accountId });

        }

        public async Task<IntegratingCredential> GetIntegratingCredentialWithAccountAsync(
            string accountId, CancellationToken cancellationToken)
        {
            var integratingCredential = await SingleOrDefaultAsync(ic => ic.AccountId == Guid.Parse(accountId), cancellationToken);
            if (integratingCredential == null) return null;
            await _dbContext.Entry(integratingCredential).Reference(ic => ic.Account).LoadAsync(cancellationToken);
            return integratingCredential;
        }

        public async Task<IntegratingCredentialDto> GetIntegratingCredentialsAsync(string accountId, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetIntegratorOpenConnection();
            var sql = $"SELECT [ZohoClientId] AS {nameof(IntegratingCredentialDto.ZohoClientId)}, " +
                     $"[ZohoClientSecret] AS {nameof(IntegratingCredentialDto.ZohoClientSecret)}, " +
                     $"[ZohoWfTriggerOption] AS {nameof(IntegratingCredentialDto.ZohoWfTriggerOption)}, " +
                     $"[ZohoTimeZone] AS {nameof(IntegratingCredentialDto.ZohoTimeZone)}, " +
                     $"[InsightClientId] AS {nameof(IntegratingCredentialDto.InsightClientId)}, " +
                     $"[InsightClientSecret] AS {nameof(IntegratingCredentialDto.InsightClientSecret)}, " +
                     $"[InsightTimeZone] AS {nameof(IntegratingCredentialDto.InsightTimeZone)}, " +
                     $"[InsightApiDomain] AS {nameof(IntegratingCredentialDto.InsightApiDomain)}, " +
                     $"[NofityEmail] AS {nameof(IntegratingCredentialDto.NofityEmail)}, " +
                     $"[CutOffDateTime] AS {nameof(IntegratingCredentialDto.CutOffDateTime)}, " +
                     $"[LogRetentionDays] AS {nameof(IntegratingCredentialDto.LogRetentionDays)}, " +
                     $"[IsConnectionActive] AS {nameof(IntegratingCredentialDto.IsCrmConnectionActive)} " +
                      "FROM [IntegratingCredentials] IC " +
                      "INNER JOIN [Accounts] A ON IC.AccountId = A.AccountId" +
                     " WHERE IC.AccountId = @AccountId";

            return await connection.QuerySingleAsync<IntegratingCredentialDto>(sql, new { AccountId = Guid.Parse(accountId) });
        }
    }
}
