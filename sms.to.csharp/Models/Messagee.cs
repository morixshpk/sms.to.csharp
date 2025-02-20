using System.Text.Json.Serialization;

namespace sms.to.csharp.Models
{
    public class Messagee
    {
        [JsonPropertyName("to")]
        public string To { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(To))
                throw new System.Exception("To field cannot be empty");
            if (string.IsNullOrEmpty(Message))
                throw new System.Exception("Message field cannot be empty");
        }
    }
}