using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using ZohoToInsightIntegrator.Contract.DataContracts;
using ZohoToInsightIntegrator.Contract.Utility;

namespace ZohoToInsightIntegrator.Data.DbContext
{
    public class SqlConnectionFactory : ISqlConnectionFactory, IDisposable
    {
        private IDbConnection _integratorConnection;

        private string _connectionString;
        public SqlConnectionFactory(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString(Common.ConnectionName);
        }

        public IDbConnection GetIntegratorOpenConnection()
        {
            if (this._integratorConnection != null && this._integratorConnection.State == ConnectionState.Open) 
                return this._integratorConnection;
            this._integratorConnection = new SqlConnection(_connectionString);
            this._integratorConnection.Open();

            return this._integratorConnection;
        }

        public IDbConnection CreateIntegratorNewConnection()
        {
            var connection = new SqlConnection(_connectionString);
            connection.Open();

            return connection;
        }
        public string GetConnectionString()=> _connectionString;

        public void Dispose()
        {
            if (this._integratorConnection != null && this._integratorConnection.State == ConnectionState.Open)
            {
                this._integratorConnection.Dispose();
            }
        }
    }
}
