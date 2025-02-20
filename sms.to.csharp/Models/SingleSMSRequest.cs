using System.Text.Json.Serialization;

namespace sms.to.csharp.Models
{
    public class SingleSMSRequest : BaseSMSRequest
    {
        [JsonPropertyName("to")]
        public string To { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }
    }

    public class SingleSMSResponse : BaseSMSResponse
    {
        [JsonPropertyName("message_id")]
        public string MessageId { get; set; }
    }
}