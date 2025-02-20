using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace sms.to.csharp.Models
{
    /// <summary>
    /// TimeZone should be a TZ identifier
    /// </summary>
    public class ScheduleSMSRequest : BaseSMSRequest
    {
        [JsonPropertyName("messages")]
        public List<Messagee> Messages { get; set; } = new List<Messagee>();

        private DateTime _scheduledFor;
        [JsonPropertyName("scheduled_for")]
        public string ScheduledFor
        {
            get
            {
                return _scheduledFor.ToString("yyyy-MM-dd HH:mm:ss");
            }
            set
            {
                if (!DateTime.TryParse(value, out DateTime date))
                {
                    throw new Exception("cannot parse datetime");
                }
                _scheduledFor = date;
            }
        }
        [JsonPropertyName("timezone")]
        public string Timezone { get; set; }
    }

    public class ScheduleSMSResponse : BaseSMSResponse
    {
        [JsonPropertyName("campaign_id")]
        public string CampaignId { get; set; }
    }
}