using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZohoToInsightIntegrator.Contract.DataContracts;
using ZohoToInsightIntegrator.Contract.DataModels.Integrator.AutoSchedulerSettings;
using ZohoToInsightIntegrator.Contract.Models;
using ZohoToInsightIntegrator.Data.DbContext;

namespace ZohoToInsightIntegrator.Data.Repository
{
    public class AutoSchedulerSettingsRepository : Repository<AutoSchedulerSettings>,IAutoSchedulerSettingsRepository
    {
        private readonly IntegratorDbContext _dbContext;
        private readonly ISqlConnectionFactory _sqlConnectionFactory;


        public AutoSchedulerSettingsRepository(
            IntegratorDbContext dbContext,
            ISqlConnectionFactory sqlConnectionFactory)
            : base(dbContext)
        {
            _dbContext = dbContext;
            _sqlConnectionFactory = sqlConnectionFactory;
        }
        public async Task<IEnumerable<AutoSchdulerTimeDto>> GetAutoSchdulerTimes(CancellationToken cancellationToken)
        {            
            var sql = @"SELECT * FROM AutoShedulerTime ORDER BY OrderNumber ASC";
            var connection = _sqlConnectionFactory.GetIntegratorOpenConnection();
            return await connection.QueryAsync<AutoSchdulerTimeDto>(sql);
        }

        public async Task<AutoSchedulerSettingsDto> GetAutoSchedulerSettings(string accountId , CancellationToken cancellationToken)
        {

            var connection = _sqlConnectionFactory.GetIntegratorOpenConnection();

            var autoSchedulerSettings = await connection.QueryFirstAsync<AutoSchedulerSettingsDto>("GetAutoShedularSettings", new { pAccountId = Guid.Parse(accountId)},
                commandType: CommandType.StoredProcedure);

            return autoSchedulerSettings;

        }

        public async Task<IEnumerable<ModuleSyncModeDto>> GetModuleSyncModes(string autoSchedulerSettingsId , CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetIntegratorOpenConnection();

            var autoSchedulerSettings = await connection.QueryAsync<ModuleSyncModeDto>("GetModuleSyncMode", new { pAutoSchedulerSettingId = autoSchedulerSettingsId },
                commandType: CommandType.StoredProcedure);

            return autoSchedulerSettings;
        }

        public Task SaveAutoSchedulerSettings(AutoSchedularSettingsCommands autoSchedulerSettings, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
