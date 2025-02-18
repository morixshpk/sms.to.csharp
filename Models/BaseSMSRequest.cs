using System.Text.Json.Serialization;

namespace sms.to.csharp.Models
{
    public abstract class BaseSMSRequest
    {
        [JsonPropertyName("sender_id")]
        public string SenderId { get; set; } = Manager.configs.SenderId;
        [JsonPropertyName("callback_url")]
        public string CallbackUrl { get; set; } = "";

    }
}
