using ZohoToInsightIntegrator.Contract.DataContracts;
using ZohoToInsightIntegrator.Contract.InsightClientContracts;
using ZohoToInsightIntegrator.Contract.Models;
using ZohoToInsightIntegrator.Data.DbContext;

namespace ZohoToInsightIntegrator.Data.Repository
{
    public class InsightModuleFieldRepository : Repository<InsightModuleField>, IInsightModuleFieldRepository
    {
        private readonly IntegratorDbContext _dbContext;
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public InsightModuleFieldRepository(
            IntegratorDbContext dbContext,
            ISqlConnectionFactory sqlConnectionFactory) : base(dbContext)
        {
            _dbContext = dbContext;
            _sqlConnectionFactory = sqlConnectionFactory;
        }
    }
}
