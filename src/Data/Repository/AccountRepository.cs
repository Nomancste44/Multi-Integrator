using Dapper;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ZohoToInsightIntegrator.Contract.DataContracts;
using ZohoToInsightIntegrator.Contract.DataModels.Integrator.Account;
using ZohoToInsightIntegrator.Contract.Models;
using ZohoToInsightIntegrator.Contract.Utility;
using ZohoToInsightIntegrator.Data.DbContext;

namespace ZohoToInsightIntegrator.Data.Repository
{
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        private readonly IntegratorDbContext _dbContext;
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public AccountRepository(
            IntegratorDbContext dbContext,
            ISqlConnectionFactory sqlConnectionFactory)
            : base(dbContext)
        {
            _dbContext = dbContext;
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<IEnumerable<ActiveAccountDto>> GetUserActiveAccount(string email, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetIntegratorOpenConnection();
            var sql = $"SELECT A.AccountId AS {nameof(ActiveAccountDto.AccountId)}, " +
                      $"A.AccountName AS {nameof(ActiveAccountDto.AccountName)} " +
                      "FROM Accounts A " +
                      "INNER JOIN Users U ON A.AccountId = U.AccountId " +
                      $"WHERE AccountName <> '{Common.DefaultAccountName}' AND A.IsActive = 1 AND U.Email = @Email";
            return await connection.QueryAsync<ActiveAccountDto>(sql, new { Email = email });
        }

        public async Task<Account> GetAccountWithIntegrationCredential(string accountId,
            CancellationToken cancellationToken)
        {
            var account = await GetAsync(Guid.Parse(accountId), cancellationToken);
            await _dbContext.Entry(account).Reference(a => a.IntegratingCredential).LoadAsync(cancellationToken);
            return account;
        }

        public async Task<IEnumerable<AccountDto>> GetAllAccounts(CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetIntegratorOpenConnection();
            var sql = $"SELECT AccountId AS {nameof(AccountDto.AccountId)}, " +
                      $"AccountName AS {nameof(AccountDto.AccountName)}, " +
                      $"IsActive AS {nameof(AccountDto.IsActive)} " +
                      $"FROM Accounts WHERE AccountName <> '{Common.DefaultAccountName}'";
            return await connection.QueryAsync<AccountDto>(sql);
        }


        public async Task<IEnumerable<ActiveAccountDto>> GetAllActiveAccounts(CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetIntegratorOpenConnection();
            var sql = $"SELECT AccountId AS {nameof(ActiveAccountDto.AccountId)}, " +
                      $"AccountName AS {nameof(ActiveAccountDto.AccountName)} " +
                      $"FROM Accounts WHERE AccountName <> '{Common.DefaultAccountName}' AND IsActive = 1";
            return await connection.QueryAsync<ActiveAccountDto>(sql);
        }
    }
}
