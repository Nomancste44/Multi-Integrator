using Dapper;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ZohoToInsightIntegrator.Contract.DataContracts;
using ZohoToInsightIntegrator.Contract.Models;
using ZohoToInsightIntegrator.Data.DbContext;

namespace ZohoToInsightIntegrator.Data.Repository
{
    public class UserRoleRepository : Repository<Role>, IUserRoleRepository
    {
        private readonly IntegratorDbContext _dbContext;
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public UserRoleRepository(
            IntegratorDbContext dbContext,
            ISqlConnectionFactory sqlConnectionFactory) : base(dbContext)
        {
            _dbContext = dbContext;
            _sqlConnectionFactory = sqlConnectionFactory;
        }
        public async Task<IEnumerable<Role>> GetAllUserRoleInfo(CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetIntegratorOpenConnection();
            const string sql = @"SELECT * FROM Roles WHERE RoleName != 'SuperAdmin'";
            return await connection.QueryAsync<Role>(sql);
        }
    }
}
