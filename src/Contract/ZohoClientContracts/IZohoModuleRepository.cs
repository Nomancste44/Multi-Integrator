using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using ZohoToInsightIntegrator.Contract.Contracts;
using ZohoToInsightIntegrator.Contract.Models;

namespace ZohoToInsightIntegrator.Contract.ZohoClientContracts
{
    public interface IZohoModuleRepository : IRepository<ZohoModule>
    {
        Task InsertFetchedZohoModules(string accountId, DataTable fetchedZohoModules);

    }
}
