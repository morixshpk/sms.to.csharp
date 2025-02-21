namespace sms.to
{
    public class Config
    {
        public string SenderId { get; set; }
        public string ApiKey { get; set; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(ApiKey))
                throw new System.Exception("ApiKey cannot be empty");

            if (string.IsNullOrEmpty(ApiKey))
                throw new System.Exception("SenderId cannot be empty");
        }
    }
}