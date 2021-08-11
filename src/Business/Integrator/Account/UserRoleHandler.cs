using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZohoToInsightIntegrator.Contract.BusinessContracts.Data.Account;
using ZohoToInsightIntegrator.Contract.Contracts;
using ZohoToInsightIntegrator.Contract.Models;

namespace ZohoToInsightIntegrator.Business.Integrator.Account
{
    public class UserRoleHandler : IUserRoleHandler
    {

        private readonly IUnitOfWork _unitOfWork;

        public UserRoleHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Role>> GetAllUserRoleInfo(CancellationToken cancellationToken)
        {
            return await _unitOfWork.UserRoleRepository.GetAllUserRoleInfo(cancellationToken);
        }
    }
}
