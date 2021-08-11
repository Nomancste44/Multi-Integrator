using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using ZohoToInsightIntegrator.Contract.DataModels.Integrator.Authentication;
using ZohoToInsightIntegrator.Contract.DataModels.Integrator.User;
using ZohoToInsightIntegrator.Contract.Models;

namespace ZohoToInsightIntegrator.Contract.BusinessContracts.Data.Account
{
    public interface IUserHandler
    {
        Task<AuthenticationResult> AuthenticateUserAsync(AuthenticateUserCommand command,
            CancellationToken cancellationToken);
        Task AddUserAsync(AddUserCommand user, CancellationToken cancellationToken);

        Task UpdateUser(UpdateUserCommand updatedUser, CancellationToken cancellationToken);
        Task ResetPasswordAsync(ResetPasswordCommand reset, CancellationToken cancellationToken);
        Task<IEnumerable<Models.User>> GetAllUserAsync(CancellationToken cancellationToken);
        Task<User> GetUserByIdAsync(string id, CancellationToken cancellationToken);

        Task SendPasswordAsync(SendPasswordCommand command, CancellationToken cancellationToken);

        Task<IEnumerable<UserDto>> GetAllUsers(CancellationToken cancellationToken);
        Task StoreRefreshToken(StoreRefreshTokenCommand command, CancellationToken cancellationToken);
        Task<AuthenticationResult> AuthenticateRefreshTokenAsync(RefreshTokenCommand command,
            CancellationToken cancellationToken);

        Task RevokeToken(RefreshTokenCommand command, CancellationToken cancellationToken);
    }
}
