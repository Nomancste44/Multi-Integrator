using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using ZohoToInsightIntegrator.Contract.DataContracts;
using ZohoToInsightIntegrator.Contract.DataModels.Integrator.ModuleMapping;
using ZohoToInsightIntegrator.Contract.Models;
using ZohoToInsightIntegrator.Data.DbContext;

namespace ZohoToInsightIntegrator.Data.Repository
{
    public class MappedModuleRepository : Repository<MappedModule>, IMappedModuleRepository
    {
        private readonly IntegratorDbContext _dbContext;
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public MappedModuleRepository(
            IntegratorDbContext dbContext,
            ISqlConnectionFactory sqlConnectionFactory) : base(dbContext)
        {
            _dbContext = dbContext;
            _sqlConnectionFactory = sqlConnectionFactory;
        }
        public async Task<IEnumerable<MappedModule>> GetMappedModulesByAccountId(string accountId,
            CancellationToken cancellationToken)
        {
            return await _dbContext.MappedModules.Where(
                x=>x.AccountId == Guid.Parse(accountId))
                .ToListAsync(cancellationToken);
        }
        public async Task<IEnumerable<MappedModuleDto>> GetMappedModules(string accountId,
            CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetIntegratorOpenConnection();
            var sql = "SELECT " +
                      $"MM.MappedModuleId AS {nameof(MappedModuleDto.MappedModuleId)}, " +
                      $"ZM.AccountId AS {nameof(MappedModuleDto.AccountId)}, " +
                      $"ZM.ZohoModuleId AS {nameof(MappedModuleDto.ZohoModuleId)}, " +
                      $"ZM.ModuleName AS {nameof(MappedModuleDto.ZohoModuleName)}, " +
                      $"ZM.SequenceNumber AS {nameof(MappedModuleDto.SequenceNumber)}, " +
                      $"MM.InsightModuleId AS {nameof(MappedModuleDto.InsightModuleId)} " +
                      "FROM ZohoModules ZM " +
                      "LEFT OUTER JOIN  MappedModules MM " +
                      "ON MM.ZohoModuleId = ZM.ZohoModuleId " +
                      "WHERE ZM.AccountId = @AccountId" ;
            return await connection.QueryAsync<MappedModuleDto>(sql, new { AccountId = accountId });
        }

        public async Task InsertMappedModules(string accountId, DataTable mappedModules)
        {
            var connection = _sqlConnectionFactory.GetIntegratorOpenConnection();
            var parameter = new DynamicParameters();
            parameter.Add("@pAccountId", accountId, DbType.String);
            parameter.Add("@SaveUpdateDataTable", mappedModules.
                AsTableValuedParameter("CustomDataType.SaveUpdateTableDataType"));

            const string sql = "procSaveOrUpdateMappedModule";

            await connection.ExecuteAsync(sql,
                parameter, commandType: CommandType.StoredProcedure);

        }
    }
}
