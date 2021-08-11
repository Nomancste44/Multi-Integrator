using System.Data;

namespace ZohoToInsightIntegrator.Contract.Contracts
{
    public interface IUniquenessChecker
    {
        bool IsUniqueValue<T>(string tableName, string columnName, 
            T value);

        bool IsConnectionActive(string accountId);
        bool IsModuleMapExist(string accountId, DataTable mappedModules);
        bool IsMappedModuleIdExist(string accountId, DataTable mappedModules);
    }
}
