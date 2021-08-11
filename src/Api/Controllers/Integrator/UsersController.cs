using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using ZohoToInsightIntegrator.Api.Configuration.Authentication;
using ZohoToInsightIntegrator.Contract.BusinessContracts.Data.Account;
using ZohoToInsightIntegrator.Contract.Contracts;
using ZohoToInsightIntegrator.Contract.DataModels.Integrator.Authentication;
using ZohoToInsightIntegrator.Contract.DataModels.Integrator.User;
using ZohoToInsightIntegrator.Contract.Models;
using ZohoToInsightIntegrator.Contract.Utility;

namespace ZohoToInsightIntegrator.Api.Controllers.Integrator
{
    [Route("api/[controller]")]
    [ApiController]
    [HasPermission(PolicyClaimTypes.Admin)]
    public class UsersController : ControllerBase
    {
        private readonly IUserHandler _userHandler;
        private readonly IUserRoleHandler _userRoleHandler;
        private readonly IExecutionContextAccessor _executionContextAccessor;
        public UsersController(
          IUserHandler userHandler,
          IUserRoleHandler userRoleHandler,
          IExecutionContextAccessor executionContextAccessor
          )
        {
            this._userHandler = userHandler;
            this._userRoleHandler = userRoleHandler;
            _executionContextAccessor = executionContextAccessor;
        }

        [HttpPost(nameof(CreateUser))]
        public async Task<IActionResult> CreateUser(AddUserCommand user, CancellationToken cancellationToken)
        {
            await _userHandler
                .AddUserAsync(user, cancellationToken);
            return Ok(HttpStatusCode.Created);
        }

        [HttpPost]
        [Route("/connect/token")]
        [AllowAnonymous, NoPermissionRequired]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateUserCommand command, CancellationToken cancellationToken)
        {

            var result = await _userHandler.AuthenticateUserAsync(command, cancellationToken);

            if (!result.IsAuthenticated) return Unauthorized(result.AuthenticationError);

            var token = AuthenticationConfig.GetJwtAuthenticationToken(result.User.Claims);
            var refreshToken = AuthenticationConfig.GenerateRefreshToken(_executionContextAccessor.IpAddress);
            await _userHandler.StoreRefreshToken(new StoreRefreshTokenCommand
            {
                Email = command.Email,
                RefreshToken = refreshToken
            }, cancellationToken);

            return Ok(new
            {
                success = true,
                token = AuthenticationConfig.GetSecurityToken(token),
                expiration = token.ValidTo,
                refreshToken = refreshToken.Token,
                AccountId = result.User.AccountId,
                AccountName = result.User.AccountName
            });

        }

        [AllowAnonymous, NoPermissionRequired]
        [HttpPost]
        [Route("/connect/refresh-token")]
        public async Task<IActionResult> RefreshToken([FromHeader] RefreshTokenCommand command, CancellationToken cancellationToken)
        {
            var result = await _userHandler.AuthenticateRefreshTokenAsync(command, cancellationToken);

            if (!result.IsAuthenticated) return Unauthorized(result.AuthenticationError);

            var token = AuthenticationConfig.GetJwtAuthenticationToken(result.User.Claims);
            var refreshToken = AuthenticationConfig.GenerateRefreshToken(_executionContextAccessor.IpAddress);
            await _userHandler.StoreRefreshToken(new StoreRefreshTokenCommand
            {
                Email = result.User.Email,
                OldRefreshToken = command.RefreshToken,
                RefreshToken = refreshToken
            }, cancellationToken);

            return Ok(new
            {
                success = true,
                token = AuthenticationConfig.GetSecurityToken(token),
                expiration = token.ValidTo,
                refreshToken = refreshToken.Token,
                AccountId = result.User.AccountId,
                AccountName = result.User.AccountName
            });
        }

        [NoPermissionRequired, Authorize]
        [HttpPost]
        [Route("/connect/revoke-token")]
        public async Task<IActionResult> RevokeToken([FromHeader] RefreshTokenCommand command,
            CancellationToken cancellationToken)
        {
            await _userHandler.RevokeToken(command, cancellationToken);
            return Ok();
        }

        [AllowAnonymous, NoPermissionRequired]
        [HttpPost(nameof(SendPassword))]
        public async Task<IActionResult> SendPassword(SendPasswordCommand command, CancellationToken cancellationToken)
        {
            await _userHandler
                .SendPasswordAsync(command, cancellationToken);
            return Ok();
        }

        [HasPermission(PolicyClaimTypes.ViewOnly)]
        [HttpPatch(nameof(ResetPassword))]
        public async Task<IActionResult> ResetPassword(ResetPasswordCommand command, CancellationToken cancellationToken)
        {
            await _userHandler
                .ResetPasswordAsync(command, cancellationToken);
            return Ok(HttpStatusCode.OK);
        }



        [HttpPut(nameof(UpdateUser))]
        public async Task<IActionResult> UpdateUser(UpdateUserCommand user, CancellationToken cancellationToken)
        {
            await _userHandler
                .UpdateUser(user, cancellationToken);
            return Ok(HttpStatusCode.OK);
        }


        [HttpGet(nameof(GetAllUsers))]
        public async Task<IEnumerable<UserDto>> GetAllUsers(CancellationToken cancellationToken)
        {
            return await _userHandler.GetAllUsers(cancellationToken);
        }

        [HttpGet(nameof(GetAllUserRoles))]
        public async Task<IEnumerable<Role>> GetAllUserRoles(CancellationToken cancellationToken)
        {
            return await _userRoleHandler.GetAllUserRoleInfo(cancellationToken);
        }
    }

}
