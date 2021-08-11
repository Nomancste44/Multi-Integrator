using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZohoToInsightIntegrator.Contract.Contracts;
using ZohoToInsightIntegrator.Contract.Models;

namespace ZohoToInsightIntegrator.Contract.DataContracts
{
    public interface IUserRoleRepository : IRepository<Role>
    {
        public Task<IEnumerable<Role>> GetAllUserRoleInfo(CancellationToken cancellationToken);
    }
}
