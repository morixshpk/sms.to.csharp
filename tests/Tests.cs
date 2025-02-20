namespace sms.to.tests
{
    [TestClass]
    public sealed class Tests
    {
        [TestInitialize]
        public void Init()
        {
            ApiClient.Init(new Config
            {
                ApiKey = "your api key from http://sms.to account",
                ApiUrl = "https://api.sms.to",
                SenderId = "Testing"
            });
        }

        [TestMethod]
        public void Test_001_SendSingleSMS_ShouldSucceed()
        {
            var sms = new SMS
            {
                To = "+355698981848",
                Message = "Morix code 123654 to login to accounts.al! " + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"),
                SenderId = "Morix"
            };

            var sent = sms.Send();

            Assert.IsTrue(sent);
        }

        [TestMethod]
        public void Test_002_EstimateSingleSMS_ShouldSucceed()
        {
            var sms = new SMS
            {
                To = "+355694076555",
                Message = "Hello, World!",
                SenderId = "Morix"
            };

            var cost = sms.Estimate();

            Assert.IsTrue(cost > 0);
        }

        [TestMethod]
        public void Test_003_SendShouldFail()
        {
            var sms = new SMS()
            {
                Message = "Hello, World!"
            };
            Assert.ThrowsException<Exception>(() => sms.Send());
        }

        [TestMethod]
        public void Test_005_SendShouldFail()
        {
            var sms = new SMS()
            {
                To = "+355694076555",
            };
            Assert.ThrowsException<Exception>(() => sms.Send());
        }
    }
}