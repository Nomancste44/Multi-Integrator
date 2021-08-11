using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ZohoToInsightIntegrator.Contract.Contracts;

using ZohoToInsightIntegrator.Contract.DataModels.Integrator.Authentication;

using ZohoToInsightIntegrator.Contract.DataModels.Integrator.User;
using ZohoToInsightIntegrator.Contract.Models;

namespace ZohoToInsightIntegrator.Contract.DataContracts
{
    public interface IUserRepository : IRepository<User>
    {

        Task<AuthenticatedUserDto> AuthenticateUserAsync(string email, CancellationToken cancellationToken);

        Task<IEnumerable<UserDto>> GetAllUsers(CancellationToken cancellationToken);

        Task<User> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
        Task<User> GetUserByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken);
    }
}
