using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using ZohoToInsightIntegrator.Contract.BusinessContracts.Data.Account;
using ZohoToInsightIntegrator.Contract.Contracts;
using ZohoToInsightIntegrator.Contract.DataModels.Integrator.Authentication;
using ZohoToInsightIntegrator.Contract.DataModels.Integrator.User;
using ZohoToInsightIntegrator.Contract.Models;
using ZohoToInsightIntegrator.Contract.Utility;

namespace ZohoToInsightIntegrator.Business.Integrator.Account
{
    public class UserHandler : IUserHandler
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly EmailService _emailService;
        private readonly IExecutionContextAccessor _contextAccessor;

        public UserHandler(
            IUnitOfWork unitOfWork,
            EmailService emailService,
            IExecutionContextAccessor contextAccessor)
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;
            _contextAccessor = contextAccessor;
        }
        private void AddClaims(AuthenticatedUserDto user)
        {
            user.Claims.Add(new Claim(PolicyClaimTypes.Email, user.Email));
            user.Claims.Add(new Claim(PolicyClaimTypes.RoleLabel, user.RoleLabel));
            user.Claims.Add(new Claim(ClaimTypes.Role, user.RoleName));

            if (user.RoleName == PolicyClaimTypes.Admin || user.RoleName == PolicyClaimTypes.SuperAdmin)
                user.Claims.Add(new Claim(PolicyClaimTypes.ViewOnly, PolicyClaimTypes.ViewOnly));

            if (user.RoleName == PolicyClaimTypes.SuperAdmin)
                user.Claims.Add(new Claim(PolicyClaimTypes.Admin, PolicyClaimTypes.Admin));
        }
        public async Task<AuthenticationResult> AuthenticateUserAsync(
            AuthenticateUserCommand command, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork
                .UsersRepository
                .AuthenticateUserAsync(command.Email, cancellationToken);

            if (user == null)
            {
                return new AuthenticationResult("Incorrect login or password");
            }

            if (!user.IsActive)
            {
                return new AuthenticationResult("User is not active");
            }
            if (!user.IsAccountActive)
            {
                return new AuthenticationResult("Account is not active");
            }

            if (!PasswordManager.VerifyHashedPassword(user.Password, command.Password))
            {
                return new AuthenticationResult("Incorrect login or password");
            }
            AddClaims(user);
            return new AuthenticationResult(user);
        }



        public async Task<IEnumerable<User>> GetAllUserAsync(
            CancellationToken cancellationToken)
        {
            return await _unitOfWork.UsersRepository.GetAllAsync(cancellationToken);
        }

        public async Task<User> GetUserByIdAsync(string id, CancellationToken cancellationToken)
        {
            return await _unitOfWork
                .UsersRepository
                .GetAsync(Guid.Parse(id), cancellationToken);
        }

        public async Task SendPasswordAsync(SendPasswordCommand command, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UsersRepository.SingleOrDefaultAsync(u => u.IsActive && u.Email == command.Email,
                cancellationToken);
            var newPassword = PasswordManager.GeneratePassword(16, 6);
            user.Password = PasswordManager.HashPassword(newPassword);
            await _unitOfWork.CommitAsync(cancellationToken);
            await _emailService.SendPasswordAsync(command.Email, newPassword);
        }

        public async Task AddUserAsync(AddUserCommand user, CancellationToken cancellationToken)
        {
            await _unitOfWork.UsersRepository.AddAsync(new User
            {
                Email = user.Email,
                RoleId = Guid.Parse(user.RoleId),
                Password = PasswordManager.HashPassword(user.Password),
                AccountId = Guid.Parse(user.AccountId),
                IsActive = true
            }, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

        }

        public async Task<IEnumerable<UserDto>> GetAllUsers(CancellationToken cancellationToken)
        {
            return await _unitOfWork.UsersRepository.GetAllUsers(cancellationToken);
        }

        public async Task StoreRefreshToken(StoreRefreshTokenCommand command,
            CancellationToken cancellationToken)
        {
            var user = await _unitOfWork
                .UsersRepository
                .GetUserByEmailAsync(command.Email, cancellationToken);

            user.RefreshTokens.Add(command.RefreshToken);

            if (!string.IsNullOrEmpty(command.OldRefreshToken))
            {
                var refreshToken = user.RefreshTokens.Single(rt => rt.Token == command.OldRefreshToken);
                refreshToken.Revoked = Common.UtcDateTime;
                refreshToken.RevokedByIp = _contextAccessor.IpAddress;
                refreshToken.ReplacedByToken = command.RefreshToken.Token;
            }

            await _unitOfWork.CommitAsync(cancellationToken);
        }

        public async Task<AuthenticationResult> AuthenticateRefreshTokenAsync(
            RefreshTokenCommand command, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UsersRepository.GetUserByRefreshTokenAsync(command.RefreshToken,
                cancellationToken);
            if (user == null) return new AuthenticationResult("Invalid Token");
            if (!user.Account.IsActive) return new AuthenticationResult("Account is active");
            if (!user.IsActive) return new AuthenticationResult("User is not active");
            
            var refreshToken = user.RefreshTokens.Single(x => x.Token == command.RefreshToken);
            if (!refreshToken.IsActive) return new AuthenticationResult("Invalid Token");

            var authenticateUserDto = new AuthenticatedUserDto
            {
                Email = user.Email,
                IsActive = user.IsActive,
                RoleName = user.Role.RoleName,
                RoleLabel = user.Role.RoleLabel,
                AccountId = user.Account.AccountId,
                AccountName = user.Account.AccountName,
            };
            AddClaims(authenticateUserDto);
            return new AuthenticationResult(authenticateUserDto);
        }

        public async Task RevokeToken(RefreshTokenCommand command, CancellationToken cancellationToken)
        {
            var refreshToken = await _unitOfWork
                .RefreshTokenRepository
                .SingleOrDefaultAsync(rt => rt.Token == command.RefreshToken, cancellationToken);
            if (refreshToken.IsActive)
            {
                refreshToken.Revoked = Common.UtcDateTime;
                refreshToken.RevokedByIp = _contextAccessor.IpAddress;
                await _unitOfWork.CommitAsync(cancellationToken);
            }
        }

        public async Task ResetPasswordAsync(ResetPasswordCommand reset, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UsersRepository.GetAsync(Guid.Parse(reset.UserId), cancellationToken);
            user.Password = PasswordManager.HashPassword(reset.Password);
            await _unitOfWork.CommitAsync(cancellationToken);
        }


        public async Task UpdateUser(UpdateUserCommand updatedUser, CancellationToken cancellationToken)
        {
            var userInfo = await _unitOfWork.UsersRepository.GetAsync(Guid.Parse(updatedUser.UserId), cancellationToken);

            userInfo.RoleId = Guid.Parse(updatedUser.RoleId);
            userInfo.AccountId = Guid.Parse(updatedUser.AccountId);
            userInfo.IsActive = updatedUser.IsActive;

            await _unitOfWork.CommitAsync(cancellationToken);
        }


    }
}
