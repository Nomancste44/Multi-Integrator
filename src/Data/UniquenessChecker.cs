using System;
using System.Data;
using Dapper;
using ZohoToInsightIntegrator.Contract.Contracts;
using ZohoToInsightIntegrator.Contract.DataContracts;

namespace ZohoToInsightIntegrator.Data
{
    public class UniquenessChecker : IUniquenessChecker
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public UniquenessChecker(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public bool IsUniqueValue<T>(string tableName, string columnName, T value)
        {
            var connection = _sqlConnectionFactory.GetIntegratorOpenConnection();
            var sql = "SELECT " +
                               "COUNT(*) " +
                               $"FROM {tableName} " +
                               $"WHERE {tableName}.{columnName} = @value";
            var result = connection.QuerySingle<int>(sql, new { value });
            return result == 0;
        }

        public bool IsConnectionActive(string accountId)
        {
            var connection = _sqlConnectionFactory.GetIntegratorOpenConnection();
            var sql = "SELECT COUNT(*) FROM Accounts " +
                      $"WHERE AccountId = @AccountId AND IsConnectionActive = 1";
            var result = connection.QuerySingle<int>(sql, new { AccountId = Guid.Parse(accountId) });
            return result == 1;
        }
        public bool IsModuleMapExist(string accountId, DataTable mappedModules)
        {
            var connection = _sqlConnectionFactory.GetIntegratorOpenConnection();
            var sql = "SELECT COUNT(*) FROM MappedModules MM " +
                      $"INNER JOIN @SaveUpdateDataTable SU " +
                      "ON MM.ZohoModuleId = SU.ZohoId " +
                      $"WHERE MM.AccountId = @AccountId " +
                      "AND SU.MappedId  is null";
            var result = connection.QuerySingle<int>(sql, 
                new
                {
                    AccountId = Guid.Parse(accountId),
                    SaveUpdateDataTable = mappedModules.
                        AsTableValuedParameter("CustomDataType.SaveUpdateTableDataType")
                });
            return result > 0;
        }

        public bool IsMappedModuleIdExist(string accountId, DataTable mappedModules)
        {
            var connection = _sqlConnectionFactory.GetIntegratorOpenConnection();
            var sql = "SELECT COUNT(*) FROM MappedModules MM " +
                      $"INNER JOIN @SaveUpdateDataTable SU " +
                      "ON MM.MappedModuleId = SU.MappedId " +
                      $"WHERE MM.AccountId = @AccountId " +
                      "AND SU.MappedId  is not null";
            var result = connection.QuerySingle<int>(sql,
                new
                {
                    AccountId = Guid.Parse(accountId),
                    SaveUpdateDataTable = mappedModules.
                        AsTableValuedParameter("CustomDataType.SaveUpdateTableDataType")
                });
            return result > 0;
        }
    }
}
