using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace sms.to.csharp.Models
{
    public class PersonalizedSMSRequest : BaseSMSRequest
    {
        [JsonPropertyName("messages")]
        public List<Messagee> Messages { get; set; } = new List<Messagee>();

        public override void Validate()
        {
            base.Validate();

            foreach (var message in Messages)
            {
                message.Validate();
            }
        }
    }

    public class PersonalizedSMSResponse : BaseSMSResponse
    {
        [JsonPropertyName("campaign_id")]
        public string CampaignId { get; set; }
    }
}