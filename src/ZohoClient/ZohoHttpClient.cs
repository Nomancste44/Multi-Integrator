using System.Net.Http;

namespace ZohoToInsightIntegrator.ZohoClient
{
    public class ZohoHttpClient
    {
        public HttpClient HttpClient { get; }

        public ZohoHttpClient(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

    }
}
