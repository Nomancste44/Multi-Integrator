using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ZohoToInsightIntegrator.Contract.DataModels.Integrator.Account;

namespace ZohoToInsightIntegrator.Contract.BusinessContracts.Data.Account
{
    public interface IAccountHandler
    {
        Task AddAccountAsync(AddAccountCommand account, CancellationToken cancellationToken);
        Task ToggleAccountActivation(ToggleAccountActivationCommand activationCommand, CancellationToken cancellationToken);
        Task<IEnumerable<AccountDto>> GetAllAccounts(CancellationToken cancellationToken);
        Task<IEnumerable<ActiveAccountDto>> GetAllActiveAccountsAsync(CancellationToken cancellationToken);
        Task StoreIntegratingCredentialsAsync(StoreIntegratingCredentialsCommand command, CancellationToken cancellationToken);
        Task<IntegratingCredentialDto> GetIntegratingCredentialsAsync(string accountId, CancellationToken cancellationToken);
        Task<IDictionary<string, string>> GetAvailableTimezones(CancellationToken cancellationToken);
    }
}
