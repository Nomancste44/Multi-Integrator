using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZohoToInsightIntegrator.Contract.BusinessContracts.Insight;
using ZohoToInsightIntegrator.Contract.Contracts;
using ZohoToInsightIntegrator.Contract.DataModels.Insight.Module;
using ZohoToInsightIntegrator.Contract.InsightClientContracts;

namespace ZohoToInsightIntegrator.Business.Insight
{
    public class InsightHandler: IInsightHandler
    {
        private readonly IInsightResourceApi _insightResourceApi;
        private readonly IUnitOfWork _unitOfWork;
        public InsightHandler(
            IInsightResourceApi insightResourceApi,
            IUnitOfWork unitOfWork)
        {
            _insightResourceApi = insightResourceApi;
            _unitOfWork = unitOfWork;
        }
        public async Task<string> GetAllData(string accountId, string moduleName, CancellationToken cancellationToken)
        {
            return await _insightResourceApi
                .GetAllData(accountId, moduleName, cancellationToken);
        }
        public async Task<IEnumerable<InsightAvailableModuleDto>> GetAvailableInsightModules(CancellationToken cancellationToken)
        {
            return await _unitOfWork.InsightModuleRepository.GetAvailableInsightModules(cancellationToken);
        }
    }
}
