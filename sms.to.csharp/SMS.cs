using sms.to.Models;

namespace sms.to
{
    /// <summary>
    /// Class to send message to recipient number. Also with this class you can estimate cost of the message.
    /// </summary>
    public class SMS
    {
        private bool _sent;
        private decimal _cost;

        /// <summary>
        /// Number of the recipient where to send the message to.
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// Message to be sent
        /// </summary>
        public string Message { get; set; }

        public string SenderId { get; set; }

        /// <summary>
        /// Validate the To property and message property of the instance
        /// </summary>
        /// <exception cref="System.Exception">Throw Exception if something not defined</exception>
        public void Validate()
        {
            if (string.IsNullOrEmpty(To))
                throw new System.Exception("To cannot be empty");
            if (string.IsNullOrEmpty(Message))
                throw new System.Exception("Message cannot be empty");
        }

        /// <summary>
        /// Validate and send message.
        /// </summary>
        /// <returns>return true if message is sent else return false</returns>
        /// <exception cref="System.Exception">Throw Exception if something not defined with validation or sending</exception>
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

        /// <summary>
        /// Estimate the cost of sending message. Return decimal value spend when seding message like that.
        /// </summary>
        /// <returns>Return cost of the message</returns>
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