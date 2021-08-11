using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZohoToInsightIntegrator.Contract.Models;

namespace ZohoToInsightIntegrator.Contract.BusinessContracts.Data.Account
{
    public interface IUserRoleHandler
    {
        Task<IEnumerable<Role>> GetAllUserRoleInfo(CancellationToken cancellationToken);
    }
}
