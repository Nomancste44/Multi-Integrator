using System.Data;

namespace ZohoToInsightIntegrator.Contract.DataContracts
{
    public interface ISqlConnectionFactory
    {
        IDbConnection GetIntegratorOpenConnection();
        IDbConnection CreateIntegratorNewConnection();
        string GetConnectionString();
    }
}
