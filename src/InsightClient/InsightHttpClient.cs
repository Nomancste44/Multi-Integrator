using System.Net.Http;

namespace ZohoToInsightIntegrator.InsightClient
{
    public class InsightHttpClient
    {
        public HttpClient HttpClient { get; }

        public InsightHttpClient(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

    }
}
