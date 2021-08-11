using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using ZohoToInsightIntegrator.Business.Zoho.Rules;
using ZohoToInsightIntegrator.Contract.BusinessContracts.Zoho;
using ZohoToInsightIntegrator.Contract.Contracts;
using ZohoToInsightIntegrator.Contract.DataModels.Integrator.ModuleMapping;
using ZohoToInsightIntegrator.Contract.DataModels.Zoho.Field;
using ZohoToInsightIntegrator.Contract.DataModels.Zoho.SetConnection;
using ZohoToInsightIntegrator.Contract.Models;
using ZohoToInsightIntegrator.Contract.ZohoClientContracts;

namespace ZohoToInsightIntegrator.Business.Zoho
{
    public class ZohoHandler : BusinessEntity, IZohoHandler
    {
        private readonly IZohoConnectionApi _zohoConnectionApi;
        private readonly IZohoResourceApi _zohoResourceApi;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUniquenessChecker _uniquenessChecker;

        public ZohoHandler(
            IZohoConnectionApi zohoConnectionApi,
            IZohoResourceApi zohoResourceApi,
            IUnitOfWork unitOfWork,
            IUniquenessChecker uniquenessChecker
            )
        {
            _zohoConnectionApi = zohoConnectionApi;
            _zohoResourceApi = zohoResourceApi;
            _unitOfWork = unitOfWork;
            _uniquenessChecker = uniquenessChecker;
        }

        public async Task<bool> SetZohoConnectionAsync(SetZohoConnectionCommand command, CancellationToken cancellationToken)
        {
            var connectionSetup = false;
            var account = await _unitOfWork
                .AccountRepository
                .GetAccountWithIntegrationCredential(command.AccountId, cancellationToken);
            var clientAccess = await _zohoConnectionApi
                .SetZohoClientAccessAsync(account.IntegratingCredential.ZohoClientId,
                    account.IntegratingCredential.ZohoClientSecret,
                    command.code, cancellationToken);
            if (string.IsNullOrEmpty(clientAccess.AccessToken)) return connectionSetup;

            account.IsConnectionActive = true;
            account.IntegratingCredential.ZohoAccessToken = clientAccess.AccessToken;
            account.IntegratingCredential.ZohoRefreshToken = clientAccess.RefreshToken;
            account.IntegratingCredential.ZohoApiDomain = clientAccess.ApiDomain;
            account.IntegratingCredential.ZohoTokenType = clientAccess.TokenType;
            await _unitOfWork.CommitAsync(cancellationToken);
            connectionSetup = true;

            return connectionSetup;
        }

        public async Task<IEnumerable<MappedModuleDto>> GetZohoAvailableModulesAsync(string accountId, CancellationToken cancellationToken)
        {
            this.CheckRule(new AccountMustHaveActiveConnectionRule(_uniquenessChecker,accountId));
            
            var fetchedZohoModules = await _zohoResourceApi.GetZohoAvailableModulesAsync(accountId, cancellationToken);

            var aDatable = new DataTable();
            aDatable.Columns.AddRange(new DataColumn[]
            {
                new DataColumn { ColumnName = "ApiName", DataType = typeof(string)},
                new DataColumn { ColumnName = "ModuleName", DataType = typeof(string)},
                new DataColumn { ColumnName = "SequenceNumber", DataType = typeof(int)},

            });
            fetchedZohoModules.ForEach(x =>
            {
                aDatable.Rows.Add(x.ApiName, x.PluralLabel, x.SequenceNumber);

            });

            await _unitOfWork.ZohoModuleRepository.InsertFetchedZohoModules(accountId, aDatable);
            return await _unitOfWork.MappedModuleRepository.GetMappedModules(accountId, cancellationToken);
        }

        public async Task<IEnumerable<Section>> GetZohoModuleFieldsAsync(string accountId,
            string zohoModuleId, string moduleApiName, CancellationToken cancellationToken)
        {
            var responses = await _zohoResourceApi.GetZohoModuleFieldsAsync(accountId,
                moduleApiName, cancellationToken);
            var res = responses.SelectMany(l => l.Sections
                .SelectMany(s => s.Fields.Select(f => new ZohoModuleField()
                {
                    AccountId = Guid.Parse(accountId),
                    IntegratorName = "ZOHOCRM",
                    ZohoModuleId = Guid.Parse(zohoModuleId),
                    SectionName = s.SectionName,
                    SectionOrder = s.SectionOrder,
                    FieldName = f.FieldName,
                    FieldApiName = f.FieldApiName,
                    FieldType = f.FieldType,
                    LockUpModuleApiName = f.LookUp.Module,
                    PickListValues = JsonSerializer.Serialize(f.PickListValues),
                    IsMandatory = f.IsMandatory,
                    ReadOnly = f.ReadOnly,
                    Visible = f.Visible,
                    FieldMaxLength = f.FieldMaxLength,
                    FieldSequenceNumber = f.FieldSequenceNumber,
                    Status = true
                }))).ToList();

            var aDatable = new DataTable();
            aDatable.Columns.AddRange(new DataColumn[]
            {
                new DataColumn { ColumnName = "SectionName", DataType = typeof(string)},
                new DataColumn { ColumnName = "SectionOrder", DataType = typeof(int)},
                new DataColumn { ColumnName = "FieldName", DataType = typeof(string)},
                new DataColumn { ColumnName = "FieldApiName", DataType = typeof(string)},
                new DataColumn { ColumnName = "FieldType", DataType = typeof(string)},
                new DataColumn { ColumnName = "LockUpModuleApiName", DataType = typeof(string)},
                new DataColumn { ColumnName = "PickListValues", DataType = typeof(string)},
                new DataColumn { ColumnName = "IsMandatory", DataType = typeof(bool)},
                new DataColumn { ColumnName = "ReadOnly", DataType = typeof(bool)},
                new DataColumn { ColumnName = "Visible", DataType = typeof(bool)},
                new DataColumn { ColumnName = "FieldMaxLength", DataType = typeof(int)},
                new DataColumn { ColumnName = "FieldSequenceNumber", DataType = typeof(int)},
                new DataColumn { ColumnName = "Status", DataType = typeof(bool)},
            });

            res.ForEach(x =>
            {
                aDatable.Rows.Add(x.SectionName, x.SectionOrder, x.FieldName,
                    x.FieldApiName, x.FieldType, x.LockUpModuleApiName, x.PickListValues, x.IsMandatory, x.ReadOnly,
                    x.Visible, x.FieldMaxLength, x.FieldSequenceNumber, x.Status);
            });
            await _unitOfWork.ZohoModuleFieldRepository.InsertZohoFields(accountId, zohoModuleId, aDatable);
            return responses;
        }

    }
}
