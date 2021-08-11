using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using ZohoToInsightIntegrator.Contract.Models;
using ZohoToInsightIntegrator.Contract.Utility;

namespace ZohoToInsightIntegrator.Api.Configuration.Authentication
{
    public static class AuthenticationConfig
    {
        public static string ValidIssuer = string.Empty;
        public static string ValidAudience = string.Empty;
        public const string SecretKey = "Zoho_To_Insight_Intergrator_Api_SecretKey_That_Would_Be_Used_To_Generate_Symmetric_Key";


        public static JwtSecurityToken GetJwtAuthenticationToken(IList<Claim> authClaims)
        {
            authClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            authClaims.Add(new Claim(JwtRegisteredClaimNames.AuthTime, DateTimeOffset.UtcNow.ToString()));

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));

            return new JwtSecurityToken(
                ValidIssuer,
                ValidAudience,
                notBefore: Common.UtcDateTime,
                expires: Common.UtcDateTime.AddHours(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );
        }

        public static string GetSecurityToken(JwtSecurityToken token)
        {
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        
        public static RefreshToken GenerateRefreshToken(string ipAddress)
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[64];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomBytes),
                Expires = Common.UtcDateTime.AddDays(7),
                CreatedByIp = ipAddress
            };
        }
    }


}
