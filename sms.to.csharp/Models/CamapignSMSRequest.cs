using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace sms.to.csharp.Models
{
    public class CamapignSMSRequest : BaseSMSRequest
    {
        [JsonPropertyName("to")]
        public List<string> To { get; set; } = new List<string>();

        [JsonPropertyName("message")]
        public string Message { get; set; }

        public override void Validate()
        {
            base.Validate();
            if (To.Count == 0)
                throw new System.Exception("To cannot be empty");
            if (string.IsNullOrEmpty(Message))
                throw new System.Exception("Message cannot be empty");
        }
    }

    public class CampaignSMSResponse : BaseSMSResponse
    {
        [JsonPropertyName("campaign_id")]
        public string CampaignId { get; set; }
    }
}