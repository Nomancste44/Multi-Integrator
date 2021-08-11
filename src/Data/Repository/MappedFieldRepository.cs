using ZohoToInsightIntegrator.Contract.DataContracts;
using ZohoToInsightIntegrator.Contract.Models;
using ZohoToInsightIntegrator.Data.DbContext;

namespace ZohoToInsightIntegrator.Data.Repository
{
    public class MappedFieldRepository : Repository<MappedField>, IMappedFieldRepository
    {
        private readonly IntegratorDbContext _dbContext;
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public MappedFieldRepository(
            IntegratorDbContext dbContext,
            ISqlConnectionFactory sqlConnectionFactory) : base(dbContext)
        {
            _dbContext = dbContext;
            _sqlConnectionFactory = sqlConnectionFactory;
        }
    }
}
