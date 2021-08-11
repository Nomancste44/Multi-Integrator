using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using ZohoToInsightIntegrator.Contract.DataContracts;
using ZohoToInsightIntegrator.Contract.DataModels.Integrator.FieldMapping;
using ZohoToInsightIntegrator.Contract.Models;
using ZohoToInsightIntegrator.Contract.ZohoClientContracts;
using ZohoToInsightIntegrator.Data.DbContext;

namespace ZohoToInsightIntegrator.Data.Repository
{
    public class ZohoModuleFieldRepository : Repository<ZohoModuleField>, IZohoModuleFieldRepository
    {
        private readonly IntegratorDbContext _dbContext;
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public ZohoModuleFieldRepository(
            IntegratorDbContext dbContext,
            ISqlConnectionFactory sqlConnectionFactory) : base(dbContext)
        {
            _dbContext = dbContext;
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<IEnumerable<MappedModuleNameDto>> GetMappedModuleName(string accountId,
            CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetIntegratorOpenConnection();
            var sql = "SELECT " +
                      $"MM.MappedModuleId AS {nameof(MappedModuleNameDto.MappedModuleId)}, " +
                      $"MM.AccountId AS {nameof(MappedModuleNameDto.AccountId)}, " +
                      $"MM.ZohoModuleId AS {nameof(MappedModuleNameDto.ZohoModuleId)}, " +
                      $"MM.InsightModuleId AS {nameof(MappedModuleNameDto.InsightModuleId)}, " +
                      $"CONCAT(ZM.ModuleName, ' VS ', IM.ModuleName) AS {nameof(MappedModuleNameDto.MappedModuleName)} " +
                      "FROM MappedModules MM " +
                      "INNER JOIN InsightModules IM " +
                      "ON MM.InsightModuleId = IM.InsightModuleId " +
                      "INNER JOIN ZohoModules ZM " +
                      "ON MM.ZohoModuleId = ZM.ZohoModuleId " +
                      "Where MM.AccountId = @AccountId";
            return await connection.QueryAsync<MappedModuleNameDto>(sql, new { AccountId = accountId });
        }


        public async Task InsertZohoFields(string accountId,string zohoModuleId, DataTable zohoFields)
        {
            var connection = _sqlConnectionFactory.GetIntegratorOpenConnection();
            var parameter = new DynamicParameters();
            parameter.Add("@pAccountId", accountId, DbType.String);
            parameter.Add("@pZohoModuleId", zohoModuleId, DbType.String);
            parameter.Add("@ZMFTable", zohoFields.
                AsTableValuedParameter("CustomDataType.ZMFTableDataType"));

            const string sql = "procFetchZohoModuleFields";

            await connection.ExecuteAsync(sql,
                parameter, commandType: CommandType.StoredProcedure);

        }
    }
}
