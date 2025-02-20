using System.Text.Json.Serialization;

namespace sms.to.csharp.Models
{
    public class Messagee
    {
        [JsonPropertyName("to")]
        public string To { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}