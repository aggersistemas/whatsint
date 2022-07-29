using System.Text.Json.Serialization;

namespace WhatsInt.Model
{
    public class TokenDto
    {
        [JsonPropertyName("expires_in")]
        public double Expires { get; set; }
        [JsonPropertyName("access_token")]
        public string Token { get; set; }
        [JsonPropertyName("token_type")]
        public string Type { get; set; }
    }
}
