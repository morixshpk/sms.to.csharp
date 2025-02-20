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
    }

    public class CampaignSMSResponse : BaseSMSResponse
    {
        [JsonPropertyName("campaign_id")]
        public string CampaignId { get; set; }
    }
}