using sms.to.csharp.Models;

namespace sms.to.csharp
{
    public class SMS
    {
        private bool _sent;
        private decimal _cost;

        public string To { get; set; }

        public string Message { get; set; }

        public string SenderId { get; set; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(To))
                throw new System.Exception("To cannot be empty");
            if (string.IsNullOrEmpty(Message))
                throw new System.Exception("Message cannot be empty");
        }

        public bool Send()
        {
            Validate();

            var req = new SingleSMSRequest
            {
                To = To,
                Message = Message,
                SenderId = SenderId
            };

            var res = ApiClient.SendSMS(req);

            if (res != null)
                _sent = res.Success;

            return _sent;
        }

        public decimal Estimate()
        {
            var req = new SingleSMSRequest
            {
                To = To,
                Message = Message
            };

            var res = ApiClient.EstimateSingleSMS(req);

            if (res != null && res.SMSCount == 1)
                _cost = res.EstimatedCost;

            return _cost;
        }
    }
}