using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ZohoToInsightIntegrator.Contract.BusinessContracts.Data.Mapping;
using ZohoToInsightIntegrator.Contract.Contracts;
using ZohoToInsightIntegrator.Contract.DataModels.Integrator.FieldMapping;
using ZohoToInsightIntegrator.Contract.ZohoClientContracts;

namespace ZohoToInsightIntegrator.Business.Integrator.Mapping
{
    public class FieldMappingHandler : IFieldMappingHandler
    {
        private readonly IZohoResourceApi _zohoResourceApi;
        private readonly IUnitOfWork _unitOfWork;

        public FieldMappingHandler(IZohoResourceApi zohoResourceApi,
            IUnitOfWork unitOfWork)
        {
            _zohoResourceApi = zohoResourceApi;
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<MappedModuleNameDto>> GetMappedModuleName(string accountId,
            CancellationToken cancellationToken)
        {
            return await _unitOfWork.ZohoModuleFieldRepository.GetMappedModuleName(accountId, cancellationToken);
        }
    }
}
