using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace sms.to.csharp.Models
{
    internal class EstimatedResponse
    {
        [JsonPropertyName("sms_count")]
        public int SMSCount { get; set; }
        [JsonPropertyName("estimated_cost")]
        public decimal EstimatedCost { get; set; }
        [JsonPropertyName("min_sms_count")]
        public int MinSMSCount { get; set; }
        [JsonPropertyName("max_sms_count")]
        public int MaxSMSCount { get; set; }
        [JsonPropertyName("contact_count")]
        public int ContactCount { get; set; }
        [JsonPropertyName("invalid_count")]
        public int InvalidCount { get; set; }
        [JsonPropertyName("errors")]
        public List<string> Errors { get; set; }
    }
}
