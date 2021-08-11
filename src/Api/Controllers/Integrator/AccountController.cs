using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using ZohoToInsightIntegrator.Api.Configuration.Authentication;
using ZohoToInsightIntegrator.Contract.BusinessContracts.Data.Account;
using ZohoToInsightIntegrator.Contract.DataModels.Integrator.Account;
using ZohoToInsightIntegrator.Contract.Utility;

namespace ZohoToInsightIntegrator.Api.Controllers.Integrator
{
    [Route("api/[controller]")]
    [ApiController]
    [HasPermission(PolicyClaimTypes.SuperAdmin)]
    public class AccountController : ControllerBase
    {
        private readonly IAccountHandler _accountHandler;

        public AccountController(IAccountHandler accountHandler)
        {
            _accountHandler = accountHandler;
        }

        [HttpPost(nameof(CreateIntegratorAccount))]
        public async Task<IActionResult> CreateIntegratorAccount(AddAccountCommand account, CancellationToken cancellationToken)
        {
            await _accountHandler
               .AddAccountAsync(account, cancellationToken);
            return Ok(HttpStatusCode.Created);
        }

        [HttpPatch(nameof(ToggleAccountActivation))]
        public async Task<IActionResult> ToggleAccountActivation(ToggleAccountActivationCommand command, CancellationToken cancellationToken)
        {
            await _accountHandler
               .ToggleAccountActivation(command, cancellationToken);
            return Ok(HttpStatusCode.OK);
        }

        [HttpGet(nameof(GetAllAccounts))]
        public async Task<IEnumerable<AccountDto>> GetAllAccounts(CancellationToken cancellationToken)
        {
            return await _accountHandler.GetAllAccounts(cancellationToken);
        }

        [HasPermission(PolicyClaimTypes.ViewOnly)]
        [HttpGet(nameof(GetAllActiveAccounts))]
        public async Task<IEnumerable<ActiveAccountDto>> GetAllActiveAccounts(CancellationToken cancellationToken)
        {
            return await _accountHandler.GetAllActiveAccountsAsync(cancellationToken);
        }

        [HasPermission(PolicyClaimTypes.Admin)]
        [HttpPost(nameof(StoreIntegratingCredentials))]
        public async Task<IActionResult> StoreIntegratingCredentials(StoreIntegratingCredentialsCommand command, CancellationToken cancellationToken)
        {
            await _accountHandler.StoreIntegratingCredentialsAsync(command, cancellationToken);
            return Ok(HttpStatusCode.Created);
        }

        [HasPermission(PolicyClaimTypes.Admin)]
        [HttpGet(nameof(GetIntegratingCredentials))]
        public async Task<IntegratingCredentialDto> GetIntegratingCredentials(string accountId, CancellationToken cancellationToken)
        {
            return await _accountHandler
                .GetIntegratingCredentialsAsync(accountId, cancellationToken);
        }

        [HasPermission(PolicyClaimTypes.Admin)]
        [HttpGet(nameof(GetAvailableTimezones))]
        public async Task<IDictionary<string, string>> GetAvailableTimezones(CancellationToken cancellationToken)
        {
            return await _accountHandler
                .GetAvailableTimezones(cancellationToken);
        }
    }
}
