using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ZohoToInsightIntegrator.Business.Zoho.Rules;
using ZohoToInsightIntegrator.Contract.BusinessContracts.Data.Mapping;
using ZohoToInsightIntegrator.Contract.Contracts;
using ZohoToInsightIntegrator.Contract.DataModels.Integrator.ModuleMapping;
using ZohoToInsightIntegrator.Contract.Models;

namespace ZohoToInsightIntegrator.Business.Integrator.Mapping
{
    public class ModuleMappingHandler : BusinessEntity, IModuleMappingHandler
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUniquenessChecker _uniquenessChecker;

        public ModuleMappingHandler(IUnitOfWork unitOfWork,
            IUniquenessChecker uniquenessChecker)
        {
            _unitOfWork = unitOfWork;
            _uniquenessChecker = uniquenessChecker;
        }
        public async Task SaveMappedModulesAsync(string accountId,
            List<ModuleMappingCommand> moduleMappingCommands,
            CancellationToken cancellationToken)
        {
            var aDatable = new DataTable();
            aDatable.Columns.AddRange(new DataColumn[]
            {
                new DataColumn { ColumnName = "MappedId", DataType = typeof(string)},
                new DataColumn { ColumnName = "ZohoId", DataType = typeof(string)},
                new DataColumn { ColumnName = "InsightId", DataType = typeof(string)},

            });
            moduleMappingCommands.ForEach(x =>
            {
                aDatable.Rows.Add(x.MappedModuleId, x.ZohoModuleId, x.InsightModuleId);
            });

            this.CheckRule(new AccountIdZohoModuleIdCombinationExistRule(_uniquenessChecker, accountId, aDatable));
            this.CheckRule(new MappedModuleIdExistRule(_uniquenessChecker, accountId, aDatable));

            await _unitOfWork.MappedModuleRepository.InsertMappedModules(accountId, aDatable);
        }

        public async Task<IEnumerable<MappedModuleDto>> GetMappedModules(string accountId,
            CancellationToken cancellationToken)
        {
            return await _unitOfWork.MappedModuleRepository.GetMappedModules(accountId, cancellationToken);
        }
    }
}
