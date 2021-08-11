using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using ZohoToInsightIntegrator.Contract.DataContracts;
using ZohoToInsightIntegrator.Contract.DataModels.Insight.Module;
using ZohoToInsightIntegrator.Contract.InsightClientContracts;
using ZohoToInsightIntegrator.Contract.Models;
using ZohoToInsightIntegrator.Data.DbContext;

namespace ZohoToInsightIntegrator.Data.Repository
{
    public class InsightModuleRepository : Repository<InsightModule>, IInsightModuleRepository
    {
        private readonly IntegratorDbContext _dbContext;
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public InsightModuleRepository(
            IntegratorDbContext dbContext,
            ISqlConnectionFactory sqlConnectionFactory) : base(dbContext)
        {
            _dbContext = dbContext;
            _sqlConnectionFactory = sqlConnectionFactory;
        }
        public async Task<IEnumerable<InsightAvailableModuleDto>> GetAvailableInsightModules(CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetIntegratorOpenConnection();
            var sql = "SELECT " +
                      $"InsightModuleId AS {nameof(InsightAvailableModuleDto.InsightModuleId)}, " +
                      $"ModuleName AS {nameof(InsightAvailableModuleDto.ModuleName)} " +
                      "FROM InsightModules";

            return await connection.QueryAsync<InsightAvailableModuleDto>(sql);
        }
    }
}
