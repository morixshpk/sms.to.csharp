using System.Text.Json.Serialization;

namespace sms.to.Models
{
    internal abstract class BaseSMSResponse
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("success")]
        public bool Success { get; set; }
    }
}
