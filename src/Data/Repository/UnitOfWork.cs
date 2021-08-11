using System.Threading;
using System.Threading.Tasks;
using ZohoToInsightIntegrator.Contract.Contracts;
using ZohoToInsightIntegrator.Contract.DataContracts;
using ZohoToInsightIntegrator.Contract.InsightClientContracts;
using ZohoToInsightIntegrator.Contract.ZohoClientContracts;
using ZohoToInsightIntegrator.Data.DbContext;

namespace ZohoToInsightIntegrator.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IntegratorDbContext _context;

        public UnitOfWork(
            IntegratorDbContext context,
            ISqlConnectionFactory sqlConnectionFactory)
        {
            _context = context;
            UsersRepository = new UserRepository(_context, sqlConnectionFactory);
            AccountRepository = new AccountRepository(_context, sqlConnectionFactory);
            ZohoModuleRepository = new ZohoModuleRepository(_context, sqlConnectionFactory);
            MappedModuleRepository = new MappedModuleRepository(_context, sqlConnectionFactory);
            UserRoleRepository = new UserRoleRepository(_context, sqlConnectionFactory);
            IntegratingCredentialRepository = new IntegratingCredentialRepository(_context, sqlConnectionFactory);
            AutoSchedulerSettingsRepository = new AutoSchedulerSettingsRepository(_context, sqlConnectionFactory);
            RefreshTokenRepository = new RefreshTokenRepository(_context, sqlConnectionFactory);
            InsightModuleRepository = new InsightModuleRepository(_context, sqlConnectionFactory);
            ZohoModuleFieldRepository = new ZohoModuleFieldRepository(_context, sqlConnectionFactory);
            MappedFieldRepository = new MappedFieldRepository(_context, sqlConnectionFactory);
        }

        public IUserRepository UsersRepository { get; }
        public IUserRoleRepository UserRoleRepository { get; }
        public IAccountRepository AccountRepository { get; }
        public IZohoModuleRepository ZohoModuleRepository { get; }
        public IMappedModuleRepository MappedModuleRepository { get; }
        public IIntegratingCredentialRepository IntegratingCredentialRepository { get; }
        public IAutoSchedulerSettingsRepository AutoSchedulerSettingsRepository { get; }
        public IRefreshTokenRepository RefreshTokenRepository { get; }
        public IInsightModuleRepository InsightModuleRepository { get; }
        public IZohoModuleFieldRepository ZohoModuleFieldRepository { get; }
        public IMappedFieldRepository MappedFieldRepository { get; }

        public async Task<int> CommitAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}