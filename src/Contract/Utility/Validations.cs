using System;
using FluentValidation;

namespace ZohoToInsightIntegrator.Contract.Utility
{
    public static class Validations
    {
        public static IRuleBuilderOptions<T, string> Url<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            bool UrlIsValidUri(string url) => Uri.TryCreate(url, UriKind.Absolute, out var outUri)
                                              && (outUri.Scheme == Uri.UriSchemeHttp || outUri.Scheme == Uri.UriSchemeHttps);
            return ruleBuilder.Must(UrlIsValidUri);
        }
    }
}