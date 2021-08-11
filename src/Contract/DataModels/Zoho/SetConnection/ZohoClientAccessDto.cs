using System.Text.Json.Serialization;

namespace ZohoToInsightIntegrator.Contract.DataModels.Zoho.SetConnection
{
    public class ZohoClientAccessDto
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }
        
        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; }
        
        [JsonPropertyName("api_domain")]
        public string ApiDomain { get; set; }
       
        [JsonPropertyName("error")]
        public string Error { get; set; }

        [JsonPropertyName("client_id")]
        public string ClientId { get; set; }

        [JsonPropertyName("client_secret")]
        public string ClientSecret { get; set; }

        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }
    }
}
