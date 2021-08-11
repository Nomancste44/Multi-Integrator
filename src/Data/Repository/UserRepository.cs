using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ZohoToInsightIntegrator.Contract.DataContracts;
using ZohoToInsightIntegrator.Contract.DataModels.Integrator.Authentication;
using ZohoToInsightIntegrator.Contract.DataModels.Integrator.User;
using ZohoToInsightIntegrator.Contract.Models;
using ZohoToInsightIntegrator.Data.DbContext;

namespace ZohoToInsightIntegrator.Data.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly IntegratorDbContext _dbContext;
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public UserRepository(
            IntegratorDbContext dbContext,
            ISqlConnectionFactory sqlConnectionFactory) : base(dbContext)
        {
            _dbContext = dbContext;
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<AuthenticatedUserDto> AuthenticateUserAsync(string email, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetIntegratorOpenConnection();
            var sql = $"SELECT U.Email AS {nameof(AuthenticatedUserDto.Email)}," +
                      $" U.IsActive AS {nameof(AuthenticatedUserDto.IsActive)}, " +
                      $"U.Password AS {nameof(AuthenticatedUserDto.Password)}, " +
                      $"R.RoleName AS {nameof(AuthenticatedUserDto.RoleName)}, " +
                      $"R.RoleLabel AS {nameof(AuthenticatedUserDto.RoleLabel)}, " +
                      $"A.AccountId AS {nameof(AuthenticatedUserDto.AccountId)}, " +
                      $"A.IsActive AS {nameof(AuthenticatedUserDto.IsAccountActive)}, " +
                      $"A.AccountName AS {nameof(AuthenticatedUserDto.AccountName)} " +
                      $"FROM Users U " +
                      $"INNER JOIN Roles R ON U.RoleId = R.RoleId AND U.Email = @Email " +
                      $"INNER JOIN Accounts A ON U.AccountId = A.AccountId";
                      

            return await connection
                .QuerySingleAsync<AuthenticatedUserDto>(sql, new { Email = email });
        }


        public async Task<IEnumerable<UserDto>> GetAllUsers(CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetIntegratorOpenConnection();
            var sql = @"SELECT 
            U.* ,
            a.AccountName,
            r.RoleName
            FROM Users AS U
            INNER JOIN Accounts AS A ON U.AccountId = A.AccountId
            INNER JOIN Roles AS R ON R.RoleId= U.RoleId";
            return await connection.QueryAsync<UserDto>(sql);


        }

        public async Task<User> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
        {
            var user = await SingleOrDefaultAsync(u => u.IsActive && u.Email == email, cancellationToken);
            await _dbContext.Entry(user).Collection(u => u.RefreshTokens).LoadAsync(cancellationToken);
            return user;
        }

        public async Task<User> GetUserByRefreshTokenAsync(string token, CancellationToken cancellationToken)
        {
            var user = await SingleOrDefaultAsync(u => u.IsActive && u.RefreshTokens.Any(t => t.Token == token), cancellationToken);
            await _dbContext.Entry(user).Reference(u => u.Role).LoadAsync(cancellationToken);
            await _dbContext.Entry(user).Reference(u => u.Account).LoadAsync(cancellationToken);
            await _dbContext.Entry(user).Collection(u => u.RefreshTokens).LoadAsync(cancellationToken);
            return user;
        }
    }

}
