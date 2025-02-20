using System.Text.Json.Serialization;

namespace sms.to.csharp.Models
{
    internal abstract class BaseSMSRequest : IRequestModel
    {
        [JsonPropertyName("sender_id")]
        public string SenderId { get; set; }
        
        public virtual void Validate()
        {
            // sender id can be nullable if it is configured from sms.to dashboard
        }
    }
}