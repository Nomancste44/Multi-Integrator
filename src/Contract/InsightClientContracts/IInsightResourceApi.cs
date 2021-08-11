using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ZohoToInsightIntegrator.Contract.InsightClientContracts
{
    public interface IInsightResourceApi
    {
        public Task<string> GetAllData(string accountId, string moduleName, CancellationToken cancellationToken);

    }
}
