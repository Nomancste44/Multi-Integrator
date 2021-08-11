using System.Threading;

namespace ZohoToInsightIntegrator.Contract.Utility
{
    public class IntegratingEndpoints
    {
        #region Zoho Endpoints
        public const string ZohoAuth = "https://accounts.zoho.com/oauth/v2/token";
        public const string ZohoAllModules = "crm/v2/settings/modules";
        public const string ZohoModuleFields = "crm/v2/settings/layouts?module=";
        #endregion
    }
}