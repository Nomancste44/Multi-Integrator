using System;
using System.Threading;
using System.Threading.Tasks;
using ZohoToInsightIntegrator.Contract.DataContracts;
using ZohoToInsightIntegrator.Contract.InsightClientContracts;
using ZohoToInsightIntegrator.Contract.ZohoClientContracts;

namespace ZohoToInsightIntegrator.Contract.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UsersRepository{get;}
        IUserRoleRepository UserRoleRepository { get; }
        IAccountRepository AccountRepository { get; }
        IZohoModuleRepository ZohoModuleRepository { get; }
        IMappedModuleRepository MappedModuleRepository { get; }
        IIntegratingCredentialRepository IntegratingCredentialRepository { get; }
        IAutoSchedulerSettingsRepository AutoSchedulerSettingsRepository { get; }
        IRefreshTokenRepository RefreshTokenRepository { get; }
        IInsightModuleRepository InsightModuleRepository { get; }
        IZohoModuleFieldRepository ZohoModuleFieldRepository { get; }
        IMappedFieldRepository MappedFieldRepository { get; }
        Task<int> CommitAsync(CancellationToken cancellationToken);
    }
}