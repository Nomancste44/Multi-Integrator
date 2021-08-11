using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ZohoToInsightIntegrator.Contract.Contracts;
using ZohoToInsightIntegrator.Contract.DataModels.Integrator.Account;
using ZohoToInsightIntegrator.Contract.Models;

namespace ZohoToInsightIntegrator.Contract.DataContracts
{
    public interface IAccountRepository : IRepository<Account>
    {
        Task<IEnumerable<DataModels.Integrator.Account.AccountDto>> GetAllAccounts(CancellationToken cancellationToken);
        Task<IEnumerable<ActiveAccountDto>> GetAllActiveAccounts(CancellationToken cancellationToken);
        Task<IEnumerable<ActiveAccountDto>> GetUserActiveAccount(string email,CancellationToken cancellationToken);

        Task<Account> GetAccountWithIntegrationCredential(string accountId, CancellationToken cancellationToken);
    }
}
