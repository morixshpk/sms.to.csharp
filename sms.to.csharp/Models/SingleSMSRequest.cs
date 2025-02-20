using System.Text.Json.Serialization;

namespace sms.to.csharp.Models
{
    public class SingleSMSRequest : BaseSMSRequest
    {
        [JsonPropertyName("to")]
        public string To { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        public override void Validate()
        {
            base.Validate();
            if (string.IsNullOrEmpty(To))
                throw new System.Exception("To cannot be empty");
            if (string.IsNullOrEmpty(Message))
                throw new System.Exception("Message cannot be empty");
        }
    }

    public class SingleSMSResponse : BaseSMSResponse
    {
        [JsonPropertyName("message_id")]
        public string MessageId { get; set; }
    }
}