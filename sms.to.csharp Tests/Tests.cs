using sms.to.csharp;
using sms.to.csharp.Models;

namespace sms.to.csharp_Tests
{
    [TestClass]
    public sealed class Tests
    {
        [TestInitialize]
        public void Init()
        {
            Manager.Init(new InitConfigs
            {
                ApiKey = "your_api_key",
                ApiUrl = "http://localhost:8085",
                SenderId = "your_sender_id"
            });
        }

        [TestMethod]
        public void Test_001_SendSingleSMS_ShouldSucceed()
        {
            var response = Manager.SendSMS(new SingleSMSRequest
            {
                To = "+1234567890",
                Message = "Hello, World!"
            });

            Assert.IsNotNull(response);
            Assert.IsTrue(response.Success);
            Assert.IsNotNull(response.MessageId);
            Assert.IsNotNull(response.EstimatedSmsCost);
        }

        [TestMethod]
        public void Test_002_SendSingleSMS_ShouldFail()
        {
            Assert.ThrowsException<Exception>(() => Manager.SendSMS(new SingleSMSRequest
            {
                Message = "Hello, World!"
            }));
        }

        [TestMethod]
        public void Test_003_EstimateSingleSMS_ShouldSucceed()
        {
            var response = Manager.EstimateSingleSms(new SingleSMSRequest
            {
                To = "+1234567890",
                Message = "Hello, World!"
            });

            Assert.IsNotNull(response);
            Assert.IsTrue(response.EstimatedCost > 0);
        }

        [TestMethod]
        public void Test_004_EstimateSingleSMS_ShouldFail()
        {
            Assert.ThrowsException<Exception>(() => Manager.EstimateSingleSms(new SingleSMSRequest
            {
                Message = "Hello, World!"
            }));
        }
    }
}
