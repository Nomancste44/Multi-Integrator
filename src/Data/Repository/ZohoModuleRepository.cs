using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using ZohoToInsightIntegrator.Contract.DataContracts;
using ZohoToInsightIntegrator.Contract.Models;
using ZohoToInsightIntegrator.Contract.ZohoClientContracts;
using ZohoToInsightIntegrator.Data.DbContext;

namespace ZohoToInsightIntegrator.Data.Repository
{
    public class ZohoModuleRepository : Repository<ZohoModule>, IZohoModuleRepository
    {
        private readonly IntegratorDbContext _dbContext;
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public ZohoModuleRepository(
            IntegratorDbContext dbContext,
            ISqlConnectionFactory sqlConnectionFactory) : base(dbContext)
        {
            _dbContext = dbContext;
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task InsertFetchedZohoModules(string accountId, DataTable fetchedZohoModules)
        {
            var connection = _sqlConnectionFactory.GetIntegratorOpenConnection();
            var parameter = new DynamicParameters();
            parameter.Add("@pAccountId", accountId, DbType.String);
            parameter.Add("@pZohoModuleDataTable", fetchedZohoModules.
                AsTableValuedParameter("CustomDataType.ZMTableDataType"));

            const string sql = "procFetchZohoModule";

            await connection.ExecuteAsync(sql,
                parameter, commandType: CommandType.StoredProcedure);
        }

    }
}
