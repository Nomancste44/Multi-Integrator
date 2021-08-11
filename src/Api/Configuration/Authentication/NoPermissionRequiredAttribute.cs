using System;

namespace ZohoToInsightIntegrator.Api.Configuration.Authentication
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class NoPermissionRequiredAttribute : Attribute
    {
    }
}