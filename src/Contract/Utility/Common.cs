using System;

namespace ZohoToInsightIntegrator.Contract.Utility
{
    public class Common
    {
        #region LocalData
        public const string ConnectionName = "IntegratorConnection";
        public const string DefaultAccountName = "DefaultAccount";

        #endregion

        #region Zoho CRM Api Calling Header

        public const string GrantTypeHeaderKey = "grant_type";
        public const string AuthorizationCodeGrantTypeHeader = "authorization_code";
        public const string RefreshTokenGrantTypeHeader = "refresh_token";
        public const string CodeHeaderKey = "code";
        public const string ZohoRedirectHeaderKey = "redirect_uri";
        public static string ZohoRedirectHeaderValue = "";
        public const string ClientIdHeaderKey = "client_id";
        public const string ClientSecretHeaderKey = "client_secret";
        public const string ZohoAuthScheme = "Zoho-oauthtoken";
        public const string AccountKey = "AccountKey";

        #endregion

        #region Insight Api Calling Header

        public const string InsightClientIdHeaderKey = "INSIGHT-USERNAME";
        public const string InsightClientSecretHeaderKey = "INSIGHT-PASSWORD";

        #endregion

        #region MediaTypes

        public const string ApplicationJson = "application/json";

        #endregion

        #region Regex Patterns
        public const string GuidRegexPattern = "^[{]?[0-9a-fA-F]{8}-([0-9a-fA-F]{4}-){3}[0-9a-fA-F]{12}[}]?$";

        #endregion


        public static DateTime UtcDateTime => DateTime.UtcNow;
    }
}
