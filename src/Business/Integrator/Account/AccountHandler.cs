using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ZohoToInsightIntegrator.Contract.BusinessContracts.Data.Account;
using ZohoToInsightIntegrator.Contract.Contracts;
using ZohoToInsightIntegrator.Contract.DataModels.Integrator.Account;
using ZohoToInsightIntegrator.Contract.Models;

namespace ZohoToInsightIntegrator.Business.Integrator.Account
{
    public class AccountHandler : IAccountHandler
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IExecutionContextAccessor _executionContextAccessor;

        public AccountHandler(
            IUnitOfWork unitOfWork,
            IExecutionContextAccessor executionContextAccessor
            )
        {
            _unitOfWork = unitOfWork;
            _executionContextAccessor = executionContextAccessor;
        }
        public async Task AddAccountAsync(AddAccountCommand account, CancellationToken cancellationToken)
        {
            await _unitOfWork.AccountRepository
                .AddAsync(new Contract.Models.Account
                {
                    AccountName = account.AccountName,
                    IsActive = true,
                    UpdatedOn = DateTime.Now
                }, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
        }

        public async Task ToggleAccountActivation(ToggleAccountActivationCommand activationCommand, CancellationToken cancellationToken)
        {
            var account = await _unitOfWork.AccountRepository.GetAsync(Guid.Parse(activationCommand.AccountId), cancellationToken);
            account.IsActive = activationCommand.IsActive;
            account.UpdatedOn = DateTime.Now;

            await _unitOfWork.CommitAsync(cancellationToken);
        }

        public Task<IEnumerable<AccountDto>> GetAllAccounts(CancellationToken cancellationToken)
        {
            return _unitOfWork.AccountRepository.GetAllAccounts(cancellationToken);
        }

        public async Task<IEnumerable<ActiveAccountDto>> GetAllActiveAccountsAsync(CancellationToken cancellationToken)
        {
            if(_executionContextAccessor.IsSuperAdmin)
                return await _unitOfWork.AccountRepository.GetAllActiveAccounts(cancellationToken);
            return await _unitOfWork.AccountRepository.GetUserActiveAccount(_executionContextAccessor.Email,
                cancellationToken);
        }

        public async Task StoreIntegratingCredentialsAsync(StoreIntegratingCredentialsCommand command, CancellationToken cancellationToken)
        {
            var intergratingCredential = await _unitOfWork
                .IntegratingCredentialRepository
                .GetIntegratingCredentialWithAccountAsync(command.AccountId, cancellationToken);
            if (intergratingCredential != null)
            {
                intergratingCredential.ZohoClientId = command.ZohoClientId;
                intergratingCredential.ZohoClientSecret = command.ZohoClientSecret;
                intergratingCredential.ZohoTimeZone = command.ZohoTimeZone;
                intergratingCredential.ZohoWfTriggerOption = command.ZohoWfTriggerOption;
                intergratingCredential.InsightClientId = command.InsightClientId;
                intergratingCredential.InsightClientSecret = command.InsightClientSecret;
                intergratingCredential.InsightApiDomain = command.InsightApiDomain;
                intergratingCredential.InsightTimeZone = command.InsightTimeZone;
                intergratingCredential.CutOffDateTime = command.CutOffDateTime;
                intergratingCredential.NofityEmail = command.NofityEmail;
                intergratingCredential.LogRetentionDays = command.LogRetentionDays;

                intergratingCredential.Account.IsConnectionActive = false;
            }
            else
            {
                await _unitOfWork
                    .IntegratingCredentialRepository
                    .AddAsync(new IntegratingCredential
                    {
                        AccountId = Guid.Parse(command.AccountId),
                        ZohoClientId = command.ZohoClientId,
                        ZohoClientSecret = command.ZohoClientSecret,
                        ZohoTimeZone = command.ZohoTimeZone,
                        ZohoWfTriggerOption = command.ZohoWfTriggerOption,
                        InsightClientId = command.InsightClientId,
                        InsightClientSecret = command.InsightClientSecret,
                        InsightApiDomain = command.InsightApiDomain,
                        InsightTimeZone = command.InsightTimeZone,
                        CutOffDateTime = command.CutOffDateTime,
                        NofityEmail = command.NofityEmail,
                        LogRetentionDays = command.LogRetentionDays
                    }, cancellationToken);

            }
            await _unitOfWork.CommitAsync(cancellationToken);
        }

        public async Task<IntegratingCredentialDto> GetIntegratingCredentialsAsync(string accountId, CancellationToken cancellationToken)
        {
            return await _unitOfWork
                .IntegratingCredentialRepository
                .GetIntegratingCredentialsAsync(accountId,
                cancellationToken);
        }

        public async Task<IDictionary<string, string>> GetAvailableTimezones(CancellationToken cancellationToken)
        {
            return await Task.FromResult<IDictionary<string,string>>(TimeZoneInfo
                    .GetSystemTimeZones()
                    .ToDictionary(z => z.Id, z => z.DisplayName));

        }
    }
}
