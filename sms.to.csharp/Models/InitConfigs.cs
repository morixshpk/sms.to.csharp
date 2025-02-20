namespace sms.to.csharp.Models
{
    public class InitConfigs : IRequestModel
    {
        public string SenderId { get; set; }
        public string ApiUrl { get; set; }
        public string ApiKey { get; set; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(ApiUrl))
                throw new System.Exception("ApiUrl cannot be empty");

            if (string.IsNullOrEmpty(ApiKey))
                throw new System.Exception("ApiKey cannot be empty");
        }
    }
}